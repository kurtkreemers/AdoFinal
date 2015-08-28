using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gemeenschap
{
    public class Genre
    {
        private int genreNrValue;
        private string genreValue;

        public int GenreNr
        {
            get { return genreNrValue; }
            set { genreNrValue = value; }
        }
        public string GenreName
        {
            get { return genreValue; }
            set { genreValue = value; }
        }
        public Genre(int genrenr, string genrenaam)
        {
            this.GenreName = genrenaam;
            this.GenreNr = genrenr;
        }
    
       
        
        
    }
}
