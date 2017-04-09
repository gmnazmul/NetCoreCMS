﻿/*
 * Author: Xonaki
 * Website: http://xonaki.com
 * Copyright (c) xonaki.com
 * License: BSD (3 Clause)
*/

using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Threading;

namespace NetCoreCMS.Web
{
    public class Program
    {
        private static CancellationTokenSource cancelTokenSource = new System.Threading.CancellationTokenSource();

        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()                
                .Build();

            host.Run(cancelTokenSource.Token);
        }

        public static void Shutdown()
        {
            cancelTokenSource.CancelAfter(10000);            
        }
    }
}
