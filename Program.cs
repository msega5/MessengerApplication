
using Autofac.Extensions.DependencyInjection;
using Autofac;
using MessengerApplication.Abstraction;
using MessengerApplication.Repo;
using MessengerApplication.Models;
using System.Security.Cryptography;

namespace MessengerApplication
{
    public class Program
    {
        public static WebApplication BuildWebApp(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            var config = new ConfigurationBuilder();
            config.AddJsonFile("appsettings.json");
            var cfg = config.Build();

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Host.ConfigureContainer<ContainerBuilder>(contaierBuilder =>
            {
                contaierBuilder.RegisterType<UserRepository>().As<IUserRepository>();
                contaierBuilder.Register(c => new MessengerContext(cfg.GetConnectionString("db"))).InstancePerDependency();
            });

            return builder.Build();
        }

        public static void Main(string[] args)
        {
            var app = BuildWebApp(args);

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            app.Run();

            static RSA GetPublicKey()
            {
                var f = File.ReadAllText("rsa/public_key.pem");
                var rsa = RSA.Create();
                rsa.ImportFromPem(f);
                return rsa;
            }
        }
    }
}
