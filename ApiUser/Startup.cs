using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace ApiUser
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
            services.AddControllers();

            AddSwagger(services);

        }

        //Configuracion del swagger
        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {   
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title="Api prueba rabbitmq",
                    Version="v1",
                    Description="Api para probar el manejo de colas a través de RabbitMq",
                    Contact=new OpenApiContact
                    {
                        Name="Luis Fernando Valera Bernal",
                        Email="lvalerab@gmail.com",
                        Url=new Uri("https://www.lfvb.es")
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger(setupAction: null);

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
