﻿using MealRandomizer.Models;
using Xamarin.Forms;

namespace MealRandomizer
{
    public partial class App : Application
    {
        internal static RandomProductsCollection RandomProductsCollection { get; } = new RandomProductsCollection(1000);

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
