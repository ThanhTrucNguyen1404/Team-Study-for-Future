using BanXeMayTTCS.DTO;
using BanXeMayTTCS.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BanXeMayTTCS.DAO
{
    public class XeMayDAO
    {
        private static XeMayDAO instance;
        public static XeMayDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new XeMayDAO();
                return instance;
            }
        }
        public List<XeMay> GetListXeMay()
        {
            string query = "EXEC SP_GET_XEMAY";
            string maXeMay;
            string tenXeMay;
            decimal gia;
            int soLuongTon;
            string moTa;
            string trangThai;
            string hinhAnh;
            string loai;
            string hang;
            string nhaCungCap;
            try
            {
                List<XeMay> list = new List<XeMay>();
                DataTable dt = DataProvider.Instance.ExecuteQuerry(query);
                foreach(DataRow dr in dt.Rows)
                {
                    maXeMay = (string)dr["MaXeMay"];
                    tenXeMay = (string)dr["TenXeMay"];
                    gia = (decimal)dr["Gia"];
                    soLuongTon = (int)dr["SoLuongTon"];
                    moTa = (string)dr["MoTa"];
                    trangThai = (string)dr["TrangThai"];
                    hinhAnh = (string)dr["HinhAnh"];
                    loai = (string)dr["Loai"];
                    hang = (string)dr["Hang"];
                    nhaCungCap = (string)dr["NhaCungCap"];
                    list.Add(new XeMay(maXeMay, tenXeMay, gia, soLuongTon, moTa, trangThai, hinhAnh, loai, hang, nhaCungCap));
                }
                return list;
            }
            catch(Exception e)
            {
                MessageBox.Show("ERROR: Có lỗi khi lấy danh sách xe máy. " + e.Message);
                return null;
            }
        }
        public List<XeMayKH> GetListXeMay_KH()
        {
            string query = "EXEC SP_GET_XEMAY_KH";
            string maXeMay;
            string tenXeMay;
            decimal gia;
            int soLuongTon;
            string moTa;
            string trangThai;
            string hinhAnh;
            string loai;
            string hang;
            string nhaCungCap;
            try
            {
                List<XeMayKH> list = new List<XeMayKH>();
                DataTable dt = DataProvider.Instance.ExecuteQuerry(query);
                foreach (DataRow dr in dt.Rows)
                {
                    maXeMay = (string)dr["MaXeMay"];
                    tenXeMay = (string)dr["TenXeMay"];
                    gia = (decimal)dr["Gia"];
                    soLuongTon = (int)dr["SoLuongTon"];
                    moTa = (string)dr["MoTa"];
                    trangThai = (string)dr["TrangThai"];
                    hinhAnh = (string)dr["HinhAnh"];
                    loai = (string)dr["Loai"];
                    hang = (string)dr["Hang"];
                    nhaCungCap = (string)dr["NhaCungCap"];
                    list.Add(new XeMayKH(maXeMay, tenXeMay, gia, soLuongTon, moTa, trangThai, hinhAnh, loai, hang, nhaCungCap));
                }
                return list;
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR: Có lỗi khi lấy danh sách xe máy. " + e.Message);
                return null;
            }
        }
        public DataTable GetListXeMayNCC(string maDonDatHang)
        {
            string query = "SELECT * FROM FN_GET_XEMAY_NCC( @MADONDATHANG )";
            try
            {
                DataTable dt = DataProvider.Instance.ExecuteQuerry(query, new object[] {maDonDatHang });
                return dt;
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR: Có lỗi khi lấy danh sách xe máy. " + e.Message);
                return null;
            }
        }
        public DataTable GetListXeMayTrongDonDat(string maDonDatHang)
        {
            string query = "SELECT * FROM FN_GET_XEMAY_TRONG_DONDAT( @MADONDATHANG )";
            try
            {
                DataTable dt = DataProvider.Instance.ExecuteQuerry(query, new object[] { maDonDatHang });
                return dt;
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR: Có lỗi khi lấy danh sách xe máy. " + e.Message);
                return null;
            }
        }
        public bool ThemXeMay(XeMay dongHo)
        {
            string query = "EXEC SP_THEM_XEMAY @MAXEMAY , @TENXEMAY , @GIA , @SOLUONGTON , @MOTA , @HINHANH , @MATRANGTHAI , @MALOAI , @MAHANG , @MANHACUNGCAP";
            try
            {
                DataProvider.Instance.ExecuteNonQuerry(query, new object[]
                {
                    dongHo.MaXeMay,
                    dongHo.TenXeMay,
                    dongHo.Gia,
                    dongHo.SoLuongTon,
                    dongHo.MoTa,
                    dongHo.HinhAnh,
                    dongHo.TrangThai,
                    dongHo.Loai,
                    dongHo.Hang,
                    dongHo.NhaCungCap
                });
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR: Có lỗi khi thêm xe máy. " + e.Message);
                return false;
            }
        }
        public bool SuaXeMay(XeMay dongHo)
        {
            string query = "EXEC SP_CAPNHAT_XEMAY @MAXEMAY , @TENXEMAY , @GIA , @MOTA , @HINHANH , @MALOAI , @MAHANG";
            try
            {
                DataProvider.Instance.ExecuteNonQuerry(query, new object[]
                {
                    dongHo.MaXeMay,
                    dongHo.TenXeMay,
                    dongHo.Gia,
                    dongHo.MoTa,
                    dongHo.HinhAnh,
                    dongHo.Loai,
                    dongHo.Hang,
                });
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR: Có lỗi khi sửa thông tin xe máy. " + e.Message);
                return false;
            }
        }
        public bool XoaXeMay(string maXeMay)
        {
            string query = "EXEC SP_XOA_XEMAY @MAXEMAY";
            try
            {
                DataProvider.Instance.ExecuteNonQuerry(query, new object[] { maXeMay });
                return true;
            }
            catch(Exception e)
            {
                MessageBox.Show("ERROR: Có lỗi khi xóa xe máy. " + e.Message);
                return false;
            }
        }
        public List<XeMayKH> Search_XeMay(string loaiDh, string nhaCungCapDh, string hangDh)
        {
            string query = "EXEC SP_TIM_KIEM_DH @MaLoai , @MaNhaCungCap , @MaHang";
            string maXeMay;
            string tenXeMay;
            decimal gia;
            int soLuongTon;
            string moTa;
            string trangThai;
            string hinhAnh;
            string loai;
            string hang;
            string nhaCungCap;
            try
            {
                List<XeMayKH> list = new List<XeMayKH>();
                DataTable dt = DataProvider.Instance.ExecuteQuerry(query, new object[] { loaiDh, nhaCungCapDh, hangDh });
                foreach (DataRow dr in dt.Rows)
                {
                    maXeMay = (string)dr["MaXeMay"];
                    tenXeMay = (string)dr["TenXeMay"];
                    gia = (decimal)dr["Gia"];
                    soLuongTon = (int)dr["SoLuongTon"];
                    moTa = (string)dr["MoTa"];
                    trangThai = (string)dr["TrangThai"];
                    hinhAnh = (string)dr["HinhAnh"];
                    loai = (string)dr["Loai"];
                    hang = (string)dr["Hang"];
                    nhaCungCap = (string)dr["NhaCungCap"];
                    list.Add(new XeMayKH(maXeMay, tenXeMay, gia, soLuongTon, moTa, trangThai, hinhAnh, loai, hang, nhaCungCap));
                }
                return list;
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR: Có lỗi khi lấy danh sách xe máy. " + e.Message);
                return null;
            }
        }
        public DataTable GetListTrangThai()
        {
            string query = "SELECT * FROM TrangThai";
            try
            {
                DataTable dt = DataProvider.Instance.ExecuteQuerry(query);
                return dt;
            }
            catch
            {
                return null;
            }
        }
        public DataTable GetListLoai()
        {
            string query = "SELECT * FROM LoaiXeMay";
            try
            {
                DataTable dt = DataProvider.Instance.ExecuteQuerry(query);
                return dt;
            }
            catch
            {
                return null;
            }
        }
        public DataTable GetListHang()
        {
            string query = "SELECT * FROM HangXeMay";
            try
            {
                DataTable dt = DataProvider.Instance.ExecuteQuerry(query);
                return dt;
            }
            catch
            {
                return null;
            }
        }
        public DataTable GetLisNhaCungCap()
        {
            string query = "SELECT * FROM NhaCungCap";
            try
            {
                DataTable dt = DataProvider.Instance.ExecuteQuerry(query);
                return dt;
            }
            catch
            {
                return null;
            }
        }

        internal bool NgungKinhDoanhSP(string maXeMay)
        {
            string query = "EXEC SP_NGUNGKINHDOANH_XEMAY @MAXEMAY";
            try
            {
                DataProvider.Instance.ExecuteNonQuerry(query, new object[] { maXeMay });
                return true;
            }
            catch(Exception e)
            {
                MessageBox.Show("ERROR: Có lỗi khi ngừng kinh doanh sản phẩm này. " + e.Message);
                return false;
            }
        }
    }
}
