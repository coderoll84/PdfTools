using System;
using System.Collections;
using System.Collections.Generic;

namespace BoldReport
{
    public class TablaList
    {
        public int TablaId { get; set; }
        public string Texto { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Moneda { get; set; }
        public bool Boleano { get; set; }
        public static IList GetData()
        {
            List<TablaList> datas = new List<TablaList>();
            TablaList data = null;
            data = new TablaList()
            {
                TablaId = 1,
                Texto = "Hola",
                Fecha = DateTime.Now,
                Moneda = 1000,
                Boleano = false
            };
            datas.Add(data);
            data = new TablaList()
            {
                TablaId = 3,
                Texto = "banda",
                Fecha = DateTime.Now,
                Moneda = 99,
                Boleano = false
            };
            datas.Add(data);
            data = new TablaList()
            {
                TablaId = 2,
                Texto = "de Coderoll",
                Fecha = DateTime.Now,
                Moneda = 200,
                Boleano = false
            };
            datas.Add(data);

            return datas;
        }
    }
}