using AVG_TASK_APP.DataAccess;
using AVG_TASK_APP.Models;
using AVG_TASK_APP.Repositories;
using AVG_TASK_APP.Repositories.Interface;
using AVG_TASK_APP.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using System;
using System.Reflection;
using System.Security.Claims;
using System.Threading;
using System.Windows;

namespace AVG_TASK_APP
{
    public partial class App : Application
    {
        private IUserRepository userRepository;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<NotifyRepository>();
            // Thêm cấu hình DbContext của bạn ở đây.
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            RepositoryBase repo = new RepositoryBase();

            if (!repo.IsServerConnected())
            {
                ShowErrorMessage("Server connection error, please check your network connection and try again !!!");
                return;
            }

            userRepository = new UserRepository();
            var assembly = Assembly.GetExecutingAssembly();
            var registryKey = Registry.CurrentUser.OpenSubKey("Software\\" + assembly.GetCustomAttribute<AssemblyTitleAttribute>().Title + "\\Login", true);

            if (registryKey == null || registryKey.GetValue("Username") == null || registryKey.GetValue("Password") == null)
            {
                ShowLoginView();
                return;
            }

            var username = (string)registryKey.GetValue("Username");
            var password = (string)registryKey.GetValue("Password");

            if (!userRepository.VerifyAccount(username, password))
            {
                ShowLoginView();
                registryKey.SetValue("Username", String.Empty);
                registryKey.SetValue("Password", String.Empty);
                return;
            }

            UserModel user = userRepository.GetByEmail(username);
            SetClaimsAndPrincipal(user);

            PageLayout pageLayout = new PageLayout();
            pageLayout.Show();
        }

        private void ShowErrorMessage(string message)
        {
            MessageBoxView msb = new MessageBoxView();
            msb.Show(message);
        }

        private void ShowLoginView()
        {
            LoginView loginView = new LoginView();
            loginView.Show();
        }

        private void SetClaimsAndPrincipal(UserModel user)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Level", user.Level.ToString()),
            }, "ApplicationCookie");

            var principal = new ClaimsPrincipal(identity);
            Thread.CurrentPrincipal = principal;
            AppDomain.CurrentDomain.SetThreadPrincipal(principal);
        }
    }
}
