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
    public partial class PivotActividadConsulta : PhoneApplicationPage
    {
        public PivotActividadConsulta()
        {
            InitializeComponent();
            mostrarDatos();

        }

        string dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "db.sqlite");

        public int priori;

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtidActividad.Text = String.Empty;
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Menus/MenuActividad.xaml?", UriKind.Relative));
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            MetodosSQLiteActividad op = new MetodosSQLiteActividad();

            if (txtidActividad.Text == String.Empty)
            {
                MessageBox.Show("ERROR digite su un ID valido");
            }
            else {
                String[] res = op.Consulta(dbPath, Int32.Parse(txtidActividad.Text));

                txtNombre.Text = res[1];
                txtDescripcionActividad.Text = res[2];

                if (res[0] != string.Empty)
                {
                    mostrarDatosUnicos(Int32.Parse(res[0]));
                    pivotPrincipal.SelectedItem = itemConsulta;
                }
                else
                {
                    mostrarDatos();
                    MessageBox.Show("La Actividad no existe");
                }
            }

        }

        public void mostrarDatosUnicos(int idActividad)
        {
            var db = new SQLiteConnection(dbPath);
            var pers = db.Table<PoliUESWP.ClasesPorTabla.Actividad>().Where(c => c.IdActividad == idActividad).ToList();
            listaActividades.ItemsSource = pers;
        }

        public void mostrarDatos()
        {
            var db = new SQLiteConnection(dbPath);
            var pers = db.Table<ClasesPorTabla.Actividad>().Where(c => c.IdActividad > 0).ToList();
            listaActividades.ItemsSource = pers;
        }

        private void listaActividad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClasesPorTabla.Actividad items = (ClasesPorTabla.Actividad)listaActividades.SelectedItem;

            txtNombre.Text = String.Empty;
            txtDescripcionActividad.Text = String.Empty;

            pivotPrincipal.SelectedItem = itemConsulta;
            MessageBox.Show("Encontrado ...");
            mostrarDatosUnicos(items.IdActividad);

        }

    }
}