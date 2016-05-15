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
using SQLite;

namespace PoliUESWP.Pivotes.Actividad
{
    public partial class PivotActividadUpdate : PhoneApplicationPage
    {
        public PivotActividadUpdate()
        {
            InitializeComponent();
            MostrarDatos();
        }

        string dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "db.sqlite");


        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            MetodosSQLiteActividad prueba = new MetodosSQLiteActividad();

            String res = prueba.Update(dbPath, Int32.Parse(txtidActividad.Text), txtNombre.Text, txtDescripcionActividad.Text);
            MessageBox.Show(res);
            MostrarDatos();
            limpiar();
            pivotPrincipal.SelectedItem = itemDatos;
        }

        public void MostrarDatos() {
            var db = new SQLiteConnection(dbPath);
            var pers = db.Table<ClasesPorTabla.Actividad>().Where(c => c.IdActividad > 0).ToList();
            listaActividades.ItemsSource = pers;
        }

        private void listaActividad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClasesPorTabla.Actividad items = (ClasesPorTabla.Actividad)listaActividades.SelectedItem;

            txtidActividad.Text = items.IdActividad.ToString();
            txtNombre.Text = items.NombreActividad;
            txtDescripcionActividad.Text = items.DescripcionActividad;

            pivotPrincipal.SelectedItem = itemUpdate;

        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Menus/MenuActividad.xaml?", UriKind.Relative));
        }

        private void limpiar() {
            txtNombre.Text = String.Empty;
            txtDescripcionActividad.Text = String.Empty;
        }
    }
}