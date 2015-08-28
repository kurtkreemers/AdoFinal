using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gemeenschap
{
    public class Film
    {
        private Int32 bandNrValue;
        private String titelValue;
        private int genrenrValue;
        private int invoorraadValue;
        private int uitvoorraadValue;
        private Decimal prijsValue;
        private int TotaalverhVallue;
        public bool Changed { get; set; }

        public int BandNr
        {
            get { return bandNrValue; }
            set { bandNrValue = value; }
        }
        public String Titel
        {
            get { return titelValue; }
            set
            {
                titelValue = value;

            }
        }
        public int GenreNr
        {
            get { return genrenrValue; }
            set
            {
                genrenrValue = value;

            }
        }
        public int InVoorraad
        {
            get { return invoorraadValue; }
            set
            {
                invoorraadValue = value;
                Changed = true;
            }
        }
        public int UitVoorraad
        {
            get { return uitvoorraadValue; }
            set
            {
                uitvoorraadValue = value;
                Changed = true;
            }
        }
        public Decimal Prijs
        {
            get { return prijsValue; }
            set { prijsValue = value; }
        }
        public int TotaalVerhuurd
        {
            get { return TotaalverhVallue; }
            set
            {
                TotaalverhVallue = value;
                Changed = true;
            }
        }

        public Film(Int32 bandnr, String titel, Int32 genrenr, Int32 invoorraad, Int32 uitvoorraad,
            Decimal prijs, Int32 totaalverhuurd)
        {
            this.BandNr = bandnr;
            this.Titel = titel;
            this.GenreNr = genrenr;
            this.InVoorraad = invoorraad;
            this.UitVoorraad = uitvoorraad;
            this.Prijs = prijs;
            this.TotaalVerhuurd = totaalverhuurd;
            this.Changed = false;
        }
        public Film()
        {

        }







    }
}
