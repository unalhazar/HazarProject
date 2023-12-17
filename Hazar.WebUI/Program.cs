using FluentValidation;
using FluentValidation.AspNetCore;
using Hazar.BusinessLayer.Abstract;
using Hazar.BusinessLayer.Concrete;
using Hazar.BusinessLayer.Validation;
using Hazar.DataAccessLayer.Abstract;
using Hazar.DataAccessLayer.Concrete;
using Hazar.EntityLayer.DTOs;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog yapılandırmasını yükle
builder.Host.UseSerilog((ctx, lc) => lc
    .ReadFrom.Configuration(ctx.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console());

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
//Product
builder.Services.AddScoped<IValidator<ProductDTO>, ProductValidation>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<IProductDataService, ProductDataManager>();
//Category
builder.Services.AddScoped<IValidator<CategoryDTO>, CategoryValidation>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();


builder.Services.AddSingleton(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
