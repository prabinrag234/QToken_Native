using QToken_Native.ViewModels;

namespace QToken_Native.Pages;

public partial class UserAuthentication : ContentPage
{
	private readonly UserAuthenticationViewModel _viewModel = new UserAuthenticationViewModel();
    private readonly BaseViewModel _baseViewModel = new BaseViewModel();
    public UserAuthentication()
	{
		InitializeComponent();
        BindingContext = _viewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _baseViewModel.CheckApiConnectivityAsync();
        await _viewModel.LoadSpecialtiesAsync();
        await _viewModel.InitializeViewStateAsync();
    }
}