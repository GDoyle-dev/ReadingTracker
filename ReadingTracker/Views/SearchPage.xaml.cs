using ReadingTracker.ViewModels;

namespace ReadingTracker.Views;

public partial class SearchPage : ContentPage
{
    private SearchViewModel _viewModel;

    public SearchPage()
    {
        InitializeComponent();
        _viewModel = new SearchViewModel();
        BindingContext = _viewModel;
    }

}
