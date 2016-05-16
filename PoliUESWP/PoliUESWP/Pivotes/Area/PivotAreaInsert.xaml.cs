using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Storage;
using System.IO;
using PoliUESWP.MetodosSQLite;
using SQLite;

namespace PoliUESWP.Pivotes.Area
{
    public partial class PivotAreaInsert : PhoneApplicationPage
    {
        public PivotAreaInsert()
        {
            InitializeComponent();
            MostrarDatos();
        }
        string dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "db.sqlite");

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Menus/MenuArea.xaml?", UriKind.Relative));
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            MetodosSQLiteArea op = new MetodosSQLiteArea();

            String res = op.Insert(dbPath, Int32.Parse(txtMaxPersonas.Text), txtNombreArea.Text, txtDesArea.Text);
            MessageBox.Show(res);
            MostrarDatos();
            pivotPrincipal.SelectedItem = listaDatos;
            limpiar();
        }
        public void MostrarDatos()
        {
            var db = new SQLiteConnection(dbPath);
            var pers = db.Table<ClasesPorTabla.Area>().Where(c => c.IdArea >= 0).ToList();
            listaArea.ItemsSource = pers;
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }
        private void limpiar()
        {
            txtMaxPersonas.Text = String.Empty;
            txtNombreArea.Text = String.Empty;
            txtDesArea.Text = String.Empty;
        }
    }
}