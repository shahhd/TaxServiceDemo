using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.Interface;

namespace TaxCalculator.Test.IntegrationTest
{
    public class IntegrationTestFixture
    {

        static object _singletonLock = new object();
        public bool fixtureLoaded = false;
        public IServiceCollection services;
        public ITaxHttpClient _taxhttpClient;
        public IntegrationTestFixture()
        {
            services = new ServiceCollection();
            if (fixtureLoaded == false)
            {
                lock (_singletonLock)
                {
                    fixtureLoaded = true;
                    this.services = this.GetServices();
                    fixtureLoaded = true;
                }
            }

        }


        private IServiceCollection GetServices()
        {
            services.AddHttpClient("taxJar", c =>
            {
                c.BaseAddress = new Uri("https://api.taxjar.com/v2/");
                c.DefaultRequestHeaders.Add("Authorization", "Bearer 5da2f821eee4035db4771edab942a4cc");
            });
            services.AddScoped<ILogging, Logging>();
            services.AddScoped<ITaxHttpClient,TaxHttpClient>();
            _taxhttpClient = services.BuildServiceProvider().GetService<ITaxHttpClient>();
            return services;
        }
    }
}
