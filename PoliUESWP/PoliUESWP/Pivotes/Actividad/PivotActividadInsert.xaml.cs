using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO;
using Windows.Storage;
using PoliUESWP.MetodosSQLite;
using PoliUESWP.ClasesPorTabla;
using SQLite;

namespace PoliUESWP.Pivotes.Actividad
{
    public partial class PivotActividadInsert : PhoneApplicationPage
    {
        public PivotActividadInsert()
        {
            InitializeComponent();
            MostrarDatos();
        }

        string dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "db.sqlite");


        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Menus/MenuActividad.xaml?", UriKind.Relative));
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            MetodosSQLiteActividad op = new MetodosSQLiteActividad();

            String res = op.Insert(dbPath, txtNombre.Text, txtDescripcionActividad.Text);
            MessageBox.Show(res);
            MostrarDatos();
            pivotPrincipal.SelectedItem = listaDatos;
            limpiar();
           
        }

        public void MostrarDatos() {
            var db = new SQLiteConnection(dbPath);
            var pers = db.Table<PoliUESWP.ClasesPorTabla.Actividad>().Where(c => c.IdActividad >= 0).ToList();
            listaActividades.ItemsSource = pers;
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }
        private void limpiar()
        {
            txtDescripcionActividad.Text = String.Empty;
            txtNombre.Text = String.Empty;
        }
    }
}