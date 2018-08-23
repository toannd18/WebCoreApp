﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace WebCoreApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls( "http://*:6852")
                .UseStartup<Startup>()
                .Build();
    }
}