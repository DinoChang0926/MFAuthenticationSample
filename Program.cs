using MFAuthenticationSample.Helper;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
#region 使網站可以儲存後重整不需重開偵錯(需安裝 Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation)
builder.Services.AddMvc().AddRazorRuntimeCompilation();
builder.Services.AddScoped<QrCodeHelper>();
builder.Services.AddScoped<MFAuthenticationHelper>();
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
