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
    public partial class PivotTarifaInsert : PhoneApplicationPage
    {
        public PivotTarifaInsert()
        {
            InitializeComponent();
            MostrarDatos();
        }

        string dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "db.sqlite");

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Menus/MenuTarifa.xaml?", UriKind.Relative));
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            MetodosSQLiteTarifa op = new MetodosSQLiteTarifa();

            if (!(txtCantPersonas.Text == String.Empty || txtTarifaUnica.Text == String.Empty))
            {
                //no valida para truncar :C jodido Tiempo :v
                String res = op.Insert(dbPath, Int32.Parse(txtCantPersonas.Text), double.Parse(txtTarifaUnica.Text));
                MessageBox.Show(res);
                MostrarDatos();
                pivotPrincipal.SelectedItem = listaDatos;
                limpiar();
            }
            else {
                MessageBox.Show("Debe llenar los campos");
            }
            
        }
        public void MostrarDatos()
        {
            var db = new SQLiteConnection(dbPath);
            var pers = db.Table<PoliUESWP.ClasesPorTabla.Tarifa>().Where(c => c.IdTarifa >= 0).ToList();
            listaTarifas.ItemsSource = pers;
        }
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }
        private void limpiar()
        {
            txtCantPersonas.Text = String.Empty;
            txtTarifaUnica.Text = String.Empty;
        }
    }
}