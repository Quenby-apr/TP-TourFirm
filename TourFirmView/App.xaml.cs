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

namespace TourFirmView
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>

    public partial class App : Application
    {
        public static OperatorViewModel Operator { get; set; }
        [STAThread]
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
            var Window = container.Resolve<WindowSignIn>();
            Window.ShowDialog();
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IExcursionStorage, ExcursionStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IGuideStorage, GuideStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IHaltStorage, HaltStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOperatorStorage, OperatorStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITourStorage, TourStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITravelStorage, TravelStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IReportStorage, ReportStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IImplementingStatistics, ImplementingStatistics>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<TravelLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<OperatorReportLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<TouristReportLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ExcursionLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<GuideLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<TourLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<HaltLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<MailLogic>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}