using DemoApp.Data;
using DemoApp.Data.Models;

namespace DemoApp.HostedService
{
    public class DbSeederHostedService : IHostedService
    {
       
        IServiceProvider serviceProvider;
        public DbSeederHostedService(
            IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
           
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
               
                  var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                  await SeedDbAsync(db);
               
            }
        }
       public async Task SeedDbAsync(ApplicationDbContext db)
        {
            await db.Database.EnsureCreatedAsync();
            if (!db.IpoInformations.Any())
            {
                var info1 = new IpoInformation
                {
                 
                    InstrumentName = "JHRML",
                    Facevalue = 20.00m,
                    Premium = 0.00m,
                    IpoRate = 20.00m,
                    MinimumAmount = 10000.00m,
                    StartDate = new DateTime(2023, 4, 17),
                    EndDate = new DateTime(2023, 4, 27)
                };
                var info2 = new IpoInformation
                {
                   
                    InstrumentName = "ICICL",
                    Facevalue = 10.00m,
                    Premium = 10.00m,
                    IpoRate = 10.00m,
                    MinimumAmount = 10000.00m,
                    StartDate = new DateTime(2023, 4, 19),
                    EndDate = new DateTime(2023, 4, 24)
                };
                var info3 = new IpoInformation
                {
                    
                    InstrumentName = "CLICL",
                    Facevalue = 10.00m,
                    Premium = 0.00m,
                    IpoRate = 10.00m,
                    MinimumAmount = 10000.00m,
                    StartDate = new DateTime(2023, 4, 17),
                    EndDate = new DateTime(2023, 5, 01)
                };


                db.IpoInformations.Add(info1);
                db.IpoInformations.Add(info2);
                db.IpoInformations.Add(info3);
                await db.SaveChangesAsync();
            }

        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        
    }
}
