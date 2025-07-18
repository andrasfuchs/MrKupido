
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSystemWebAdapters();
builder.Services.AddHttpForwarder();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.UseSystemWebAdapters();

app.MapControllerRoute("RecipeEng", "eng/recipe/{id}", new {language = "eng", controller = "Recipe", action = "Details"});

app.MapControllerRoute("RecipeHun", "hun/recept/{id}", new {language = "hun", controller = "Recipe", action = "Details"});

app.MapControllerRoute("Default", "{language=xxx}/{controller=Home}/{action=Index}/{id?}");

app.MapDefaultControllerRoute();
app.MapForwarder("/{**catch-all}", app.Configuration["ProxyTo"]).Add(static builder => ((RouteEndpointBuilder)builder).Order = int.MaxValue);

app.Run();
