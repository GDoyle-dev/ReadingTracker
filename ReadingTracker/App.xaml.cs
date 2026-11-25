using Microsoft.Extensions.DependencyInjection;

namespace ReadingTracker
{
    public partial class App : Application
    {
        public static HttpClient Http { get; } = new HttpClient();
        public App()
        {
            InitializeComponent();
            Http.Timeout = TimeSpan.FromSeconds(15);
            Http.DefaultRequestHeaders.UserAgent.ParseAdd("ReadingTracker/1.0");
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}