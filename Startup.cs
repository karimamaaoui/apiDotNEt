using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace MyApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

            });

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Configure error handling for production environment
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");

                // Serve the static video files
                c.RoutePrefix = "swagger";
                c.DocumentTitle = "API Documentation";
                c.InjectStylesheet("/swagger-custom/custom-styles.css"); // Customize the Swagger UI styles if needed
                var videosPath = Path.Combine(env.ContentRootPath, "Videos");
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(videosPath),
                    RequestPath = "/videos"
                });
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
