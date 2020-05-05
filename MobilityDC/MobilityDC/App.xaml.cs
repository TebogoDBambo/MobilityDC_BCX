using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobilityDC.Services;
using MobilityDC.Views;
using Autofac.Core;
using MobilityDC.Models.Commons;

namespace MobilityDC
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();           

            MainPage = new NavigationPage(new SplashScreen());            
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            AppSession.CompanyId = "MOS";
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
