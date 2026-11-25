using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ReadingTracker.Models;
using ReadingTracker.Services;

namespace ReadingTracker.ViewModels
{
    internal class SearchViewModel : INotifyPropertyChanged
    {
        private readonly BookSearchService _search = new BookSearchService();
        private string _query = "";
        private bool _isBusy;
        private string _message = "";

        public ObservableCollection<SearchDoc> Results { get; } = new();
        public Command SearchCommand { get; }

        public SearchViewModel()
        {
            SearchCommand = new Command(async () => await SearchAsync());
        }

        public string Query
        {
            get => _query;
            set
            {
                if (_query == value)
                    return;
                _query = value;
                OnPropertyChanged();
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (_isBusy == value)
                    return;
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public string Message
        {
            get => _message;
            set
            {
                if (_message == value)
                    return;
                _message = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasMessage));
            }
        }

        public bool HasMessage => !string.IsNullOrEmpty(Message);


        private async Task SearchAsync()
        {
            if (IsBusy)
                return;

            string q = (Query ?? "").Trim();
            if (q == "")
            {
                Message = "Type a title to search.";
                return;
            }

            try
            {
                IsBusy = true;
                Message = "";
                Results.Clear();

                List<SearchDoc> docs = await _search.SearchByTitleAsync(q);
                foreach (SearchDoc doc in docs)
                    Results.Add(doc);

                if (docs.Count == 0)
                    Message = $"No books found for \"{q}\".";
            }
            catch (Exception)
            {
                Message = "Could not retrieve books. Check your connection and try again.";
            }
            finally
            {
                IsBusy = false;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
