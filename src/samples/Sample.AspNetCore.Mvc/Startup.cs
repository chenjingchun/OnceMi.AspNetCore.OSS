using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnceMi.AspNetCore.OSS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.AspNetCore.Mvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //default minio
            //���Ĭ�϶��󴢴�������Ϣ
            services.AddOSSService(option =>
            {
                option.Provider = OSSProvider.Minio;
                option.Endpoint = "oss.oncemi.com:9000";
                option.AccessKey = "r***************t";
                option.SecretKey = "u*************************A";
                option.IsEnableHttps = true;
                option.IsEnableCache = true;
            });
            //aliyun oss
            //�������Ϊ��aliyunoss����OSS���󴢴�������Ϣ
            services.AddOSSService("aliyunoss", option =>
            {
                option.Provider = OSSProvider.Aliyun;
                option.Endpoint = "oss-cn-hangzhou.aliyuncs.com";
                option.AccessKey = "L*********************U";
                option.SecretKey = "D**************************M";
                option.IsEnableHttps = true;
                option.IsEnableCache = true;
            });
            //qcloud oss
            //�������ļ��м��ؽڵ�Ϊ��OSSProvider����������Ϣ
            services.AddOSSService("QCloud", "OSSProvider");

            //qiniu oss
            //�������Ϊ��qiuniu����OSS���󴢴�������Ϣ
            services.AddOSSService("qiuniu", option =>
            {
                option.Provider = OSSProvider.Qiniu;
                option.Region = "CN_East";  //֧�ֵ�ֵ��CN_East(����)/CN_South(����)/CN_North(����)/US_North(����)/Asia_South(������)
                option.AccessKey = "B****************************L";
                option.SecretKey = "Z*************************************g";
                option.IsEnableHttps = true;
                option.IsEnableCache = true;
            });

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
