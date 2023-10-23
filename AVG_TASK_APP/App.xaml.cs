using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AVG_TASK_APP
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IUserRepository userRepository;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            userRepository = new UserRepository();

            var assembly = Assembly.GetExecutingAssembly();

            var registryKey = Registry.CurrentUser.OpenSubKey("Software\\" + assembly.GetCustomAttribute<AssemblyTitleAttribute>().Title + "\\Login");
            if (registryKey == null)
            {
                LoginView loginView = new LoginView();
                loginView.Show();
                return;
            }

            var username = (string)registryKey.GetValue("Username", String.Empty);
            var password = (string)registryKey.GetValue("Password", String.Empty);


            if (username == null || password == null)
            {
                LoginView loginView = new LoginView();
                loginView.Show();
                return;
            }

            var isValidUser = userRepository.verifyAccount(username, password.ToString());
            if (!isValidUser)
            {
                LoginView loginView = new LoginView();
                loginView.Show();

                registryKey.SetValue("Username", String.Empty);
                registryKey.SetValue("Password", String.Empty);
                return;
            }

            Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
            PageLayout layout = new PageLayout();
            layout.Show();
        }
    }
}
