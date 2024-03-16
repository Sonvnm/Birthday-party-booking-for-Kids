var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add HttpClient service
builder.Services.AddHttpClient();

builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies");


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

/*// Configure CORS
app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:5297") 
           .AllowAnyHeader()
           .AllowAnyMethod();
});*/

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();

    // Map the Login page as the default route
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=User}/{action=Login}/{id?}");
});


app.Run();
