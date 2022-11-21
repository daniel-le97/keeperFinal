using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace keeperFinal;

public class Program
{
  public static void Main(string[] args)
  {
    CreateHostBuilder(args).Build().Run();
  }

  public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
      // webBuilder.UseKestrel();
      // webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
      // webBuilder.UseUrls("https://locahost:5001");
      webBuilder.UseStartup<Startup>();
    });
}

