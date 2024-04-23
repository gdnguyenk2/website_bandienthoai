
/*CƠ SỞ DỮ LIỆU ĐƯỢC XÂY DỰNG BỞI CAO TẤN CÔNG VÀ LÊ HỮU TÀI
THỰC HIỆN ĐỀ TÀI WEBSITE BÁN ĐIỆN THOẠI DI ĐỘNG VÀ PHỤ KIỆN*/

CREATE DATABASE WebBanDienThoai

USE WebBanDienThoai
GO

CREATE TABLE NhaSanXuat
(
    MaNSX INT IDENTITY NOT NULL,
    TenNSX NVARCHAR(100) NOT NULL, 
    ThongTin NVARCHAR(MAX) NULL,
    Logo NVARCHAR(MAX) NULL,
   
    CONSTRAINT PK_NSX PRIMARY KEY(MaNSX)
)
GO

CREATE TABLE NhaCungCap
(
   MaNCC INT IDENTITY NOT NULL,
   TenNCC NVARCHAR(100) NOT NULL,
   DiaChi NVARCHAR(255) NOT NULL,
   Email NVARCHAR(100) NOT NULL,
   SoDienThoai VARCHAR(12) NOT NULL,
   Fax NVARCHAR(50) NULL,

   CONSTRAINT PK_NCC PRIMARY KEY (MaNCC)
)
GO

CREATE TABLE PhieuNhap
(
    MaPN INT IDENTITY NOT NULL,
    MaNCC INT NOT NULL,
    NgayNhap DATETIME NOT NULL,
    DaXoa BIT NOT NULL,
        
    CONSTRAINT PK_PN PRIMARY KEY (MaPN),
    CONSTRAINT FK_PN_NCC FOREIGN KEY (MaNCC) REFERENCES NhaCungCap(MaNCC) ON DELETE CASCADE
)
GO

CREATE TABLE LoaiThanhVien
(
    MaLoaiTV INT IDENTITY NOT NULL,
    TenLoai NVARCHAR(50) NOT NULL,
    UuDai INT NULL,

    CONSTRAINT PK_LTV PRIMARY KEY (MaLoaiTV)
)
GO

CREATE TABLE ThanhVien
(
    MaTV INT IDENTITY NOT NULL,
    MaLoaiTV INT NULL,
    TaiKhoan NVARCHAR(100) NOT NULL,
    MatKhau NVARCHAR(100) NOT NULL,
    HoTen NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(255) NULL,
    Email NVARCHAR(255) NULL,
    SoDienThoai VARCHAR(12) NULL,
    CauHoi NVARCHAR(MAX) NULL,
    CauTraLoi NVARCHAR(MAX) NULL,
    HinhDaiDien NVARCHAR(MAX) DEFAULT ('default.png') NULL,
    MaToken NVARCHAR(50) NULL,
    ThoiGianMaToken DATETIME NULL,

    CONSTRAINT PK_TV PRIMARY KEY (MaTV),
    CONSTRAINT FK_TV_LTV FOREIGN KEY(MaLoaiTV) REFERENCES LoaiThanhVien(MaLoaiTV) ON DELETE CASCADE
)
GO

CREATE TABLE KhachHang
(
    MaKH INT IDENTITY NOT NULL,
    MaTV INT NULL,
    TenKH NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(255) NULL,
    Email NVARCHAR(255) NULL,
    SoDienThoai VARCHAR(12) NULL,

    CONSTRAINT PK_KH PRIMARY KEY(MaKH),
    CONSTRAINT FK_KH_TV FOREIGN KEY(MaTV) REFERENCES ThanhVien(MaTV) ON DELETE CASCADE
)
GO

CREATE TABLE DanhMuc
(
    MaDanhMuc INT IDENTITY NOT NULL,
    TenDanhMuc NVARCHAR(100) NOT NULL,

    CONSTRAINT PK_DM PRIMARY KEY(MaDanhMuc)
)
GO

CREATE TABLE LoaiSanPham
(
    MaLoaiSP INT IDENTITY NOT NULL,
    MaDanhMuc INT NOT NULL,
    TenLoaiSP NVARCHAR(100) NOT NULL,
    Icon NVARCHAR(MAX) NULL,
    BiDanh NVARCHAR(50) NULL,

    CONSTRAINT PK_LSP PRIMARY KEY(MaLoaiSP),
    CONSTRAINT FK_LSP_DM FOREIGN KEY(MaDanhMuc) REFERENCES DanhMuc(MaDanhMuc)
)
GO

CREATE TABLE KhuyenMai
(
    MaKhuyenMai INT IDENTITY NOT NULL,
    TenKhuyenMai NVARCHAR(255) NOT NULL,
    MoTa NVARCHAR(MAX) NULL,
    PhanTramGiamGia INT NOT NULL,
    NgayBatDau DATE NOT NULL,
    NgayKetThuc DATE NOT NULL,

    CONSTRAINT PK_KhuyenMai PRIMARY KEY(MaKhuyenMai)
)
GO

CREATE TABLE SanPham
(
    MaSP INT IDENTITY NOT NULL,
    MaNCC INT NOT NULL,
    MaNSX INT NOT NULL,
    MaLoaiSP INT NOT NULL,
	MaKhuyenMai  INT NULL,

    TenSP NVARCHAR(255) NOT NULL,
    DonGia DECIMAL(18,0) NULL,
    NgayCapNhat DATETIME NULL,
    MoTa NVARCHAR(MAX) NULL,
	CauHinh NVARCHAR(MAX) NULL,
    HinhAnh NVARCHAR(MAX) NULL,
    HinhAnh2 NVARCHAR(MAX) NULL,
    HinhAnh3 NVARCHAR(MAX) NULL,
    HinhAnh4 NVARCHAR(MAX) NULL,
    SoLuongTon INT NOT NULL,
    LuotXem INT NULL,
    LuotBinhChon INT NULL,
    LuotBinhLuan INT NULL,
    SoLanMua INT NULL,
    Moi BIT NOT NULL,
	BanChay BIT NOT NULL,
    DaXoa BIT NOT NULL,

    CONSTRAINT PK_SP PRIMARY KEY(MaSP), 
    CONSTRAINT FK_SP_LSP FOREIGN KEY(MaLoaiSP) REFERENCES LoaiSanPham(MaLoaiSP),
    CONSTRAINT FK_SP_NSX FOREIGN KEY(MaNSX) REFERENCES NhaSanXuat(MaNSX),
    CONSTRAINT FK_SP_NCC FOREIGN KEY(MaNCC) REFERENCES NhaCungCap(MaNCC),
	CONSTRAINT FK_SP_KM FOREIGN KEY(MaKhuyenMai) REFERENCES KhuyenMai(MaKhuyenMai)
)
GO

CREATE TABLE ChiTietPhieuNhap
(
    MaCTPN INT IDENTITY NOT NULL,
    MaPN INT NOT NULL,
    MaSP INT NOT NULL,
    DonGiaNhap DECIMAL(18,0) NOT NULL,
    SoLuongNhap INT NOT NULL,

    CONSTRAINT PK_CTPN PRIMARY KEY (MaCTPN),
    CONSTRAINT FK_CTPN_PN FOREIGN KEY(MaPN) REFERENCES PhieuNhap(MaPN),
    CONSTRAINT FK_CTPN_SP FOREIGN KEY(MaSP) REFERENCES SanPham(MaSP)
)
GO

CREATE TABLE DonDatHang
(
    MaDDH INT IDENTITY NOT NULL,
    MaKH INT NULL,
    NgayDatHang DATETIME NOT NULL,
    NgayGiao DATETIME NULL,
    DaThanhToan BIT NOT NULL,
    QuaTang NVARCHAR(255) NULL,
    TinhTrang NVARCHAR(50) NULL,
    DaXoa BIT DEFAULT(0) NULL,

    CONSTRAINT PK_DDH PRIMARY KEY(MaDDH),
    CONSTRAINT FK_DDH_KH FOREIGN KEY(MaKH) REFERENCES KhachHang(MaKH) ON DELETE CASCADE
)
GO

CREATE TABLE ChiTietDonDatHang
(
    MaChiTietDDH INT IDENTITY NOT NULL,
    MaDDH INT NOT NULL,
    MaSP INT NOT NULL,
    TenSP NVARCHAR(255) NOT NULL,
    SoLuong INT NOT NULL,
    DonGia DECIMAL(18,0) NOT NULL,

    CONSTRAINT PK_CTDDH PRIMARY KEY(MaChiTietDDH),
    CONSTRAINT FK_CTDDH_DDH FOREIGN KEY(MaDDH) REFERENCES DonDatHang(MaDDH),
    CONSTRAINT FK_CTDDH_SP FOREIGN KEY(MaSP) REFERENCES SanPham(MaSP)
)
GO

CREATE TABLE BinhLuan
(
    MaBinhLuan INT IDENTITY NOT NULL,
    MaTV INT NOT NULL,
    MaSP INT NOT NULL,
    NoiDungBL NVARCHAR(MAX) NULL,

    CONSTRAINT PK_BL PRIMARY KEY(MaBinhLuan),
    CONSTRAINT FK_BL_TV FOREIGN KEY(MaTV) REFERENCES ThanhVien(MaTV),
    CONSTRAINT FK_BL_SP FOREIGN KEY(MaSP) REFERENCES SanPham(MaSP)
)
GO

CREATE TABLE CongNo
(
    MaCongNo INT IDENTITY NOT NULL,
    MaNCC INT NOT NULL,
    SoTienNo DECIMAL(18, 0) NOT NULL,
    NgayCongNo DATE NOT NULL,

    CONSTRAINT PK_CongNo PRIMARY KEY (MaCongNo),
    CONSTRAINT FK_CongNo_NCC FOREIGN KEY (MaNCC) REFERENCES NhaCungCap(MaNCC) ON DELETE CASCADE
)
GO

CREATE TABLE Quyen
(
    MaQuyen NVARCHAR(50) NOT NULL,
    TenQuyen NVARCHAR(50),
    
    CONSTRAINT PK_QUYEN PRIMARY KEY(MaQuyen)
)
GO

CREATE TABLE LoaiThanhVien_Quyen
(
    MaLoaiTV INT NOT NULL,
    MaQuyen NVARCHAR(50) NOT NULL,
    GhiChu NVARCHAR(100),

    CONSTRAINT PK_LTV_QUYEN PRIMARY KEY(MaLoaiTV, MaQuyen),
    CONSTRAINT FK_LTVQ_Q FOREIGN KEY (MaQuyen) REFERENCES Quyen(MaQuyen),
    CONSTRAINT FK_LTVQ_TV FOREIGN KEY (MaLoaiTV) REFERENCES LoaiThanhVien(MaLoaiTV),
)
GO

INSERT INTO LoaiThanhVien(TenLoai, UuDai) VALUES
(N'Admin', null),
(N'Thường', null),
(N'Member', null),
(N'VIP', null)
GO

INSERT INTO Quyen(MaQuyen, TenQuyen) VALUES
('Admin', N'Quản trị'),
('Product', N'Sản phẩm')
GO

INSERT INTO LoaiThanhVien_Quyen(MaLoaiTV, MaQuyen, GhiChu) VALUES
(1, 'Admin', null),
(2, 'Product', null)
GO

--Insert THANHVIEN
--MaTV INT IDENTITY NOT NULL,
--MaLoaiTV INT NOT NULL,
--TaiKhoan NVARCHAR(100) NOT NULL,
--MatKhau NVARCHAR(100) NOT NULL,
--HoTen NVARCHAR(100) NOT NULL,
--DiaChi NVARCHAR(255) NULL,
--Email NVARCHAR(255) NULL,
--SoDienThoai VARCHAR(12) NULL,
--CauHoi NVARCHAR(MAX) NULL,
--CauTraLoi NVARCHAR(MAX) NULL

INSERT INTO ThanhVien(MaLoaiTV, TaiKhoan, MatKhau, HoTen, DiaChi, Email, SoDienThoai, CauHoi, CauTraLoi) VALUES
(1, N'ThanhLong', N'123456', N'NguyenThanhLong', N'An Tân, Thái Thụy, Thái Bình', N'thanhlong02@gmail.com', '05725756331', null, null),
(1, N'TaiLe', N'123456', N'Lê Hữu Tài', N'Số nhà 123, Xã Dĩ An, Huyện Dĩ An, Tỉnh Bình Dương', N'lehuutai09@gmail.com', '0324195842', null, null),
(2, N'ThanhThao', N'123456', N'Nguyễn Thị Thanh Thảo', N'212/321/17 Nguyễn Văn Nguyễn, Phường Tân Định, Quận 1, Thành phố Hồ Chí Minh', N'thanhthaonopro@gamil.com', '0342594124', null, null),
(2, N'CongHuy', N'123456', N'Huỳnh Công Huy', N'1028/39 Tân Kỳ Tân Quý, Phường Bình Hưng Hòa, Quận Bình Tân, Thành phố Hồ Chí Minh', N'conghuyhuynh1610@gmail.com', '0942943054', null, null)
GO

--INSERT KHACHHANG
--MaKH INT IDENTITY NOT NULL,
--MaTV INT NULL,
--TenKH NVARCHAR(100) NOT NULL,
--DiaChi NVARCHAR(255) NULL,
--Email NVARCHAR(255) NULL,
--SoDienThoai VARCHAR(12) NULL

INSERT INTO KhachHang(MaTV, TenKH, DiaChi, Email, SoDienThoai) VALUES
(1, N'NguyenThanhLong', N'An Tân, Thái Thụy, Thái Bình', N'thanhlong02@gmail.com', '05725756331'),
(2, N'Lê Hữu Tài', N'Số nhà 123, Xã Dĩ An, Huyện Dĩ An, Tỉnh Bình Dương', N'lehuutai09@gmail.com', '0324195842'),
(null, N'Lê Trí Cường', null, null, null),
(null, N'Nguyễn Quốc Tiến', null, null, null)
GO

--INSERT DONDATHANG
--MaDDH INT IDENTITY NOT NULL,
--MaKH INT NOT NULL,
--NgayDatHang DATETIME NOT NULL,
--TinhTrang BIT NOT NULL, 1: Đã giao; 0: Chưa giao
--NgayGiao DATETIME NULL,
--DaThanhToan BIT NOT NULL, 1: Đã thanh toán; 0: Chưa thanh toán
--UuDai INT NULL Đơn vị %



--INSERT NHACUNGCAP
--MaNCC INT IDENTITY NOT NULL,
--TenNCC NVARCHAR(100) NOT NULL,
--DiaChi NVARCHAR(255) NOT NULL,
--Email NVARCHAR(100) NOT NULL,
--SoDienThoai VARCHAR(12) NOT NULL,
--Fax NVARCHAR(50) NULL

INSERT INTO NhaCungCap(TenNCC, DiaChi, Email, SoDienThoai, Fax) VALUES
(N'Nguyễn Văn Hùng', N'Số nhà 1011, Phường Mỹ Đình, Quận Từ Liêm, Thành phố Hà Nội', N'nguyenvanhung@gmail.com', N'0362156365', null),
(N'Bùi Thanh Tân', N'Số nhà 1415, Phường Vạn Phúc, Quận Hà Đông, Thành phố Hà Nội', N'buithanhtan2020@gmail.com', N'03623141265', null), 
(N'Đinh Văn Dũng', N'Số nhà 1617, Phường Cầu Giấy, Quận Cầu Giấy, Thành phố Hà Nội', N'vandungdungvan@gmail.com', N'0309735176', null), 
(N'Nguyễn Thị Thanh Nhàn', N'Số nhà 1819, Phường Phúc Lợi, Quận Long Biên, Thành phố Hà Nội', N'thanhnhan@gmail.com', N'0309346376', null),
(N'Vũ Quang Vinh', N'Số nhà 2021, Phường Hà Trung, Thành phố Hà Tĩnh, Tỉnh Hà Tĩnh', N'quangvinh@gmail.com', N'0987535143', null),
(N'Nguyễn Hồng Sơn', N'Số nhà 2223, Phường Nam Định, Thành phố Nam Định, Tỉnh Nam Định', N'hongson@gmail.com', N'0989734211', null),
(N'Võ Thị Hồng Như', N'Số nhà 2627, Phường Hải Phòng, Quận Hải Phòng, Thành phố Hải Phòng', N'hongnhu@gmail.com', N'0409545113', null)
GO

INSERT INTO DanhMuc (TenDanhMuc) VALUES
(N'Điện thoại'),
(N'Phụ kiện')
GO

--INSERT LOAISANPHAM
--MaLoaiSP INT IDENTITY NOT NULL,
--TenLoaiSP NVARCHAR(100) NOT NULL,
--Icon NVARCHAR(MAX) NULL,
--BiDanh NVARCHAR(50) NULL

INSERT INTO LoaiSanPham(MaDanhMuc, TenLoaiSP, Icon, BiDanh) VALUES
(1, N'Iphone', N'logo-iphone-220x48.png', null),
(1, N'Oppo', N'OPPO42-b_5.jpg', null),
(1, N'Vivo', N'vivo-logo-220-220x48-3.png', null),
(1, N'Samsung', N'samsungnew-220x48-1.png', null),
(1, N'Xiaomi', N'logo-xiaomi-220x48-5.png', null),
(2, N'Sạc dự phòng', null, null),
(2, N'Tai nghe', null, null)
GO

--INSERT NHASANXUAT
--MaNSX INT IDENTITY NOT NULL,
--TenNSX NVARCHAR(100) NOT NULL, 
--ThongTin NVARCHAR(255) NULL,
--Logo NVARCHAR(MAX) NULL

INSERT INTO NhaSanXuat(TenNSX, ThongTin, Logo) VALUES
(N'Apple', N'Apple Inc. là một tập đoàn công nghệ có trụ sở tại Cupertino, California, Mỹ. Họ nổi tiếng với việc sản xuất các sản phẩm công nghệ như iPhone, iPad, Macbook, và nhiều sản phẩm khác. Apple được thành lập vào năm 1976 bởi Steve Jobs, Steve Wozniak và Ronald Wayne. Hãng có một lịch sử dài về đổi mới và thiết kế đẹp, và họ luôn nằm trong số những công ty công nghệ hàng đầu thế giới.', N'Iphone.png'),
(N'OPPO ', N'OPPO là một công ty điện tử tiêu dùng và điện thoại di động có trụ sở tại Đông Quan, Quảng Đông, Trung Quốc. OPPO được thành lập vào năm 2001 và nhanh chóng trở thành một trong những thương hiệu điện thoại di động phổ biến trên toàn thế giới. Họ nổi tiếng với việc sản xuất các smartphone chất lượng cao và camera mạnh mẽ.', N'oppo.png'),
(N'Vivo', N'Vivo là một công ty điện tử tiêu dùng và điện thoại di động có trụ sở tại Đông Quan, Quảng Đông, Trung Quốc. Họ được thành lập vào năm 2009 và cũng là một thương hiệu nổi tiếng trong ngành công nghệ di động. Vivo tập trung vào việc cung cấp các sản phẩm có hiệu năng mạnh mẽ và tính năng camera tiên tiến.', N'vivo.png'),
(N'Samsung', N'Samsung là một tập đoàn đa quốc gia có trụ sở tại Seoul, Hàn Quốc. Họ là một trong những công ty điện tử lớn nhất thế giới và sản xuất nhiều loại sản phẩm khác nhau, bao gồm điện thoại di động, TV, máy lạnh, và nhiều sản phẩm khác. Samsung được thành lập vào năm 1938 và có một lịch sử dài về đổi mới và phát triển công nghệ.', N'samsung.png'),
(N'Xiaomi', N'Xiaomi là một công ty công nghệ có trụ sở tại Bắc Kinh, Trung Quốc. Họ được thành lập vào năm 2010 và nhanh chóng trở thành một trong những thương hiệu điện thoại di động phổ biến và giá cả phải chăng trên toàn thế giới. Xiaomi cũng sản xuất các sản phẩm khác như máy tính bảng, điện tử gia đình và các sản phẩm thông minh.', N'xiaomi.png'),
(N'AVA', N'AVA là một công ty chuyên sản xuất và phân phối các sản phẩm điện tử tiêu dùng, bao gồm tai nghe, loa, và các thiết bị âm thanh. Họ được biết đến với chất lượng âm thanh cao cấp và thiết kế đẹp mắt. AVA đã hoạt động trong ngành công nghiệp âm thanh từ nhiều năm và có mạng lưới phân phối rộng khắp thế giới.', N'ava.png'),
(N'HAVIT', N'HAVIT là một thương hiệu nổi tiếng trong lĩnh vực sản xuất các sản phẩm công nghệ tiêu dùng, bao gồm bàn phím, chuột, tai nghe, và các phụ kiện máy tính. HAVIT chú trọng vào việc cung cấp sản phẩm có chất lượng và giá cả hợp lý cho người tiêu dùng. Họ có một loạt sản phẩm đa dạng để đáp ứng nhu cầu của khách hàng trên toàn thế giới.', N'havit.png')
GO

--INSERT PHIEUNHAP
--MaPN INT IDENTITY NOT NULL,
--MaNCC INT NOT NULL,
--NgayNhap DATETIME NOT NULL,
--DaXoa BIT NOT NULL 1: Đã xóa; 0: Chưa xóa

INSERT INTO PhieuNhap(MaNCC, NgayNhap, DaXoa) VALUES
(1, '2023-08-01', 0),
(2, '2023-12-01', 0),
(3, '2023-10-01', 0), 
(4, '2023-11-01', 0), 
(5, '2024-01-01', 0), 
(6, '2023-05-01', 0),
(7, '2023-06-01', 0)
GO

--INSERT KHUYENMAI
--MaKhuyenMai INT IDENTITY NOT NULL,
--TenKhuyenMai NVARCHAR(255) NOT NULL,
--MoTa NVARCHAR(MAX) NULL,
--PhanTramGiamGia INT NOT NULL,
--NgayBatDau DATE NOT NULL,
--NgayKetThuc DATE NOT NULL

INSERT INTO KhuyenMai(TenKhuyenMai, MoTa, PhanTramGiamGia, NgayBatDau, NgayKetThuc) VALUES
(N'Khuyễn mãi đợt 1 cho iPhone', N'Khuyến mãi 5% cho Iphone', 5, '2024-04-20', '2024-04-30'),
(N'Khuyến mãi đợt 2 cho Samsung', N'Khuyến mãi 10% cho Samsung', 10, '2024-04-20', '2024-04-30'),
(N'Khuyến mãi đợt 3 cho tai nghe', N'Khuyến mãi 15% cho tai nghe', 15, '2024-04-20', '2024-04-30'),
(N'Khuyến mãi đợt 4 cho sạc dự phòng', N'Khuyến mãi 20% cho tai nghe', 20, '2024-04-20', '2024-04-30')
GO

--INSERT SANPHAM
--MaSP INT IDENTITY NOT NULL,
--MaNCC INT NOT NULL,
--MaNSX INT NOT NULL,
--MaLoaiSP INT NOT NULL,

--TenSP NVARCHAR(255) NOT NULL,
--DonGia DECIMAL(18,0) NOT NULL,
--NgayCapNhat DATETIME NULL,
--MoTa NVARCHAR(MAX) NULL,
--HinhAnh NVARCHAR(MAX) NULL,
--HinhAnh2 NVARCHAR(MAX) NULL,
--HinhAnh3 NVARCHAR(MAX) NULL,
--SoLuongTon INT NOT NULL,
--LuotXem INT NULL,
--LuotBinhChon INT NULL,
--LuotBinhLuan INT NULL,
--SoLanMua INT NULL,
--Moi BIT NOT NULL,
--DaXoa BIT NOT NULL
INSERT INTO SanPham(MaNCC, MaNSX, MaLoaiSP, MaKhuyenMai, TenSP, DonGia, NgayCapNhat, MoTa,CauHinh, HinhAnh, HinhAnh2, HinhAnh3,HinhAnh4, SoLuongTon, LuotXem, LuotBinhChon, LuotBinhLuan, SoLanMua, Moi,BanChay, DaXoa) VALUES
(1, 1, 1, 1, N'Điện thoại iPhone 14 Pro Max 128GB', 28000000, '2023-08-01', N'iPhone 14 Pro Max một siêu phẩm trong giới smartphone được nhà Táo tung ra thị trường vào tháng 09/2022. Máy trang bị con chip Apple A16 Bionic vô cùng mạnh mẽ, đi kèm theo đó là thiết kế hình màn hình mới, hứa hẹn mang lại những trải nghiệm đầy mới mẻ cho người dùng iPhone.',N'Kích thước màn hình: 6.7" <br>Công nghệ màn hình: OLED <br>Độ phân giải: Super Retina XDR (1290 x 2796 Pixels) <br>Tần số quét : 120Hz <br>Camera Sau: Chính 48 MP & Phụ 12 MP, 12 MP <br>Camera Trước: 12MP <br>Hệ điều hành: iOS 16 <br>Chip: Apple A16 Bionic 6 nhân <br>Ram: 6 GB <br>Dung Lượng: 128GB <br>Sim và Sạc: 1 Nano SIM & 1 eSIM, 20W <br>Dung lượng pin: 4323 mAh <br>Bảo hành : 12 tháng <br>', N'iphone-14-pro-max-purple-1.jpg', N'iphone-14-pro-max-vang-1.jpg', N'iphone-14-pro-max-note.jpg',N'iphone-14-pro-max-vang-1.jpg', 10, null, null, null, 0, 1,1, 0),
(1, 1, 1, 1, N'Điện thoại iPhone 14 Pro Max 256GB', 29000000, '2023-08-01', N'iPhone 14 Pro Max một siêu phẩm trong giới smartphone được nhà Táo tung ra thị trường vào tháng 09/2022. Máy trang bị con chip Apple A16 Bionic vô cùng mạnh mẽ, đi kèm theo đó là thiết kế hình màn hình mới, hứa hẹn mang lại những trải nghiệm đầy mới mẻ cho người dùng iPhone.',N'Kích thước màn hình: 6.7" <br>Công nghệ màn hình: OLED <br>Độ phân giải: Super Retina XDR (1290 x 2796 Pixels) <br>Tần số quét : 120Hz <br>Camera Sau: Chính 48 MP & Phụ 12 MP, 12 MP <br>Camera Trước: 12MP <br>Hệ điều hành: iOS 16 <br>Chip: Apple A16 Bionic 6 nhân <br>Ram: 6 GB <br>Dung Lượng: 256GB <br>Sim và Sạc: 1 Nano SIM & 1 eSIM, 20W <br>Dung lượng pin: 4323 mAh <br>Bảo hành : 12 tháng <br>', N'iphone-14-pro-max-purple-1.jpg', N'iphone-14-pro-max-vang-1.jpg', N'iphone-14-pro-max-note.jpg',N'iphone-14-pro-max-vang-1.jpg', 10, null, null, null, 0, 1,1, 0),
(1, 1, 1, 1, N'Điện thoại iPhone 14 Pro Max 512GB', 30000000, '2023-08-01', N'iPhone 14 Pro Max một siêu phẩm trong giới smartphone được nhà Táo tung ra thị trường vào tháng 09/2022. Máy trang bị con chip Apple A16 Bionic vô cùng mạnh mẽ, đi kèm theo đó là thiết kế hình màn hình mới, hứa hẹn mang lại những trải nghiệm đầy mới mẻ cho người dùng iPhone.',N'Kích thước màn hình: 6.7" <br>Công nghệ màn hình: OLED <br>Độ phân giải: Super Retina XDR (1290 x 2796 Pixels) <br>Tần số quét : 120Hz <br>Camera Sau: Chính 48 MP & Phụ 12 MP, 12 MP <br>Camera Trước: 12MP <br>Hệ điều hành: iOS 16 <br>Chip: Apple A16 Bionic 6 nhân <br>Ram: 6 GB <br>Dung Lượng: 512GB <br>Sim và Sạc: 1 Nano SIM & 1 eSIM, 20W <br>Dung lượng pin: 4323 mAh <br>Bảo hành : 12 tháng <br>', N'iphone-14-pro-max-purple-1.jpg', N'iphone-14-pro-max-vang-1.jpg', N'iphone-14-pro-max-note.jpg',N'iphone-14-pro-max-vang-1.jpg', 10, null, null, null, 0, 1,0, 0),
(1, 1, 1, 1, N'Điện thoại iPhone 14 Pro Max 1TB', 31000000, '2023-08-01', N'iPhone 14 Pro Max một siêu phẩm trong giới smartphone được nhà Táo tung ra thị trường vào tháng 09/2022. Máy trang bị con chip Apple A16 Bionic vô cùng mạnh mẽ, đi kèm theo đó là thiết kế hình màn hình mới, hứa hẹn mang lại những trải nghiệm đầy mới mẻ cho người dùng iPhone.',N'Kích thước màn hình: 6.7" <br>Công nghệ màn hình: OLED <br>Độ phân giải: Super Retina XDR (1290 x 2796 Pixels) <br>Tần số quét : 120Hz <br>Camera Sau: Chính 48 MP & Phụ 12 MP, 12 MP <br>Camera Trước: 12MP <br>Hệ điều hành: iOS 16 <br>Chip: Apple A16 Bionic 6 nhân <br>Ram: 6 GB <br>Dung Lượng: 1TB <br>Sim và Sạc: 1 Nano SIM & 1 eSIM, 20W <br>Dung lượng pin: 4323 mAh <br>Bảo hành : 12 tháng <br>', N'iphone-14-pro-max-purple-1.jpg', N'iphone-14-pro-max-vang-1.jpg', N'iphone-14-pro-max-note.jpg',N'iphone-14-pro-max-vang-1.jpg', 10, null, null, null, 0, 1,0, 0),
(1, 1, 1, 1, N'Điện thoại iPhone 13 Pro Max 123GB', 22000000, '2023-08-01', N'Điện thoại iPhone 13 Pro Max 128 GB - siêu phẩm được mong chờ nhất ở nửa cuối năm 2021 đến từ Apple. Máy có thiết kế không mấy đột phá khi so với người tiền nhiệm, bên trong đây vẫn là một sản phẩm có màn hình siêu đẹp, tần số quét được nâng cấp lên 120 Hz mượt mà, cảm biến camera có kích thước lớn hơn, cùng hiệu năng mạnh mẽ với sức mạnh đến từ Apple A15 Bionic, sẵn sàng cùng bạn chinh phục mọi thử thách.',N'Kích thước màn hình: 6.7" <br>Công nghệ màn hình: OLED <br>Độ phân giải: Super Retina XDR (1284 x 2778 Pixels) <br>Tần số quét : 120Hz <br>Camera Sau: 3 camera 12 MP<br>Camera Trước: 12MP <br>Hệ điều hành: iOS 15 <br>Chip: Apple A15 Bionic 6 nhân <br>Ram: 6 GB <br>Dung Lượng: 128GB <br>Sim và Sạc: 1 Nano SIM & 1 eSIM, 20W <br>Dung lượng pin: 4352 mAh <br>Bảo hành : 12 tháng <br>', N'iphone-13-pro-max-1-1.jpg', N'iphone-13-pro-max-1.jpg', N'iphone-13-pro-max-n-2.jpg',N'iphone-13-pro-max-1.jpg', 10, null, null, null, 0, 1,1, 0),
(1, 1, 1, 1, N'Điện thoại iPhone 13 Pro Max 256GB', 23000000, '2023-08-01', N'Điện thoại iPhone 13 Pro Max 256 GB - siêu phẩm được mong chờ nhất ở nửa cuối năm 2021 đến từ Apple. Máy có thiết kế không mấy đột phá khi so với người tiền nhiệm, bên trong đây vẫn là một sản phẩm có màn hình siêu đẹp, tần số quét được nâng cấp lên 120 Hz mượt mà, cảm biến camera có kích thước lớn hơn, cùng hiệu năng mạnh mẽ với sức mạnh đến từ Apple A15 Bionic, sẵn sàng cùng bạn chinh phục mọi thử thách.',N'Kích thước màn hình: 6.7" <br>Công nghệ màn hình: OLED <br>Độ phân giải: Super Retina XDR (1284 x 2778 Pixels) <br>Tần số quét : 120Hz <br>Camera Sau: 3 camera 12 MP<br>Camera Trước: 12MP <br>Hệ điều hành: iOS 15 <br>Chip: Apple A15 Bionic 6 nhân <br>Ram: 6 GB <br>Dung Lượng: 256GB <br>Sim và Sạc: 1 Nano SIM & 1 eSIM, 20W <br>Dung lượng pin: 4352 mAh <br>Bảo hành : 12 tháng <br>', N'iphone-13-pro-max-1-1.jpg', N'iphone-13-pro-max-1.jpg', N'iphone-13-pro-max-n-2.jpg',N'iphone-13-pro-max-1.jpg', 10, null, null, null, 0, 1,1, 0),
(1, 1, 1, 1, N'Điện thoại iPhone 13 Pro Max 512GB', 24000000, '2023-08-01', N'Điện thoại iPhone 13 Pro Max 512 GB - siêu phẩm được mong chờ nhất ở nửa cuối năm 2021 đến từ Apple. Máy có thiết kế không mấy đột phá khi so với người tiền nhiệm, bên trong đây vẫn là một sản phẩm có màn hình siêu đẹp, tần số quét được nâng cấp lên 120 Hz mượt mà, cảm biến camera có kích thước lớn hơn, cùng hiệu năng mạnh mẽ với sức mạnh đến từ Apple A15 Bionic, sẵn sàng cùng bạn chinh phục mọi thử thách.',N'Kích thước màn hình: 6.7" <br>Công nghệ màn hình: OLED <br>Độ phân giải: Super Retina XDR (1284 x 2778 Pixels) <br>Tần số quét : 120Hz <br>Camera Sau: 3 camera 12 MP<br>Camera Trước: 12MP <br>Hệ điều hành: iOS 15 <br>Chip: Apple A15 Bionic 6 nhân <br>Ram: 6 GB <br>Dung Lượng: 512GB <br>Sim và Sạc: 1 Nano SIM & 1 eSIM, 20W <br>Dung lượng pin: 4352 mAh <br>Bảo hành : 12 tháng <br>', N'iphone-13-pro-max-1-1.jpg', N'iphone-13-pro-max-1.jpg', N'iphone-13-pro-max-n-2.jpg',N'iphone-13-pro-max-1.jpg', 10, null, null, null, 0, 1,0, 0),
(1, 1, 1, 1, N'Điện thoại iPhone 12 Pro Max 128GB', 18000000, '2023-08-01', N'iPhone 12 Pro Max 128 GB một siêu phẩm smartphone đến từ Apple. Máy có một hiệu năng hoàn toàn mạnh mẽ đáp ứng tốt nhiều nhu cầu đến từ người dùng và mang trong mình một thiết kế đầy vuông vức, sang trọng.',N'Kích thước màn hình: 6.7" <br>Công nghệ màn hình: OLED <br>Độ phân giải: Super Retina XDR (1284 x 2778 Pixels) <br>Tần số quét : 60Hz <br>Camera Sau: 3 camera 12 MP<br>Camera Trước: 12MP <br>Hệ điều hành: iOS 15 <br>Chip: Apple A14 Bionic 6 nhân <br>Ram: 6 GB <br>Dung Lượng: 128GB <br>Sim và Sạc: 1 Nano SIM & 1 eSIM, 20W <br>Dung lượng pin: 3687 mAh <br>Bảo hành : 12 tháng <br>', N'iphone-12-pro-max-512gb-1-org.jpg', N'iphone-12-pro-max-512gb-bac-1-org.jpg', N'iphone-12-pro-max-512gb-note-2.jpg',N'iphone-12-pro-max-512gb-bac-1-org.jpg', 10, null, null, null, 0, 0,1, 0),
(1, 1, 1, 1, N'Điện thoại iPhone 12 Pro Max 256GB', 19000000, '2023-08-01', N'iPhone 12 Pro Max 256 GB một siêu phẩm smartphone đến từ Apple. Máy có một hiệu năng hoàn toàn mạnh mẽ đáp ứng tốt nhiều nhu cầu đến từ người dùng và mang trong mình một thiết kế đầy vuông vức, sang trọng.',N'Kích thước màn hình: 6.7" <br>Công nghệ màn hình: OLED <br>Độ phân giải: Super Retina XDR (1284 x 2778 Pixels) <br>Tần số quét : 120Hz <br>Camera Sau: 3 camera 12 MP<br>Camera Trước: 12MP <br>Hệ điều hành: iOS 15 <br>Chip: Apple A15 Bionic 6 nhân <br>Ram: 6 GB <br>Dung Lượng: 256GB <br>Sim và Sạc: 1 Nano SIM & 1 eSIM, 20W <br>Dung lượng pin: 3687 mAh <br>Bảo hành : 12 tháng <br>', N'iphone-12-pro-max-512gb-1-org.jpg', N'iphone-12-pro-max-512gb-bac-1-org.jpg', N'iphone-12-pro-max-512gb-note-2.jpg',N'iphone-12-pro-max-512gb-bac-1-org.jpg', 10, null, null, null, 0, 0,0, 0),
(2, 2, 2, null, N'Điện thoại OPPO Find N2 Flip 5G', 3000000, '2023-08-01', N'OPPO Find N2 Flip 5G - chiếc điện thoại gập đầu tiên của OPPO đã được giới thiệu chính thức tại Việt Nam vào tháng 03/2023. Sở hữu cấu hình mạnh mẽ cùng thiết kế siêu nhỏ gọn giúp tối ưu kích thước, chiếc điện thoại sẽ cùng bạn nổi bật trong mọi không gian với vẻ ngoài đầy cá tính.',N'Kích thước màn hình: Chính 6.8" & Phụ 3.26" <br>Công nghệ màn hình: AMOLED <br>Độ phân giải: Chính: FHD+ (2520 x 1080 Pixels) & Phụ: (720 x 382 Pixels) <br>Tần số quét : 120 Hz & 60 Hz <br>Camera Sau: Chính 50 MP & Phụ 8 MP<br>Camera Trước: 32MP <br>Hệ điều hành: Android 13 <br>Chip: MediaTek Dimensity 9000+ 8 nhân <br>Ram: 8 GB <br>Dung Lượng: 256GB <br>Sim và Sạc: 2 Nano SIM, 44W <br>Dung lượng pin: 4300 mAh <br>Bảo hành : 12 tháng <br>', N'oppo-n2-flip-tim-1-1.jpg', N'oppo-n2-flip-den-1.jpg', N'oppo-n2-flip-tim-note.jpg',N'oppo-n2-flip-den-1.jpg', 10, null, null, null, 0, 1,0, 0),
(2, 2, 2, null, N'Điện thoại OPPO Reno10 Pro 5G', 4000000, '2023-08-01', N'OPPO Reno10 Pro 5G là một trong những sản phẩm của OPPO được ra mắt trong 2023. Với thiết kế đẹp mắt, màn hình lớn và hiệu năng mạnh mẽ, Reno10 Pro chắc chắn sẽ là lựa chọn đáng cân nhắc dành cho những ai đang tìm kiếm chiếc máy tầm trung để phục vụ tốt mọi nhu cầu.',N'Kích thước màn hình: 6.7"<br>Công nghệ màn hình: Chính: AMOLED<br>Độ phân giải: Chính: Full HD+ (1080 x 2412 Pixels)<br>Tần số quét : 120 Hz <br>Camera Sau: Chính 50 MP & Phụ 32 MP, 8 MP<br>Camera Trước: 32 MP <br>Hệ điều hành: Android 13<br>Chip: Snapdragon Snapdragon 778G 5G 8 nhân<br>Ram: 12 GB <br>Dung Lượng: 256 GB <br>Sim và Sạc: 2 Nano SIM, 80W <br>Dung lượng pin: 4600 mAh <br>Bảo hành : 12 tháng ', N'oppo-reno10-pro-xam-1-1.jpg', N'oppo-reno10-pro-tim-1-2.jpg', N'oppo-reno10-pro-note.jpg',N'oppo-reno10-pro-tim-1-2.jpg', 10, null, null, null, 0, 1,1, 0),
(2, 2, 2, null, N'Điện thoại OPPO A77s', 5000000, '2023-08-01', N'OPPO vừa cho ra mắt mẫu điện thoại tầm trung mới với tên gọi OPPO A77s, máy sở hữu màn hình lớn, thiết kế đẹp mắt, hiệu năng ổn định cùng khả năng mở rộng RAM lên đến 8 GB vô cùng nổi bật trong phân khúc.',N'Kích thước màn hình: 6.56" <br>Công nghệ màn hình: AMOLED <br>Độ phân giải: Chính: Full HD+ (1080 x 2376 Pixels)<br>Tần số quét : 120 Hz <br>Camera Sau: Chính 64 MP & Phụ 8 MP, 2 MP<br>Camera Trước: 32MP <br>Hệ điều hành: Android 12 <br>Chip: MediaTek Dimensity 1300 8 nhân<br>Ram: 8 GB <br>Dung Lượng: 128GB <br>Sim và Sạc: 2 Nano SIM, 66W <br>Dung lượng pin: 4830 mAh <br>Bảo hành : 12 tháng ', N'oppo-a77s-den-1.jpg', N'oppo-a77s-xanh-1.jpg', N'oppo-a77s-note-2.jpg', N'oppo-a77s-xanh-1.jpg', 10, null, null, null, 0, 1, 0,0),
(3, 3, 3, null, N'Điện thoại vivo V25 Pro 5G', 5000000, '2023-08-01', N'VIVO V25 Pro 5G vừa được ra mắt với một mức giá bán cực kỳ hấp dẫn, thế mạnh của máy thuộc về phần hiệu năng nhờ trang bị con chip MediaTek Dimensity 1300 và cụm camera sắc nét 64 MP, hứa hẹn mang lại cho người dùng những trải nghiệm ổn định trong suốt quá trình sử dụng.',N'Kích thước màn hình: 6.56" <br>Công nghệ màn hình: AMOLED <br>Độ phân giải: Chính: Full HD+ (1080 x 2376 Pixels)<br>Tần số quét : 120 Hz <br>Camera Sau: Chính 64 MP & Phụ 8 MP, 2 MP<br>Camera Trước: 32MP <br>Hệ điều hành: Android 12 <br>Chip: MediaTek Dimensity 1300 8 nhân<br>Ram: 8 GB <br>Dung Lượng: 128GB <br>Sim và Sạc: 2 Nano SIM, 66W <br>Dung lượng pin: 4830 mAh <br>Bảo hành : 12 tháng ', N'vivo-v25-pro-5g-sld-xanh-1.jpg', N'vivo-v25-pro-5g-den-1.jpg', N'vivo-v25-pro-5g-note-2.jpg', N'vivo-v25-pro-5g-den-1.jpg', 10, null, null, null, 0, 1,0, 0),
(3, 3, 3, null, N'Điện thoại vivo Y02A', 6600000, '2023-08-01', N'VIVO Y02A mẫu điện thoại được nhà vivo cho ra mắt hướng đến nhóm người dùng yêu thích sự đơn giản trong thiết kế, hiệu năng tốt có thể xử lý các tác vụ thường ngày và một viên pin lớn đáp ứng được nhu cầu sử dụng lâu dài.',N'Kích thước màn hình: 6.51"<br>Công nghệ màn hình: IPS LCD<br>Độ phân giải: Chính: HD+ (720 x 1600 Pixels)<br>Tần số quét : 60 Hz <br>Camera Sau: 8MP<br>Camera Trước: 5MP <br>Hệ điều hành: Android 12 <br>Chip: MediaTek Helio P35 8 nhân<br>Ram: 3 GB <br>Dung Lượng: 32GB <br>Sim và Sạc: 2 Nano SIM, 10W <br>Dung lượng pin: 5000 mAh <br>Bảo hành : 12 tháng ', N'vivo-y02-den-1.jpg', N'vivo-y02-tim-1.jpg', N'vivo-y02-note.jpg',N'vivo-y02-tim-1.jpg', 10, null, null, null, 0, 1,1, 0),
(3, 3, 3, null, N'Điện thoại vivo V27e', 3300000, '2023-08-01', N'vivo V27e một trong những chiếc điện thoại tầm trung nổi bật của vivo trong năm 2023. Với thiết kế độc đáo và khả năng chụp ảnh - quay phim ấn tượng, vì thế máy đã mang lại cho vivo nhiều niềm tự hào khi ra mắt tại thị trường Việt Nam, hứa hẹn mang đến trải nghiệm tuyệt vời đến với người dùng.',N'Kích thước màn hình: 6.62"<br>Công nghệ màn hình: AMOLED<br>Độ phân giải: Chính: Full HD+ (1080 x 2400 Pixels)<br>Tần số quét : 120 Hz <br>Camera Sau: Chính 64 MP & Phụ 2 MP, 2 MP<br>Camera Trước: 32MP <br>Hệ điều hành: Android 13<br>Chip: MediaTek Helio G99 8 nhân<br>Ram: 8 GB <br>Dung Lượng: 256GB <br>Sim và Sạc: 2 Nano SIM, 66W <br>Dung lượng pin: 4600 mAh <br>Bảo hành : 12 tháng ', N'vivo-v27e-tim-1-1.jpg', N'vivo-v27e-den-1.jpg', N'vivo-v27e-note.jpg',N'vivo-v27e-den-1.jpg', 10, null, null, null, 0, 1, 1,0),
(4, 4, 4, 2, N'Điện thoại Samsung Galaxy Z Fold5 5G 256GB ', 7000000, '2023-08-01', N'Samsung Galaxy Z Fold5 là mẫu điện thoại cao cấp được ra mắt vào tháng 07/2023 với nhiều điểm đáng chú ý như thiết kế gập độc đáo, hiệu năng mạnh mẽ cùng camera quay chụp tốt, điều này giúp cho máy thu hút được nhiều sự quan tâm của đông đảo người dùng yêu công nghệ hiện nay.',N'Kích thước màn hình: Chính 7.6" & Phụ 6.2" <br>Công nghệ màn hình: Dynamic AMOLED 2X<br>Độ phân giải: Chính: Chính: QXGA+ (2176 x 1812 Pixels) & Phụ: HD+ (2316 x 904 Pixels)<br>Tần số quét : 120 Hz <br>Camera Sau: Chính 50 MP & Phụ 12 MP, 10 MP<br>Camera Trước: 10 MP & 4 MPP <br>Hệ điều hành: Android 13<br>Chip: Snapdragon 8 Gen 2 for Galaxy<br>Ram: 12 GB <br>Dung Lượng: 256GB <br>Sim và Sạc: 2 Nano SIM hoặc 1 Nano SIM + 1 eSIM, 25W <br>Dung lượng pin: 4400 mAh <br>Bảo hành : 12 tháng ', N'samsung-galaxy-zfold5-xanh-256gb-1-1.jpg', N'samsung-galaxy-zfold5-den-256gb-1.jpg', N'samsung-galaxy-zfold5-note.jpg',N'samsung-galaxy-zfold5-den-256gb-1.jpg', 10, null, null, null, 0, 1,1, 0),
(4, 4, 4, 2, N'Điện thoại Samsung Galaxy Z Fold5 5G 512GB ', 7900000, '2023-08-01', N'Samsung Galaxy Z Fold5 là mẫu điện thoại cao cấp được ra mắt vào tháng 07/2023 với nhiều điểm đáng chú ý như thiết kế gập độc đáo, hiệu năng mạnh mẽ cùng camera quay chụp tốt, điều này giúp cho máy thu hút được nhiều sự quan tâm của đông đảo người dùng yêu công nghệ hiện nay.',N'Kích thước màn hình: Chính 7.6" & Phụ 6.2" <br>Công nghệ màn hình: Dynamic AMOLED 2X<br>Độ phân giải: Chính: Chính: QXGA+ (2176 x 1812 Pixels) & Phụ: HD+ (2316 x 904 Pixels)<br>Tần số quét : 120 Hz <br>Camera Sau: Chính 50 MP & Phụ 12 MP, 10 MP<br>Camera Trước: 10 MP & 4 MPP <br>Hệ điều hành: Android 13<br>Chip: Snapdragon 8 Gen 2 for Galaxy<br>Ram: 12 GB <br>Dung Lượng: 512GB <br>Sim và Sạc: 2 Nano SIM hoặc 1 Nano SIM + 1 eSIM, 25W <br>Dung lượng pin: 4400 mAh <br>Bảo hành : 12 tháng ', N'samsung-galaxy-zfold5-xanh-256gb-1-1.jpg', N'samsung-galaxy-zfold5-den-256gb-1.jpg', N'samsung-galaxy-zfold5-note.jpg',N'samsung-galaxy-zfold5-den-256gb-1.jpg', 10, null, null, null, 0, 1,0, 0),
(4, 4, 4, 2, N'Điện thoại Samsung Galaxy Z Fold5 5G 1TB', 8900000, '2023-08-01', N'Samsung Galaxy Z Fold5 là mẫu điện thoại cao cấp được ra mắt vào tháng 07/2023 với nhiều điểm đáng chú ý như thiết kế gập độc đáo, hiệu năng mạnh mẽ cùng camera quay chụp tốt, điều này giúp cho máy thu hút được nhiều sự quan tâm của đông đảo người dùng yêu công nghệ hiện nay.',N'Kích thước màn hình: Chính 7.6" & Phụ 6.2" <br>Công nghệ màn hình: Dynamic AMOLED 2X<br>Độ phân giải: Chính: Chính: QXGA+ (2176 x 1812 Pixels) & Phụ: HD+ (2316 x 904 Pixels)<br>Tần số quét : 120 Hz <br>Camera Sau: Chính 50 MP & Phụ 12 MP, 10 MP<br>Camera Trước: 10 MP & 4 MPP <br>Hệ điều hành: Android 13<br>Chip: Snapdragon 8 Gen 2 for Galaxy<br>Ram: 12 GB <br>Dung Lượng: 1TB <br>Sim và Sạc: 2 Nano SIM hoặc 1 Nano SIM + 1 eSIM, 25W <br>Dung lượng pin: 4400 mAh <br>Bảo hành : 12 tháng ', N'samsung-galaxy-zfold5-xanh-256gb-1-1.jpg', N'samsung-galaxy-zfold5-den-256gb-1.jpg', N'samsung-galaxy-zfold5-note.jpg',N'samsung-galaxy-zfold5-den-256gb-1.jpg', 10, null, null, null, 0, 1,1, 0),
(4, 4, 4, 2, N'Điện thoại Samsung Galaxy Z Flip4 5G 128GB', 5000000, '2023-08-01', N'Samsung Galaxy Z Flip4 128GB đã chính thức ra mắt thị trường công nghệ, đánh dấu sự trở lại của Samsung trên con đường định hướng người dùng về sự tiện lợi trên những chiếc điện thoại gập. Với độ bền được gia tăng cùng kiểu thiết kế đẹp mắt giúp Flip4 trở thành một trong những tâm điểm sáng giá cho nửa cuối năm 2022.',N'Kích thước màn hình: Chính 6.7" & Phụ 1.9"<br>Công nghệ màn hình: Chính: Dynamic AMOLED 2X, Phụ: Super AMOLED<br>Độ phân giải: Chính: Chính: Chính: FHD+ (2640 x 1080 Pixels) x Phụ: (260 x 512 Pixels)<br>Tần số quét : 120 Hz <br>Camera Sau: Chính 2 camera 12 MP<br>Camera Trước: 10 MP <br>Hệ điều hành: Android 12<br>Chip: Snapdragon Snapdragon 8+ Gen 1 8 nhân<br>Ram: 8 GB <br>Dung Lượng: 128 GB <br>Sim và Sạc: 1 Nano SIM & 1 eSIM, 25W <br>Dung lượng pin: 3700 mAh <br>Bảo hành : 12 tháng ', N'samsung-galaxy-flip4-glr-tim-1.jpg', N'samsung-galaxy-flip-den-1.jpg', N'samsung-galaxy-z-flip4-note-1-1.jpg',N'samsung-galaxy-flip-den-1.jpg', 10, null, null, null, 0, 1,0, 0),
(4, 4, 4, 2, N'Điện thoại Samsung Galaxy Z Flip4 5G 256GB', 5900000, '2023-08-01', N'Samsung Galaxy Z Flip4 256GB đã chính thức ra mắt thị trường công nghệ, đánh dấu sự trở lại của Samsung trên con đường định hướng người dùng về sự tiện lợi trên những chiếc điện thoại gập. Với độ bền được gia tăng cùng kiểu thiết kế đẹp mắt giúp Flip4 trở thành một trong những tâm điểm sáng giá cho nửa cuối năm 2022.',N'Kích thước màn hình: Chính 6.7" & Phụ 1.9"<br>Công nghệ màn hình: Chính: Dynamic AMOLED 2X, Phụ: Super AMOLED<br>Độ phân giải: Chính: Chính: Chính: FHD+ (2640 x 1080 Pixels) x Phụ: (260 x 512 Pixels)<br>Tần số quét : 120 Hz <br>Camera Sau: Chính 2 camera 12 MP<br>Camera Trước: 10 MP <br>Hệ điều hành: Android 12<br>Chip: Snapdragon Snapdragon 8+ Gen 1 8 nhân<br>Ram: 8 GB <br>Dung Lượng: 256 GB <br>Sim và Sạc: 1 Nano SIM & 1 eSIM, 25W <br>Dung lượng pin: 3700 mAh <br>Bảo hành : 12 tháng ', N'samsung-galaxy-flip4-glr-tim-1.jpg', N'samsung-galaxy-flip-den-1.jpg', N'samsung-galaxy-z-flip4-note-1-1.jpg',N'samsung-galaxy-flip-den-1.jpg', 10, null, null, null, 0, 1,1, 0),
(4, 4, 4, 2, N'Điện thoại Samsung Galaxy Z Flip4 5G 512GB', 6990000, '2023-08-01', N'Samsung Galaxy Z Flip4 512GB đã chính thức ra mắt thị trường công nghệ, đánh dấu sự trở lại của Samsung trên con đường định hướng người dùng về sự tiện lợi trên những chiếc điện thoại gập. Với độ bền được gia tăng cùng kiểu thiết kế đẹp mắt giúp Flip4 trở thành một trong những tâm điểm sáng giá cho nửa cuối năm 2022.',N'Kích thước màn hình: Chính 6.7" & Phụ 1.9"<br>Công nghệ màn hình: Chính: Dynamic AMOLED 2X, Phụ: Super AMOLED<br>Độ phân giải: Chính: Chính: Chính: FHD+ (2640 x 1080 Pixels) x Phụ: (260 x 512 Pixels)<br>Tần số quét : 120 Hz <br>Camera Sau: Chính 2 camera 12 MP<br>Camera Trước: 10 MP <br>Hệ điều hành: Android 12<br>Chip: Snapdragon Snapdragon 8+ Gen 1 8 nhân<br>Ram: 8 GB <br>Dung Lượng: 512 GB <br>Sim và Sạc: 1 Nano SIM & 1 eSIM, 25W <br>Dung lượng pin: 3700 mAh <br>Bảo hành : 12 tháng ', N'samsung-galaxy-flip4-glr-tim-1.jpg', N'samsung-galaxy-flip-den-1.jpg', N'samsung-galaxy-z-flip4-note-1-1.jpg',N'samsung-galaxy-flip-den-1.jpg', 10, null, null, null, 0, 1,0, 0),
(5, 5, 5, null, N'Điện thoại Xiaomi 12 5G', 10000000, '2023-08-01', N'Điện thoại Xiaomi đang dần khẳng định chỗ đứng của mình trong phân khúc flagship bằng việc ra mắt Xiaomi 12 với bộ thông số ấn tượng, máy có một thiết kế gọn gàng, hiệu năng mạnh mẽ, màn hình hiển thị chi tiết cùng khả năng chụp ảnh sắc nét nhờ trang bị ống kính đến từ Sony.',N'Kích thước màn hình: 6.28"<br>Công nghệ màn hình: Chính: AMOLED<br>Độ phân giải: Chính: Chính: Full HD+ (1080 x 2400 Pixels)<br>Tần số quét : 120 Hz <br>Camera Sau: Chính 50 MP & Phụ 13 MP, 5 MP<br>Camera Trước: 32 MP <br>Hệ điều hành: Android 12<br>Chip: Snapdragon Snapdragon 8+ Gen 1 8 nhân<br>Ram: 8 GB <br>Dung Lượng: 256 GB <br>Sim và Sạc: 2 Nano SIM, 67W <br>Dung lượng pin: 4500 mAh <br>Bảo hành : 12 tháng ', N'xiaomi-mi-12-1-1.jpg', N'xiaomi-mi-12-1.jpg', N'xiaomi-mi-12-note.jpg',N'xiaomi-mi-12-1.jpg', 10, null, null, null, 0, 1,1, 0),
(5, 5, 5, null, N'Điện thoại Xiaomi Redmi Note 12S', 6000000, '2023-08-01', N'Xiaomi Redmi Note 12S sẽ là chiếc điện thoại tiếp theo được nhà Xiaomi tung ra thị trường Việt Nam trong thời gian tới (05/2023). Điện thoại sở hữu một lối thiết kế hiện đại, màn hình hiển thị chi tiết đi cùng với đó là một hiệu năng mượt mà xử lý tốt các tác vụ.',N'Kích thước màn hình: 6.43"<br>Công nghệ màn hình: Chính: AMOLED<br>Độ phân giải: Chính: Chính: Full HD+ (1080 x 2400 Pixels)<br>Tần số quét : 90 Hz <br>Camera Sau: Chính 108 MP & Phụ 8 MP, 2 MP<br>Camera Trước: 16 MP <br>Hệ điều hành: Android 13<br>Chip: Snapdragon MediaTek Helio G96 8 nhân<br>Ram: 8 GB <br>Dung Lượng: 256 GB <br>Sim và Sạc: 2 Nano SIM, 33W <br>Dung lượng pin: 5000 mAh <br>Bảo hành : 12 tháng ', N'xiaomi-redmi-note-12s-1-1.jpg', N'xiaomi-redmi-note-12s-xanh-1.jpg', N'xiaomi-redmi-note-12s-note.jpg',N'xiaomi-redmi-note-12s-xanh-1.jpg', 10, null, null, null, 0, 1,0, 0),
(5, 5, 5, null, N'Điện thoại Xiaomi Redmi Note 12 Pro 128GB', 6900000, '2023-08-01', N'Xiaomi Redmi Note 12 Pro 4G tiếp tục sẽ là mẫu điện thoại tầm trung được nhà Xiaomi giới thiệu đến thị trường Việt Nam trong năm 2023, máy nổi bật với camera 108 MP chất lượng, thiết kế viền mỏng cùng hiệu năng đột phá nhờ trang bị chip Snapdragon 732G.',N'Kích thước màn hình: 6.67"<br>Công nghệ màn hình: Chính: AMOLED<br>Độ phân giải: Chính: Chính: Full HD+ (1080 x 2400 Pixels)<br>Tần số quét : 120 Hz <br>Camera Sau: Chính 108 MP & Phụ 8 MP, 2 MP<br>Camera Trước: 16 MP <br>Hệ điều hành: Android 11<br>Chip: Snapdragon Snapdragon 732G 8 nhân<br>Ram: 8 GB <br>Dung Lượng: 128 GB <br>Sim và Sạc: 2 Nano SIM (SIM 2 chung khe thẻ nhớ), 67W <br>Dung lượng pin: 5000 mAh <br>Bảo hành : 12 tháng ', N'xiami-redmi-12-pro-xam-1.jpg', N'xiaomi-redmi-12-pro-4g-xanh-duong-1.jpg', N'xiaomi-redmi-12-note-2.jpg',N'xiaomi-redmi-12-pro-4g-xanh-duong-1.jpg', 10, null, null, null, 0, 1,1, 0),
(6, 6, 6, 3, N'Pin sạc dự phòng Polymer 10000mAh Type C 15W AVA+ JP399', 500000, '2023-08-01', N'Pin sạc dự phòng Polymer 10000mAh Type C 15W AVA+ JP399 mang đến cho người dùng 1 thiết kế nhỏ gọn, trang bị gam màu sang trọng và kiểu dáng tối giản, dung lượng 10000mAh và hiệu suất 64%, pin sạc dự phòng cung cấp pin cho thiết bị tối ưu.',N'Hiệu suất: 64% <br>Dung lượng pin: 10000 mAh <br>Thời gian sạc: 5 - 6 giờ (dùng Adapter 2A) <br>Nguồn Vào: Type C: 5V - 3A, Micro USB: 5V - 2A<br>Nguồn ra: Type-C: 5V - 3, AUSB: 5V - 2.4A<br>Công nghệ: Đèn LED báo hiệu<br>Kích thước: Dài 9.2 cm - 6.3 cm - Dày 1.9 cm<br>Bảo hành: 12 tháng<br>Khối lượng: 190 g', N'pin-sac-du-phong-polymer-10000mah-type-c-15w-ava-jp399-xanh-1-1.jpg', N'pin-sac-du-phong-polymer-10000mah-type-c-15w-ava-jp399-hong-1.jpg', N'pin-sac-du-phong-polymer-10000mah-type-c-15w-ava-jp399-hong-12.jpg',N'pin-sac-du-phong-polymer-10000mah-type-c-15w-ava-jp399-hong-1.jpg', 10, null, null, null, 0, 1,0, 0),
(6, 6, 6, 3, N'Pin sạc dự phòng Polymer 10000mAh 12W AVA+ JP299', 550000, '2023-08-01', N'Pin sạc dự phòng Polymer 10000mAh 12W AVA+ JP299 mang đến cho khách hàng 1 thiết kế đẹp mắt, nhờ sở hữu gam màu sang trọng và kiểu dáng tối giản. Sở hữu dung lượng 10000mAh và hiệu suất 64%, pin sạc dự phòng cung cấp năng lượng cho thiết bị tối ưu.',N'Hiệu suất: 64% <br>Dung lượng pin: 10000 mAh <br>Thời gian sạc: 5 - 6 giờ (dùng Adapter 2A) <br>Nguồn Vào: Type C: Micro USB: 5V - 2A<br>Nguồn ra: USB 1: 5V - 2.4, AUSB 2: 5V - 2.4A<br>Công nghệ: Đèn LED báo hiệu<br>Kích thước: Dày 2.2 cm - Rộng 6.1 cm - Dài 8.9 cm<br>Bảo hành: 12 tháng<br>Khối lượng: 186.2 g', N'pin-sac-du-phong-polymer-10000mah-ava-jp299-den-1.jpg', N'pin-sac-du-phong-polymer-10000mah-ava-jp299-trang-1.jpg', N'pin-sac-du-phong-polymer-10000mah-ava-jp299-trang-8.jpg', N'pin-sac-du-phong-polymer-10000mah-ava-jp299-trang-1.jpg', 10, null, null, null, 0, 1,1, 0),
(6, 6, 6, 3, N'Pin sạc dự phòng Polymer 10000mAh Type C 10W AVA+ PB100S', 5900000, '2023-08-01', N'Sạc điện thoại của bạn nhiều lần với dung lượng sạc dự phòng 10Pin sạc dự phòng Polymer 10000mAh 12W AVA+ JP299 mang đến cho khách hàng 1 thiết kế đẹp mắt, nhờ sở hữu gam màu sang trọng và kiểu dáng tối giản. Sở hữu dung lượng 10000 mAh và hiệu suất 64%, pin sạc dự phòng cung cấp năng lượng cho thiết bị tối ưu.000 mAh',N'Hiệu suất: 65% <br>Dung lượng pin: 10000 mAh <br>Thời gian sạc: 5 - 6 giờ (dùng Adapter 2A) <br>Nguồn Vào: Type C: Micro USB/Type-C: 5V - 2A<br>Nguồn ra: Type-C: 5V - 2, AUSB: 5V - 2A<br>Công nghệ: Đèn LED báo hiệu<br>Kích thước: Dày 1.5 cm - Rộng 6.9 cm - Dài 14.6 cm<br>Bảo hành: 12 tháng<br>Khối lượng: 230 g', N'pin-polymer-10000mah-type-c-ava-pb100s-trang-3.jpg', N'pin-polymer-10000mah-type-c-ava-pb100s-den-3.jpg', N'pin-polymer-10000mah-type-c-ava-pb100s-den-13-1.jpg',N'pin-polymer-10000mah-type-c-ava-pb100s-den-3.jpg', 10, null, null, null, 0, 1,1, 0),
(7, 7, 7, 4, N'Tai nghe Bluetooth True Wireless HAVIT TW945', 1000000, '2023-08-01', N'Tai nghe Bluetooth True Wireless HAVIT TW945 mang đến thiết kế sang trọng với kiểu dáng tối giản và màu sắc đa dạng, âm thanh đầy đủ và rõ ràng, tích hợp nhiều tính năng và tiện ích khác, phục vụ tốt nhu cầu sử dụng cơ bản hàng ngày của đa số người dùng.',N'Dung lượng pin: Dùng 18 giờ <br>Thời gian sạc: Sạc 2 giờ <br>Cổng sạc: Type-C <br>Công nghệ âm thanh: Màng loa 13 mm <br>Tiện ích: Game Mode, Sử dụng độc lập 1 bên tai nghe, Có mic thoại, Tương thích trợ lý ảo <br>Tương thích: MacOSAndroid, iOS, Windows <br>Công nghệ kết nối: Bluetooth 5.3<br>Phím điều khiển: Phát/dừng chơi nhạc, Chuyển bài hát, Từ chối cuộc gọi,<br>Bật trợ lí ảo, Bật/tắt game mode, Nhận/Ngắt cuộc gọi<br>Kích thước: Dài 3.3 cm - Rộng 1.9 cm - Cao 1.6 cm <br>Bảo hành: 12 tháng <br>Khối lượng: 4 g', N'tai-nghe-bluetooth-true-wireless-havit-tw945-den-2.jpg', N'tai-nghe-bluetooth-true-wireless-havit-tw945-cam-2.jpg', N'tai-nghe-bluetooth-true-wireless-havit-tw945-tim-note.jpg',N'tai-nghe-bluetooth-true-wireless-havit-tw945-cam-2.jpg', 10, null, null, null, 0, 1,0, 0),
(7, 7, 7, 4, N'Tai nghe Bluetooth True Wireless HAVIT TW971', 1000000, '2023-08-01', N'Tai nghe Bluetooth True Wireless HAVIT TW971 mang đến một thiết kế trong suốt, âm thanh rõ ràng và sống động, cùng với nhiều công nghệ tiện ích được tích hợp, hứa hẹn đáp ứng nhu cầu nghe nhạc hay gọi thoại cơ bản hằng ngày cho người dùng.',N'Dung lượng pin: Dùng 18 giờ <br>Thời gian sạc: Sạc 2 giờ <br>Cổng sạc: Type-C <br>Công nghệ âm thanh: Không có <br>Tiện ích: Có mic thoại, Tương thích trợ lý ảo<br>Tương thích: MacOSAndroid, iOS, Windows <br>Công nghệ kết nối: Bluetooth 5.3<br>Phím điều khiển: Phát/dừng chơi nhạc, Chuyển bài hát, Từ chối cuộc gọi,<br> Bật trợ lí ảo, Bật/tắt game mode, Nhận/Ngắt cuộc gọi<br>Kích thước: Dài 3.3 cm - Rộng 2.9 cm - Cao 1.7 cm<br>Bảo hành: 12 tháng <br>Khối lượng: 3.9 g', N'tai-nghe-bluetooth-true-wireless-havit-tw971-11.jpg', N'tai-nghe-bluetooth-true-wireless-havit-tw971-1.jpg', N'tai-nghe-bluetooth-true-wireless-havit-tw971-7.jpg', N'tai-nghe-bluetooth-true-wireless-havit-tw971-1.jpg', 10, null, null, null, 0, 1,0, 0),
(7, 7, 7, 4, N'Tai nghe Bluetooth True Wireless HAVIT TW967', 2000000, '2023-08-01', N'Tai nghe Bluetooth True Wireless HAVIT TW967 được thiết kế với phong cách năng động, màu sắc đẹp mắt, âm thanh sống động, trang bị kết nối không dây gọn gàng, mang đến cho bạn những trải nghiệm tối ưu.',N'Dung lượng pin: Dùng 18 giờ <br>Thời gian sạc: Sạc 2 giờ <br>Cổng sạc: Type-C <br>Công nghệ âm thanh: Không có <br>Tiện ích: Sử dụng độc lập 1 bên tai nghe, Có mic thoại<br>Tương thích: MacOSAndroid, iOS, Windows <br>Công nghệ kết nối: Bluetooth 5.1<br>Phím điều khiển: Phát/dừng chơi nhạc, Chuyển bài hát, Từ chối cuộc gọi,<br> Bật trợ lí ảo, Bật/tắt game mode, Nhận/Ngắt cuộc gọi<br>Kích thước: Dài 3.3 cm - Rộng 2.3 cm - Cao 2.2 cm<br>Bảo hành: 12 tháng <br>Khối lượng: 4 g', N'tai-nghe-bluetooth-true-wireless-havit-tw967-trang-2.jpg', N'tai-nghe-bluetooth-true-wireless-havit-tw967-den-1.jpg', N'tai-nghe-bluetooth-true-wireless-havit-tw967-notecopy.jpg',N'tai-nghe-bluetooth-true-wireless-havit-tw967-den-1.jpg', 10, null, null, null, 0, 1, 1,0)
GO
--INSERT BINHLUAN
--MaBinhLuan INT IDENTITY NOT NULL,
--MaTV INT NOT NULL,
--MaSP INT NOT NULL,
--NoiDungBL NVARCHAR(MAX) NULL

--INSERT INTO BINHLUAN(MaTV, MaSP, NoiDungBL) VALUES
--(1, 1, N'Sản phẩm chất lượng.'),
--(2, 2, N'Cảm thấy không như mong muốn của tôi.')
--GO

--INSERT CHITIETPHIEUNHAP
--MaCTPN INT IDENTITY NOT NULL,
--MaPN INT NOT NULL,
--MaSP INT NOT NULL,
--DonGiaNhap DECIMAL(18,0) NOT NULL,
--SoLuongNhap INT NOT NULL
INSERT INTO ChiTietPhieuNhap(MaPN, MaSP, DonGiaNhap, SoLuongNhap) VALUES
(1, 1, 23900000, 10),
(1, 2, 25900000, 10),
(1, 3, 30900000, 10),
(1, 4, 37900000, 10),
(1, 5, 20900000, 10),
(1, 6, 22900000, 10),
(1, 7, 25900000, 10),
(1, 8, 11900000, 10),
(1, 9, 12900000, 10),
(2, 10, 15900000, 10),
(2, 11, 10900000, 10),
(2, 12, 3990000, 10),
(3, 13, 7090000, 10),
(3, 14, 1590000, 10),
(3, 15, 6990000, 10),
(4, 16, 30990000, 10),
(4, 17, 33990000, 10),
(4, 18, 40900000, 10),
(4, 19, 11990000, 10),
(4, 20, 12990000, 10),
(4, 21, 13990000, 10),
(5, 22, 9990000, 10),
(5, 23, 3990000, 10),
(5, 24, 4990000, 10),
(6, 25, 239000, 10),
(6, 26, 139000, 10),
(6, 27, 209000, 10),
(7, 28, 289000, 10),
(7, 29, 289000, 10),
(7, 30, 239000, 10)
GO
--INSERT MAU
--MaMau INT IDENTITY NOT NULL,
--TenMau NVARCHAR(50) NULL


