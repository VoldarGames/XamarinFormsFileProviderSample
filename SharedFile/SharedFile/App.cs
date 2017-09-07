using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace SharedFile
{
    public class App : Application
    {
        public App()
        {
            var fileContent = DependencyService.Get<IAccessFile>().ReadFile();
            // The root page of your application
            var content = new ContentPage
            {
                Title = "SharedFile Contents",
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                        new Label {
                            HorizontalTextAlignment = TextAlignment.Center,
                            Text = fileContent
                        },
                        new Button()
                        {
                            Text = "Go To ClientApp",
                            Command = new Command(() =>
                            {
                                DependencyService.Get<IGoToIntentApp>().GoToClientApp();
                            }) 
                        }
                    }
                }
            };

            MainPage = new NavigationPage(content);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
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
