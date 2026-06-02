using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using DataAccessLayer.EntityFramework;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<SqlVisualStudioKoprusu_EntityFramework>();


//[Tarayıcı / Sayfa İsteği] 
//       │
//       ▼
//[SlidersController] ──(İçeriye ISliderService ister)
//       │
//       ▼ (.NET devreye girer: "Sana SliderManager veriyorum")
//[SliderManager] ──(İçeriye ISliderDal ister)
//       │
//       ▼ (.NET devreye girer: "Sana EfSliderDal veriyorum")
//[EfSliderDal] ──(Veritabanına gider ve veriyi çeker)

builder.Services.AddScoped<ISliderService, SliderManager>();
builder.Services.AddScoped<ISliderDal, EfSliderDal>();

builder.Services.AddScoped<ITopCategoryAreaService, TopCategoryAreaManager>();
builder.Services.AddScoped<ITopCategoryAreaDal, EfTopCategoryAreaDal>();

builder.Services.AddScoped<IFooterService, FooterManager>();
builder.Services.AddScoped<IFooterDal, EfFooterDal>();




// Add services to the container.
builder.Services.AddControllersWithViews();

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
