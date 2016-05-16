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
    public partial class PivotTarifaDelete : PhoneApplicationPage
    {
        public PivotTarifaDelete()
        {
            InitializeComponent();
            MostrarDatos();
        }
        string dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "db.sqlite");


        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            MetodosSQLiteTarifa op = new MetodosSQLiteTarifa();

            if (txtIdTarifa.Text == String.Empty)
            {
                MessageBox.Show("Ingrese un ID");
            }
            else {
                String[] res = op.Delete(dbPath, Int32.Parse(txtIdTarifa.Text));
                if (res != null)
                {
                    txtNumeroPersonas.Text = res[1];
                    txtTarifaUnica.Text = res[2];
                    MessageBox.Show(res[3]);
                    MostrarDatos();
                    pivotPrincipal.SelectedItem = itemDatos;
                }
                else {
                    MessageBox.Show("No Existe");
                }
            }
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Menus/MenuTarifa.xaml?", UriKind.Relative));
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtIdTarifa.Text = String.Empty;
            txtNumeroPersonas.Text = String.Empty;
            txtTarifaUnica.Text = String.Empty;
        }
        public void MostrarDatos()
        {
            var db = new SQLiteConnection(dbPath);
            var pers = db.Table<ClasesPorTabla.Tarifa>().Where(c => c.IdTarifa >= 0).ToList();
            listaTarifas.ItemsSource = pers;
        }

        private void listaTarifas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClasesPorTabla.Tarifa items = (ClasesPorTabla.Tarifa)listaTarifas.SelectedItem;

            txtIdTarifa.Text = items.IdTarifa.ToString();
            txtNumeroPersonas.Text = items.CantidadPersonas.ToString();
            txtTarifaUnica.Text = items.TarifaUnica.ToString();

            pivotPrincipal.SelectedItem = itemDelete;
            ///De el array carga en pantalla

        }
    }
}