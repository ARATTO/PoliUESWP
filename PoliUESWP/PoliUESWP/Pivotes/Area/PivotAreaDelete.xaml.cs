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
    public partial class PivotAreaDelete : PhoneApplicationPage
    {
        public PivotAreaDelete()
        {
            InitializeComponent();
            MostrarDatos();
        }
        string dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "db.sqlite");

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            MetodosSQLiteArea op = new MetodosSQLiteArea();

            if (txtIdArea.Text == String.Empty)
            {
                MessageBox.Show("Ingrese un ID");
            }
            else {
                String[] res = op.Delete(dbPath, Int32.Parse(txtIdArea.Text));
                if (res != null)
                {
                    txtMaxPersonas.Text = res[1];
                    txtNombreArea.Text = res[2];
                    txtDesArea.Text = res[3];
                    MessageBox.Show(res[4]);
                    MostrarDatos();
                    pivotPrincipal.SelectedItem = itemDelete;
                }
                else {
                    MessageBox.Show("No Existe");
                }
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtMaxPersonas.Text = String.Empty;
            txtNombreArea.Text = String.Empty;
            txtDesArea.Text = String.Empty;
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Menus/MenuArea.xaml?", UriKind.Relative));
        }
        public void MostrarDatos()
        {
            var db = new SQLiteConnection(dbPath);
            var pers = db.Table<ClasesPorTabla.Area>().Where(c => c.IdArea >= 0).ToList();
            listaArea.ItemsSource = pers;
        }

        private void listaArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClasesPorTabla.Area items = (ClasesPorTabla.Area)listaArea.SelectedItem;

            txtIdArea.Text = items.IdArea.ToString();
            txtMaxPersonas.Text = items.IdArea.ToString();
            txtNombreArea.Text = items.NombreArea;
            txtDesArea.Text = items.DescArea;

            pivotPrincipal.SelectedItem = itemConsulta;
        }
    }
}