using Microsoft.EntityFrameworkCore;
using NhaHang.Data;
using NhaHang.Services;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Thêm cấu hình xác thực cookie cho cả admin và customer
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "CustomerCookie";
})
.AddCookie("CustomerCookie", options =>
{
    options.LoginPath = "/Auth/Login";
    options.AccessDeniedPath = "/Auth/Login";
})
.AddCookie("AdminCookie", options =>
{
    options.LoginPath = "/Admin/Login";
    options.AccessDeniedPath = "/Admin/Login";
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
builder.Services.AddHttpContextAccessor();

// Thêm session support
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Chạy migrate nếu có tham số 'migrate'
if (args.Contains("migrate"))
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<NhaHang.Data.ApplicationDbContext>();
        db.Database.Migrate();
    }
    Console.WriteLine("Migration thành công!");
    return;
}

// Chỉ seed nếu có tham số 'seed'
if (args.Contains("seed"))
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        NhaHang.Data.SeedData.Initialize(services);
    }
    Console.WriteLine("Seed thành công!");
    return; // Thoát, không chạy web server
}

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
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Thêm session middleware
app.UseAuthentication(); // Bắt buộc phải có dòng này!
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.MapGet("/api/cron/check-payments", async (ShopService shopService, ApplicationDbContext db) =>
{
    var donHangs = await shopService.GetAllOrdersAsync();
    int checkedCount = 0, paidCount = 0;
    List<object> allTransactions = new();
    // Lấy api key từ ThongTinThanhToan
    var bankInfo = db.ThongTinThanhToans.FirstOrDefault();
    string apiKey = bankInfo?.ApiKey ?? string.Empty;
    string apiUrl = $"https://api.sieuthicode.net/historyapiacbv2/{apiKey}";
    using var http = new HttpClient();
    string rawJson = null;
    try
    {
        var response = await http.GetAsync(apiUrl);
        if (response.IsSuccessStatusCode)
        {
            rawJson = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(rawJson);
            var root = doc.RootElement;
            if (root.TryGetProperty("transactions", out var arr) && arr.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in arr.EnumerateArray())
                {
                    allTransactions.Add(JsonSerializer.Deserialize<object>(item.GetRawText()));
                }
            }
        }
    }
    catch { }
    foreach (var don in donHangs.Where(d => d.TrangThai != "Đã thanh toán" && d.PhuongThucThanhToan == "Chuyển khoản"))
    {
        var ok = await shopService.CheckAndMarkPaymentAsync(don.MaDonHang);
        checkedCount++;
        if (ok) paidCount++;
    }
    return Results.Ok(new { success = true, checkedOrders = checkedCount, paidOrders = paidCount });
});

app.Run();
