﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace PoliUESWP.Menus
{
    public partial class MenuActividad : PhoneApplicationPage
    {
        public MenuActividad()
        {
            InitializeComponent();
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pivotes/Actividad/PivotActividadUpdate.xaml?", UriKind.Relative));
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pivotes/Actividad/PivotActividadInsert.xaml?", UriKind.Relative));
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pivotes/Actividad/PivotActividadDelete.xaml?", UriKind.Relative));
        }

        private void btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pivotes/Actividad/PivotActividadConsulta.xaml?", UriKind.Relative));
        }

        private void btnPrincipal_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml?", UriKind.Relative));
        }
    }
}