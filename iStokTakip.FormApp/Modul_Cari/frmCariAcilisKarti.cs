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
    public partial class frmCariAcilisKarti : DevExpress.XtraEditors.XtraForm
    {
        DatabaseDataContext db = new DatabaseDataContext();
        Mesajlar msj = new Mesajlar();
        Numara num = new Numara();
        Formlar frm = new Formlar();

        bool edit = false;
        int cariId = -1;
        int grupId = -1;

        public frmCariAcilisKarti()
        {
            InitializeComponent();
        }

        private void frmCariAcilisKarti_Load(object sender, EventArgs e)
        {
            txtCariKodu.Text = num.CariKodNumarasi();
        }
        
        void Temizle()
        {
            foreach (Control item in groupControl3.Controls)
            {
                if (item is TextEdit )
                {
                    (item as TextEdit).ResetText();
                }
            }
        }
    }
}