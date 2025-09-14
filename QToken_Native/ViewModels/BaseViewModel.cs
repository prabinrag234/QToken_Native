using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using QToken_Native.API;
using System.ComponentModel;

namespace QToken_Native.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        // Reusable client for this VM instance
        private readonly HttpClient _client = new()
        {
            BaseAddress = new Uri(APIHost.Host),
            Timeout = TimeSpan.FromSeconds(3)
        };

        // Method to check API connectivity with retries
        public async Task CheckApiConnectivityAsync(int retryIntervalSeconds = 5, int maxRetries = 5)
        {
            int attempt = 0;

            while (attempt < maxRetries)
            {
                try
                {
                    var response = await _client.GetAsync(APIEndpoints.IsAlive);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        await Toast.Make("Qtoken is online", ToastDuration.Short, 14).Show();
                        return; // stop retrying
                    }
                    else
                    {
                        await Toast.Make($"Qtoken error: {response.StatusCode}", ToastDuration.Short, 14).Show();
                    }
                }
                catch (Exception ex)
                {
                    await Toast.Make("Qtoken host not reachable", ToastDuration.Short, 14).Show();
                }
                attempt++;
                await Task.Delay(TimeSpan.FromSeconds(retryIntervalSeconds));
            }
            await Toast.Make("Max retries reached. API still unreachable.", ToastDuration.Long, 14).Show();
        }
    }
}
