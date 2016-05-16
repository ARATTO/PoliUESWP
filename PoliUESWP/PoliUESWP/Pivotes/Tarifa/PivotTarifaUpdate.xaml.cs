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

namespace PoliUESWP.Pivotes.Tarifa
{
    public partial class PivotTarifaUpdate : PhoneApplicationPage
    {
        public PivotTarifaUpdate()
        {
            InitializeComponent();
            MostrarDatos();
        }
        string dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "db.sqlite");

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Menus/MenuTarifa.xaml?", UriKind.Relative));
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            MetodosSQLiteTarifa prueba = new MetodosSQLiteTarifa();

            if (txtCantPersonas.Text == String.Empty || txtTarifaUnica.Text == String.Empty)
            {
                MessageBox.Show("ERROR digite los datos faltantes");
            }
            else {
                String res = prueba.Update(dbPath, Int32.Parse(txtIdTarifa.Text), Int32.Parse(txtCantPersonas.Text), double.Parse(txtTarifaUnica.Text));
                MessageBox.Show(res);
                MostrarDatos();
                limpiar();
                pivotPrincipal.SelectedItem = itemDatos;
            }
        }
        private void limpiar() {
            txtCantPersonas.Text = String.Empty;
            txtTarifaUnica.Text = String.Empty;
        }
        public void MostrarDatos()
        {
            var db = new SQLiteConnection(dbPath);
            var pers = db.Table<ClasesPorTabla.Tarifa>().Where(c => c.IdTarifa > 0).ToList();
            listaTarifas.ItemsSource = pers;
        }

        private void listaTarifa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClasesPorTabla.Tarifa items = (ClasesPorTabla.Tarifa)listaTarifas.SelectedItem;

            txtIdTarifa.Text = items.IdTarifa.ToString();
            txtCantPersonas.Text = items.CantidadPersonas.ToString();
            txtTarifaUnica.Text = items.TarifaUnica.ToString();

            pivotPrincipal.SelectedItem = itemUpdate;
        }
    }
}