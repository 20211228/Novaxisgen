Create database Novaxisgen;
Use Novaxisgen;

CREATE TABLE NguoiDung (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) UNIQUE NOT NULL,
    HoTen NVARCHAR(255),
    Email NVARCHAR(255) UNIQUE NOT NULL,
    MatKhau NVARCHAR(255) NOT NULL,
    MaGioiThieu NVARCHAR(50),              
    GoogleAuthKey NVARCHAR(255),           
    CapVip NVARCHAR(50) DEFAULT N'',
    NgayTao DATETIME DEFAULT GETDATE()
);

CREATE TABLE ViNguoiDung (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    UserId BIGINT NOT NULL,
    DiaChiVi NVARCHAR(255),                
    NgayCapNhat DATETIME DEFAULT GETDATE(),
	SoDu DECIMAL(18,2),
    FOREIGN KEY (UserId) REFERENCES NguoiDung(Id)
);

CREATE TABLE GoiDauTu (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    TenGoi NVARCHAR(100),
    MoTa NVARCHAR(255),
    SoTien DECIMAL(18,2),
    SoTangHoaHongTrucTiep INT DEFAULT 1, 
	HoaHongNhiPhan DECIMAL(5,2),
    NgayTao DATETIME DEFAULT GETDATE()
);


CREATE TABLE KyHanDauTu (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    GoiDauTuId BIGINT NOT NULL,
    SoThang INT NOT NULL,                   -- Ví dụ: 6 hoặc 12 tháng
    LaiSuatNgay DECIMAL(5,2) NOT NULL,      -- Ví dụ: 0.2 (%/ngày)
    FOREIGN KEY (GoiDauTuId) REFERENCES GoiDauTu(Id)
);


CREATE TABLE DauTuNguoiDung (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    UserId BIGINT NOT NULL,
    GoiDauTuId BIGINT NOT NULL,
    KyHanId BIGINT NOT NULL,
    SoTienDauTu DECIMAL(18,8) NOT NULL,
    NgayBatDau DATE Default GetDate(),
    TrangThai NVARCHAR(50) DEFAULT N'Đang hoạt động',
    FOREIGN KEY (UserId) REFERENCES NguoiDung(Id),
    FOREIGN KEY (GoiDauTuId) REFERENCES GoiDauTu(Id),
    FOREIGN KEY (KyHanId) REFERENCES KyHanDauTu(Id)
);


CREATE TABLE LaiHangNgay (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    UserId BIGINT NOT NULL,
    DauTuId BIGINT NOT NULL,
    Ngay DATE NOT NULL,
    SoTienLai DECIMAL(18,8) NOT NULL,
    NgayTao DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES NguoiDung(Id),
    FOREIGN KEY (DauTuId) REFERENCES DauTuNguoiDung(Id)
);


CREATE TABLE HoaHongTrucTiep (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    NguoiNhanId BIGINT NOT NULL,
    NguoiTuyenDuoiId BIGINT NOT NULL,
    CapTang INT NOT NULL,                   -- L1 đến L9
    SoTienHoaHong DECIMAL(18,8),
    NgayTao DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (NguoiNhanId) REFERENCES NguoiDung(Id),
    FOREIGN KEY (NguoiTuyenDuoiId) REFERENCES NguoiDung(Id)
);


CREATE TABLE CayNhiPhan (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    UserId BIGINT NOT NULL,
    ChaId BIGINT,                            -- Người bảo trợ trong cây nhị phân
    Nhanh NVARCHAR(10),                      -- 'Trai' hoặc 'Phai'
    NgayTao DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES NguoiDung(Id),
    FOREIGN KEY (ChaId) REFERENCES NguoiDung(Id)
);


CREATE TABLE DoanhSoNhiPhan (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    UserId BIGINT NOT NULL,
    TongTrai DECIMAL(18,8) DEFAULT 0,
    TongPhai DECIMAL(18,8) DEFAULT 0,
    CapNhatGanNhat DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES NguoiDung(Id)
);


CREATE TABLE HoaHongNhiPhan (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    UserId BIGINT NOT NULL,
    ChuKyNgay DATE NOT NULL,
    DoanhSoNhoHon DECIMAL(18,8) NOT NULL,
    TyLeHoaHong DECIMAL(5,2) NOT NULL,
    SoTienHoaHong DECIMAL(18,8) NOT NULL,
    NgayTao DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES NguoiDung(Id)
);


CREATE TABLE CapVIP (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TenCap NVARCHAR(50) NOT NULL,             
    MoTa NVARCHAR(200),
    MucHoaHong DECIMAL(5,2) NOT NULL,         -- % hoa hồng lãnh đạo
    DieuKienDatVip NVARCHAR(200) NOT NULL    
);

CREATE TABLE HoaHongLanhDao (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    NguoiNhanId BIGINT NOT NULL,
    NguoiTuyenDuoiId BIGINT NOT NULL,
    CapVip INT NOT NULL,              -- 1 = Leader, 2 = Senior, 3 = Diamond,...
    SoTienHoaHong DECIMAL(18,8) NOT NULL,
    NgayTao DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (NguoiNhanId) REFERENCES NguoiDung(Id),
    FOREIGN KEY (NguoiTuyenDuoiId) REFERENCES NguoiDung(Id),
    FOREIGN KEY (CapVip) REFERENCES CapVIP(Id)
);

CREATE TABLE GiaoDichVi (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    UserId BIGINT NOT NULL,
    LoaiGiaoDich NVARCHAR(50),             -- 'Nap', 'Rut', 'Chuyen'
    SoTien DECIMAL(18,8) NOT NULL,
    DiaChiNhan NVARCHAR(255),              -- nếu là rút/chuyển
    TrangThai NVARCHAR(50) DEFAULT N'Chờ xử lý',
    NgayTao DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES NguoiDung(Id)
);