using iStokTakip.FormApp.Modul_Stok;
using iStokTakip.FormApp.Modul_Cari;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iStokTakip.FormApp.Functions
{
    public class Formlar
    {
        #region StokFormları
        public int StokListesi(bool Secim = false)
        {
            frmStokListesi frm = new frmStokListesi();
            if (Secim)
            {
                frm.secim = Secim;
                frm.ShowDialog();
            }
            else
            {
                frm.MdiParent = AnaForm.ActiveForm;
                frm.Show();
            }
            return AnaForm.Aktarma;
        }
        public int StokGruplari(bool Secim = false)
        {
            frmStokGruplari frm = new frmStokGruplari();
            if (Secim) frm.secim = Secim;
            frm.ShowDialog();
            return AnaForm.Aktarma;

        }
        public void StokHareketleri(bool Ac = false)
        {

        }
        public void StokKarti(bool Ac = false)
        {
            frmStokKarti frm = new frmStokKarti();
            frm.ShowDialog();
        } 
        #endregion

        public int CariGruplari(bool Secim = false)
        {
            frmCariGruplari frm = new frmCariGruplari();
            if (Secim) frm.secim = Secim;
            frm.ShowDialog();
            return AnaForm.Aktarma;
        }
    }
}
