using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabReportAPI
{
    public class Program
    {

         /// <summary>
         /// Program main function method to invoke API
         /// </summary>
         /// <param name="args"></param>
        public static void Main(string[] args)
        {
            try 
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch(Exception ex)
            {
                Startup.ExceptionLogger.WriteEventLogToFile(ex, "Program Start - Main");
            }
            
        }

        /// <summary>
        /// Function for API Host builder
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
