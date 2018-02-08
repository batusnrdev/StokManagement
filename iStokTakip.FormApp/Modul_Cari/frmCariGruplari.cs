using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using iStokTakip.FormApp.Functions;

namespace iStokTakip.FormApp.Modul_Cari
{
    public partial class frmCariGruplari : DevExpress.XtraEditors.XtraForm
    {
        DatabaseDataContext db = new DatabaseDataContext();
        Mesajlar msj = new Mesajlar();

        bool edit = false;
        int secimId = -1;
        public bool secim = false;

        public frmCariGruplari()
        {
            InitializeComponent();
        }

        void Temizle()
        {
            txtGrupAdi.ResetText();
            txtGrupKodu.ResetText();
            edit = false;
            secimId = -1;
            Listele();

        }
        void Listele()
        {
            var lst = from s in db.TBL_CARIGRUPLARIs select s;
            gridStokGrup.DataSource = lst;
        }

        void Sec()
        {
            try
            {
                edit = true;
                secimId = int.Parse(gridView1.GetFocusedRowCellValue("ID").ToString());
                txtGrupAdi.Text = gridView1.GetFocusedRowCellValue("GRUPADI").ToString();
                txtGrupKodu.Text = gridView1.GetFocusedRowCellValue("GRUPKODU").ToString();

            }
            catch (Exception)
            {
                edit = false;
                secimId = -1;
            }
        }
        void Kaydet()
        {
            try
            {
                TBL_CARIGRUPLARI cariGruplari = new TBL_CARIGRUPLARI();
                cariGruplari.GRUPADI = txtGrupAdi.Text;
                cariGruplari.GRUPKODU = txtGrupKodu.Text;
                cariGruplari.SAVEDATE = DateTime.Now;
                cariGruplari.SAVEUSER = AnaForm.UserID;
                db.TBL_CARIGRUPLARIs.InsertOnSubmit(cariGruplari);
                db.SubmitChanges();
                msj.YeniKayit("Cari Grup Kaydı oluşturuldu");
                Temizle();
            }
            catch (Exception ex)
            {
                msj.Hata(ex);
            }
        }
        void Guncelle()
        {
            try
            {
                TBL_CARIGRUPLARI cariGruplari = db.TBL_CARIGRUPLARIs.First(x => x.ID == secimId);
                cariGruplari.GRUPADI = txtGrupAdi.Text;
                cariGruplari.GRUPKODU = txtGrupKodu.Text;
                cariGruplari.EDITDATE = DateTime.Now;
                cariGruplari.EDITUSER = AnaForm.UserID;
                db.SubmitChanges();
                msj.Guncelle(true);
                Temizle();
            }
            catch (Exception ex)
            {
                msj.Hata(ex);
            }
        }
        void Sil()
        {
            try
            {
                db.TBL_CARIGRUPLARIs.DeleteOnSubmit(db.TBL_CARIGRUPLARIs.First(x => x.ID == secimId));
                db.SubmitChanges();
                Temizle();
            }
            catch (Exception ex)
            {
                msj.Hata(ex);
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (edit && secimId > 0 && msj.Guncelle() == DialogResult.Yes)
            {
                Guncelle();
            }
            else
            {
                if (string.IsNullOrEmpty(txtGrupAdi.Text) || string.IsNullOrEmpty(txtGrupKodu.Text))
                {
                    MessageBox.Show("Alanlar boş geçilemez");
                    edit = false;
                    secimId = -1;
                }
                else
                {
                    Kaydet();
                }
            }

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (edit && secimId > 0 && msj.Sil() == DialogResult.Yes) Sil();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCariGruplari_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void gridStokGrup_DoubleClick(object sender, EventArgs e)
        {
            Sec();
            if (secim && secimId > 0)
            {
                this.Close();
            }
        }
    }
}