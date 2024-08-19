using Microsoft.Extensions.Logging;
using TaskPlanner.MVVM.Views;
using TaskPlanner.Services;

namespace TaskPlanner
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Roboto-Regular.ttf", "Roboto");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            //register the DatabaseView
            builder.Services.AddSingleton<DatabaseService>();

            //register the MainView with the DatabaseService Dependency
            builder.Services.AddTransient<PrivacyPrinciples>();

            return builder.Build();
        }
    }
}
