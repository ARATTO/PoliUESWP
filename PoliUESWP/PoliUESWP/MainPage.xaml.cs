using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PoliUESWP.Resources;
using System.IO;
using Windows.Storage;
using SQLite;
using PoliUESWP.ClasesPorTabla;

namespace PoliUESWP
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Código de ejemplo para traducir ApplicationBar
            //BuildLocalizedApplicationBar();
            AgregarDatos();
        }
        string dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "db.sqlite");

        private void btnActividad_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Menus/MenuActividad.xaml?", UriKind.Relative));
        }

        private void btnTarifa_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Menus/MenuTarifa.xaml?", UriKind.Relative));
        }

        private void btnArea_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Menus/MenuArea.xaml?", UriKind.Relative));
        }

        private void btnSolicitud_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Menus/MenuSolicitud.xaml?", UriKind.Relative));
        }


        public void AgregarDatos()
        {
            //Actividad
            using (var db = new SQLiteConnection(dbPath))
            {

                db.RunInTransaction(() =>
                {
                    db.Insert(new Actividad()
                    {
                        NombreActividad = "Academica",
                        DescripcionActividad = "Enseñanza y aprendizaje"
                    });
                    db.Insert(new Actividad()
                    {
                        NombreActividad = "Cultural",
                        DescripcionActividad = "Actividad Cultural"
                    });
                    db.Insert(new Actividad()
                    {
                        NombreActividad = "Deportiva",
                        DescripcionActividad = "Para hacer deportes :v "
                    });
                    db.Insert(new Actividad()
                    {
                        NombreActividad = "Politica",
                        DescripcionActividad = "Caracter Politico"
                    });
                });
            }
            //Tarifa
            using (var db = new SQLiteConnection(dbPath))
            {

                db.RunInTransaction(() =>
                {
                    db.Insert(new Tarifa()
                    {
                        CantidadPersonas = 10,
                        TarifaUnica = 10.0
                    });
                    db.Insert(new Tarifa()
                    {
                        CantidadPersonas = 30,
                        TarifaUnica = 50.0
                    });
                    db.Insert(new Tarifa()
                    {
                        CantidadPersonas = 50,
                        TarifaUnica = 100.0
                    });
                    db.Insert(new Tarifa()
                    {
                        CantidadPersonas = 100,
                        TarifaUnica = 150.5
                    });
                });
            }
            //Area
            using (var db = new SQLiteConnection(dbPath))
            {

                db.RunInTransaction(() =>
                {
                    db.Insert(new Area()
                    {
                        MaxPersonas = 30,
                        NombreArea = "Papi Futbol",
                        DescArea = "Cancha para Maitros"
                    });
                    db.Insert(new Area()
                    {
                        MaxPersonas = 50,
                        NombreArea = "Voleybol",
                        DescArea = "Jugar Voleybol"
                    });
                    db.Insert(new Area()
                    {
                        MaxPersonas = 60,
                        NombreArea = "BasketBall",
                        DescArea = "Jugar Basket :v"
                    });
                });
            }
            //Solicitud
            using (var db = new SQLiteConnection(dbPath))
            {

                db.RunInTransaction(() =>
                {
                    db.Insert(new Solicitud()
                    {
                        Motivo = "Jugar",
                        Fecha = "08/16/2016",
                        Actividad = 1,
                        Tarifa = 1
                    });
                    db.Insert(new Solicitud()
                    {
                        Motivo = "Graduacion",
                        Fecha = "06/06/2666",
                        Actividad = 2,
                        Tarifa = 3
                    });
                });
            }
            //Detalle Solicitud
            using (var db = new SQLiteConnection(dbPath))
            {

                db.RunInTransaction(() =>
                {
                    db.Insert(new DetalleSolicitud()
                    {
                        FechaInicio = "",
                        FechaFin = "",
                        CobroTotal = 500.48,
                        IDSolicitud = 1,
                        IDArea = 2
                    });
                    db.Insert(new DetalleSolicitud()
                    {
                        FechaInicio = "",
                        FechaFin = "",
                        CobroTotal = 84.7,
                        IDSolicitud = 2,
                        IDArea = 1
                    });
                });
            }

        }

        // Código de ejemplo para compilar una ApplicationBar traducida
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Establecer ApplicationBar de la página en una nueva instancia de ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Crear un nuevo botón y establecer el valor de texto en la cadena traducida de AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Crear un nuevo elemento de menú con la cadena traducida de AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}