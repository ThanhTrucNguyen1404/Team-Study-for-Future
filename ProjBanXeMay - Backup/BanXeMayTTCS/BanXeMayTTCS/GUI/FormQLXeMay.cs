using BanXeMayTTCS.DAO;
using BanXeMayTTCS.DTO;
using BanXeMayTTCS.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BanXeMayTTCS.GUI
{
    public partial class FormQLXeMay : Form
    {
        private List<XeMay> listXeMay;
        private BindingSource bindingSource = new BindingSource();

        private const int CHUCNANG_THEM = 0;
        private const int CHUCNANG_SUA = 1;
        private int chucNangThucThi;

        private int position;
        private string hinhAnh;

        private const string TRANGTHAI_DEFAULT = "THH";
        public FormQLXeMay()
        {
            InitializeComponent();
            LoadUI();
            LoadData();
        }
        #region Giao diện
        private void LoadData()
        {
            listXeMay = XeMayDAO.Instance.GetListXeMay();
            bindingSource.DataSource = listXeMay;
            gridControlXeMay.DataSource = bindingSource;
            bindingSource.Position = 0;
        }
        private void LoadUI()
        {
            ResetTatCaComponent();
            // Combobox trạng thái
            comboBoxTrangThai.DataSource = XeMayDAO.Instance.GetListTrangThai();
            comboBoxTrangThai.DisplayMember = "TrangThai";
            comboBoxTrangThai.ValueMember = "MaTrangThai";
            // Combobox loại xe máy
            comboBoxLoai.DataSource = XeMayDAO.Instance.GetListLoai();
            comboBoxLoai.DisplayMember = "TenLoai";
            comboBoxLoai.ValueMember = "MaLoai";
            // Combobox hãng xe máy
            comboBoxHang.DataSource = XeMayDAO.Instance.GetListHang();
            comboBoxHang.DisplayMember = "TenHang";
            comboBoxHang.ValueMember = "MaHang";
            // Combobox nhà cung cấp
            comboBoxNhaCungCap.DataSource = XeMayDAO.Instance.GetLisNhaCungCap();
            comboBoxNhaCungCap.DisplayMember = "TenNhaCungCap";
            comboBoxNhaCungCap.ValueMember = "MaNhaCungCap";
        }
        private void FillInput()
        {
            position = bindingSource.Position;
            XeMay dongHo = listXeMay[position];
            txtMaXeMay.Text = dongHo.MaXeMay;
            txtTenXeMay.Text = dongHo.TenXeMay;
            txtGia.Text = Convert.ToString(dongHo.Gia);
            txtSoLuongTon.Text = Convert.ToString(dongHo.SoLuongTon);
            txtMoTa.Text = dongHo.MoTa;
            comboBoxTrangThai.Text = dongHo.TrangThai;
            comboBoxLoai.Text = dongHo.Loai;
            comboBoxHang.Text = dongHo.Hang;
            comboBoxNhaCungCap.Text = dongHo.NhaCungCap;
            this.hinhAnh = dongHo.HinhAnh;
            try
            {
                pictureBoxHinhAnh.Image = Image.FromFile(MyFormat.GetFilePath(dongHo.HinhAnh));
            }
            catch (Exception e)
            {
                pictureBoxHinhAnh.Image = pictureBoxHinhAnh.ErrorImage;
                MessageBox.Show(e.Message);
            }
            if(chucNangThucThi == -1)
            {
                btnNgungKinhDoanh.Enabled = btnSua.Enabled = dongHo.TrangThai.Equals("Ngừng kinh doanh") ? false : true;
            }
        }
        private void ResetTatCaComponent()
        {
            chucNangThucThi = -1;
            // Enable các input và chuyển về ReadOnly
            ThayDoiInputEnabled(true);
            ThayDoiInputReadOnly(true);
            // Enable Grid control xe máy
            gridControlXeMay.Enabled = true;
            // Enable tất cả nút trừ Ghi và Thoát chức năng
            ThayDoiButtonEnabled(true);
            btnGhi.Enabled = false;
            btnThoatChucNang.Enabled = false;
        }
        private void ResetGiaTriInput()
        {
            txtMaXeMay.ResetText();
            txtTenXeMay.ResetText();
            txtGia.ResetText();
            txtSoLuongTon.ResetText();
            txtMoTa.ResetText();
            comboBoxTrangThai.SelectedIndex = 0;
            comboBoxLoai.SelectedIndex = 0;
            comboBoxHang.SelectedIndex = 0;
            comboBoxNhaCungCap.SelectedIndex = 0;
            hinhAnh = "";
            pictureBoxHinhAnh.Image = pictureBoxHinhAnh.InitialImage;
        }
        private void ThayDoiInputEnabled(bool enabled)
        {
            txtMaXeMay.Enabled = enabled;
            txtTenXeMay.Enabled = enabled;
            txtGia.Enabled = enabled;
            txtSoLuongTon.Enabled = enabled;
            txtMoTa.Enabled = enabled;
            comboBoxTrangThai.Enabled = enabled;
            comboBoxLoai.Enabled = enabled;
            comboBoxHang.Enabled = enabled;
            comboBoxNhaCungCap.Enabled = enabled;
        }
        private void ThayDoiInputReadOnly(bool readOnly)
        {
            txtMaXeMay.ReadOnly = readOnly;
            txtTenXeMay.ReadOnly = readOnly;
            txtGia.ReadOnly = readOnly;
            txtSoLuongTon.ReadOnly = readOnly;
            txtMoTa.ReadOnly = readOnly;
            comboBoxTrangThai.Enabled = !readOnly;
            comboBoxLoai.Enabled = !readOnly;
            comboBoxHang.Enabled = !readOnly;
            comboBoxNhaCungCap.Enabled = !readOnly;
        }
        private void ThayDoiButtonEnabled(bool enabled)
        {
            btnThem.Enabled = enabled;
            btnSua.Enabled = enabled;
            btnGhi.Enabled = enabled;
            btnXoa.Enabled = enabled;
            btnNgungKinhDoanh.Enabled = enabled;
            btnTaiLai.Enabled = enabled;
            btnThoatChucNang.Enabled = enabled;
        }
        #endregion Giao diện
        #region Chức năng
        private bool InputHopLe()
        {
            if (txtMaXeMay.Text.Equals(""))
            {
                MessageBox.Show("Mã xe máy không được trống");
                return false;
            }
            if (txtTenXeMay.Text.Equals(""))
            {
                MessageBox.Show("Tên xe máy không được trống");
                return false;
            }
            if (txtGia.Text.Equals(""))
            {
                MessageBox.Show("Giá bán xe máy không được trống");
                return false;
            }
            if (hinhAnh.Equals(""))
            {
                MessageBox.Show("Hình đại diện không được trống");
                return false;
            }
            return true;
        }
        private void ThemXeMay()
        {
            string maXeMay = txtMaXeMay.Text;
            string tenXeMay = txtTenXeMay.Text;
            decimal gia = decimal.Parse(txtGia.Text);
            int soLuongTon = int.Parse(txtSoLuongTon.Text);
            string moTa = txtMoTa.Text;
            string trangThai = (string)comboBoxTrangThai.SelectedValue;
            string hinhAnh = Path.GetFileName(this.hinhAnh);
            string loai = (string)comboBoxLoai.SelectedValue;
            string hang = (string)comboBoxHang.SelectedValue;
            string nhaCungCap = (string)comboBoxNhaCungCap.SelectedValue;
            XeMay dongHo = new XeMay(maXeMay, tenXeMay, gia, soLuongTon, moTa, trangThai, hinhAnh, loai, hang, nhaCungCap);
            if (XeMayDAO.Instance.ThemXeMay(dongHo))
            {
                // Chuyển ảnh đến folder resource
                if(!File.Exists(MyFormat.PATH_RESOURCE + dongHo.HinhAnh))
                {
                    try
                    {
                        File.Copy(this.hinhAnh, MyFormat.PATH_RESOURCE + dongHo.HinhAnh);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR: Lỗi chuyển file. " + ex.Message);
                    }
                }
                // Thay mã bằng giá trị
                MessageBox.Show("Thêm xe máy thành công");
                dongHo.TrangThai = comboBoxTrangThai.Text;
                dongHo.Loai = comboBoxLoai.Text;
                dongHo.Hang = comboBoxHang.Text;
                dongHo.NhaCungCap = comboBoxNhaCungCap.Text;
                // Thêm vào gridview
                listXeMay.Add(dongHo);
                gridViewXeMay.RefreshData();
                bindingSource.Position = listXeMay.Count - 1;
                // Reset
                ResetTatCaComponent();
            }
            else
                MessageBox.Show("Thêm xe máy thất bại");
        }
        private void SuaXeMay()
        {
            string maXeMay = txtMaXeMay.Text;
            string tenXeMay = txtTenXeMay.Text;
            decimal gia = decimal.Parse(txtGia.Text);
            string moTa = txtMoTa.Text;
            string trangThai = (string)comboBoxTrangThai.SelectedValue;
            string hinhAnh = Path.GetFileName(this.hinhAnh);
            string loai = (string)comboBoxLoai.SelectedValue;
            string hang = (string)comboBoxHang.SelectedValue;
            XeMay dongHo = new XeMay(maXeMay, tenXeMay, gia, moTa, trangThai, hinhAnh, loai, hang);
            if (XeMayDAO.Instance.SuaXeMay(dongHo))
            {
                MessageBox.Show("Sửa thông tin xe máy thành công");
                XeMay dongHoCu = listXeMay[bindingSource.Position];
                // Chuyển ảnh đến folder Image
                if(listXeMay[bindingSource.Position].HinhAnh != this.hinhAnh)
                {
                    if (!File.Exists(MyFormat.PATH_RESOURCE + dongHo.HinhAnh))
                    {
                        try
                        {
                            File.Copy(this.hinhAnh, MyFormat.PATH_RESOURCE + dongHo.HinhAnh);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("ERROR: Lỗi chuyển file. " + ex.Message);
                        }
                    }
                }
                dongHoCu.TenXeMay = tenXeMay;
                dongHoCu.Gia = gia;
                dongHoCu.MoTa = moTa;
                dongHoCu.TrangThai = comboBoxTrangThai.Text;
                dongHoCu.HinhAnh = hinhAnh;
                dongHoCu.Loai = comboBoxLoai.Text;
                dongHoCu.Hang = comboBoxHang.Text;
                gridViewXeMay.RefreshData();
                // Reset
                ResetTatCaComponent();
            }
            else
                MessageBox.Show("Thêm xe máy thất bại");
        }
        private void XoaXeMay()
        {
            if (XeMayDAO.Instance.XoaXeMay(txtMaXeMay.Text))
            {
                MessageBox.Show("Xóa xe máy thành công");
                listXeMay.RemoveAt(bindingSource.Position);
                bindingSource.Position = 0;
                gridViewXeMay.RefreshData();
                ResetTatCaComponent();
            }
            else
                MessageBox.Show("Xóa xe máy thất bại");
        }
        private void ThucThiChucNang()
        {
            // Disable tất cả nút chức năng khác trừ Ghi và Thoát
            ThayDoiInputEnabled(true);
            ThayDoiInputReadOnly(false);
            ThayDoiButtonEnabled(false);
            btnGhi.Enabled = true;
            btnThoatChucNang.Enabled = true;
            // Thực thi
            switch (chucNangThucThi)
            {
                case CHUCNANG_THEM:
                    gridControlXeMay.Enabled = false;
                    ResetGiaTriInput();
                    txtSoLuongTon.Text = "0";
                    txtSoLuongTon.ReadOnly = true;
                    comboBoxTrangThai.SelectedValue = TRANGTHAI_DEFAULT;
                    comboBoxTrangThai.Enabled = false;
                    break;
                case CHUCNANG_SUA:
                    txtSoLuongTon.ReadOnly = true;
                    comboBoxNhaCungCap.Enabled = false;
                    comboBoxTrangThai.Enabled = false;
                    break;
            }
        }
        private void GhiDuLieu()
        {
            switch (chucNangThucThi)
            {
                case CHUCNANG_THEM:
                    if (InputHopLe())
                        ThemXeMay();
                    break;
                case CHUCNANG_SUA:
                    if (InputHopLe())
                    {
                        SuaXeMay();
                    }
                    break;
            }
        }
        #endregion Chức năng
        private void gridViewXeMay_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            FillInput();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            chucNangThucThi = CHUCNANG_THEM;
            ThucThiChucNang();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            chucNangThucThi = CHUCNANG_SUA;
            ThucThiChucNang();
        }

        private void btnGhi_Click(object sender, EventArgs e)
        {
            GhiDuLieu();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có thực sự muốn xóa xe máy này?\nSau khi xóa không thể phục hồi",
                "Xóa xe máy", 
                MessageBoxButtons.YesNoCancel);
            if(result == DialogResult.Yes)
            {
                XoaXeMay();
            }
        }
        private void btnNgungKinhDoanh_Click(object sender, EventArgs e)
        {
            if(listXeMay.Count > 0)
            {
                DialogResult result = MessageBox.Show(
                    "Bạn có thực sự muốn ngừng kinh doanh sản phẩm này?\nSau khi thực hiện sẽ không thể kinh doanh lại",
                    "Ngừng kinh doanh sản phẩm",
                    MessageBoxButtons.YesNoCancel);
                if(result == DialogResult.Yes)
                {
                    XeMay dongHo = listXeMay[bindingSource.Position];
                    if (XeMayDAO.Instance.NgungKinhDoanhSP(dongHo.MaXeMay))
                    {
                        MessageBox.Show("Ngừng kinh doanh sản phẩm thành công");
                    }
                    else
                        MessageBox.Show("Ngừng kinh doanh sản phẩm thất bại");
                }
            }
        }
        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThoatChucNang_Click(object sender, EventArgs e)
        {
            ResetTatCaComponent();
            FillInput();
        }
        private void pictureBoxHinhAnh_Click(object sender, EventArgs e)
        {
            if(chucNangThucThi == CHUCNANG_THEM || chucNangThucThi == CHUCNANG_SUA)
            {
                if (openFileDialogChonAnh.ShowDialog() == DialogResult.OK)
                {
                    hinhAnh = openFileDialogChonAnh.FileName;
                    pictureBoxHinhAnh.Image = Image.FromFile(hinhAnh);
                }
            }
        }

        private void txtMaXeMay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
