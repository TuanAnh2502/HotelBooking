-- Tạo cơ sở dữ liệu
CREATE DATABASE HotelBooking;
GO

-- Sử dụng cơ sở dữ liệu mới
USE HotelBooking;

GO
-- Tạo bảng Người dùng
CREATE TABLE tblUser (
    ID_User INT PRIMARY KEY IDENTITY(1, 1),
    sEmail NVARCHAR(255),
    sMatkhau NVARCHAR(255),
    sTendaydu NVARCHAR(100),
    sSDT NVARCHAR(20),
    sLoaiuser NVARCHAR(20)
);

GO
-- Tạo bảng Khách sạn
CREATE TABLE tblKhachSan (
    ID_Khachsan INT PRIMARY KEY IDENTITY(1, 1),
    sTenkhachsan NVARCHAR(255),
    sDiachi NVARCHAR(255),
    sMotakhachsan NVARCHAR(MAX),
    sAnhkhachsan NVARCHAR(MAX),
    sDanhgia DECIMAL(3, 2)
);
ALTER TABLE tblKhachSan
ADD ID_User INT;
ALTER TABLE tblKhachSan
ADD CONSTRAINT FK_tblKhachSan_tblUser
FOREIGN KEY (ID_User) REFERENCES tblUser(ID_User);

GO
-- Tạo bảng Phòng
CREATE TABLE tblPhong (
    ID_Phong INT PRIMARY KEY IDENTITY(1, 1),
    ID_Khachsan INT,
    sKieuPhong NVARCHAR(50),
    fGiaphong float,
    sPhongcontrong INT,
    sMotaphong NVARCHAR(MAX),
	sTiennghi NVARCHAR(MAX),
    sAnhphong NVARCHAR(MAX),
    FOREIGN KEY (ID_Khachsan) REFERENCES tblKhachSan(ID_Khachsan)
);
GO

-- Tạo bảng Đặt phòng
CREATE TABLE tblDatphong (
    ID_Madatphong INT PRIMARY KEY IDENTITY(1, 1),
    ID_User INT,
    ID_Phong INT,
    dNgaycheckin DATE,
    dNgaycheckout DATE,
    sHinhthucthanhtoan NVARCHAR(MAX),
    FOREIGN KEY (ID_User) REFERENCES tblUser(ID_User),
    FOREIGN KEY (ID_Phong) REFERENCES tblPhong(ID_Phong)
);
GO
-- Tạo bảng Chi tiết đặt phòng
CREATE TABLE tblChiTietDatPhong (
    ID_Chitietdatphong INT PRIMARY KEY IDENTITY(1, 1),
    ID_Madatphong INT,
    sTenuser NVARCHAR(100),
    sEmailkhachhang NVARCHAR(255),
    sSDTkhachhang NVARCHAR(20),
    sChuthich NVARCHAR(MAX),
    FOREIGN KEY (ID_Madatphong) REFERENCES tblDatphong(ID_Madatphong)
);
GO

-- Tạo bảng Đánh giá và xếp hạng
CREATE TABLE tblDanhGia (
    ID_Danhgia INT PRIMARY KEY IDENTITY(1, 1),
    ID_Khachsan INT,
    ID_Khachhang INT,
    iSosaodanhgia INT,
    sNoidung NVARCHAR(MAX),
    FOREIGN KEY (ID_Khachsan) REFERENCES tblKhachSan(ID_Khachsan),
    FOREIGN KEY (ID_Khachhang) REFERENCES tblUser(ID_User)
);
GO

select *from tblUsers
