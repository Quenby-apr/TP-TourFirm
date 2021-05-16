using System;
using System.Configuration;
using System.Windows;
using TourFirmBusinessLogic.BusinessLogic;
using TourFirmBusinessLogic.HelperModels;
using TourFirmBusinessLogic.Interfaces;
using TourFirmBusinessLogic.ViewModels;
using TourFirmDatabaseImplement.Implements;
using Unity;
using Unity.Lifetime;

namespace TouristTourFirmView
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static TouristViewModel Tourist { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var container = BuildUnityContainer();

            MailLogic.MailConfig(new MailConfig
            {
                SmtpClientHost = ConfigurationManager.AppSettings["SmtpClientHost"],
                SmtpClientPort = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpClientPort"]),
                MailLogin = ConfigurationManager.AppSettings["MailLogin"],
                MailPassword = ConfigurationManager.AppSettings["MailPassword"],
                MailName = ConfigurationManager.AppSettings["MailName"]
            });

            var authWindow = container.Resolve<WindowSignIn>();
            authWindow.ShowDialog();
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<ITravelStorage, TravelStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IExcursionStorage, ExcursionStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IPlaceStorage, PlaceStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IGuideStorage, GuideStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITouristStorage, TouristStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITourStorage, TourStorage>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<TravelLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<TouristReportLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ExcursionLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<PlaceLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<GuideLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<TouristLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<TourLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<MailLogic>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}