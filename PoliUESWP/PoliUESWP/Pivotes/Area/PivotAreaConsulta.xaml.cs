using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PoliUESWP.MetodosSQLite;
using Windows.Storage;
using System.IO;
using SQLite;

namespace PoliUESWP.Pivotes.Area
{
    public partial class PivotAreaConsulta : PhoneApplicationPage
    {
        public PivotAreaConsulta()
        {
            InitializeComponent();
            MostrarDatos();
        }
        string dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "db.sqlite");

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            MetodosSQLiteArea op = new MetodosSQLiteArea();

            String[] res = op.Consulta(dbPath, Int32.Parse(txtIdArea.Text));

            txtMaxPersonas.Text = res[1];
            txtNombreArea.Text = res[2];
            txtDesArea.Text = res[3];

            if (res[0] != string.Empty)
            {
                mostrarDatosUnicos(Int32.Parse(res[0]));
                pivotPrincipal.SelectedItem = itemConsulta;
            }
            else
            {
                MostrarDatos();
                MessageBox.Show("La Tarifa no existe");
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtIdArea.Text = String.Empty;
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Menus/MenuArea.xaml?", UriKind.Relative));
        }
        public void mostrarDatosUnicos(int idArea)
        {
            var db = new SQLiteConnection(dbPath);
            var pers = db.Table<ClasesPorTabla.Area>().Where(c => c.IdArea == idArea).ToList();
            listaArea.ItemsSource = pers;
        }

        public void MostrarDatos()
        {
            var db = new SQLiteConnection(dbPath);
            var pers = db.Table<ClasesPorTabla.Area>().Where(c => c.IdArea > 0).ToList();
            listaArea.ItemsSource = pers;
        }

        private void listaArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClasesPorTabla.Area items = (ClasesPorTabla.Area)listaArea.SelectedItem;

            txtMaxPersonas.Text = String.Empty;
            txtNombreArea.Text = String.Empty;
            txtDesArea.Text = String.Empty;

            pivotPrincipal.SelectedItem = itemConsulta;
            MessageBox.Show("Encontrado ...");
            mostrarDatosUnicos(items.IdArea);
        }
    }
}