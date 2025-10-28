using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Novaxisgen.Models;
using System.Text.Json;

namespace Novaxisgen.Services
{
    public class TransactionScannerService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly TimeSpan _transactionTimeout = TimeSpan.FromMinutes(10); // Thời gian chờ giao dịch

        public TransactionScannerService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var etherscan = scope.ServiceProvider.GetRequiredService<EtherscanService>();
                    var db = scope.ServiceProvider.GetRequiredService<NovaxisgenContext>();

                    var wallets = db.ViNguoiDungs.ToList();

                    foreach (var wallet in wallets)
                    {
                        try
                        {
                            var json = await etherscan.GetTransactionHistoryAsync(wallet.DiaChiVi);
                            if (json == null) continue;

                            if (json.RootElement.TryGetProperty("result", out var result) && result.ValueKind == JsonValueKind.Array)
                            {
                                foreach (var tx in result.EnumerateArray())
                                {
                                    string? hash = tx.TryGetProperty("hash", out var h) ? h.GetString() : null;
                                    string? to = tx.TryGetProperty("to", out var t) ? t.GetString()?.ToLower() : null;
                                    string? from = tx.TryGetProperty("from", out var f) ? f.GetString()?.ToLower() : null;
                                    string? valueStr = tx.TryGetProperty("value", out var v) ? v.GetString() : "0";

                                    if (string.IsNullOrEmpty(hash) || string.IsNullOrEmpty(to) || string.IsNullOrEmpty(valueStr))
                                        continue;

                                    decimal value = 0;
                                    try
                                    {
                                        value = decimal.Parse(valueStr) / (decimal)Math.Pow(10, 18);
                                    }
                                    catch { continue; }

                                    // Nếu là giao dịch mới đến ví này
                                    if (to == wallet.DiaChiVi.ToLower() && !db.GiaoDichBlockchains.Any(x => x.TxHash == hash))
                                    {
                                        var giaoDich = new GiaoDichBlockchain
                                        {
                                            UserId = wallet.UserId,
                                            TxHash = hash,
                                            TuVi = from,
                                            DenVi = to,
                                            SoTien = value,
                                            TokenSymbol = wallet.TokenSymbol,
                                            NgayTao = DateTime.Now,
                                            TrangThai = "Hoàn tất"
                                        };

                                        db.GiaoDichBlockchains.Add(giaoDich);

                                        wallet.SoDu += value;
                                        wallet.NgayCapNhat = DateTime.Now;

                                        Console.WriteLine($"💰 Nhận được {value} {wallet.TokenSymbol} từ {from}");
                                    }
                                }

                                await db.SaveChangesAsync();
                            }

                            // ✅ Xử lý giao dịch chờ xử lý quá thời gian
                            var pendingTxs = db.GiaoDichVis
                                .Where(g => g.TrangThai == "Chờ xử lý")
                                .ToList();

                            foreach (var tx in pendingTxs)
                            {
                                if (DateTime.Now - tx.NgayTao > _transactionTimeout)
                                {
                                    tx.TrangThai = "Thất bại";
                                    Console.WriteLine($"❌ Giao dịch {tx.MaDonHang} thất bại do timeout");
                                }
                            }

                            await db.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"❌ Lỗi khi quét ví {wallet.DiaChiVi}: {ex.Message}");
                        }
                    }
                }

                await Task.Delay(5000, stoppingToken); // mỗi 5 giây quét 1 lần
            }
        }
    }
}
