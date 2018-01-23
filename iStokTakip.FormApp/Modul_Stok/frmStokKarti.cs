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
using System.Data.Linq;

namespace iStokTakip.FormApp.Modul_Stok
{
    public partial class frmStokKarti : DevExpress.XtraEditors.XtraForm
    {
        DatabaseDataContext db = new DatabaseDataContext();
        Numara num = new Numara();
        Formlar frm = new Formlar();
        Mesajlar msj = new Mesajlar();
        Resim img = new Resim();


        bool edit = false;
        bool resim = false;
        int grupId = -1;
        int stokId = -1;
        OpenFileDialog dosya = new OpenFileDialog();

        public frmStokKarti()
        {
            InitializeComponent();
        }

        private void frmStokKarti_Load(object sender, EventArgs e)
        {
            txtStokKodu.Text = num.StokKodNumarasi();
        }

        void Temizle()
        {
            txtAlisFiyat.Text = "0";
            txtAlisKdv.Text = "0";
            txtBarkod.ResetText();
            txtBirim.SelectedIndex = 0;
            txtGrupAdi.ResetText();
            txtGrupKodu.ResetText();
            txtSatisFiyat.Text = "0";
            txtSatisKdv.Text = "0";
            txtStokAdi.ResetText();
            txtStokKodu.Text = num.StokKodNumarasi();
            pictureBox1.Image = null;
            edit = false;
            resim = false;
            grupId = -1;
            stokId = -1;
            AnaForm.Aktarma = -1;

        }

        void ResimSec()
        {
            dosya.Filter = "Jpg(*.jpg)|*.jpg|Jpeg(*.jpeg)|*.jpeg";
            if (dosya.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = dosya.FileName;
                resim = true;
            }
        }

        private void btnResimSec_Click(object sender, EventArgs e)
        {
            ResimSec();
        }

        void Kaydet()
        {
            try
            {

                TBL_STOKLAR stok = new TBL_STOKLAR();
                stok.STOKADI = txtStokAdi.Text;
                stok.STOKALISFIYAT = decimal.Parse(txtAlisFiyat.Text);
                stok.STOKALISKDV = decimal.Parse(txtAlisKdv.Text);
                stok.STOKBARKOD = txtBarkod.Text;
                stok.STOKBIRIM = txtBirim.Text;
                stok.STOKGRUPID = grupId;
                stok.STOKKODU = txtStokKodu.Text;
                stok.STOKRESIM = new Binary(img.UpdateResim(pictureBox1.Image));
                stok.STOKSATISFIYAT = decimal.Parse(txtSatisFiyat.Text);
                stok.STOKSATISKDV = decimal.Parse(txtSatisKdv.Text);
                stok.STOKSAVEDATE = DateTime.Now;
                stok.STOKSAVEUSER = AnaForm.UserID;
                db.TBL_STOKLARs.InsertOnSubmit(stok);
                db.SubmitChanges();
                msj.YeniKayit("Yeni stok kaydı oluşturuldu");
                Temizle();
            }
            catch (Exception ex)
            {

                msj.Hata(ex);
            }
        }

        void Guncelle()
        {
            TBL_STOKLAR stok = db.TBL_STOKLARs.First(x => x.ID == stokId);
            stok.STOKADI = txtStokAdi.Text;
            stok.STOKALISFIYAT = decimal.Parse(txtAlisFiyat.Text);
            stok.STOKALISKDV = decimal.Parse(txtAlisKdv.Text);
            stok.STOKBARKOD = txtBarkod.Text;
            stok.STOKBIRIM = txtBirim.Text;
            stok.STOKGRUPID = grupId;
            stok.STOKKODU = txtStokKodu.Text;
            if(resim) stok.STOKRESIM = new Binary(img.UpdateResim(pictureBox1.Image));
            stok.STOKSATISFIYAT = decimal.Parse(txtSatisFiyat.Text);
            stok.STOKSATISKDV = decimal.Parse(txtSatisKdv.Text);
            stok.STOKEDITDATE = DateTime.Now;
            stok.STOKEDITUSER = AnaForm.UserID;
            db.SubmitChanges();
            msj.Guncelle(true);
            Temizle();
        }

        void Sil()
        {
            try
            {
                db.TBL_STOKLARs.DeleteOnSubmit(db.TBL_STOKLARs.First(x => x.ID == stokId));
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                msj.Hata(ex);
            }

        }
        
        public void Ac(int ID)
        {
            edit = true; 
            stokId = ID;
            TBL_STOKLAR stok = db.TBL_STOKLARs.First(x => x.ID == stokId);
            GrupAc(stok.STOKGRUPID.Value);
            pictureBox1.Image = img.ShowResim(stok.STOKRESIM.ToArray());
            txtAlisKdv.Text = stok.STOKALISKDV.ToString();
            txtAlisFiyat.Text = stok.STOKALISFIYAT.ToString();
            txtBarkod.Text = stok.STOKBARKOD;
            txtBirim.Text = stok.STOKBIRIM;
            txtSatisFiyat.Text = stok.STOKSATISFIYAT.ToString();
            txtSatisKdv.Text = stok.STOKSATISKDV.ToString();
            txtStokAdi.Text = stok.STOKADI;
            txtStokKodu.Text = stok.STOKKODU;

        }

        void GrupAc(int ID)
        {
            grupId = ID;
            txtGrupAdi.Text = db.TBL_STOKGRUPLARIs.First(x => x.ID == grupId).GRUPADI;
            txtGrupKodu.Text = db.TBL_STOKGRUPLARIs.First(x => x.ID == grupId).GRUPKODU;
        }

        private void txtStokKodu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int ID = frm.StokListesi(true);
            if (ID>0)
            {
                Ac(ID);
                AnaForm.Aktarma = -1;
            }
        }

        private void txtGrupKodu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int Id = frm.StokGruplari(true);
            if (Id>0)
            {
                GrupAc(Id);

            }
            AnaForm.Aktarma = -1;
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {

            if (edit && stokId > 0 && msj.Sil() == DialogResult.Yes) Sil();
            Temizle();
           
            
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (edit && stokId>0 && msj.Guncelle()==DialogResult.Yes)
            {
                Guncelle();
            }
            else
            {
                Kaydet();
            }
        }
    }
}