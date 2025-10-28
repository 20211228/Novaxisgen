CREATE DATABASE Novaxisgen;
USE Novaxisgen;


CREATE TABLE CapVIP (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TenCap NVARCHAR(50) NOT NULL,             
    MoTa NVARCHAR(200),
    MucHoaHong DECIMAL(5,2) NOT NULL,         
    DieuKienDatVip NVARCHAR(200) NOT NULL ,
	DieuKienDoanhSo DECIMAL(18,2) NULL,
    DieuKienCapCon INT NULL
);
-- ==========================================
-- BẢNG NGƯỜI DÙNG
-- ==========================================
CREATE TABLE NguoiDung (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) UNIQUE NOT NULL,
    HoTen NVARCHAR(255),
    Email NVARCHAR(255) UNIQUE NOT NULL,
    MatKhau NVARCHAR(255) NOT NULL,             
    GoogleAuthKey NVARCHAR(255),           
    CapVip INT,
    NgayTao DATETIME DEFAULT GETDATE(),
	FOREIGN KEY (CapVip) REFERENCES CapVIP(Id)
);

-- ==========================================
-- VÍ NGƯỜI DÙNG
-- ==========================================
CREATE TABLE ViNguoiDung (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    UserId BIGINT NOT NULL,
    DiaChiVi NVARCHAR(255),                
    NgayCapNhat DATETIME DEFAULT GETDATE(),
	SoDu DECIMAL(18,2),
	DonViTienTe NVARCHAR(10) DEFAULT N'USDT',
	TrangThai NVARCHAR(50) DEFAULT N'Đang hoạt động',
    FOREIGN KEY (UserId) REFERENCES NguoiDung(Id)
);

-- ==========================================
-- GÓI ĐẦU TƯ & KỲ HẠN
-- ==========================================
CREATE TABLE GoiDauTu (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    TenGoi NVARCHAR(100),
    MoTa NVARCHAR(255),
    SoTien DECIMAL(18,2),
    SoTangHoaHongTrucTiep INT DEFAULT 1, 
	SoTangHoaHongLai INT DEFAULT 1, 
	HoaHongNhiPhan DECIMAL(5,2),
    NgayTao DATETIME DEFAULT GETDATE()
);

CREATE TABLE KyHanDauTu (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    GoiDauTuId BIGINT NOT NULL,
    SoThang INT NOT NULL,                   
    LaiSuatNgay DECIMAL(5,2) NOT NULL,      
    FOREIGN KEY (GoiDauTuId) REFERENCES GoiDauTu(Id)
);

-- ==========================================
-- ĐẦU TƯ NGƯỜI DÙNG
-- ==========================================
CREATE TABLE DauTuNguoiDung (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    UserId BIGINT NOT NULL,
    GoiDauTuId BIGINT NOT NULL,
    KyHanId BIGINT NOT NULL,
    SoTienDauTu DECIMAL(18,8) NOT NULL,
    NgayBatDau DATE DEFAULT GETDATE(),
    NgayKetThuc DATE,
    TrangThai NVARCHAR(50) DEFAULT N'Đang hoạt động',
    FOREIGN KEY (UserId) REFERENCES NguoiDung(Id),
    FOREIGN KEY (GoiDauTuId) REFERENCES GoiDauTu(Id),
    FOREIGN KEY (KyHanId) REFERENCES KyHanDauTu(Id)
);

-- ==========================================
-- LÃI HÀNG NGÀY
-- ==========================================
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

-- ==========================================
-- HOA HỒNG TRỰC TIẾP
-- ==========================================
CREATE TABLE HoaHongTrucTiep (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    NguoiNhanId BIGINT NOT NULL,
    NguoiTuyenDuoiId BIGINT NOT NULL,
    CapTang INT NOT NULL,                   
    SoTienHoaHong DECIMAL(18,8),
    NgayTao DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (NguoiNhanId) REFERENCES NguoiDung(Id),
    FOREIGN KEY (NguoiTuyenDuoiId) REFERENCES NguoiDung(Id)
);


CREATE TABLE TyLeHoaHongTrucTiep (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    GoiDauTuId BIGINT NOT NULL,
    Tang INT NOT NULL,                     -- Tầng hoa hồng (1, 2, 3, ...)
    TyLe DECIMAL(5,2) NOT NULL,            -- % hoa hồng
    FOREIGN KEY (GoiDauTuId) REFERENCES GoiDauTu(Id)
);


-- ==========================================
-- 🌳 CÂY NHỊ PHÂN (CHỈ ĐỊNH)
-- ==========================================
CREATE TABLE CayNhiPhan (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    UserId BIGINT NOT NULL,                -- Người đang nằm trong cây
    PlacementId BIGINT NULL,               -- Người phía trên (nơi mình được đặt)
	SponsorId BIGINT NULL,
	SponsorNode NVARCHAR(255) NULL,        -- Vị trí trong cây giới thiệu
    PlacementNode NVARCHAR(255) NULL,  
    NgayTao DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES NguoiDung(Id),
    FOREIGN KEY (PlacementId) REFERENCES NguoiDung(Id),
	FOREIGN KEY (SponsorId) REFERENCES NguoiDung(Id)
);

-- ==========================================
-- DOANH SỐ NHỊ PHÂN & HOA HỒNG NHỊ PHÂN
-- ==========================================
CREATE TABLE DoanhSoNhiPhan (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    UserId BIGINT NOT NULL,
    TongTrai DECIMAL(18,8) DEFAULT 0,
    TongPhai DECIMAL(18,8) DEFAULT 0,
	DoanhSoDaTinhTrai DECIMAL(18,8) DEFAULT 0,
    DoanhSoDaTinhPhai DECIMAL(18,8) DEFAULT 0,
    CapNhatGanNhat DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES NguoiDung(Id)
);

CREATE TABLE HoaHongNhiPhan (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    UserId BIGINT NOT NULL,
    DoanhSoNhoHon DECIMAL(18,8) NOT NULL,
    TyLeHoaHong DECIMAL(5,2) NOT NULL,
    SoTienHoaHong DECIMAL(18,8) NOT NULL,
    NgayTao DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES NguoiDung(Id)
);

-- ==========================================
-- CẤP VIP & HOA HỒNG LÃNH ĐẠO
-- ==========================================




CREATE TABLE HoaHongLanhDao (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    NguoiNhanId BIGINT NOT NULL,
    NguoiTuyenDuoiId BIGINT NOT NULL,
    CapVip INT NOT NULL,              
    SoTienHoaHong DECIMAL(18,8) NOT NULL,
    NgayTao DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (NguoiNhanId) REFERENCES NguoiDung(Id),
    FOREIGN KEY (NguoiTuyenDuoiId) REFERENCES NguoiDung(Id),
    FOREIGN KEY (CapVip) REFERENCES CapVIP(Id)
);

-- ==========================================
-- GIAO DỊCH VÍ
-- ==========================================
CREATE TABLE GiaoDichVi (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    UserId BIGINT NOT NULL,
    LoaiGiaoDich NVARCHAR(50),             
    MaDonHang NVARCHAR(50), 
	SoTien DECIMAL(18,8) NOT NULL,
    DiaChiNhan NVARCHAR(255),              
    TrangThai NVARCHAR(50) DEFAULT N'Chờ xử lý',
    NgayTao DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES NguoiDung(Id)
);

CREATE TABLE HoaHongLaiTrenLai (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    NguoiNhanId BIGINT NOT NULL,
    NguoiTuyenDuoiId BIGINT NOT NULL,
    CapTang INT NOT NULL,
    SoTienHoaHong DECIMAL(18,8),
    Ngay DATE DEFAULT GETDATE(),
    NgayTao DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (NguoiNhanId) REFERENCES NguoiDung(Id),
    FOREIGN KEY (NguoiTuyenDuoiId) REFERENCES NguoiDung(Id)
);


CREATE TABLE TyLeHoaHongLaiLai (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    GoiDauTuId BIGINT NOT NULL,
    Tang INT NOT NULL,                     -- Tầng hoa hồng (1, 2, 3, ...)
    TyLe DECIMAL(5,2) NOT NULL,            -- % hoa hồng
    FOREIGN KEY (GoiDauTuId) REFERENCES GoiDauTu(Id)
);

SET IDENTITY_INSERT CapVIP ON;

INSERT INTO CapVIP (Id,TenCap, MoTa, MucHoaHong, DieuKienDatVip, DieuKienDoanhSo, DieuKienCapCon)
VALUES 
(1,'VIP1', N'Cần doanh số 5000 mỗi nhánh', 1.00, N'Doanh số 5000 mỗi nhánh', 5000, NULL),
(2,'VIP2', N'Cần 2 nhánh đạt VIP1', 2.00, N'2 nhánh đạt VIP1', NULL, 1),
(3,'VIP3', N'Cần 2 nhánh đạt VIP2', 3.00, N'2 nhánh đạt VIP2', NULL, 2),
(4,'VIP4', N'Cần 2 nhánh đạt VIP3', 4.00, N'2 nhánh đạt VIP2', NULL, 3),
(5,'VIP5', N'Cần 2 nhánh đạt VIP4', 5.00, N'2 nhánh đạt VIP2', NULL, 4),
(6,'VIP6', N'Cần 2 nhánh đạt VIP5', 6.00, N'2 nhánh đạt VIP2', NULL, 5),
(7,'VIP7', N'Cần 2 nhánh đạt VIP6', 7.00, N'2 nhánh đạt VIP2', NULL, 6),
(8,'VIP8', N'Cần 2 nhánh đạt VIP7', 8.00, N'2 nhánh đạt VIP2', NULL, 7),
(9,'VIP9', N'Cần 2 nhánh đạt VIP8', 9.00, N'2 nhánh đạt VIP2', NULL, 8);
INSERT INTO CapVIP (Id,TenCap, MoTa, MucHoaHong, DieuKienDatVip, DieuKienDoanhSo, DieuKienCapCon)
VALUES (0,'VIP0', N'Cần doanh số 5000 mỗi nhánh', 0, N'Doanh số 5000 mỗi nhánh', 0, NULL);

INSERT INTO NguoiDung (Username, HoTen, Email, MatKhau, GoogleAuthKey, CapVip)
VALUES 
(N'user1', N'Nguyễn Văn A', N'user1@gmail.com', N'123456',  N'GAKEY001', 1),
(N'user2', N'Trần Thị B', N'user2@gmail.com', N'123456',  N'GAKEY002', 2),
(N'user3', N'Lê Văn C', N'user3@gmail.com', N'123456', N'GAKEY003', 3),
(N'user4', N'Phạm Thị D', N'user4@gmail.com', N'123456', N'GAKEY004', 1),
(N'user5', N'Hoàng Văn E', N'user5@gmail.com', N'123456',  N'GAKEY005', 2),
(N'user6', N'Nguyễn Văn A', N'user6@gmail.com', N'123456',  N'GAKEY001', 0),
(N'user7', N'Trần Thị B', N'user7@gmail.com', N'123456',  N'GAKEY002', 0);
UPDATE NguoiDung
SET CapVip = 0;

INSERT INTO GoiDauTu (TenGoi, MoTa, SoTien, SoTangHoaHongTrucTiep, HoaHongNhiPhan)
VALUES
(N'Bronze', NULL, 100, 1, 1),
(N'Silver', NULL, 500, 2, 1.5),
(N'Gold', NULL, 1000, 3, 2),
(N'Platinum', NULL, 2500, 4, 2.5),
(N'Emerald', NULL, 5000, 5, 3),
(N'Sapphire', NULL, 10000, 6, 3.5),
(N'Ruby', NULL, 25000, 7, 4),
(N'Diamond', NULL, 50000, 8, 4.5),
(N'Crown Diamond', NULL, 100000, 9, 5);



INSERT INTO KyHanDauTu (GoiDauTuId, SoThang, LaiSuatNgay)
VALUES
(1, 6, 0.2),
(1, 12, 0.33),
(1, 18, 0.4),
(1, 24, 0.5),

(2, 6, 0.2),
(2, 12, 0.33),
(2, 18, 0.4),
(2, 24, 0.5),

(3, 6, 0.2),
(3, 12, 0.33),
(3, 18, 0.4),
(3, 24, 0.5),

(4, 6, 0.2),
(4, 12, 0.33),
(4, 18, 0.4),
(4, 24, 0.5),

(5, 6, 0.2),
(5, 12, 0.33),
(5, 18, 0.4),
(5, 24, 0.5),

(6, 6, 0.2),
(6, 12, 0.33),
(6, 18, 0.4),
(6, 24, 0.5),

(7, 6, 0.2),
(7, 12, 0.33),
(7, 18, 0.4),
(7, 24, 0.5),

(8, 6, 0.2),
(8, 12, 0.33),
(8, 18, 0.4),
(8, 24, 0.5),

(9, 6, 0.2),
(9, 12, 0.33),
(9, 18, 0.4),
(9, 24, 0.5);


INSERT INTO DauTuNguoiDung (UserId, GoiDauTuId, KyHanId, SoTienDauTu, NgayBatDau, TrangThai)
VALUES
(1, 3, 10, 1000.00, '2025-10-01', N'Đang hoạt động'),  -- user1 đầu tư gói Gold kỳ hạn 12 tháng (Lãi 0.33%)
(1, 5, 19, 5000.00, '2025-10-05', N'Đang hoạt động'),  -- user1 đầu tư Emerald kỳ hạn 18 tháng
(2, 2, 5, 500.00, '2025-10-10', N'Đang hoạt động'),     -- user2 đầu tư Silver 6 tháng
(3, 4, 16, 2500.00, '2025-10-15', N'Đang hoạt động'),   -- user3 đầu tư Platinum 12 tháng
(4, 1, 1, 100.00, '2025-10-20', N'Đang hoạt động');     -- user4 đầu tư Bronze 6 tháng



-- ==========================================
-- 💰 VÍ NGƯỜI DÙNG
-- ==========================================
INSERT INTO ViNguoiDung (UserId, DiaChiVi, SoDu)
VALUES
(1, N'0xABC123USER1', 1500.00),
(2, N'0xABC123USER2', 700.00),
(3, N'0xABC123USER3', 3000.00),
(4, N'0xABC123USER4', 250.00),
(5, N'0xABC123USER5', 1000.00);


-- ==========================================
-- 📈 TỶ LỆ HOA HỒNG TRỰC TIẾP
-- ==========================================
INSERT INTO TyLeHoaHongTrucTiep (GoiDauTuId, Tang, TyLe)
VALUES
(1, 1, 5.0),

(2, 1, 5.0),
(2, 2, 2.0),
(2, 3, 1.0),

(3, 1, 5.0),
(3, 2, 2.0),
(3, 3, 1.0),
(3, 4, 0.5),
(3, 5, 0.5),

(4, 1, 5.0),
(4, 2, 2.0),
(4, 3, 1.0),
(4, 4, 0.5),
(4, 5, 0.5),

(5, 1, 5.0),
(5, 2, 2.0),
(5, 3, 1.0),
(5, 4, 0.5),
(5, 5, 0.5),
(5, 6, 0.5),

(6, 1, 5.0),
(6, 2, 2.0),
(6, 3, 1.0),
(6, 4, 0.5),
(6, 5, 0.5),
(6, 6, 0.5),
(6, 7, 0.5),

(7, 1, 5.0),
(7, 2, 2.0),
(7, 3, 1.5),
(7, 4, 0.5),
(7, 5, 0.5),
(7, 6, 0.5),
(7, 7, 0.5),
(7, 8, 0.5),
(7, 9, 0.5),

(8, 1, 5.0),
(8, 2, 2.0),
(8, 3, 1.5),
(8, 4, 0.5),
(8, 5, 0.5),
(8, 6, 0.5),
(8, 7, 0.5),
(8, 8, 0.5),
(8, 9, 0.5),

(9, 1, 5.0),
(9, 2, 2.0),
(9, 3, 1.5),
(9, 4, 0.5),
(9, 5, 0.5),
(9, 6, 0.5),
(9, 7, 0.5),
(9, 8, 0.5),
(9, 9, 0.5);

-- ==========================================
-- 📈 TỶ LỆ HOA HỒNG lãi/lãi
-- ==========================================

INSERT INTO TyLeHoaHongLaiLai (GoiDauTuId, Tang, TyLe)
VALUES
(1, 1, 12.0),

(2, 1, 12.0),
(2, 2, 8.0),


(3, 1, 12.0),
(3, 2, 8.0),
(3, 3, 5.0),


(4, 1, 12.0),
(4, 2, 8.0),
(4, 3, 5.0),
(4, 4, 4.0),


(5, 1, 12.0),
(5, 2, 8.0),
(5, 3, 5.0),
(5, 4, 4.0),
(5, 5, 3.0),


(6, 1, 12.0),
(6, 2, 8.0),
(6, 3, 5.0),
(6, 4, 4.0),
(6, 5, 3.0),



(7, 1, 12.0),
(7, 2, 8.0),
(7, 3, 5.0),
(7, 4, 4.0),
(7, 5, 3.0),



(8, 1, 12.0),
(8, 2, 8.0),
(8, 3, 5.0),
(8, 4, 4.0),
(8, 5, 3.0),



(9, 1, 12.0),
(9, 2, 8.0),
(9, 3, 5.0),
(9, 4, 4.0),
(9, 5, 3.0);



-- ==========================================
-- 🌳 CÂY NHỊ PHÂN
-- ==========================================
INSERT INTO CayNhiPhan (UserId, PlacementId, SponsorId, SponsorNode, PlacementNode)
VALUES
(6, NULL, NULL, NULL, NULL),  -- user6 là gốc cây
(7, 6, 6, N'6-1', N'6-0'),
(8, 6, 6, N'6-2', N'6-1'),
(9, 7, 7, N'7-1', N'7-0'),
(10, 7, 7, N'7-2', N'7-1'),
(12, 8, 8, N'8-1', N'8-0'),
(13, 8, 8, N'8-2', N'8-1');
--(1, NULL, NULL, NULL, NULL),  -- user1 là gốc cây
--(2, 1, 1, N'1-1', N'1-0'),
--(3, 1, 1, N'1-2', N'1-1'),
--(4, 2, 2, N'2-1', N'2-0'),
--(5, 2, 2, N'2-2', N'2-1');









delete from KyHanDauTu
delete from TyLeHoaHongTrucTiep
delete from GoiDauTu
delete from CapVIP where Id = 10
delete from NguoiDung
delete from DauTuNguoiDung
delete from CayNhiPhan
delete from TyLeHoaHongLaiLai
delete from ViNguoiDung
delete from DoanhSoNhiPhan
delete from HoaHongNhiPhan