namespace MonkeyFinder;

public partial class DersPage : ContentPage
{
	public DersPage(DersViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}