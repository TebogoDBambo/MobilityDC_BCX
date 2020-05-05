using System;
using CommonServiceLocator;

namespace MobilityDC.ViewModels
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            Bootstrap.Initialize();
        }

        public HomeViewModel Home
        {
            get { return ServiceLocator.Current.GetInstance<HomeViewModel>(); }
        }

        public BulkPickSearchViewModel BulkPickSearch
        {
            get { return ServiceLocator.Current.GetInstance<BulkPickSearchViewModel>(); }
        }

        public BulkPickViewModel BulkPick
        {
            get { return ServiceLocator.Current.GetInstance<BulkPickViewModel>(); }
        }

        public FinePickSearchViewModel FinePickSearch
        {
            get { return ServiceLocator.Current.GetInstance<FinePickSearchViewModel>(); }
        }

        public FinePickViewModel FinePick
        {
            get { return ServiceLocator.Current.GetInstance<FinePickViewModel>(); }
        }

        public StorePickSearchViewModel StorePickSearch
        {
            get { return ServiceLocator.Current.GetInstance<StorePickSearchViewModel>(); }
        }

        public StorePickViewModel StorePick
        {
            get { return ServiceLocator.Current.GetInstance<StorePickViewModel>(); }
        }

        public LoginViewModel Login
        {
            get { return ServiceLocator.Current.GetInstance<LoginViewModel>(); }
        }

        public OptionMenuViewModel OptionMenu
        {
            get { return ServiceLocator.Current.GetInstance<OptionMenuViewModel>(); }
        }


    }
}
