using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TourFirmBusinessLogic.BusinessLogic;
using TourFirmBusinessLogic.Interfaces;
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
        [STAThread]
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var container = BuildUnityContainer();
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
            currentContainer.RegisterType<ExcursionLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<GuideLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<HaltLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<OperatorLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<TourLogic>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
