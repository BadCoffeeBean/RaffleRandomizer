using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RaffleRandomizer.Core;
using System;

namespace RaffleRandomizer.Service
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) 
		{
			return Host
				.CreateDefaultBuilder(args)
				.ConfigureServices((hostContext, services) => 
				{
					services.AddSingleton<RaffleService>();
				});
		}
	}
}
