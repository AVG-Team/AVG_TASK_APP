using AVG_TASK_APP.CustomControls;
using AVG_TASK_APP.DataAccess;
using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.Repositories.Interface;
using AVG_TASK_APP.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
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

        public void ConfigureServices(IServiceCollection services)
        {
            // Other services...

            services.AddScoped<NotifyRepository>();

            // Add your DbContext configuration here.
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            userRepository = new UserRepository();

            var assembly = Assembly.GetExecutingAssembly();

            var registryKey = Registry.CurrentUser.OpenSubKey("Software\\" + assembly.GetCustomAttribute<AssemblyTitleAttribute>().Title + "\\Login", true);
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

            UserModel user = userRepository.GetByEmail(username);
            var identity = new ClaimsIdentity(new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Level", user.Level.ToString()),
            }, "ApplicationCookie");

            var principal = new ClaimsPrincipal(identity);
            Thread.CurrentPrincipal = principal;
            AppDomain.CurrentDomain.SetThreadPrincipal(principal);


            ManageTaskLayout pageLayout = new ManageTaskLayout(1);
            pageLayout.Show();
        }
    }
}
