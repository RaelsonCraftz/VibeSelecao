using System;
using System.Collections.Generic;
using Vibe.Mobile.ViewModels;
using Vibe.Mobile.Views;
using Xamarin.Forms;

namespace Vibe.Mobile
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

        // Este é um "hack" para possibilitar que o botão de hardware de voltar no Android envie o comando para o OnBackButtonPressed da página
        // Sem isso, o Shell executa a função sem que a página tome ciência do evento
        protected override bool OnBackButtonPressed()
        {
            var page = (Shell.Current?.CurrentItem?.CurrentItem as IShellSectionController)?.PresentedPage;

            if (page.SendBackButtonPressed())
                return true;
            else
                return base.OnBackButtonPressed();
        }
    }
}
