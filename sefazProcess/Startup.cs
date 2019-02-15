using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using sefazProcess.Business;
using sefazProcess.Business.Implementations;
using sefazProcess.Models;
using sefazProcess.Repository;
using sefazProcess.Repository.Implementations;
using Swashbuckle.AspNetCore.Swagger;
using WebApiContrib.Core.Formatter.Csv;

namespace sefazProcess
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            using (var client = new DatabaseContext())
            {
                client.Database.EnsureCreated();
            }
                        
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //adding connection sqlite
            services.AddEntityFrameworkSqlite().AddDbContext<DatabaseContext>();

            //add swagger
            services.AddSwaggerGen(w =>
            {
                w.SwaggerDoc("v1", new Info
                {
                    Title = "SefazProcess",
                    Version = "v1",
                    Description = "a simple example of API built in .net core 2.0",
                    Contact = new Contact
                    {
                        Name = "Rafael Honório",
                        Email = "rafael.contatotrab@gmail.com",
                        Url = "https://github.com/rafael-hs"

                    },
                });
            });
            services.AddApiVersioning();

            var csvFormatterOptions = new CsvFormatterOptions();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //dependence inject
            services.AddScoped<IProductBusiness, ProductBusImpl>();
            services.AddScoped<IProductRepository, ProductRepositoryImpl>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //adding middlewate to serve generated swagger as a JSON endPoint
            app.UseSwagger();
            app.UseSwaggerUI(u => 
            {
                u.SwaggerEndpoint("/swagger/v1/swagger.json", "SefazProcess V1");
            });
            var option = new RewriteOptions();

            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);
            //app.UseHttpsRedirection();
            app.UseMvc();
        }

    }
}
