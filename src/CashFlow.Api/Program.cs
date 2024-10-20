
using CashFlow.Api.Filters;
using CashFlow.Api.Middleware;
using CashFlow.Application;
using CashFlow.Infrastructure;

namespace CashFlow.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers((options) => options.Filters.Add<ExceptionFilter>());
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "CashFlow.Api", Version = "v1" });
                c.EnableAnnotations();
            });

            builder.Services.AddInfraStructure(builder.Configuration);
            builder.Services.AddApplication();
            builder.Services.AddRouting(s => s.LowercaseUrls = true);

            var app = builder.Build();

            app.UseMiddleware<CultureMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
