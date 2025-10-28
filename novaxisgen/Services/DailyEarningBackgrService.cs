
namespace Novaxisgen.Services
{
    public class DailyEarningBackgrService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<DailyEarningBackgrService> _logger;

        public DailyEarningBackgrService(IServiceScopeFactory scopeFactory, ILogger<DailyEarningBackgrService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Tinh lai hang ngay tu dong bat dau");
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;

                try
                {
                    if (now.Hour == 0 && now.Minute == 0)
                    {
                        using (var scope = _scopeFactory.CreateScope())
                        {
                            var service = scope.ServiceProvider.GetRequiredService<DailyEarningService>();
                            await service.DailyEarning();
                        }

                        _logger.LogInformation($" Lãi ngày {now:dd/MM/yyyy} đã được tính cho tất cả người dùng.");
                        await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "❌ Lỗi khi tính lãi.");
                }
                

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
