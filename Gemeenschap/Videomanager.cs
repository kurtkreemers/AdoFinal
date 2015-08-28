using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data;

namespace Gemeenschap
{
    public class Videomanager
    {
        public ObservableCollection<Film> GetFilms()
        {
            ObservableCollection<Film> films = new ObservableCollection<Film>();
            var manager = new VideoDbManager();
            using(var conVideoAdo = manager.GetConnection())
            {
                using(var comRead = conVideoAdo.CreateCommand())
                {
                    comRead.CommandType = CommandType.Text;
                    comRead.CommandText = "SELECT * from Films";

                    conVideoAdo.Open();

                    using(var rdrFilms = comRead.ExecuteReader())
                    {
                        Int32 vidBandNrPos = rdrFilms.GetOrdinal("BandNr");
                        Int32 vidTitelPos = rdrFilms.GetOrdinal("Titel");
                        Int32 vidGenrePos = rdrFilms.GetOrdinal("GenreNr");
                        Int32 vidInPos = rdrFilms.GetOrdinal("InVoorraad");
                        Int32 vidUitPos = rdrFilms.GetOrdinal("UitVoorraad");
                        Int32 vidPrijsPos = rdrFilms.GetOrdinal("Prijs");
                        Int32 vidTotverhPos = rdrFilms.GetOrdinal("TotaalVerhuurd");

                        while (rdrFilms.Read())
                        {
                            films.Add(new Film(rdrFilms.GetInt32(vidBandNrPos), rdrFilms.GetString(vidTitelPos),
                                rdrFilms.GetInt32(vidGenrePos), rdrFilms.GetInt32(vidInPos), rdrFilms.GetInt32(vidUitPos),
                                rdrFilms.GetDecimal(vidPrijsPos), rdrFilms.GetInt32(vidTotverhPos)));
                        }
                    }
                }
            }
            return films;
        }
        public  List<Genre> GetGenre()
        {
            List<Genre> genres = new List<Genre>();
            var manager = new VideoDbManager();
            using (var conGenres = manager.GetConnection())
            {
                using (var comGenresZoeken = conGenres.CreateCommand())
                {
                    comGenresZoeken.CommandType = CommandType.Text;
                    comGenresZoeken.CommandText = "select GenreNr,Genre from Genres order by Genre";
                    conGenres.Open();
                    using (var rdrGenres = comGenresZoeken.ExecuteReader())
                    {
                        Int32 genreNumPos = rdrGenres.GetOrdinal("GenreNr");
                        Int32 genreNaamPos = rdrGenres.GetOrdinal("Genre");
                        while (rdrGenres.Read())
                        {
                            genres.Add(new Genre(rdrGenres.GetInt32(genreNumPos), rdrGenres.GetString(genreNaamPos)));
                        }

                    }
                }
            }
            return genres;
        }
        public void SchrijfVerwijderingen(List<Film> oudefilms)
        {
            var manager = new VideoDbManager();
            using (var conVideoAdo = manager.GetConnection())
            {
                using (var comDelete = conVideoAdo.CreateCommand())
                {
                    comDelete.CommandType = CommandType.Text;
                    comDelete.CommandText = "DELETE from Films where BandNr=@bandNr";

                    var parBandNr = comDelete.CreateParameter();
                    parBandNr.ParameterName = "@bandNr";
                    comDelete.Parameters.Add(parBandNr);
                    conVideoAdo.Open();
                    foreach (Film eenFilm in oudefilms)
                    {
                        parBandNr.Value = eenFilm.BandNr;
                        comDelete.ExecuteNonQuery();
                    }
                }
            }
        }

        public void SchrijfToevoegingen(List<Film> nieuwefilms)
        {
            var manager = new VideoDbManager();
            using (var conVideoAdo = manager.GetConnection())
            {
                using (var comInsert = conVideoAdo.CreateCommand())
                {
                    comInsert.CommandType = CommandType.Text;
                    comInsert.CommandText = "INSERT into Films (Titel,GenreNr,InVoorraad,UitVoorraad,Prijs,TotaalVerhuurd)"
                                            + "values (@titel,@genrenr,@invoorraad,@uitvoorraad,@prijs,@totaalverhuurd)";

                    var parTitel = comInsert.CreateParameter();
                    parTitel.ParameterName = "@titel";
                    comInsert.Parameters.Add(parTitel);

                    var parGenreNr = comInsert.CreateParameter();
                    parGenreNr.ParameterName = "@genrenr";
                    comInsert.Parameters.Add(parGenreNr);

                    var parInVoorraad = comInsert.CreateParameter();
                    parInVoorraad.ParameterName = "@invoorraad";
                    comInsert.Parameters.Add(parInVoorraad);

                    var parUitVoorraad = comInsert.CreateParameter();
                    parUitVoorraad.ParameterName = "@uitvoorraad";
                    comInsert.Parameters.Add(parUitVoorraad);

                    var parPrijs = comInsert.CreateParameter();
                    parPrijs.ParameterName = "@prijs";
                    comInsert.Parameters.Add(parPrijs);

                    var parTotaalVerhuurd = comInsert.CreateParameter();
                    parTotaalVerhuurd.ParameterName = "@totaalverhuurd";
                    comInsert.Parameters.Add(parTotaalVerhuurd);

                    conVideoAdo.Open();
                    foreach (Film eenFilm in nieuwefilms)
                    {
                        parTitel.Value = eenFilm.Titel;
                        parGenreNr.Value = eenFilm.GenreNr;
                        parInVoorraad.Value = eenFilm.InVoorraad;
                        parUitVoorraad.Value = eenFilm.UitVoorraad;
                        parPrijs.Value = eenFilm.Prijs;
                        parTotaalVerhuurd.Value = eenFilm.TotaalVerhuurd;
                        comInsert.ExecuteNonQuery();
                    }
                }
            }
        }
        public void SchrijfWijzigingen(List<Film> gewijzigdefilms)
        {
            var manager = new VideoDbManager();
            using (var conVideoAdo = manager.GetConnection())
            {
                using (var comUpdate = conVideoAdo.CreateCommand())
                {
                    comUpdate.CommandType = CommandType.Text;
                    comUpdate.CommandText = "UPDATE Films set Titel=@titel, GenreNr=@genrenr,InVoorraad=@invoorraad, "
                                            + "UitVoorraad=@uitvoorraad, Prijs=@prijs, TotaalVerhuurd=@totaalverhuurd where BandNr=@bandnr";

                    var parBandNr = comUpdate.CreateParameter();
                    parBandNr.ParameterName = "@bandnr";
                    comUpdate.Parameters.Add(parBandNr);

                    var parTitel = comUpdate.CreateParameter();
                    parTitel.ParameterName = "@titel";
                    comUpdate.Parameters.Add(parTitel);

                    var parGenreNr = comUpdate.CreateParameter();
                    parGenreNr.ParameterName = "@genrenr";
                    comUpdate.Parameters.Add(parGenreNr);

                    var parInVoorraad = comUpdate.CreateParameter();
                    parInVoorraad.ParameterName = "@invoorraad";
                    comUpdate.Parameters.Add(parInVoorraad);

                    var parUitVoorraad = comUpdate.CreateParameter();
                    parUitVoorraad.ParameterName = "@uitvoorraad";
                    comUpdate.Parameters.Add(parUitVoorraad);

                    var parPrijs = comUpdate.CreateParameter();
                    parPrijs.ParameterName = "@prijs";
                    comUpdate.Parameters.Add(parPrijs);

                    var parTotaalVerhuurd = comUpdate.CreateParameter();
                    parTotaalVerhuurd.ParameterName = "@totaalverhuurd";
                    comUpdate.Parameters.Add(parTotaalVerhuurd);

                    conVideoAdo.Open();
                    foreach (Film eenFilm in gewijzigdefilms)
                    {
                        parBandNr.Value = eenFilm.BandNr;
                        parTitel.Value = eenFilm.Titel;
                        parGenreNr.Value = eenFilm.GenreNr;
                        parInVoorraad.Value = eenFilm.InVoorraad;
                        parUitVoorraad.Value = eenFilm.UitVoorraad;
                        parPrijs.Value = eenFilm.Prijs;
                        parTotaalVerhuurd.Value = eenFilm.TotaalVerhuurd;
                        comUpdate.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
