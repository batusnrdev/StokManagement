﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iStokTakip.FormApp.Functions
{
    class Numara
    {
        DatabaseDataContext db = new DatabaseDataContext();
        Mesajlar msj = new Mesajlar();

        public string StokKodNumarasi()
        {
            try
            {
                int kod = int.Parse((from s in db.TBL_STOKLARs orderby s.ID descending select s).First().STOKKODU);
                kod++;
                string num = kod.ToString().PadLeft(7, '0');
                return num;
            }
            catch (Exception)
            {

                return "0000001";
            }
        }
    }
}
