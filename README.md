# NhaHangChain - Hệ thống quản lý chuỗi nhà hàng

## 🚀 **Trạng thái hiện tại**
✅ **Ứng dụng đã sẵn sàng sử dụng!**
- ✅ Đã sửa tất cả lỗi build và runtime
- ✅ Đã cải tiến cấu trúc 2 tầng: Khách hàng + Admin
- ✅ Đã đăng ký đầy đủ các services trong DI container
- ✅ Giao diện responsive và thân thiện người dùng
- ✅ Database đã được tạo và seed data sẵn sàng

## Yêu cầu hệ thống
- .NET 9.0 trở lên
- SQL Server (hoặc SQLite)
- Entity Framework Core

## 🚀 **Cài đặt & chạy nhanh**

1. **Clone project:**
   ```bash
   git clone <repo>
   cd nhaHang
   ```

2. **Cấu hình database** trong `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=NhaHangChain;Trusted_Connection=true;MultipleActiveResultSets=true"
   }
   ```

3. **Tạo database & seed data:**
   ```bash
   dotnet ef database update
   ```

4. **Chạy ứng dụng:**
   ```bash
   dotnet run
   ```

5. **Truy cập:** http://localhost:5232

## 🏗️ **Cấu trúc 2 tầng mới**

### 🏠 **Tầng 1: Trang chủ cho khách hàng** (`/`)
- **Mô tả:** Giao diện chính cho khách hàng đặt hàng trực tuyến
- **Tính năng:** 
  - Xem thực đơn nổi bật (6 món đầu tiên)
  - Xem khuyến mãi đang chạy (3 khuyến mãi)
  - Đặt hàng trực tuyến
  - Xem đơn hàng của mình
- **Không cần đăng nhập:** Tất cả khách hàng đều có thể truy cập
- **Responsive design:** Tối ưu cho mobile và desktop

### 🔐 **Tầng 2: Khu vực quản trị Admin** (`/admin/*`)
- **URL đăng nhập:** `/admin/login`
- **Mô tả:** Giao diện quản trị dành riêng cho admin
- **Phân quyền:** Chỉ tài khoản admin (SuperAdmin, Manager) mới đăng nhập được
- **Layout riêng:** Sử dụng `_AdminLayout.cshtml` với sidebar navigation

#### 🔑 **Tài khoản mẫu cho Admin:**
- **Quản lý cấp cao:**  
  Email: `admin@demo.com`  
  Mật khẩu: `123456`
- **Quản lý chi nhánh:**  
  Email: `branch1@demo.com`  
  Mật khẩu: `123456`

#### 📊 **Các trang quản trị:**
- **Dashboard:** `/admin/dashboard` - Thống kê tổng quan hệ thống
- **Quản lý chi nhánh:** `/admin/branches` - Thêm/sửa/xóa chi nhánh
- **Quản lý thực đơn:** `/admin/menu` - Quản lý món ăn và danh mục
- **Bán hàng (POS):** `/admin/orders` - Hệ thống bán hàng tại quầy
- **Báo cáo:** `/admin/reports` - Báo cáo doanh thu, hiệu suất
- **Khuyến mãi:** `/admin/marketing` - Quản lý chương trình khuyến mãi
- **Quản trị viên:** `/admin/quantrivien` - Quản lý tài khoản admin
- **Danh mục:** `/admin/danhmuc` - Quản lý danh mục món ăn

### 🛒 **Trang đặt hàng cho khách hàng** (`/shop/*`)
- **Mô tả:** Giao diện đặt hàng dành cho khách hàng
- **Tính năng:**
  - Xem thực đơn đầy đủ
  - Đặt hàng trực tuyến
  - Theo dõi đơn hàng
  - Xem lịch sử đặt hàng

## 🛠️ **Tính năng chính**

### 🔐 **Khu vực Admin:**
- ✅ Dashboard tổng quan với thống kê real-time
- ✅ Quản lý chi nhánh, thực đơn, khuyến mãi
- ✅ Hệ thống POS bán hàng tại quầy
- ✅ Báo cáo doanh thu, hiệu suất chi nhánh
- ✅ Quản lý tài khoản admin và phân quyền
- ✅ Giao diện quản trị chuyên nghiệp với sidebar navigation
- ✅ Responsive design cho tablet và desktop

### 🏠 **Khu vực Khách hàng:**
- ✅ Giao diện đặt hàng trực tuyến thân thiện
- ✅ Xem thực đơn đa dạng với hình ảnh
- ✅ Khuyến mãi hấp dẫn và dễ áp dụng
- ✅ Theo dõi đơn hàng trực tuyến
- ✅ Responsive design cho mobile và desktop

## 📁 **Cấu trúc dự án đã cải tiến**

```
Pages/
├── Index.cshtml                    # Trang chủ khách hàng
├── Shared/
│   └── _Layout.cshtml             # Layout chung cho khách hàng
├── Shop/                          # Trang đặt hàng khách hàng
└── Admin/                         # Khu vực quản trị (MỚI)
    ├── _AdminLayout.cshtml        # Layout riêng cho admin (MỚI)
    ├── Login.cshtml               # Trang đăng nhập admin (MỚI)
    ├── Dashboard/                 # Dashboard quản trị
    ├── Branches/                  # Quản lý chi nhánh
    ├── Menu/                      # Quản lý thực đơn
    ├── Orders/                    # POS bán hàng
    ├── Reports/                   # Báo cáo
    ├── Marketing/                 # Quản lý khuyến mãi
    ├── QuanTriVien/              # Quản lý admin
    └── DanhMuc/                  # Quản lý danh mục

Services/                          # Business Logic Layer
├── MenuService.cs                 # Quản lý thực đơn
├── MarketingService.cs            # Quản lý khuyến mãi
├── BranchService.cs               # Quản lý chi nhánh
├── OrderService.cs                # Quản lý đơn hàng
├── QuanTriVienService.cs         # Quản lý admin
├── KhachHangService.cs           # Quản lý khách hàng
├── DanhMucService.cs             # Quản lý danh mục
├── ShopService.cs                 # Quản lý shop
└── ImageService.cs                # Xử lý hình ảnh
```

## 🔧 **Các vấn đề đã được sửa**

### ✅ **Build Errors:**
- ✅ Sửa lỗi CSS `@media` queries trong Razor pages (thay `@media` thành `@@media`)
- ✅ Sửa lỗi namespace và using statements
- ✅ Sửa lỗi property names trong models

### ✅ **Runtime Errors:**
- ✅ Đăng ký đầy đủ các services trong DI container
- ✅ Sửa lỗi missing properties trong models
- ✅ Tạo EF migrations để cập nhật database
- ✅ Sửa lỗi NOT NULL constraint violations trong seed data

### ✅ **Cấu trúc ứng dụng:**
- ✅ Tách biệt hoàn toàn khu vực admin và khách hàng
- ✅ Tạo trang đăng nhập admin riêng tại `/admin/login`
- ✅ Tạo layout admin riêng với sidebar navigation
- ✅ Cập nhật homepage chỉ hiển thị thông tin cho khách hàng

## 🔒 **Bảo mật & Phân quyền**

### 🛡️ **Phân quyền rõ ràng:**
- ✅ Khách hàng chỉ có thể truy cập trang chủ và đặt hàng
- ✅ Chỉ admin mới đăng nhập được vào `/admin/login`
- ✅ Tài khoản khách hàng không thể truy cập khu vực quản trị
- ✅ Giao diện admin và khách hàng hoàn toàn tách biệt

### 🔐 **Bảo mật:**
- ✅ Tất cả trang admin đều có `[Authorize(Roles = "SuperAdmin,Manager")]`
- ✅ Trang đăng nhập admin chỉ chấp nhận tài khoản admin
- ✅ Khách hàng không thể truy cập hoặc đăng nhập vào khu vực admin

## 📊 **Database Schema**

### 🏢 **Chi nhánh (ChiNhanh):**
- MaChiNhanh, TenChiNhanh, DiaChi, SoDienThoai, Email, TenQuanLy

### 🍽️ **Thực đơn (MonAn):**
- MaMonAn, TenMonAn, MoTa, Gia, HinhAnh, MaDanhMuc, MaChiNhanh

### 🛒 **Đơn hàng (DonHang):**
- MaDonHang, MaKhachHang, TenKhachHang, SoDienThoai, DiaChiGiaoHang, TrangThai

### 💰 **Khuyến mãi (KhuyenMai):**
- MaKhuyenMai, MaGiamGia, MoTa, NgayBatDau, NgayKetThuc, TyLeGiam

### 👥 **Quản trị viên (QuanTriVien):**
- MaQuanTriVien, Email, MatKhau, HoTen, VaiTro, MaChiNhanh

## 🎯 **Hướng dẫn sử dụng**

### 🏠 **Cho khách hàng:**
1. Truy cập http://localhost:5232
2. Xem thực đơn nổi bật và khuyến mãi
3. Click "Đặt hàng" để vào trang shop
4. Chọn món ăn và đặt hàng

### 🔐 **Cho admin:**
1. Truy cập http://localhost:5232/admin/login
2. Đăng nhập với tài khoản admin
3. Sử dụng sidebar để điều hướng
4. Quản lý chi nhánh, thực đơn, đơn hàng, báo cáo

## 🚀 **Deployment**

### **Development:**
```bash
dotnet run
```

### **Production:**
```bash
dotnet publish -c Release
dotnet bin/Release/net9.0/publish/NhaHang.dll
```

## 📝 **Lưu ý quan trọng**

> **Ứng dụng đã sẵn sàng sử dụng!**
> - Tất cả lỗi build và runtime đã được sửa
> - Database đã được tạo và có dữ liệu mẫu
> - Giao diện responsive và thân thiện người dùng
> - Phân quyền rõ ràng giữa admin và khách hàng

> **Hỗ trợ:**
> - Nếu gặp lỗi, hãy kiểm tra connection string trong `appsettings.json`
> - Đảm bảo SQL Server đang chạy
> - Chạy `dotnet ef database update` nếu cần tạo database mới

