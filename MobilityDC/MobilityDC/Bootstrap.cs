using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using MobilityDC.Services.NavigationService;
using MobilityDC.ViewModels;

namespace MobilityDC
{
    public class Bootstrap
    {
        public static void Initialize()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<HomeViewModel>().AsSelf();
            builder.RegisterType<BulkPickSearchViewModel>().AsSelf();
            builder.RegisterType<BulkPickViewModel>().AsSelf();
            builder.RegisterType<FinePickSearchViewModel>().AsSelf();
            builder.RegisterType<FinePickViewModel>().AsSelf();
            builder.RegisterType<StorePickSearchViewModel>().AsSelf();
            builder.RegisterType<StorePickViewModel>().AsSelf();
            builder.RegisterType<LoginViewModel>().AsSelf();
            builder.RegisterType<OptionMenuViewModel>().AsSelf();
            builder.RegisterType<NavigationService>().As<INavigationService>();

            IContainer container = builder.Build();

            AutofacServiceLocator serviceLocator = new AutofacServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
        }
    }
}
