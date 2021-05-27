using System.ServiceModel;
using System.Xml;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SoapCore;
using WebServices.Services;
using WebServices.SoapServices;

namespace WebServices
{
    public class Startup
    {
        private static readonly BasicHttpBinding BindingPadraoSoap = new BasicHttpBinding
        {
            ReaderQuotas = XmlDictionaryReaderQuotas.Max,
            Security = new BasicHttpSecurity
            {
                Mode = BasicHttpSecurityMode.TransportCredentialOnly,
                Transport = new HttpTransportSecurity
                {
                    ClientCredentialType = HttpClientCredentialType.Basic
                }
            }
        };

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSoapCore();
            services.AddScoped<IBooksService, BooksService>();
            services.AddScoped<BooksService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebServices", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebServices v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.UseSoapEndpoint<IBooksService>(opcoes =>
                {
                    opcoes.Path = "/BooksService";
                    opcoes.Binding = BindingPadraoSoap;
                });

                endpoints.MapControllers();
            });
        }
    }
}
