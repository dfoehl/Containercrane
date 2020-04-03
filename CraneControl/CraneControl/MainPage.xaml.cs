
using CraneAPI;
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
            controller = new CraneController();
            DataContext = this;
        }

        private async void UrlChanged(object sender, TextChangedEventArgs e)
        {
            controller.BaseAdress = new Uri(urlBox.Text);
            await controller.Initialize();
        }

        private async void CraneRightPressed(object sender, PointerRoutedEventArgs e)
        {
            await controller.StartMoveAsync(EScope.Crane, EDirection.Right);
        }

        private async void CraneLeftPressed(object sender, PointerRoutedEventArgs e)
        {
            await controller.StartMoveAsync(EScope.Crane, EDirection.Left);
        }

        private async void CraneReleased(object sender, PointerRoutedEventArgs e)
        {
            await controller.StopMoveAsync(EScope.Crane);
        }

        private async void CrabRightPressed(object sender, PointerRoutedEventArgs e)
        {
            await controller.StartMoveAsync(EScope.Crab, EDirection.Right);
        }

        private async void CrabLeftPressed(object sender, PointerRoutedEventArgs e)
        {
            await controller.StartMoveAsync(EScope.Crab, EDirection.Left);
        }

        private async void CrabReleased(object sender, PointerRoutedEventArgs e)
        {
            await controller.StopMoveAsync(EScope.Crab);
        }

        private async void SpreaderUpPressed(object sender, PointerRoutedEventArgs e)
        {
            await controller.StartMoveAsync(EScope.Spreader, EDirection.Up);
        }

        private async void SpreaderDownPressed(object sender, PointerRoutedEventArgs e)
        {
            await controller.StartMoveAsync(EScope.Spreader, EDirection.Down);
        }

        private async void SpreaderReleased(object sender, PointerRoutedEventArgs e)
        {
            await controller.StopMoveAsync(EScope.Spreader);
        }
    }
}
