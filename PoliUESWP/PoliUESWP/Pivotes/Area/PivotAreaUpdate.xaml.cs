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

namespace PoliUESWP.Pivotes.Area
{
    public partial class PivotAreaUpdate : PhoneApplicationPage
    {
        public PivotAreaUpdate()
        {
            InitializeComponent();
            MostrarDatos();
        }
        string dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "db.sqlite");

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            MetodosSQLiteArea prueba = new MetodosSQLiteArea();
            if (txtDesArea.Text == String.Empty || txtMaxPersonas.Text == String.Empty)
            {
                MessageBox.Show("ERROR Ingrese datos faltantes");
            }
            else {
                String res = prueba.Update(dbPath, Int32.Parse(txtIdArea.Text), Int32.Parse(txtMaxPersonas.Text), txtNombreArea.Text, txtDesArea.Text);
                MessageBox.Show(res);
                MostrarDatos();
                limpiar();
                pivotPrincipal.SelectedItem = listaDatos;
            }
            
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Menus/MenuArea.xaml?", UriKind.Relative));
        }
        private void limpiar()
        {
            txtMaxPersonas.Text = String.Empty;
            txtNombreArea.Text = String.Empty;
            txtDesArea.Text = String.Empty;
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

            txtIdArea.Text = items.IdArea.ToString();
            txtMaxPersonas.Text = items.MaxPersonas.ToString();
            txtNombreArea.Text = items.NombreArea;
            txtDesArea.Text = items.DescArea;

            pivotPrincipal.SelectedItem = itemUpdate;
        }
    }
}