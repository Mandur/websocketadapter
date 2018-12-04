using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace websocketadapter
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }
            // else
            // {
            //     app.UseHsts();
            // }
app.UseMvc();
    //         app.UseMvc(routes =>
    //         {
    //         routes.MapRoute( name: "default",
    // template: "{controller=Values}/{action=Get}/{id?}");
    //         });

    
            // var trackPackageRouteHandler = new RouteHandler(context =>
            // {
            //     var routeValues = context.GetRouteData().Values;
            //     return context.Response.WriteAsync(
            //         $"Hello! Route values: {string.Join(", ", routeValues)}");
            // });

    //         var routeBuilder = new RouteBuilder(app, trackPackageRouteHandler);

    //         routeBuilder.MapRoute(
    //             "api Route",
    //             "api/{operation:values}");

    //         routeBuilder.MapGet("hello/{name}", context =>
    //         {
    //             var name = context.GetRouteValue("name");
    // // The route handler when HTTP GET "hello/<anything>" matches
    // // To match HTTP GET "hello/<anything>/<anything>, 
    // // use routeBuilder.MapGet("hello/{*name}"
    //         return context.Response.WriteAsync($"Hi, {name}!");
    //                 });

    //                 var routes = routeBuilder.Build();
    //                 app.UseRouter(routes);
         }
    }
}
