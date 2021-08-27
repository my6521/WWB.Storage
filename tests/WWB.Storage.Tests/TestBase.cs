using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWB.Storage.Tests
{
    public class TestBase
    {
        protected IStorageProviderFactory StorageProviderFactory { get; set; }

        protected IStorageProvider StorageProvider { get; set; }

        protected Stream TestStream { get; set; }
        protected string Bucket { get; set; }


        public TestBase()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<IHostingEnvironment, HostingEnvironment>();
            services.AddStrorage();

            var serviceProvider = services.BuildServiceProvider();

            StorageProviderFactory = serviceProvider.GetRequiredService<IStorageProviderFactory>();

            var str = "demo";
            var array = Encoding.UTF8.GetBytes(str);
            TestStream = new MemoryStream(array);
            Bucket = "test";
        }

        protected string GetTestFileName()
        {
            return Guid.NewGuid().ToString("N") + ".txt";
        }

    }
}
