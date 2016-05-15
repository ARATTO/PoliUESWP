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

namespace PoliUESWP.Pivotes.Actividad
{
    public partial class PivotActividadDelete : PhoneApplicationPage
    {
        public PivotActividadDelete()
        {
            InitializeComponent();
            MostrarDatos();
        }

        string dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "db.sqlite");


        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Menus/MenuActividad.xaml?", UriKind.Relative));
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtIdActividad.Text = String.Empty;
            txtPrioridad.Text = String.Empty;
            txtDescripcionActividad.Text = String.Empty;
        }

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            MetodosSQLiteActividad op = new MetodosSQLiteActividad();

            if (txtIdActividad.Text == String.Empty)
            {
                MessageBox.Show("Ingrese un ID");
            }
            else {
                String[] res = op.Delete(dbPath, Int32.Parse(txtIdActividad.Text));
                if (res != null)
                {
                    txtPrioridad.Text = res[1];
                    txtDescripcionActividad.Text = res[2];
                    MessageBox.Show(res[3]);
                    MostrarDatos();
                    pivotPrincipal.SelectedItem = itemDatos;
                }
                else {
                    MessageBox.Show("No Existe");
                }
            }
            
            

        }

        public void MostrarDatos() {
            var db = new SQLiteConnection(dbPath);
            var pers = db.Table<ClasesPorTabla.Actividad>().Where(c => c.IdActividad >= 0).ToList();
            listaActividades.ItemsSource = pers;
        }

        private void listaActividad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PoliUESWP.ClasesPorTabla.Actividad items = (ClasesPorTabla.Actividad)listaActividades.SelectedItem;

            txtIdActividad.Text = items.IdActividad.ToString();
            txtPrioridad.Text = items.NombreActividad;
            txtDescripcionActividad.Text = items.DescripcionActividad;

            pivotPrincipal.SelectedItem = itemDelete;
            ///De el array carga en pantalla
       
        }
    }
}