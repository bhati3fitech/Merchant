using AdminPanel.Interface;
using AdminPanel.Repository;
using MerchantUPBP.Interface;
using MerchantUPBP.Repo;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout set karein
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IMerchant, MerchantRepo>();
builder.Services.AddSingleton<IPaymentMode, PaymentModeRepo>();
builder.Services.AddSingleton<IPaymentModeMaster, PaymentModeMasterRepo>();
builder.Services.AddSingleton<IMerchantPayment, MerchantPaymentRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=MerchantLogin}/{id?}");

app.Run();
