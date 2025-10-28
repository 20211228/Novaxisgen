namespace Novaxisgen.Services
{
    public class DailyLaiTrenLai : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public DailyLaiTrenLai(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;
                var runTime = DateTime.Today.AddHours(0).AddMinutes(5); // chạy lúc 00:05 sáng

                if (now.Hour == 0 && now.Minute == 10)
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var service = scope.ServiceProvider.GetRequiredService<LaiTrenLaiService>();
                        await service.TinhHoaHongLaiTrenLaiHangNgay();
                    }

                    // chờ đến ngày mai
                    await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
