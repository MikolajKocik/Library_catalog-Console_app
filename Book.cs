using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Catalog;
    class Book
    {
        public string Tytul { get; set; }
        public string Autorzy { get; set; }
        public int Strony { get; set; }
        public int Rok { get; set; }

        public Book(string tytul, string autorzy, int strony, int rok)
        {
            Tytul = tytul;
            Autorzy = autorzy;
            Strony = strony;
            Rok = rok;
        }
    }

