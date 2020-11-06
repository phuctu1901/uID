using Acr.UserDialogs;
using Hyperledger.Aries.Agents;
using IdentifyMe.Extensions;
using IdentifyMe.Models.Setting;
using IdentifyMe.Services.Interfaces;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace IdentifyMe.ViewModels.Setting
{
    public class SettingViewModel : ABaseViewModel
    {
        public SettingViewModel(IUserDialogs userDialogs, 
            INavigationService navigationService) : 
            base (nameof(SettingViewModel), userDialogs, navigationService)
        {
            Title = "Setting";
            IList<NetworkItem> networkItems = new List<NetworkItem>()
            {                
                new NetworkItem("Bcovrin Test", "bcovrin-test"),
                //new NetworkItem("IdentifyMe Test Network", "identifyme-test"),
                //new NetworkItem("Bcovrin Green Dev", "bcovrin-dev")
                new NetworkItem("Sovrin Staging Network", "sovrin-staging"),
                new NetworkItem("Sovrin Builder Network", "sovrin-builder"),

                //  case "sovrin-staging": return "Sovrin Staging Network";
                //case "sovrin-live": return "Sovrin Network";
                //case "sovrin-builder": return "Sovrin Builder Network";
                //case "bcovrin-test": return "Bcovrin Test Network";
        };
            _networkItems.InsertRange(networkItems);
            string selectedNetwork = Preferences.Get("PoolConfigurationName", "");
            _selectedNetworkItem = networkItems.SingleOrDefault(s => s.PoolName == selectedNetwork);
        }

        public override async Task InitializeAsync(object navigationData)
        {
            
            await base.InitializeAsync(navigationData);         
        }

        private NetworkItem _selectedNetworkItem;

        public NetworkItem SelectedNetworkItem
        {
            get => _selectedNetworkItem;
            set 
            {
                this.RaiseAndSetIfChanged(ref _selectedNetworkItem, value);
                Preferences.Set("PoolConfigurationName", _selectedNetworkItem.PoolName);
            }
        }

        private RangeEnabledObservableCollection<NetworkItem> _networkItems = new RangeEnabledObservableCollection<NetworkItem>();

        public RangeEnabledObservableCollection<NetworkItem> NetworkItems
        {
            get => this._networkItems;
            set => this.RaiseAndSetIfChanged(ref _networkItems, value);
        }


    }
}
