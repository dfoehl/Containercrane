
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x407 dokumentiert.

namespace CraneControl
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        CraneController controller;

        public MainPage()
        {
            this.InitializeComponent();

        }

        private void UrlChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CrabLeftClicked(object sender, RoutedEventArgs e)
        {

        }

        private void SpreaderUpClicked(object sender, RoutedEventArgs e)
        {

        }

        private void CrabRightClicked(object sender, RoutedEventArgs e)
        {

        }

        private void SpreaderDownClicked(object sender, RoutedEventArgs e)
        {

        }

        private void CraneRightClicked(object sender, RoutedEventArgs e)
        {

        }

        private void CraneLeftClicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
