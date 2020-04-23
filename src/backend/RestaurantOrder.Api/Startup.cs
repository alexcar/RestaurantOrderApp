using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestaurantOrder.Domain.Commands;
using RestaurantOrder.Domain.Handlers;
using RestaurantOrder.Domain.Queries;
using RestaurantOrder.Domain.Repositories;
using RestaurantOrder.Domain.Services;
using RestaurantOrder.Infrastructure.Repositories;

namespace RestaurantOrder.Api
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
			services
				.AddScoped<IOrderService, OrderService>()
				.AddScoped<ITimeOfDayQueries, TimeOfDayQueries>()
				.AddScoped<ICommand, CreateOrderCommand>()
				.AddScoped<IHandler<CreateOrderCommand>, OrderHandler>()
				.AddScoped<IRepository, Repository>()
				.AddScoped<IDishRepository, DishRepository>()
				.AddScoped<IDishTypeRepository, DishTypeRepository>()
				.AddScoped<ITimeOfDayRepository, TimeOfDayRepository>()
				.AddControllers();

			services.AddCors(options => options.AddPolicy("ApiCorsPolicy", build =>
			{
				build.WithOrigins("http://localhost:4200")
					.AllowAnyMethod()
					.AllowAnyHeader();
			}));

			services
				.AddOpenApiDocument(settings =>
				{
					settings.Title = "Restaurant Order App";
					settings.DocumentName = "V3";
					settings.Version = "V3";
				});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();
			app.UseCors("ApiCorsPolicy");
			app.UseHttpsRedirection();
			app.UseAuthorization();
			app.UseOpenApi().UseSwaggerUi3();

			//app.UseEndpoints(endpoints =>
			//{
			//	endpoints.MapControllers();
			//});

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=api/order}/{action=Get}/{id?}");

				// endpoints.MapControllers();
			});
		}
	}
}
