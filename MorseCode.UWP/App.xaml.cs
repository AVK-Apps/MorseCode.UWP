using MorseCode.UWP.Classes;
using MorseCode.UWP.Views;
using System;
using System.Diagnostics;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MorseCode.UWP
{
    sealed partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
        }

        public static Settings Settings;

        protected async override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Settings = await SettingsHelper.GetSettings();

            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;

            ApplicationView.GetForCurrentView().TitleBar.ButtonForegroundColor = (Color)Current.Resources["SystemAccentColor"];
            ApplicationView.GetForCurrentView().TitleBar.ButtonBackgroundColor = Colors.Transparent;
            ApplicationView.GetForCurrentView().TitleBar.ButtonHoverForegroundColor = (Color)Current.Resources["SystemAccentColor"];
            ApplicationView.GetForCurrentView().TitleBar.ButtonHoverBackgroundColor = Colors.Transparent;

            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    if (Settings.ClassicView)
                    {
                        rootFrame.Navigate(typeof(MainPageTab));
                    }
                    else
                    {
                        rootFrame.Navigate(typeof(MainPage));
                    }
                }
            }

            Window.Current.Activate();
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            SettingsHelper.SaveSettings(Settings);

            deferral.Complete();
        }
    }
}
