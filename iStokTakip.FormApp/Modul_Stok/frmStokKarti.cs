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

namespace iStokTakip.FormApp.Modul_Stok
{
    public partial class frmStokKarti : DevExpress.XtraEditors.XtraForm
    {
        DatabaseDataContext db = new DatabaseDataContext();
        Numara num = new Numara();
        Formlar frm = new Formlar();
        Mesajlar msj = new Mesajlar();

        bool edit = false;
        int grupId = -1;
        int stokId = -1;

        public frmStokKarti()
        {
            InitializeComponent();
        }

        private void frmStokKarti_Load(object sender, EventArgs e)
        {
            txtStokKodu.Text = num.StokKodNumarasi();
        }
       

       
    }
}