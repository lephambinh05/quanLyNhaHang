using Microsoft.EntityFrameworkCore;
using NhaHang.Data;
using NhaHang.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Thêm cấu hình xác thực cookie cho admin
builder.Services.AddAuthentication("AdminCookie")
    .AddCookie("AdminCookie", options =>
    {
        options.LoginPath = "/admin/login";
        options.AccessDeniedPath = "/admin/login";
    });

builder.Services.AddAuthorization();

// Đăng ký ApplicationDbContext sử dụng SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Đăng ký các services
builder.Services.AddScoped<MenuService>();
builder.Services.AddScoped<MarketingService>();
builder.Services.AddScoped<BranchService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<DanhMucService>();
builder.Services.AddScoped<KhachHangService>();
builder.Services.AddScoped<QuanTriVienService>();
builder.Services.AddScoped<ShopService>();
builder.Services.AddScoped<ImageService>();

var app = builder.Build();

// Seed dữ liệu mẫu khi khởi động
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    NhaHang.Data.SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication(); // Bắt buộc phải có dòng này!
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
