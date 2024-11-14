-- Tạo cơ sở dữ liệu quản lý nhà thuốc
CREATE DATABASE QuanLyNhaThuoc;
USE QuanLyNhaThuoc;

-- Bảng 2: Hãng sản xuất
CREATE TABLE HangSX (
    MaHangSX CHAR(5) PRIMARY KEY NOT NULL,
    TenHang VARCHAR(50) NOT NULL,
    QuocGia VARCHAR(50),
    CONSTRAINT unique_hangsx UNIQUE (TenHang,QuocGia)
);

-- Bảng 3: Nhà cung cấp
CREATE TABLE NhaCungCap (
    MaNhaCungCap CHAR(5) PRIMARY KEY NOT NULL,
    TenNhaCungCap VARCHAR(50) NOT NULL,
    DiaChi VARCHAR(255),
    ThongTinDaiDien TEXT,
    CONSTRAINT unique_nhacungcap UNIQUE (TenNhaCungCap,DiaChi)
);

-- Bảng 5: Loại thuốc
CREATE TABLE LoaiThuoc (
    MaLoai CHAR(5) PRIMARY KEY NOT NULL,
    TenLoai VARCHAR(50) NOT NULL,
    GhiChu TEXT,
    CONSTRAINT unique_loaithuoc UNIQUE (TenLoai)
);

-- Bảng 5: User
CREATE TABLE User (
    MaUser CHAR(5) PRIMARY KEY NOT NULL,
    TenUser VARCHAR(50) ,
    SoDienThoai CHAR(10) NOT NULL,
    DiaChi VARCHAR(255),
    Pwd VARCHAR(64) NOT NULL,
	NgaySinh DATE,
    NhanVien BOOLEAN NOT NULL,
    CONSTRAINT unique_user UNIQUE (SoDienThoai)
);

-- Bảng 1: Thuốc
CREATE TABLE Thuoc (
    MaThuoc CHAR(5) PRIMARY KEY NOT NULL,
    TenThuoc VARCHAR(100) NOT NULL,
    MaHangSX CHAR(5) NOT NULL,
    MaNhaCungCap CHAR(5) NOT NULL,
    CongDung TEXT,
    SoLuongTon INT DEFAULT 0,
    MaLoai CHAR(5) NOT NULL,
    FOREIGN KEY (MaHangSX) REFERENCES HangSX(MaHangSX),
    FOREIGN KEY (MaNhaCungCap) REFERENCES NhaCungCap(MaNhaCungCap),
    FOREIGN KEY (MaLoai) REFERENCES LoaiThuoc(MaLoai),
    CONSTRAINT unique_thuoc UNIQUE (TenThuoc,MaHangSX,MaNhaCungCap,MaLoai)
);

-- Bảng 7: Phiếu nhập
CREATE TABLE PhieuNhap (
    MaPN CHAR(5) PRIMARY KEY NOT NULL,
    MaNhanVien CHAR(5) NOT NULL,
    MaNhaCungCap CHAR(5) NOT NULL,
    NgayNhap DATE NOT NULL,
    FOREIGN KEY (MaNhanVien) REFERENCES User(MaUser),
    FOREIGN KEY (MaNhaCungCap) REFERENCES NhaCungCap(MaNhaCungCap),
    CONSTRAINT unique_phieunhap UNIQUE (MaNhanVien,MaNhaCungCap,NgayNhap)
);

-- Bảng 8: Chi tiết Phiếu nhập
CREATE TABLE ChiTietPhieuNhap (
    MaPN CHAR(5) NOT NULL,
    MaThuoc CHAR(5) NOT NULL,
    SoLuong INT NOT NULL,
    DonGia DECIMAL(10, 3) NOT NULL,
    PRIMARY KEY (MaPN, MaThuoc),
    FOREIGN KEY (MaPN) REFERENCES PhieuNhap(MaPN),
    FOREIGN KEY (MaThuoc) REFERENCES Thuoc(MaThuoc),
    CONSTRAINT unique_chitietphieunhap UNIQUE (MaPN,MaThuoc,SoLuong,DonGia)
);

-- Bảng 9: Phiếu xuất
CREATE TABLE PhieuXuat (
    MaPX CHAR(5) PRIMARY KEY NOT NULL,
    MaNhanVien CHAR(5) NOT NULL,
    MaKhachHang CHAR(5) NOT NULL,
    NgayXuat DATE NOT NULL,
    CONSTRAINT fk_nhanvien FOREIGN KEY (MaNhanVien) REFERENCES User(MaUser) ON DELETE CASCADE,
    CONSTRAINT fk_khachhang FOREIGN KEY (MaKhachHang) REFERENCES User(MaUser) ON DELETE CASCADE,
    CONSTRAINT unique_phieuxuat UNIQUE (MaNhanVien, MaKhachHang, NgayXuat)
);

-- Bảng 5: Chi tiết Phiếu xuất
CREATE TABLE ChiTietPhieuXuat (
    MaPX CHAR(5) NOT NULL,
    MaThuoc CHAR(5) NOT NULL,
    SoLuong INT NOT NULL,
    DonGia DECIMAL(10, 2) NOT NULL,
    PRIMARY KEY (MaPX, MaThuoc),
    FOREIGN KEY (MaPX) REFERENCES PhieuXuat(MaPX),
    FOREIGN KEY (MaThuoc) REFERENCES Thuoc(MaThuoc),
    CONSTRAINT unique_chitietphieuxuat UNIQUE (MaPX,MaThuoc,SoLuong,DonGia)
);

-- Trigger trước khi thêm Hãng sản xuất
DROP TRIGGER IF EXISTS before_insert_HangSX;
DELIMITER //
CREATE TRIGGER before_insert_HangSX BEFORE INSERT ON hangsx
FOR EACH ROW
BEGIN
    DECLARE new_id CHAR(5);
    DECLARE id_number INT;
    SELECT CAST(SUBSTRING(MaHangSX, 2, 4) AS UNSIGNED) INTO id_number FROM hangsx ORDER BY MaHangSX DESC LIMIT 1;
    
    IF id_number IS NULL THEN -- Nếu chưa có bản ghi nào, bắt đầu từ 1
        SET id_number = 1;
    ELSE
        SET id_number = id_number + 1;
    END IF;
    
    SET new_id = CONCAT('B', LPAD(id_number, 4, '0')); -- điền thêm bên trái các kí tự '0'
	SET NEW.MaHangSX = new_id;
END; //
DELIMITER ;

-- Trigger trước khi thêm Nhà cung cấp
DROP TRIGGER IF EXISTS before_insert_NhaCungCap;
DELIMITER //
CREATE TRIGGER before_insert_NhaCungCap BEFORE INSERT ON NhaCungCap
FOR EACH ROW
BEGIN
    DECLARE new_id CHAR(5);
    DECLARE id_number INT;
    SELECT CAST(SUBSTRING(MaNhaCungCap, 2, 4) AS UNSIGNED) INTO id_number FROM NhaCungCap ORDER BY MaNhaCungCap DESC LIMIT 1;
    
    IF id_number IS NULL THEN -- Nếu chưa có bản ghi nào, bắt đầu từ 1
        SET id_number = 1;
    ELSE
        SET id_number = id_number + 1;
    END IF;
    
    SET new_id = CONCAT('F', LPAD(id_number, 4, '0')); -- điền thêm bên trái các kí tự '0'
	SET NEW.MaNhaCungCap = new_id;
END; //
DELIMITER ;

-- Trigger trước khi thêm LoạiThuốc
DROP TRIGGER IF EXISTS before_insert_LoaiThuoc;
DELIMITER //
CREATE TRIGGER before_insert_LoaiThuoc BEFORE INSERT ON LoaiThuoc
FOR EACH ROW
BEGIN
    DECLARE new_id CHAR(5);
    DECLARE id_number INT;
    SELECT CAST(SUBSTRING(MaLoai, 2, 4) AS UNSIGNED) INTO id_number FROM LoaiThuoc ORDER BY MaLoai DESC LIMIT 1;
    
    IF id_number IS NULL THEN -- Nếu chưa có bản ghi nào, bắt đầu từ 1 
        SET id_number = 1;
    ELSE
        SET id_number = id_number + 1;
    END IF;
    
    SET new_id = CONCAT('T', LPAD(id_number, 4, '0')); -- điền thêm bên trái các kí tự '0'
	SET NEW.MaLoai = new_id;
END; //
DELIMITER ;

-- Trigger trước khi thêm Người dùng
DROP TRIGGER IF EXISTS before_insert_User;
DELIMITER //
CREATE TRIGGER before_insert_User BEFORE INSERT ON User
FOR EACH ROW
BEGIN
    DECLARE new_id CHAR(5);
    DECLARE id_number INT;
    
	-- Kiểm tra nếu số điện thoại không phải là chuỗi gồm đúng 10 ký tự số
    IF NEW.SoDienThoai NOT REGEXP '^[0-9]{10}$' THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Số điện thoại phải là chuỗi gồm đúng 10 chữ số';
    END IF;
    
    -- Băm mật khẩu (dùng SHA2, 256-bit)
    SET NEW.Pwd = SHA2(NEW.Pwd, 256);
    SELECT CAST(SUBSTRING(MaUser, 2, 4) AS UNSIGNED) INTO id_number FROM User ORDER BY MaUser DESC LIMIT 1;
    
    IF id_number IS NULL THEN -- Nếu chưa có bản ghi nào, bắt đầu từ 1
        SET id_number = 1;
    ELSE
        SET id_number = id_number + 1;
    END IF;
    
    SET new_id = CONCAT('U', LPAD(id_number, 4, '0')); -- điền thêm bên trái các kí tự '0'
	SET NEW.MaUser = new_id;
END; //
DELIMITER ;

-- Trigger trước khi thêm Thuốc
DROP TRIGGER IF EXISTS before_insert_Thuoc;
DELIMITER //
CREATE TRIGGER before_insert_Thuoc BEFORE INSERT ON Thuoc
FOR EACH ROW
BEGIN
    DECLARE new_id CHAR(5);
    DECLARE id_number INT;
    SELECT CAST(SUBSTRING(MaThuoc, 2, 4) AS UNSIGNED) INTO id_number FROM Thuoc ORDER BY MaThuoc DESC LIMIT 1;
    
    IF id_number IS NULL THEN -- Nếu chưa có bản ghi nào, bắt đầu từ 1
        SET id_number = 1;
    ELSE
        SET id_number = id_number + 1;
    END IF;
    
    SET new_id = CONCAT('M', LPAD(id_number, 4, '0')); -- điền thêm bên trái các kí tự '0'
	SET NEW.MaThuoc = new_id;
END; //
DELIMITER ;

-- Lấy thông tin Thuốc bằng mã thuốc
DROP PROCEDURE IF EXISTS usp_layThongTinThuoc;
DELIMITER //
CREATE PROCEDURE usp_layThongTinThuoc (
	IN in_MaThuoc CHAR(5)
)
BEGIN
	SELECT 
		MaThuoc,
        TenThuoc,
        MaLoai,
        MaHangSX,
        MaNhaCungCap,
        CongDung,
        SoLuongTon
	FROM thuoc AS m
    WHERE m.MaThuoc = in_MaThuoc;
end //
DELIMITER ;

-- Lấy thông tin Người dùng bằng mã người dùng
DROP PROCEDURE IF EXISTS usp_layThongTinNguoiDung;
DELIMITER //
CREATE PROCEDURE usp_layThongTinNguoiDung (
	IN in_MaUser CHAR(5)
)
BEGIN
	SELECT 
		MaUser,
		TenUser,
		SoDienThoai,
		DiaChi,
		NgaySinh,
		NhanVien
	FROM user AS u
    WHERE u.MaUser = in_MaUser;
END //
DELIMITER ;

-- Lấy thông tin Người dùng bằng số điện thoại
DROP PROCEDURE IF EXISTS usp_layThongTinNguoiDungBangSDT;
DELIMITER //
CREATE PROCEDURE usp_layThongTinNguoiDungBangSDT (
	IN in_SDT CHAR(10)
)
BEGIN
	SELECT 
		MaUser,
		TenUser,
		SoDienThoai,
		DiaChi,
		NgaySinh,
		NhanVien
	FROM user AS u
    WHERE u.SoDienThoai = in_SDT;
END //
DELIMITER ;

-- Lấy thông tin phiếu nhập bằng mã phiếu
DROP PROCEDURE IF EXISTS usp_layThongTinPhieuNhap;
DELIMITER //
CREATE PROCEDURE usp_layThongTinPhieuNhap (
	IN in_MaPN CHAR(5)
)
BEGIN
	SELECT 
		MaPN,
		MaNhanVien,
		MaNhaCungCap,
		NgayNhap
	FROM phieunhap AS i
    WHERE i.MaPN = in_MaPN;
END //
DELIMITER ;

-- Lấy thông tin chi tiết phiếu nhập bằng mã phiếu
DROP PROCEDURE IF EXISTS usp_layThongTinChiTietPhieuNhap;
DELIMITER //
CREATE PROCEDURE usp_layThongTinChiTietPhieuNhap (
	IN in_MaPN CHAR(5)
)
BEGIN
	SELECT 
		MaThuoc,
        SoLuong,
        DonGia
	FROM chitietphieunhap AS ii
    WHERE ii.MaPN = in_MaPN;
END //
DELIMITER ;

-- Hiển thị danh sách thuốc
DROP PROCEDURE IF EXISTS usp_hienThiDanhSachThuoc;
DELIMITER //
CREATE PROCEDURE usp_hienThiDanhSachThuoc (
    IN in_count INT,
    IN in_offset INT
)
BEGIN
    SELECT 
        m.MaThuoc AS "Mã Thuốc", 
        m.TenThuoc AS "Tên Thuốc", 
        t.Tenloai AS "Loại Thuốc", 
        s.TenNhaCungCap AS "Nhà cung cấp", 
        f.TenHang AS "Hãng SX", 
        m.CongDung AS "Công dụng",
        m.SoLuongTon AS "Số lượng"
    FROM thuoc AS m
    LEFT JOIN loaithuoc AS t ON m.Maloai = t.Maloai
    LEFT JOIN nhacungcap AS s ON m.MaNhaCungCap = s.MaNhaCungCap
    LEFT JOIN hangsx AS f ON m.MaHangSX = f.MaHangSX
	ORDER BY m.MaThuoc
    LIMIT in_offset, in_count;
END //
DELIMITER ;

-- Hiển thị danh sách khách hàng
DROP PROCEDURE IF EXISTS usp_hienThiDanhSachKhachHang;
DELIMITER //
CREATE PROCEDURE usp_hienThiDanhSachKhachHang (
    IN in_count INT,
    IN in_offset INT
)
BEGIN
    SELECT 
        u.MaUser AS "Mã", 
        u.TenUser AS "Họ và tên", 
        u.NgaySinh AS "Ngày sinh", 
        u.SoDienThoai AS "SĐT", 
        u.DiaChi AS "Địa chỉ"
    FROM user AS u
    WHERE u.NhanVien = 0
	ORDER BY u.NhanVien
    LIMIT in_offset, in_count;
END //
DELIMITER ;

-- Hiển thị lịch sử mua hàng của khách hàng
DROP PROCEDURE IF EXISTS usp_lichSuMuaCuaKhachHang;
DELIMITER //
CREATE PROCEDURE usp_lichSuMuaCuaKhachHang (
    IN in_MaKhachHang CHAR(5)
)
BEGIN
    SELECT 
        d.MaPX AS "Mã Phiếu", 
        d.NgayXuat AS "Ngày xuất", 
        SUM(i.DonGia * i.SoLuong) AS "Tổng tiền"
    FROM phieuxuat AS d
    JOIN chitietphieuxuat AS i ON d.MaPX = i.MaPX
    WHERE d.MaKhachHang = in_MaKhachHang
    GROUP BY d.MaPX, d.NgayXuat
	ORDER BY d.MaPX;
END //
DELIMITER ;

-- Tính tổng số tiền phiếu nhập
DROP FUNCTION IF EXISTS tinhTongSoTienPhieuNhap;
DELIMITER //
CREATE FUNCTION tinhTongSoTienPhieuNhap(in_MaPN CHAR(5))
RETURNS DECIMAL(15, 3)
DETERMINISTIC
BEGIN
    DECLARE total DECIMAL(15, 3) DEFAULT 0;

    SELECT SUM(ct.SoLuong * ct.DonGia) INTO total
    FROM phieunhap pn 
    JOIN ChiTietPhieuNhap ct ON pn.MaPN = ct.MaPN
    WHERE pn.MaPN = in_MaPN;
    
	IF total IS NULL THEN
		SET total = 0;
    END IF;
    
    RETURN total;
END //
DELIMITER ;

-- Tính tổng số tiền phiếu xuất
DROP FUNCTION IF EXISTS tinhTongSoTienPhieuXuat;
DELIMITER //
CREATE FUNCTION tinhTongSoTienPhieuXuat(in_MaPX CHAR(5))
RETURNS DECIMAL(15, 3)
DETERMINISTIC
BEGIN
    DECLARE total DECIMAL(15, 3) DEFAULT 0;

    SELECT SUM(dd.SoLuong * dd.DonGia) INTO total
    FROM ChiTietPhieuXuat dd
    WHERE dd.MaPX = in_MaPX;

	IF total IS NULL THEN
    SET total = 0;
    END IF;

    -- Trả về tổng số tiền
    RETURN total;
END //
DELIMITER ;

-- Hiển thị danh sách phiếu nhập
DROP PROCEDURE IF EXISTS usp_hienThiDanhSachPhieuNhap;
DELIMITER //
CREATE PROCEDURE usp_hienThiDanhSachPhieuNhap (
    IN in_count INT,
    IN in_offset INT
)
BEGIN
    SELECT 
        i.MaPN AS "Mã Phiếu", 
        e.TenUser AS "Tên Nhân viên", 
        s.TenNhaCungCap AS "Tên Nhà CC", 
        i.NgayNhap AS "Ngày nhập",
        tinhTongSoTienPhieuNhap(i.MaPN) AS "Tổng"
    FROM phieunhap AS i
    JOIN user AS e ON i.MaNhanVien = e.MaUser
    JOIN nhacungcap AS s ON i.MaNhaCungCap = s.MaNhaCungCap
	ORDER BY i.MaPN
    LIMIT in_offset, in_count;
END //
DELIMITER ;

-- Hiển thị chi tiết của một phiếu nhập
DROP PROCEDURE IF EXISTS usp_hienThiChiTietPhieuNhap;
DELIMITER //
CREATE PROCEDURE usp_hienThiChiTietPhieuNhap (
    IN in_MaPhieuNhap CHAR(5)
)
BEGIN
    SELECT 
        ii.MaThuoc AS "Mã thuốc",
        m.TenThuoc AS "Tên thuốc",
        ii.SoLuong AS "Số lượng",
        ii.DonGia AS "Đơn giá"
    FROM chitietphieunhap AS ii
    LEFT JOIN thuoc AS m ON m.MaThuoc = ii.MaThuoc
    WHERE ii.MaPN = in_MaPhieuNhap
	ORDER BY ii.MaThuoc;
END //
DELIMITER ;

-- Procedure hiển thị danh sách phiếu xuất
DROP PROCEDURE IF EXISTS usp_hienThiDanhSachPhieuXuat;
DELIMITER //
CREATE PROCEDURE usp_hienThiDanhSachPhieuXuat (
    IN in_count INT,
    IN in_offset INT
)
BEGIN
    SELECT 
        d.MaPX AS "Mã Phiếu",
        e.TenUser AS "Tên Nhân viên",
        c.TenUser AS "Tên Khách Hàng",
        d.NgayXuat AS "Ngày xuất",
        tinhTongSoTienPhieuXuat(d.MaPX) AS "Tổng"
    FROM phieuxuat AS d
    LEFT JOIN user AS e ON e.MaUser = d.MaNhanVien 
    LEFT JOIN user AS c ON c.MaUser = d.MaKhachHang
	ORDER BY d.MaPX
    LIMIT in_offset, in_count;
END //
DELIMITER ;

-- Procedure hiển thị chi tiết của một phiếu xuất
DROP PROCEDURE IF EXISTS usp_hienThiChiTietPhieuXuat;
DELIMITER //
CREATE PROCEDURE usp_hienThiChiTietPhieuXuat (
    IN in_MaPhieuXuat CHAR(5)
)
BEGIN
    SELECT 
        dd.MaThuoc AS "Mã thuốc",
        m.TenThuoc AS "Tên thuốc",
        dd.SoLuong AS "Số lượng",
        dd.DonGia AS "Đơn giá"
    FROM chitietphieuxuat AS dd
    LEFT JOIN thuoc AS m ON m.MaThuoc = dd.MaThuoc
    WHERE dd.MaPX = in_MaPhieuXuat
	ORDER BY dd.MaThuoc;
END //
DELIMITER ;

-- Hiển thị lịch sử mua thuốc của khách hàng
DROP PROCEDURE IF EXISTS usp_hienThiLichSuMuaThuoc;
DELIMITER //
CREATE PROCEDURE usp_hienThiLichSuMuaThuoc (
    IN in_maKhachHang CHAR(5)
)
BEGIN
    SELECT 
        d.MaPX AS "Mã phiếu",
        d.NgayXuat AS "Ngày mua",
        e.TenUser AS "Nhân viên",
        SUM(dd.SoLuong * dd.DonGia) AS "Tổng"
    FROM phieuxuat AS d 
        LEFT JOIN user AS e ON d.MaNhanVien = e.MaUser 
        LEFT JOIN chitietphieuxuat AS dd ON d.MaPX = dd.MaPX
    WHERE d.MaKhachHang = in_maKhachHang
    GROUP BY d.MaPX, e.TenUser, d.NgayXuat;
END //
DELIMITER ;

-- Hiển thị chi tiết hóa đơn của khách hàng
DROP PROCEDURE IF EXISTS usp_hienThiChiTietHoaDon;
DELIMITER //
CREATE PROCEDURE usp_hienThiChiTietHoaDon (
    IN in_maHoaDon CHAR(5)
)
BEGIN
    SELECT 
        m.TenThuoc AS "Tên thuốc",
        dd.SoLuong AS "Số lượng",
        dd.DonGia AS "Đơn giá"
    FROM chitietphieuxuat AS dd
        LEFT JOIN thuoc AS m ON m.MaThuoc = dd.MaThuoc
    WHERE dd.MaPX = in_maHoaDon;
END //
DELIMITER ;

-- Tìm kiếm thuốc theo mã, tên thuốc, tên loại và nhà sản xuất và nhà cung cấp
DROP PROCEDURE IF EXISTS Tim_Thuoc;
DELIMITER //
CREATE PROCEDURE Tim_Thuoc (
	IN ma_thuoc CHAR(5),
    IN ten_thuoc VARCHAR(255),
    IN ten_loai VARCHAR(255),
    IN ten_ncc VARCHAR(255),
    IN ten_hangsx VARCHAR(255)
)
BEGIN
    SELECT
		 t.MaThuoc AS "Mã Thuốc", 
		 t.TenThuoc AS "Tên Thuốc", 
		 l.Tenloai AS "Loại Thuốc", 
		 ncc.TenNhaCungCap AS "Nhà cung cấp", 
		 h.TenHang AS "Hãng SX", 
		 t.CongDung AS "Công dụng",
		 t.SoLuongTon AS "Số lượng"
    FROM Thuoc t
    JOIN HangSX h ON t.MaHangSX = h.MaHangSX
    JOIN NhaCungCap ncc ON t.MaNhaCungCap = ncc.MaNhaCungCap
    JOIN LoaiThuoc l ON t.MaLoai = l.MaLoai
    WHERE 
		(ma_thuoc IS NULL OR ma_thuoc = '' OR t.MaThuoc = ma_thuoc)
		AND (
			(ten_thuoc IS NULL OR ten_thuoc = '' OR t.TenThuoc LIKE CONCAT('%', ten_thuoc, '%'))
			AND (ten_hangsx IS NULL OR ten_hangsx = '' OR h.TenHang LIKE CONCAT('%', ten_hangsx, '%'))
			AND (ten_ncc IS NULL OR ten_ncc = '' OR ncc.TenNhaCungCap LIKE CONCAT('%', ten_ncc, '%'))
			AND (ten_loai IS NULL OR ten_loai = '' OR l.TenLoai LIKE CONCAT('%', ten_loai, '%'))
		)
	ORDER BY t.MaThuoc;
END //
DELIMITER ;

-- Tìm kiếm theo tên khách hàng theo ngày mua (from_to_) và tên khách hàng / sdt
DROP PROCEDURE IF EXISTS Tim_Khach_Hang_Co_Ngay_Mua;
DELIMITER //
CREATE PROCEDURE Tim_Khach_Hang_Co_Ngay_Mua (
    IN searchValue VARCHAR(255),
    IN fromDate DATE,
    IN toDate DATE
)
BEGIN
    SELECT DISTINCT
		 u.MaUser AS "Mã", 
		 u.TenUser AS "Họ và Tên", 
		 u.NgaySinh AS "Ngày sinh", 
		 u.SoDienThoai AS "SĐT", 
		 u.DiaChi AS "Địa chỉ"         
    FROM User u
    LEFT JOIN PhieuXuat px ON u.MaUser = px.MaKhachHang
    WHERE 
		u.NhanVien = False
        AND px.NgayXuat BETWEEN fromDate AND toDate
        AND (
            searchValue = '' 
            OR u.TenUser LIKE CONCAT('%',searchValue, '%') 
            OR u.SoDienThoai LIKE CONCAT('%', searchValue, '%')
        )
	ORDER BY u.MaUser;
END //
DELIMITER ;

-- Tìm kiếm theo tên khách hàng theo tên khách hàng / sdt
DROP PROCEDURE IF EXISTS Tim_Khach_Hang;
DELIMITER //
CREATE PROCEDURE Tim_Khach_Hang (
    IN searchValue VARCHAR(255)
)
BEGIN
    SELECT DISTINCT
		 u.MaUser AS "Mã", 
		 u.TenUser AS "Họ và Tên", 
		 u.NgaySinh AS "Ngày sinh", 
		 u.SoDienThoai AS "SĐT", 
		 u.DiaChi AS "Địa chỉ"
    FROM User u
    LEFT JOIN PhieuXuat px ON u.MaUser = px.MaKhachHang
    WHERE 
		u.NhanVien = False
        AND(
            searchValue = '' 
            OR u.TenUser LIKE CONCAT('%',searchValue, '%') 
            OR u.SoDienThoai LIKE CONCAT('%', searchValue, '%')
        )
	ORDER BY u.MaUser;
END //
DELIMITER ;

-- Tìm kiếm phiếu nhập theo mã phiếu nhập, mã nhân viên / tên nhân viên, mã ncc / tên ncc và ngày nhập
DROP PROCEDURE IF EXISTS Tim_Phieu_Nhap;
DELIMITER //
CREATE PROCEDURE Tim_Phieu_Nhap (
	IN ma_phieu_nhap CHAR(5),
    IN nhan_vien VARCHAR(255),
    IN ncc VARCHAR(255),
    IN fromDate DATE,
    IN toDate DATE
)
BEGIN
    SELECT DISTINCT
		 pn.MaPN AS "Mã Phiếu", 
		 nv.TenUser AS "Tên Nhân Viên", 
		 ncc.TenNhaCungCap AS "Tên Nhà CC", 
		 pn.NgayNhap AS "Ngày Nhập",
         tinhTongSoTienPhieuNhap(pn.MaPN) AS "Tổng"
    FROM PhieuNhap pn
    JOIN User nv ON nv.MaUser = pn.MaNhanVien
    JOIN NhaCungCap ncc ON ncc.MaNhaCungCap = pn.MaNhaCungCap
    WHERE 
		(ma_phieu_nhap = '' OR pn.MaPN LIKE CONCAT('%',ma_phieu_nhap,'%'))
		AND (
			nhan_vien = '' 
            OR pn.MaNhanVien LIKE CONCAT('%',nhan_vien,'%') 
            OR nv.TenUser LIKE CONCAT('%',nhan_vien,'%')
		)
		AND (
			ncc = '' 
            OR pn.MaNhaCungCap LIKE CONCAT('%',ncc,'%') 
            OR ncc.TenNhaCungCap LIKE CONCAT('%',ncc,'%')
		)
		AND (pn.NgayNhap BETWEEN fromDate AND toDate)
	ORDER BY pn.MaPN;
END //
DELIMITER ;

-- Tìm kiếm hóa đơn theo mã phiếu xuất, mã nhân viên/tên nhân viên, mã khách hàng / tên khách hàng và ngày nhập
DROP PROCEDURE IF EXISTS Tim_Hoa_Don;
DELIMITER //
CREATE PROCEDURE Tim_Hoa_Don (
    IN ma_phieu_xuat CHAR(5),
    IN nhan_vien VARCHAR(255),
    IN khach_hang VARCHAR(255),
    IN fromDate DATE,
    IN toDate DATE
)
BEGIN
    SELECT
		px.MaPX AS "Mã Phiếu", 
		nv.TenUser AS "Tên Nhân Viên", 
		kh.TenUser AS "Tên Khách Hàng", 
		px.NgayXuat AS "Ngày Xuất",
        tinhTongSoTienPhieuXuat(px.MaPX) AS "Tổng"
    FROM PhieuXuat px
    JOIN User nv ON nv.MaUser = px.MaNhanVien 
    JOIN User kh ON kh.MaUser = px.MaKhachHang
    WHERE 
        (ma_phieu_xuat = '' OR px.MaPX LIKE CONCAT('%', ma_phieu_xuat, '%'))
        AND (
            nhan_vien = '' 
            OR px.MaNhanVien LIKE CONCAT('%', nhan_vien, '%') 
            OR nv.TenUser LIKE CONCAT('%', nhan_vien, '%')
        )
        AND (
            khach_hang = '' 
            OR px.MaKhachHang LIKE CONCAT('%', khach_hang, '%') 
            OR kh.TenUser LIKE CONCAT('%', khach_hang, '%')
        )
        AND (px.NgayXuat BETWEEN fromDate AND toDate)
    ORDER BY px.MaPX;
END //
DELIMITER ;

DROP PROCEDURE IF EXISTS Tim_Hoa_Don_Cua_Khanh_Hang;
DELIMITER //
CREATE PROCEDURE Tim_Hoa_Don_Cua_Khanh_Hang (
    IN ma_khach_hang CHAR(5),
    IN ma_phieu_xuat CHAR(5),
    IN fromDate DATE,
    IN toDate DATE
)
BEGIN
    SELECT
        px.MaPX AS "Mã phiếu",
        px.NgayXuat AS "Ngày mua",
        nv.TenUser AS "Nhân viên",
        tinhTongSoTienPhieuXuat(px.MaPX) AS "Tổng"
    FROM PhieuXuat px
    JOIN User AS kh ON kh.MaUser = px.MaKhachHang
    JOIN User AS nv ON nv.MaUser = px.MaNhanVien
    WHERE 
        (ma_phieu_xuat = '' OR px.MaPX LIKE CONCAT('%', ma_phieu_xuat, '%'))
        AND px.MaKhachHang = ma_khach_hang
        AND (px.NgayXuat BETWEEN fromDate AND toDate)
    ORDER BY px.MaPX;
END //
DELIMITER ;

-- Kiểm tra đăng nhập. Trả về true nếu sdt và mật khẩu đúng, ngược lại trả về false
DROP FUNCTION IF EXISTS checking_login;
DELIMITER //
CREATE FUNCTION checking_login(p_SoDienThoai CHAR(10), p_Pwd VARCHAR(64)) 
RETURNS BOOLEAN
DETERMINISTIC
BEGIN
    DECLARE hashedPwd VARCHAR(64);
    DECLARE userExists BOOLEAN DEFAULT FALSE;
    
    SET hashedPwd = SHA2(p_Pwd, 256);
    
    SELECT COUNT(*) > 0 INTO userExists
    FROM user
    WHERE SoDienThoai = p_SoDienThoai AND Pwd = hashedPwd;
    
    RETURN userExists; -- Trả về TRUE nếu thông tin đăng nhập đúng, FALSE nếu sai
END //
DELIMITER ;

-- Đăng ký tài khoản khách hàng
DROP FUNCTION  IF EXISTS sign_up;
DELIMITER //
CREATE FUNCTION sign_up (
    p_SoDienThoai CHAR(10),
    p_Pwd VARCHAR(64),
    p_TenUser VARCHAR(50),
    p_DiaChi VARCHAR(255),
    p_ngaySinh DATE
) 
RETURNS BOOLEAN
DETERMINISTIC
BEGIN
	INSERT INTO User(SoDienThoai, Pwd, TenUser, DiaChi, NhanVien, NgaySinh)
	VALUES (p_SoDienThoai, p_Pwd, p_TenUser, p_DiaChi, FALSE, p_ngaySinh);
	RETURN TRUE;
END //
DELIMITER ;

-- Đổi mật khẩu của khách hàng
DROP FUNCTION IF EXISTS DoiMatKhau;
DELIMITER //
CREATE FUNCTION DoiMatKhau(MaUser CHAR(5), old_pwd VARCHAR(64), new_pwd VARCHAR(64))
RETURNS BOOLEAN
DETERMINISTIC
BEGIN
    DECLARE result BOOLEAN DEFAULT 0;

    -- Kiểm tra mật khẩu cũ có đúng không
    IF (SELECT u.Pwd FROM User u WHERE u.MaUser = MaUser) = SHA2(old_pwd, 256) THEN
        -- Nếu đúng, cập nhật mật khẩu mới đã băm
        UPDATE User AS u
        SET u.Pwd = SHA2(new_pwd, 256)
        WHERE u.MaUser = MaUser;

        SET result = 1; -- Mật khẩu cũ đúng, trả về true
    END IF;

    RETURN result;
END //
DELIMITER ;

-- Cập nhật số lượng thuốc 
DROP PROCEDURE IF EXISTS CapNhatSoLuong;
DELIMITER //
CREATE PROCEDURE CapNhatSoLuong(
    IN p_MaThuoc CHAR(5),
    IN p_SoLuongNhap INT,
    IN p_SoLuongXuat INT
)
BEGIN
    DECLARE current_stock INT;
   
    START TRANSACTION; -- Bắt đầu transaction

    -- Cập nhật số lượng nhập
    UPDATE thuoc
    SET SoLuongTon = SoLuongTon + p_SoLuongNhap
    WHERE MaThuoc = p_MaThuoc;

    -- Lấy số lượng tồn hiện tại sau khi nhập
    SELECT SoLuongTon INTO current_stock
    FROM thuoc WHERE MaThuoc = p_MaThuoc;

    -- Kiểm tra nếu số lượng tồn kho < số lượng xuất thì thông báo lỗi
    IF current_stock < p_SoLuongXuat THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Số lượng tồn kho không đủ để xuất';
        ROLLBACK; -- Hủy transaction
    ELSE
        -- Nếu đủ điều kiện, cập nhật số lượng xuất
        UPDATE thuoc
        SET SoLuongTon = SoLuongTon - p_SoLuongXuat
        WHERE MaThuoc = p_MaThuoc;
       
        COMMIT; -- Commit transaction
    END IF;
END //
DELIMITER ;

-- Thêm phiếu nhập với chi tiết của phiếu nhập đó
DROP PROCEDURE IF EXISTS InsertPhieuNhapWithDetails;
DELIMITER //
CREATE PROCEDURE InsertPhieuNhapWithDetails(
    IN MaNhanVien CHAR(5), 
    IN MaNhaCungCap CHAR(5), 
    IN NgayNhap VARCHAR(10),
    IN ChiTietPhieuNhap JSON -- Pass details as JSON array with MaThuoc, SoLuong, DonGia
)
BEGIN
    DECLARE new_id CHAR(5);
    DECLARE id_number INT;
    DECLARE idx INT DEFAULT 0;
    DECLARE details_count INT;
    DECLARE MaThuoc CHAR(5);
    DECLARE SoLuong INT;
    DECLARE DonGia DECIMAL(10, 2);
    
    START TRANSACTION; -- Start a transaction
    
    -- Generate new ID for PhieuNhap
    SELECT CAST(SUBSTRING(MaPN, 2, 4) AS UNSIGNED) INTO id_number 
    FROM PhieuNhap ORDER BY MaPN DESC LIMIT 1;

    IF id_number IS NULL 
    THEN SET id_number = 1;
    ELSE SET id_number = id_number + 1;
    END IF;

    SET new_id = CONCAT('I', LPAD(id_number, 4, '0'));

    -- Insert into PhieuNhap
    INSERT INTO PhieuNhap (MaPN, MaNhanVien, MaNhaCungCap, NgayNhap)
    VALUES (new_id, MaNhanVien, MaNhaCungCap, NgayNhap);

    SET details_count = JSON_LENGTH(ChiTietPhieuNhap); -- Get the count of items in the JSON array

    -- Loop through JSON array for ChiTietPhieuNhap entries
    WHILE idx < details_count DO
        -- Extract details for each item
        SET MaThuoc = JSON_UNQUOTE(JSON_EXTRACT(ChiTietPhieuNhap, CONCAT('$[', idx, '].MaThuoc')));
        SET SoLuong = JSON_UNQUOTE(JSON_EXTRACT(ChiTietPhieuNhap, CONCAT('$[', idx, '].SoLuong')));
        SET DonGia = JSON_UNQUOTE(JSON_EXTRACT(ChiTietPhieuNhap, CONCAT('$[', idx, '].DonGia')));

        -- Insert into ChiTietPhieuNhap
        INSERT INTO ChiTietPhieuNhap (MaPN, MaThuoc, SoLuong, DonGia)
        VALUES (new_id, MaThuoc, SoLuong, DonGia);
        
		CALL CapNhatSoLuong( MaThuoc, SoLuong, 0 );
		SET idx = idx + 1;
    END WHILE;
	
    COMMIT; -- Commit the transaction
END //
DELIMITER ;

-- Thêm phiếu xuất với chi tiết của phiếu xuất đó
DROP PROCEDURE IF EXISTS InsertPhieuXuatWithDetails;
DELIMITER //
CREATE PROCEDURE InsertPhieuXuatWithDetails(
    IN MaNhanVien CHAR(5), 
    IN MaKhachHang CHAR(5), 
    IN NgayXuat VARCHAR(10),
    IN ChiTietPhieuXuat JSON -- Pass details as JSON array with MaThuoc, SoLuong, DonGia
)
BEGIN
    DECLARE new_id CHAR(5);
    DECLARE id_number INT;
    DECLARE idx INT DEFAULT 0;
    DECLARE details_count INT;
    DECLARE MaThuoc CHAR(5);
    DECLARE SoLuong INT;
    DECLARE DonGia DECIMAL(10, 2);
    
    START TRANSACTION; -- Start a transaction
    
    IF NgayXuat IS NULL OR  NgayXuat = '' 
    THEN SET NgayXuat = CURDATE();
	ELSE SET NgayXuat = STR_TO_DATE(NgayXuat, '%Y-%m-%d');
    END IF;
    
    -- Generate new ID for PhieuNhap
    SELECT CAST(SUBSTRING(MaPX, 2, 4) AS UNSIGNED) INTO id_number 
    FROM PhieuXuat ORDER BY MaPX DESC LIMIT 1;

    IF id_number IS NULL 
    THEN SET id_number = 1;
    ELSE SET id_number = id_number + 1;
    END IF;

    SET new_id = CONCAT('D', LPAD(id_number, 4, '0'));

    -- Insert into PhieuXuat
    INSERT INTO PhieuXuat (MaPX, MaNhanVien, MaKhachHang, NgayXuat)
    VALUES (new_id, MaNhanVien, MaKhachHang, NgayXuat);

    SET details_count = JSON_LENGTH(ChiTietPhieuXuat); -- Get the count of items in the JSON array

    -- Loop through JSON array for ChiTietPhieuXuat entries
    WHILE idx < details_count DO
        -- Extract json
        SET MaThuoc = JSON_UNQUOTE(JSON_EXTRACT(ChiTietPhieuXuat, CONCAT('$[', idx, '].MaThuoc')));
        SET SoLuong = JSON_UNQUOTE(JSON_EXTRACT(ChiTietPhieuXuat, CONCAT('$[', idx, '].SoLuong')));
        SET DonGia = JSON_UNQUOTE(JSON_EXTRACT(ChiTietPhieuXuat, CONCAT('$[', idx, '].DonGia')));

        -- Thêm ChiTietPhieuXuat
        INSERT INTO ChiTietPhieuXuat (MaPX, MaThuoc, SoLuong, DonGia)
        VALUES (new_id, MaThuoc, SoLuong, DonGia);
        
		CALL CapNhatSoLuong( MaThuoc, 0, SoLuong );
        SET idx = idx + 1;
    END WHILE;
    COMMIT; -- Commit transaction
END //
DELIMITER ;

-- Thêm thuốc
DROP PROCEDURE IF EXISTS AddThuoc;
DELIMITER //
CREATE PROCEDURE AddThuoc (
    IN p_TenThuoc VARCHAR(100),
    IN p_MaHangSX CHAR(5),
    IN p_MaNhaCungCap CHAR(5),
    IN p_CongDung TEXT,
    IN p_MaLoai CHAR(5)
)
BEGIN
	INSERT INTO Thuoc (TenThuoc, MaHangSX, MaNhaCungCap, CongDung, MaLoai)
	VALUES (p_TenThuoc, p_MaHangSX, p_MaNhaCungCap, p_CongDung, p_MaLoai);
END //
DELIMITER ;

-- Sửa thuốc
DROP PROCEDURE IF EXISTS EditThuoc;
DELIMITER //
CREATE PROCEDURE EditThuoc (
    IN p_MaThuoc CHAR(5),
    IN p_TenThuoc VARCHAR(100),
    IN p_MaHangSX CHAR(5),
    IN p_MaNhaCungCap CHAR(5),
    IN p_CongDung TEXT,
    IN p_MaLoai CHAR(5)
)
BEGIN
    -- Kiểm tra xem thuốc có tồn tại hay không
    IF EXISTS (SELECT 1 FROM Thuoc WHERE MaThuoc = p_MaThuoc) THEN
        UPDATE Thuoc
        SET 
            TenThuoc = p_TenThuoc,
            MaHangSX = p_MaHangSX,
            MaNhaCungCap = p_MaNhaCungCap,
            CongDung = p_CongDung,
            MaLoai = p_MaLoai
        WHERE 
            MaThuoc = p_MaThuoc;
    ELSE -- Nếu thuốc không tồn tại, báo lỗi
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Thuốc không tồn tại!';
    END IF;
END //
DELIMITER ;

-- Xóa thuốc
DROP PROCEDURE IF EXISTS DeleteThuoc;
DELIMITER //
CREATE PROCEDURE DeleteThuoc(IN p_MaThuoc CHAR(5))
BEGIN
	-- Kiểm tra xem thuốc có tồn tại hay không
	IF EXISTS (SELECT 1 FROM Thuoc WHERE MaThuoc = p_MaThuoc) THEN
		 DELETE FROM Thuoc
		WHERE MaThuoc = p_MaThuoc;
	ELSE -- Nếu thuốc không tồn tại, báo lỗi
		SIGNAL SQLSTATE '45000'
		SET MESSAGE_TEXT = 'Thuốc không tồn tại!';
	END IF;
END //
DELIMITER ;

-- Thêm hãng sản xuất
DROP PROCEDURE IF EXISTS AddHangSX
DELIMITER //
CREATE PROCEDURE AddHangSX (
    IN p_TenHang VARCHAR(50),
    IN p_QuocGia VARCHAR(50)
)
BEGIN
	INSERT INTO HangSX (TenHang, QuocGia)
	VALUES (p_TenHang, p_QuocGia);
END //
DELIMITER ;

-- Sửa hãng sản xuất
DROP PROCEDURE IF EXISTS EditHangSX
DELIMITER //
CREATE PROCEDURE EditHangSX (
    IN p_MaHangSX CHAR(5),
    IN p_TenHang VARCHAR(50),
    IN p_QuocGia VARCHAR(50)
)
BEGIN
    IF EXISTS (SELECT 1 FROM HangSX WHERE MaHangSX = p_MaHangSX) THEN
        UPDATE HangSX
        SET 
            TenHang = p_TenHang,
            QuocGia = p_QuocGia
        WHERE MaHangSX = p_MaHangSX;
    ELSE
        SIGNAL SQLSTATE '45000' -- Nếu thuốc không tồn tại, báo lỗi
        SET MESSAGE_TEXT = 'Hãng sản xuất không tồn tại!';
    END IF;
END //
DELIMITER ;

-- Xóa hãng sản xuất
DROP PROCEDURE IF EXISTS DeleteHangSX;
DELIMITER //
CREATE PROCEDURE DeleteHangSX(IN p_MaHangSX CHAR(5))
BEGIN
	IF EXISTS (SELECT 1 FROM HangSX WHERE MaHangSX = p_MaHangSX) THEN
		DELETE FROM HangSX
		WHERE MaHangSX = p_MaHangSX;
	ELSE -- Nếu hãng sản xuất không tồn tại, báo lỗi
		SIGNAL SQLSTATE '45000'
		SET MESSAGE_TEXT = 'HangSX không tồn tại!';
	END IF;
END //
DELIMITER ;

-- Thêm Nhà Cung Cấp
DROP PROCEDURE IF EXISTS AddNhaCungCap;
DELIMITER //
CREATE PROCEDURE AddNhaCungCap (
    IN p_TenNhaCungCap VARCHAR(50),
    IN p_DiaChi VARCHAR(255),
    IN p_ThongTinDaiDien TEXT
)
BEGIN
	INSERT INTO NhaCungCap (TenNhaCungCap, DiaChi, ThongTinDaiDien)
	VALUES (p_TenNhaCungCap, p_DiaChi, p_ThongTinDaiDien);
END //
DELIMITER ;

-- Sửa Nhà Cung Cấp
DROP PROCEDURE IF EXISTS EditNhaCungCap;
DELIMITER //
CREATE PROCEDURE EditNhaCungCap (
    IN p_MaNhaCungCap CHAR(5),
    IN p_TenNhaCungCap VARCHAR(50),
    IN p_DiaChi VARCHAR(255),
	IN p_ThongTinDaiDien TEXT
)
BEGIN
    IF EXISTS (SELECT 1 FROM NhaCungCap WHERE MaNhaCungCap = p_MaNhaCungCap) THEN
        UPDATE NhaCungCap
        SET 
            TenNhaCungCap = p_TenNhaCungCap,
            DiaChi = p_DiaChi,
			ThongTinDaiDien = p_ThongTinDaiDien
        WHERE MaNhaCungCap = p_MaNhaCungCap;
    ELSE -- Nếu Nhà Cung Cấp không tồn tại, báo lỗi
        SIGNAL SQLSTATE '45000' 
        SET MESSAGE_TEXT = 'NhaCungCap không tồn tại!';
    END IF;
END //
DELIMITER ;

-- Xóa Nhà Cung Cấp
DROP PROCEDURE IF EXISTS DeleteNhaCungCap;
DELIMITER //
CREATE PROCEDURE DeleteNhaCungCap (IN p_MaNhaCungCap CHAR(5))
BEGIN
	IF EXISTS (SELECT 1 FROM NhaCungCap WHERE MaNhaCungCap = p_MaNhaCungCap) THEN
		 DELETE FROM NhaCungCap
		WHERE MaNhaCungCap = p_MaNhaCungCap;
	ELSE -- Nếu Nhà Cung Cấp không tồn tại, báo lỗi
		SIGNAL SQLSTATE '45000'
		SET MESSAGE_TEXT = 'NhaCungCap không tồn tại!';
	END IF;
END //
DELIMITER ;

-- Thêm Loại Thuốc
DROP PROCEDURE IF EXISTS AddLoaiThuoc;
DELIMITER //
CREATE PROCEDURE AddLoaiThuoc (
    IN p_TenLoai VARCHAR(50),
    IN p_GhiChu TEXT
)
BEGIN
	INSERT INTO LoaiThuoc (TenLoai, GhiChu)
	VALUES (p_TenLoai, p_GhiChu);
END //
DELIMITER ;

-- Sửa Loại Thuốc
DROP PROCEDURE IF EXISTS EditLoaiThuoc;
DELIMITER //
CREATE PROCEDURE EditLoaiThuoc (
    IN p_MaLoai CHAR(5),
    IN p_TenLoai VARCHAR(50),
	IN p_GhiChu TEXT
)
BEGIN
    IF EXISTS (SELECT 1 FROM LoaiThuoc WHERE MaLoai = p_MaLoai) THEN
        UPDATE LoaiThuoc
        SET 
            TenLoai = p_TenLoai,
            GhiChu = p_GhiChu
        WHERE MaLoai = p_MaLoai;
    ELSE -- Nếu Loại Thuốc không tồn tại, báo lỗi
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'LoaiThuoc không tồn tại!';
    END IF;
END //
DELIMITER ;

-- Xóa Loại Thuốc
DROP PROCEDURE IF EXISTS DeleteLoaiThuoc;
DELIMITER //
CREATE PROCEDURE DeleteLoaiThuoc (IN p_MaLoai CHAR(5))
BEGIN
	IF EXISTS (SELECT 1 FROM LoaiThuoc WHERE MaLoai = p_MaLoai) THEN
		 DELETE FROM LoaiThuoc
		WHERE MaLoai = p_MaLoai;
	ELSE -- Nếu Loại Thuốc không tồn tại, báo lỗi
		SIGNAL SQLSTATE '45000'
		SET MESSAGE_TEXT = 'LoaiThuoc không tồn tại!';
	END IF;
END //
DELIMITER ;

-- Thêm User
DROP PROCEDURE IF EXISTS AddUser;
DELIMITER //
CREATE PROCEDURE AddUser (
    IN p_SoDienThoai CHAR(10),
    IN p_Pwd VARCHAR(64),
    IN p_TenUser VARCHAR(50) ,
    IN p_DiaChi VARCHAR(255),
    IN p_NgaySinh DATE
)
BEGIN
	SELECT sign_up(p_SoDienThoai, p_Pwd, p_TenUser, p_DiaChi, p_NgaySinh) as user;
END //
DELIMITER ;

-- Sửa User
DROP PROCEDURE IF EXISTS EditUser
DELIMITER //
CREATE PROCEDURE EditUser (
	IN p_MaUser CHAR(5),
    IN p_SoDienThoai CHAR(10),
    IN p_TenUser VARCHAR(50) ,
    IN p_DiaChi VARCHAR(255),
    IN p_NgaySinh DATE
)
BEGIN
    IF EXISTS (SELECT 1 FROM User WHERE MaUser = p_MaUser) THEN
        UPDATE User
        SET 
            MaUser= p_MaUser,
			SoDienThoai = p_SoDienThoai,
			TenUser = p_TenUser,
			DiaChi = p_DiaChi,
			NgaySinh =  p_NgaySinh
        WHERE MaUser = p_MaUser;
    ELSE -- Nếu User không tồn tại, báo lỗi
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'User không tồn tại!';
    END IF;
END //
DELIMITER ;

-- Xóa User
DROP PROCEDURE IF EXISTS DeleteUser
DELIMITER //
CREATE PROCEDURE DeleteUser (IN p_MaUser CHAR(5))
BEGIN
	IF EXISTS (SELECT 1 FROM User WHERE MaUser = p_MaUser) THEN
		 DELETE FROM User
		WHERE MaUser = p_MaUser;
	ELSE -- Nếu User không tồn tại, báo lỗi
		SIGNAL SQLSTATE '45000'
		SET MESSAGE_TEXT = 'User không tồn tại!';
	END IF;
END //
DELIMITER ;

-- Xóa phiếu nhập, xóa xong số lượng tồn của mỗi thuốc thuộc phiếu nhập sẽ được cập nhật lại trở về lúc chưa nhập
DROP PROCEDURE IF EXISTS DeletePhieuNhap;
DELIMITER //
CREATE PROCEDURE DeletePhieuNhap(
    IN p_MaPN CHAR(5)
)
BEGIN
    -- Khai báo biến
    DECLARE done INT DEFAULT FALSE;
    DECLARE M CHAR(5);
    DECLARE SL INT;
    DECLARE MaThuocExists INT;
    
    -- Khai báo handler cho cursor
    DECLARE details_cursor CURSOR FOR 
        SELECT MaThuoc, SoLuong 
        FROM ChiTietPhieuNhap 
        WHERE MaPN = p_MaPN;
        
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
	SELECT COUNT(*) INTO MaThuocExists 
		FROM ChiTietPhieuNhap 
		WHERE MaPN = p_MaPN;
        
	IF MaThuocExists = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Không tồn tại chi tiết phiếu nhập với mã này';
    END IF;
    
    START TRANSACTION; -- Bắt đầu một transaction
    OPEN details_cursor; -- Mở cursor

    read_loop: LOOP
        FETCH details_cursor INTO M, SL;
        IF done THEN
            LEAVE read_loop;
        END IF;

        -- Cập nhật số lượng tồn kho của thuốc
        CALL CapNhatSoLuong( M, 0, SL );
    END LOOP;

    CLOSE details_cursor; -- Đóng cursor
    DELETE FROM ChiTietPhieuNhap WHERE MaPN = p_MaPN; -- Xóa chi tiết phiếu nhập
    DELETE FROM PhieuNhap WHERE MaPN = p_MaPN; -- Xóa phiếu nhập

    COMMIT; -- Commit transaction
END //
DELIMITER ;

-- Xóa phiếu xuất, xóa xong số lượng tồn của mỗi thuốc thuộc phiếu xuất sẽ được cập nhật lại trở về lúc chưa xuất
DROP PROCEDURE IF EXISTS DeletePhieuXuat;
DELIMITER //
CREATE PROCEDURE DeletePhieuXuat(
    IN p_MaPX CHAR(5)
)
BEGIN
    -- Khai báo biến
    DECLARE done INT DEFAULT FALSE;
    DECLARE M CHAR(5);
    DECLARE SL INT;
    DECLARE MaThuocExists INT;
    
    -- Khai báo handler cho cursor
    DECLARE details_cursor CURSOR FOR 
        SELECT MaThuoc, SoLuong 
        FROM ChiTietPhieuXuat 
        WHERE MaPX = p_MaPX;
        
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
	SELECT COUNT(*) INTO MaThuocExists 
		FROM ChiTietPhieuXuat 
		WHERE MaPX = p_MaPX;
        
	IF MaThuocExists = 0 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Không tồn tại chi tiết phiếu xuất với mã này';
    END IF;
   
    START TRANSACTION; -- Bắt đầu một transaction
    OPEN details_cursor; -- Mở cursor

    read_loop: LOOP
        FETCH details_cursor INTO M, SL;
        IF done THEN
            LEAVE read_loop;
        END IF;

        CALL CapNhatSoLuong( M, SL, 0 ); -- Cập nhật số lượng tồn kho của thuốc
    END LOOP;

    CLOSE details_cursor; -- Đóng cursor
    DELETE FROM ChiTietPhieuXuat WHERE MaPX = p_MaPX; -- Xóa chi tiết phiếu xuất
    DELETE FROM PhieuXuat WHERE MaPX = p_MaPX; -- Xóa phiếu xuất

    COMMIT; -- Commit transaction
END //
DELIMITER ;

INSERT INTO hangsx (TenHang, QuocGia) VALUES
('Pfizer', 'USA'),
('Johnson & Johnson', 'USA'),
('Novartis', 'Switzerland'),
('Roche', 'Switzerland'),
('Merck & Co.', 'USA'),
('Sanofi', 'France'),
('AstraZeneca', 'UK'),
('GSK (GlaxoSmithKline)', 'UK'),
('Bayer', 'Germany'),
('Bristol-Myers Squibb', 'USA'),
('Takeda', 'Japan'),
('Boehringer Ingelheim', 'Germany'),
('Amgen', 'USA'),
('Teva Pharmaceutical', 'Israel'),
('Novo Nordisk', 'Denmark'),
('Sun Pharmaceutical', 'India'),
('Otsuka Pharmaceutical', 'Japan'),
('Chugai Pharmaceutical', 'Japan');

INSERT INTO NhaCungCap(TenNhaCungCap, DiaChi, ThongTinDaiDien) VALUES
('Pfizer Inc.', '235 E 42nd St, New York, NY 10017, USA', 'Dr. Albert Bourla'),
('GlaxoSmithKline (GSK)', '980 Great West Road, Brentford, Middlesex, TW8 9GS, UK', 'Emma Walmsley'),
('Johnson & Johnson', 'One Johnson & Johnson Plaza, New Brunswick, NJ 08933, USA', 'Alex Gorsky'),
('Novartis AG', 'Lichtstrasse 35, Basel, 4056, Switzerland', 'Vasant Narasimhan'),
('Merck & Co., Inc.', '2000 Galloping Hill Road, Kenilworth, NJ 07033, USA', 'Robert Davis'),
('Sanofi', '54 Rue La Boétie, 75008 Paris, France', 'Paul Hudson'),
('Roche Holding AG', 'Grenzacherstrasse 124, Basel, 4070, Switzerland', 'Severin Schwan'),
('AstraZeneca', '1 Francis Crick Avenue, Cambridge, CB2 0AA, UK', 'Pascal Soriot'),
('Bayer AG', 'Kaiser-Wilhelm-Allee 1, 51368 Leverkusen, Germany', 'Werner Baumann'),
('Eli Lilly and Company', '893 S Delaware St, Indianapolis, IN 46285, USA', 'David A. Ricks'),
('Boehringer Ingelheim', 'Binger Strasse 173, 55216 Ingelheim am Rhein, Germany', 'Hubertus von Baumbach'),
('AbbVie Inc.', '1 N Waukegan Rd, North Chicago, IL 60064, USA', 'Richard Gonzalez'),
('Teva Pharmaceutical Industries Ltd.', '124 Dvora HaNevi''a St, Petah Tikva 4951476, Israel', 'Kare Schultz'),
('Amgen Inc.', 'One Amgen Center Drive, Thousand Oaks, CA 91320-1799, USA', 'Robert A. Bradway'),
('Takeda Pharmaceutical Company', '1-1, Nihonbashi-Honcho 2-Chome, Chuo-ku, Tokyo 103-8668, Japan', 'Christophe Weber'),
('Bristol-Myers Squibb', '430 E 29th St, New York, NY 10016, USA', 'Giovanni Caforio');

INSERT INTO User(TenUser, SoDienThoai, DiaChi, Pwd, NgaySinh, NhanVien) VALUES
('Cao Hoàng Khải', '0947890123', '471 Đường IJK, Quận Bình Thạnh, TP.HCM', '0947890123','2003-07-23',true),
('Nguyễn Lê Hoàng', '0903456789', '67 Đường GHI, Quận 3, TP.HCM', '0903456789','2003-05-06', true),
('Huỳnh Chí Hiếu', '0911234567', '111 Đường EFG, Quận 11, TP.HCM', '0911234567','2003-02-28', true),
('Bùi Thị Quyền Trân', '0923456789', '231 Đường OPQ, Quận 6, TP.HCM', '0923456789','2003-11-29',true),
('Võ Công Anh Tú', '0123456789', 'Bình Minh, Vĩnh Long', '0123456789','2002-10-10',true);

INSERT INTO User(TenUser, SoDienThoai, DiaChi, Pwd, NhanVien) VALUES
('Nguyễn Văn Hải', '0901234567', '123 Đường ABC, Quận 1, TP.HCM', '0901234567', false),
('Trần Thị Lan', '0902345678', '45 Đường DEF, Quận 2, TP.HCM', '0902345678', false),
('Phạm Thị Ngọc', '0904567890', '89 Đường JKL, Quận 4, TP.HCM', '0904567890', false),
('Bùi Văn Minh', '0905678901', '12 Đường MNO, Quận 5, TP.HCM', '0905678901', false),
('Đặng Thị Hạnh', '0906789012', '34 Đường PQR, Quận 6, TP.HCM', '0906789012', false),
('Vũ Văn Dũng', '0907890123', '56 Đường STU, Quận 7, TP.HCM', '0907890123', false),
('Trần Thị Phương', '0908901234', '78 Đường VWX, Quận 8, TP.HCM', '0908901234', false),
('Hoàng Văn Quân', '0909012345', '90 Đường YZA, Quận 9, TP.HCM', '0909012345', false),
('Phan Thị Thu', '0910123456', '101 Đường BCD, Quận 10, TP.HCM', '0910123456', false),
('Lê Minh Tâm', '0912345678', '121 Đường HIJ, Quận 12, TP.HCM', '0912345678', false),
('Đỗ Thị Hương', '0913456789', '131 Đường KLM, Quận Tân Bình, TP.HCM', '0913456789', false),
('Phạm Văn Sơn', '0914567890', '141 Đường NOP, Quận Tân Phú, TP.HCM', '0914567890', false),
('Võ Thị Mai', '0915678901', '151 Đường QRS, Quận Bình Tân, TP.HCM', '0915678901', false),
('Trương Văn Toàn', '0916789012', '161 Đường TUV, Quận Bình Thạnh, TP.HCM', '0916789012', false),
('Dương Thị Hoa', '0917890123', '171 Đường WXY, Quận Phú Nhuận, TP.HCM', '0917890123', false),
('Đào Văn Thắng', '0918901234', '181 Đường ZAB, Quận Thủ Đức, TP.HCM', '0918901234', false),
('Lê Thị Bích', '0919012345', '191 Đường CDE, Quận Gò Vấp, TP.HCM', '0919012345', false),
('Nguyễn Thị Cúc', '0920123456', '201 Đường FGH, Quận 9, TP.HCM', '0920123456', false),
('Phan Văn Long', '0921234567', '211 Đường IJK, Quận 2, TP.HCM', '0921234567', false),
('Trần Thị Hằng', '0922345678', '221 Đường LMN, Quận 7, TP.HCM', '0922345678', false),
('Đỗ Văn Hiếu', '0924567890', '241 Đường RST, Quận 4, TP.HCM', '0924567890', false),
('Lý Thị Thu Hà', '0925678901', '251 Đường UVW, Quận 3, TP.HCM', '0925678901', false),
('Võ Văn Phúc', '0926789012', '261 Đường XYZ, Quận 5, TP.HCM', '0926789012', false),
('Đặng Thị Hiền', '0927890123', '271 Đường ABC, Quận 1, TP.HCM', '0927890123', false),
('Nguyễn Văn Cường', '0928901234', '281 Đường DEF, Quận 10, TP.HCM', '0928901234', false);

INSERT INTO LoaiThuoc(TenLoai, GhiChu) VALUES
('Thuốc kháng sinh', 'Sản phẩm của Pfizer, điều trị nhiễm khuẩn'),
('Thuốc giảm đau', 'Sản phẩm của Johnson & Johnson, giảm đau nhanh'),
('Thuốc kháng viêm', 'Sản phẩm của Novartis, dùng điều trị viêm khớp'),
('Thuốc kháng virus', 'Sản phẩm của GSK, hỗ trợ điều trị cúm và các bệnh virus khác'),
('Thuốc tiểu đường', 'Sản phẩm của Sanofi, giúp kiểm soát đường huyết'),
('Thuốc tim mạch', 'Sản phẩm của Bayer, hỗ trợ điều trị bệnh tim mạch'),
('Thuốc chống trầm cảm', 'Sản phẩm của Eli Lilly, hỗ trợ điều trị trầm cảm'),
('Thuốc điều trị ung thư', 'Sản phẩm của Roche, điều trị ung thư'),
('Thuốc chống loãng xương', 'Sản phẩm của Amgen, giúp tăng cường mật độ xương'),
('Thuốc chống đông máu', 'Sản phẩm của Boehringer Ingelheim, phòng ngừa đột quỵ'),
('Thuốc điều trị HIV', 'Sản phẩm của Gilead Sciences, ngăn chặn virus HIV phát triển'),
('Thuốc kháng histamine', 'Sản phẩm của Teva, điều trị dị ứng'),
('Thuốc trị cao huyết áp', 'Sản phẩm của AstraZeneca, giảm huyết áp hiệu quả'),
('Thuốc điều trị hen suyễn', 'Sản phẩm của GlaxoSmithKline, kiểm soát hen suyễn'),
('Thuốc chống co giật', 'Sản phẩm của Bristol-Myers Squibb, dùng điều trị động kinh'),
('Thuốc điều trị Parkinson', 'Sản phẩm của AbbVie, hỗ trợ điều trị Parkinson'),
('Thuốc an thần', 'Sản phẩm của Takeda, dùng để điều trị rối loạn giấc ngủ'),
('Thuốc ức chế miễn dịch', 'Sản phẩm của Novartis, hỗ trợ sau phẫu thuật cấy ghép tạng');

INSERT INTO Thuoc (TenThuoc, MaHangSX, MaNhaCungCap, CongDung, MaLoai) VALUES 
('Thuốc kháng sinh XYZ', 'B0009', 'F0001', 'Điều trị nhiễm trùng', 'T0001'),
('Thuốc kháng sinh GHI', 'B0004', 'F0001', 'Trị nhiễm khuẩn', 'T0001'),
('Thuốc kháng sinh XYZ', 'B0017', 'F0001', 'Điều trị nhiễm khuẩn', 'T0001'),
('Thuốc giảm đau XYZ', 'B0002', 'F0002', 'Giảm đau tức thời', 'T0002'),
('Thuốc chống đau nửa đầu ABC', 'B0002', 'F0002', 'Giảm đau nửa đầu', 'T0002'),
('Thuốc giảm đau GHI', 'B0001', 'F0002', 'Giảm đau nhanh', 'T0002'),
('Thuốc giảm sốt ABC', 'B0002', 'F0002', 'Hạ nhiệt cơ thể', 'T0002'),
('Thuốc chống viêm ABC', 'B0003', 'F0003', 'Chống viêm khớp', 'T0003'),
('Thuốc kháng viêm GHI', 'B0004', 'F0003', 'Điều trị viêm da', 'T0003'),
('Thuốc chống viêm XYZ', 'B0005', 'F0003', 'Giảm viêm nhanh chóng', 'T0003'),
('Thuốc kháng virus PQR', 'B0006', 'F0004', 'Điều trị các bệnh về virus', 'T0004'),
('Thuốc điều trị tiểu đường DEF', 'B0005', 'F0005', 'Kiểm soát đường huyết', 'T0005'),
('Thuốc điều trị tiểu đường ABC', 'B0005', 'F0005', 'Kiểm soát đường huyết', 'T0005'),
('Thuốc điều trị tim mạch DEF', 'B0006', 'F0006', 'Bảo vệ sức khỏe tim', 'T0006'),
('Thuốc chống trầm cảm MNO', 'B0010', 'F0007', 'Giảm căng thẳng, trầm cảm', 'T0007'),
('Thuốc chống trầm cảm DEF', 'B0015', 'F0007', 'Giảm lo âu, trầm cảm', 'T0007'),
('Thuốc điều trị ung thư XYZ', 'B0011', 'F0008', 'Điều trị ung thư', 'T0008'),
('Thuốc điều trị ung thư GHI', 'B0008', 'F0008', 'Trị ung thư phổi', 'T0008'),
('Thuốc chống ung thư ABC', 'B0016', 'F0008', 'Điều trị ung thư gan', 'T0008'),
('Thuốc điều trị ung thư GHI', 'B0018', 'F0008', 'Trị ung thư ruột', 'T0008'),
('Thuốc điều trị loãng xương ABC', 'B0018', 'F0009', 'Tăng mật độ xương', 'T0009'),
('Thuốc chống loãng xương ABC', 'B0010', 'F0009', 'Bảo vệ sức khỏe xương', 'T0009'),
('Thuốc điều trị loãng xương GHI', 'B0017', 'F0009', 'Tăng cường mật độ xương', 'T0009'),
('Thuốc điều trị loãng xương DEF', 'B0007', 'F0009', 'Tăng cường xương khớp', 'T0009'),
('Thuốc chống đông máu XYZ', 'B0013', 'F0010', 'Ngăn ngừa đông máu', 'T0010'),
('Thuốc chống đông máu STU', 'B0007', 'F0010', 'Phòng ngừa đông máu', 'T0010'),
('Thuốc điều trị HIV DEF', 'B0012', 'F0011', 'Điều trị HIV', 'T0011'),
('Thuốc điều trị HIV GHI', 'B0012', 'F0011', 'Điều trị HIV hiệu quả', 'T0011'),
('Thuốc chống dị ứng XYZ', 'B0018', 'F0012', 'Giảm ngứa, dị ứng', 'T0012'),
('Thuốc điều trị dị ứng ABC', 'B0014', 'F0012', 'Giảm phản ứng dị ứng', 'T0012'),
('Thuốc chống dị ứng ABC', 'B0006', 'F0012', 'Điều trị dị ứng da', 'T0012'),
('Thuốc kháng histamine GHI', 'B0014', 'F0012', 'Điều trị dị ứng', 'T0012'),
('Thuốc điều trị cao huyết áp XYZ', 'B0002', 'F0013', 'Kiểm soát huyết áp', 'T0013'),
('Thuốc điều trị cao huyết áp DEF', 'B0003', 'F0013', 'Ổn định huyết áp', 'T0013'),
('Thuốc điều trị huyết áp DEF', 'B0017', 'F0013', 'Giảm huyết áp', 'T0013'),
('Thuốc điều trị cao huyết áp JKL', 'B0008', 'F0013', 'Điều trị huyết áp cao', 'T0013'),
('Thuốc điều trị hen suyễn XYZ', 'B0017', 'F0014', 'Kiểm soát cơn hen', 'T0014'),
('Thuốc trị co giật XYZ', 'B0015', 'F0015', 'Kiểm soát co giật', 'T0015'),
('Thuốc điều trị Parkinson GHI', 'B0017', 'F0016', 'Giảm triệu chứng Parkinson', 'T0016'),
('Thuốc an thần BTQT', 'B0015', 'F0001', 'Giúp an thần, dễ ngủ', 'T0017'),
('Thuốc ức chế miễn dịch DEF', 'B0018', 'F0016', 'Sau phẫu thuật cấy ghép', 'T0018');

-- Thêm Phiếu Nhập
CALL InsertPhieuNhapWithDetails('U0001', 'F0001', '2024-11-02', 
'[{"MaThuoc": "M0001", "SoLuong": 300, "DonGia": 35000},
  {"MaThuoc": "M0002", "SoLuong": 300, "DonGia": 40000},
  {"MaThuoc": "M0003", "SoLuong": 300, "DonGia": 45000}]'
);
CALL InsertPhieuNhapWithDetails('U0002', 'F0002', '2024-11-06', 
'[{"MaThuoc": "M0004", "SoLuong": 300, "DonGia": 50000},
  {"MaThuoc": "M0005", "SoLuong": 300, "DonGia": 55000},
  {"MaThuoc": "M0006", "SoLuong": 300, "DonGia": 60000},
  {"MaThuoc": "M0007", "SoLuong": 300, "DonGia": 45000}]'
);
CALL InsertPhieuNhapWithDetails('U0003', 'F0003', '2024-11-06', 
'[{"MaThuoc": "M0008", "SoLuong": 300, "DonGia": 35000},
  {"MaThuoc": "M0009", "SoLuong": 300, "DonGia": 45000},
  {"MaThuoc": "M0010", "SoLuong": 300, "DonGia": 45000},
  {"MaThuoc": "M0011", "SoLuong": 300, "DonGia": 55000}]'
);
CALL InsertPhieuNhapWithDetails('U0005', 'F0004', '2024-11-07', 
'[{"MaThuoc": "M0012", "SoLuong": 300, "DonGia": 35000}]'
);
CALL InsertPhieuNhapWithDetails('U0001', 'F0005', '2024-11-07', 
'[{"MaThuoc": "M0014", "SoLuong": 300, "DonGia": 35000},
  {"MaThuoc": "M0013", "SoLuong": 300, "DonGia": 40000}]'
);
CALL InsertPhieuNhapWithDetails('U0002', 'F0006', '2024-11-08', 
'[{"MaThuoc": "M0015", "SoLuong": 300, "DonGia": 35000},
  {"MaThuoc": "M0016", "SoLuong": 300, "DonGia": 40000}]'
);
CALL InsertPhieuNhapWithDetails('U0003', 'F0007', '2024-11-08', 
'[{"MaThuoc": "M0017", "SoLuong": 300, "DonGia": 55000},
  {"MaThuoc": "M0018", "SoLuong": 300, "DonGia": 40000}]'
);
CALL InsertPhieuNhapWithDetails('U0004', 'F0008', '2024-11-09', 
'[{"MaThuoc": "M0019", "SoLuong": 300, "DonGia": 55000},
  {"MaThuoc": "M0020", "SoLuong": 300, "DonGia": 40000},
  {"MaThuoc": "M0021", "SoLuong": 300, "DonGia": 55000},
  {"MaThuoc": "M0022", "SoLuong": 300, "DonGia": 45000}]'
);
CALL InsertPhieuNhapWithDetails('U0005', 'F0009', '2024-11-09', 
'[{"MaThuoc": "M0023", "SoLuong": 300, "DonGia": 55000},
  {"MaThuoc": "M0024", "SoLuong": 300, "DonGia": 40000},
  {"MaThuoc": "M0025", "SoLuong": 300, "DonGia": 55000},
  {"MaThuoc": "M0026", "SoLuong": 300, "DonGia": 45000}]'
);
CALL InsertPhieuNhapWithDetails('U0004', 'F0010', '2024-11-09', 
'[{"MaThuoc": "M0027", "SoLuong": 300, "DonGia": 55000},
  {"MaThuoc": "M0028", "SoLuong": 300, "DonGia": 40000}]'
);
CALL InsertPhieuNhapWithDetails('U0003', 'F0011', '2024-11-10', 
'[{"MaThuoc": "M0029", "SoLuong": 300, "DonGia": 55000},
  {"MaThuoc": "M0030", "SoLuong": 300, "DonGia": 45000}]'
);
CALL InsertPhieuNhapWithDetails('U0005', 'F0012', '2024-11-10', 
'[{"MaThuoc": "M0031", "SoLuong": 300, "DonGia": 55000},
  {"MaThuoc": "M0032", "SoLuong": 300, "DonGia": 35000},
  {"MaThuoc": "M0033", "SoLuong": 300, "DonGia": 55000},
  {"MaThuoc": "M0034", "SoLuong": 300, "DonGia": 45000}]'
);
CALL InsertPhieuNhapWithDetails('U0002', 'F0013', '2024-11-11', 
'[{"MaThuoc": "M0035", "SoLuong": 300, "DonGia": 50000},
  {"MaThuoc": "M0036", "SoLuong": 300, "DonGia": 35000},
  {"MaThuoc": "M0037", "SoLuong": 300, "DonGia": 55000},
  {"MaThuoc": "M0038", "SoLuong": 300, "DonGia": 25000}]'
);
CALL InsertPhieuNhapWithDetails('U0001', 'F0014', '2024-11-12', 
'[{"MaThuoc": "M0039", "SoLuong": 300, "DonGia": 50000}]'
);
CALL InsertPhieuNhapWithDetails('U0002', 'F0015', '2024-11-12', 
'[{"MaThuoc": "M0040", "SoLuong": 300, "DonGia": 50000}]'
);
CALL InsertPhieuNhapWithDetails('U0003', 'F0016', '2024-11-12', 
'[{"MaThuoc": "M0041", "SoLuong": 300, "DonGia": 50000}]'
);
CALL InsertPhieuNhapWithDetails('U0004', 'F0001', '2024-11-12', 
'[{"MaThuoc": "M0001", "SoLuong": 300, "DonGia": 50000}]'
);
CALL InsertPhieuNhapWithDetails('U0005', 'F0003', '2024-11-12', 
'[{"MaThuoc": "M0003", "SoLuong": 300, "DonGia": 50000}]'
);

-- Thêm Phiếu Xuất
CALL InsertPhieuXuatWithDetails('U0001', 'U0006', '2024-11-13', 
'[{"MaThuoc": "M0001", "SoLuong": 10, "DonGia": 45000},
  {"MaThuoc": "M0002", "SoLuong": 5, "DonGia": 50000},
  {"MaThuoc": "M0003", "SoLuong": 3, "DonGia": 55000}]'
);
 CALL InsertPhieuXuatWithDetails('U0002', 'U0007', '2024-11-13', 
'[{"MaThuoc": "M0004", "SoLuong": 10, "DonGia": 60000},
  {"MaThuoc": "M0005", "SoLuong": 5, "DonGia": 65000},
  {"MaThuoc": "M0006", "SoLuong": 3, "DonGia": 70000},
  {"MaThuoc": "M0007", "SoLuong": 3, "DonGia": 55000}]'
);
CALL InsertPhieuXuatWithDetails('U0001', 'U0008', '2024-11-13', 
'[{"MaThuoc": "M0008", "SoLuong": 10, "DonGia": 65000},
  {"MaThuoc": "M0009", "SoLuong": 5, "DonGia": 50000} ]'
);
CALL InsertPhieuXuatWithDetails('U0002', 'U0009', '2024-11-13', 
'[{"MaThuoc": "M0010", "SoLuong": 10, "DonGia": 65000},
  {"MaThuoc": "M0011", "SoLuong": 5, "DonGia": 55000}]'
);
CALL InsertPhieuXuatWithDetails('U0001', 'U0010', '2024-11-13', 
'[{"MaThuoc": "M0035", "SoLuong": 10, "DonGia": 60000},
  {"MaThuoc": "M0036", "SoLuong": 15, "DonGia": 45000},
  {"MaThuoc": "M0037", "SoLuong": 10, "DonGia": 65000},
  {"MaThuoc": "M0038", "SoLuong": 7, "DonGia": 35000}]'
);
CALL InsertPhieuXuatWithDetails('U0002', 'U0011', '2024-11-13', 
'[{"MaThuoc": "M0023", "SoLuong": 18, "DonGia": 65000},
  {"MaThuoc": "M0024", "SoLuong": 15, "DonGia": 50000},
  {"MaThuoc": "M0025", "SoLuong": 12, "DonGia": 65000},
  {"MaThuoc": "M0036", "SoLuong": 7, "DonGia": 55000}]'
);
CALL InsertPhieuXuatWithDetails('U0001', 'U0012', '2024-11-13', 
'[{"MaThuoc": "M0019", "SoLuong": 18, "DonGia": 65000},
  {"MaThuoc": "M0020", "SoLuong": 15, "DonGia": 50000},
  {"MaThuoc": "M0022", "SoLuong": 7, "DonGia": 55000}]'
);

CALL InsertPhieuXuatWithDetails('U0003', 'U0013', '2024-11-14', 
'[{"MaThuoc": "M0001", "SoLuong": 10, "DonGia": 45000},
  {"MaThuoc": "M0002", "SoLuong": 5, "DonGia": 50000},
  {"MaThuoc": "M0003", "SoLuong": 3, "DonGia": 55000}]'
);
CALL InsertPhieuXuatWithDetails('U0004', 'U0014', '2024-11-14', 
'[{"MaThuoc": "M0027", "SoLuong": 10, "DonGia": 65000},
  {"MaThuoc": "M0028", "SoLuong": 5, "DonGia": 50000},
  {"MaThuoc": "M0029", "SoLuong": 3, "DonGia": 65000},
  {"MaThuoc": "M0030", "SoLuong": 3, "DonGia": 50000}]'
);
CALL InsertPhieuXuatWithDetails('U0003', 'U0015', '2024-11-14', 
'[{"MaThuoc": "M0008", "SoLuong": 10, "DonGia": 65000},
  {"MaThuoc": "M0009", "SoLuong": 5, "DonGia": 50000}]'
);
CALL InsertPhieuXuatWithDetails('U0004', 'U0009', '2024-11-14', 
'[{"MaThuoc": "M0010", "SoLuong": 10, "DonGia": 65000},
  {"MaThuoc": "M0011", "SoLuong": 5, "DonGia": 55000}]'
);
CALL InsertPhieuXuatWithDetails('U0003', 'U0016', '2024-11-14', 
'[{"MaThuoc": "M0035", "SoLuong": 10, "DonGia": 60000},
  {"MaThuoc": "M0036", "SoLuong": 15, "DonGia": 45000},
  {"MaThuoc": "M0037", "SoLuong": 10, "DonGia": 65000},
  {"MaThuoc": "M0038", "SoLuong": 7, "DonGia": 35000}]'
);
CALL InsertPhieuXuatWithDetails('U0004', 'U0017', '2024-11-14', 
'[{"MaThuoc": "M0023", "SoLuong": 18, "DonGia": 65000},
  {"MaThuoc": "M0024", "SoLuong": 15, "DonGia": 50000},
  {"MaThuoc": "M0025", "SoLuong": 12, "DonGia": 65000},
  {"MaThuoc": "M0036", "SoLuong": 7, "DonGia": 55000}]'
);
CALL InsertPhieuXuatWithDetails('U0005', 'U0018', '2024-11-14', 
'[{"MaThuoc": "M0019", "SoLuong": 18, "DonGia": 65000},
  {"MaThuoc": "M0020", "SoLuong": 15, "DonGia": 50000},
  {"MaThuoc": "M0022", "SoLuong": 7, "DonGia": 55000}]'
);
