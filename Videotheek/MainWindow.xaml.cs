using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Gemeenschap;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Videotheek
{
    public partial class MainWindow : Window
    {
        bool toevoegAct;
        private CollectionViewSource filmViewSource;
        public ObservableCollection<Film> filmsOb = new ObservableCollection<Film>();
        public List<Film> OudeFilms = new List<Film>();
        public List<Film> NieuweFilms = new List<Film>();
        public List<Film> GewijzigdeFilms = new List<Film>();
        
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            filmViewSource = ((CollectionViewSource)(this.FindResource("filmViewSource")));
            FilmsLaden();
            GenresComboBoxVullen();           
            toevoegAct = false;

        }
       
        private void btToevoegbevestig_Click(object sender, RoutedEventArgs e)
        {
            
            if (!toevoegAct)
            { Film nieuweFilm = new Film();
                layoutActive();
                toevoegAct = true;
                filmsOb.Add(nieuweFilm);
                lbFilms.SelectedItem = nieuweFilm;
            }
            else
            {
                if (!CheckOpFouten())
                {                   
                    toevoegAct = false;
                    layoutOriginal();
                }
                else
                    MessageBox.Show("Er zijn nog fouten aanwezig, kan niet toevoegen", "Toevoegen", MessageBoxButton.OK, MessageBoxImage.Error);


            }
        }

        private void btVerwijdAnnuleer_Click(object sender, RoutedEventArgs e)
        {          
            if (toevoegAct)
            {               
                toevoegAct = false;
                Film removeFilm = new Film();
                removeFilm = (Film)lbFilms.SelectedItem;
                filmsOb.Remove(removeFilm);
                NieuweFilms.Remove(removeFilm);
                OudeFilms.Remove(removeFilm);
                layoutOriginal();
                lbFilms.SelectedIndex = 0;
                
            }
            else
            {
                if (MessageBox.Show("Ben je zeker dat je deze film wil verwijderen", "Verwijderen", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    Film oudeFilm = new Film();
                    oudeFilm = (Film)lbFilms.SelectedItem;
                    filmsOb.Remove(oudeFilm);
                }
            }
        }
        private void btVerhuur_Click(object sender, RoutedEventArgs e)
        {
            Binding b = BindingOperations.GetBinding(inVoorraadTextBox, TextBox.TextProperty);
            b.ValidationRules.Clear();

            int inVoorraad = Convert.ToInt32(inVoorraadTextBox.Text);
            int uitVoorraad = Convert.ToInt32(uitVoorraadTextBox.Text);
            int totaalVerh = Convert.ToInt32(totaalVerhuurdTextBox.Text);
            if (inVoorraad > 0)
            {
                inVoorraadTextBox.Text = (inVoorraad - 1).ToString();
                uitVoorraadTextBox.Text = (uitVoorraad + 1).ToString();
                totaalVerhuurdTextBox.Text = (totaalVerh + 1).ToString();
            }
            else
                MessageBox.Show("Alle films zijn verhuurd!!!", "Verhuur", MessageBoxButton.OK, MessageBoxImage.Asterisk);

            b.ValidationRules.Add(new GetalGroterDanNulRule());
            b.ValidationRules.Add(new IntGetalIngaveRule());
        }

        private void btAllesOpslaan_Click(object sender, RoutedEventArgs e)
        {
            foreach (Film eenFilm in filmsOb)
            {
                if (eenFilm.Changed == true)
                {
                    GewijzigdeFilms.Add(eenFilm);
                    eenFilm.Changed = false;
                }
            }
            if (GewijzigdeFilms.Count() != 0 || OudeFilms.Count() != 0 || NieuweFilms.Count() != 0)
            {
                Videomanager manager = new Videomanager();
                if (MessageBox.Show("Wilt u alles wegschrijven naar de database ?", "Opslaan", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {

                    if (GewijzigdeFilms.Count() != 0)
                    {
                        manager.SchrijfWijzigingen(GewijzigdeFilms);
                    }

                    GewijzigdeFilms.Clear();

                    if (NieuweFilms.Count() != 0)
                    {
                        manager.SchrijfToevoegingen(NieuweFilms);
                    }

                    NieuweFilms.Clear();

                    if (OudeFilms.Count() != 0)
                    {
                        manager.SchrijfVerwijderingen(OudeFilms);
                    }

                    OudeFilms.Clear();
                    FilmsLaden();
                }
                
            }
            else
                MessageBox.Show("Geen wijzigingen om op te slaan!!!", "Opslaan", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void layoutOriginal()
        {
            btToevoegbevestig.Content = "Toevoegen";
            btVerwijdAnnuleer.Content = "Verwijderen";
            btAllesOpslaan.IsEnabled = true;
            btVerhuur.IsEnabled = true;
            Panel.SetZIndex(labelUitvr, 0);
            Panel.SetZIndex(labelInvr, 0);
            Panel.SetZIndex(btVerhuur, 1);
            lbFilms.IsEnabled = true;
            titelTextBox.IsReadOnly = true;
            genreNrCbBox.IsEnabled = false;
            inVoorraadTextBox.IsReadOnly = true;
            uitVoorraadTextBox.IsReadOnly = true;
            prijsTextBox.IsReadOnly = true;
            totaalVerhuurdTextBox.IsReadOnly = true;
            lbFilms.SelectedIndex = 0;
           
        }
        private void layoutActive()
        {
            btToevoegbevestig.Content = "Bevestigen";
            btVerwijdAnnuleer.Content = "Annuleren";
            btAllesOpslaan.IsEnabled = false;
            btVerhuur.IsEnabled = false;
            Panel.SetZIndex(labelUitvr, 1);
            Panel.SetZIndex(labelInvr, 1);
            Panel.SetZIndex(btVerhuur, 0);
            lbFilms.IsEnabled = false;
            titelTextBox.IsReadOnly = false;
            genreNrCbBox.IsEnabled = true;
            inVoorraadTextBox.IsReadOnly = false;
            uitVoorraadTextBox.IsReadOnly = false;
            prijsTextBox.IsReadOnly = false;
            totaalVerhuurdTextBox.IsReadOnly = false;

        }
        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (Film oudeFilm in e.OldItems)
                {
                    OudeFilms.Add(oudeFilm);
                }
            }
            if (e.NewItems != null)
            {
                foreach (Film nieuweFilm in e.NewItems)
                {
                    NieuweFilms.Add(nieuweFilm);
                }
            }
        }
        bool CheckOpFouten()
        {
            bool foutGevonden = false;
            foreach (var c in grid1.Children)
            {
                if (Validation.GetHasError((DependencyObject)c))
                {
                    foutGevonden = true;
                }
            }
            return foutGevonden;
        }
        private void GenresComboBoxVullen()
        {
            Videomanager manager = new Videomanager();
            genreNrCbBox.DisplayMemberPath = "GenreName";
            genreNrCbBox.SelectedValuePath = "GenreNr";
            List<Genre> genresList = manager.GetGenre();
            foreach (Genre genre in genresList)
            {
                genreNrCbBox.Items.Add(genre);
            }
        }
        private void FilmsLaden()
        {
            var manager = new Videomanager();
            filmsOb = manager.GetFilms();
            filmViewSource.Source = filmsOb;
            filmsOb.CollectionChanged += this.OnCollectionChanged;
           
        }
    }
}
