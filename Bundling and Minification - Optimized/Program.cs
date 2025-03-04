namespace BundlingMinificationDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add WebOptimizer services
            // Allows configuration of CSS/ JS bundles.
            builder.Services.AddWebOptimizer(pipeline =>
            {
                // AddCssBundle & AddJavaScriptBundle: Creates bundles and specifies the output location and input files.
                // Bundle CSS
                pipeline.AddCssBundle("/css/bundle.css", "css/site.css", "css/StyleSheet2.css", "css/MyStyleSheet.css");

                // Bundle JS
                pipeline.AddJavaScriptBundle("/js/bundle.js", "js/site.js", "js/JavaScript2.js", "js/MyJavaScript.js");

                // Minify 
                pipeline.MinifyCssFiles();
                pipeline.MinifyJsFiles();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            // Use WebOptimizer before static files
            app.UseWebOptimizer();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}