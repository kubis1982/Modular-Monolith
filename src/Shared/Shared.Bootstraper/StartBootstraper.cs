using ModularMonolith.Shared;
using ModularMonolith.Shared.Extensions;

namespace ModularMonolith
{
    public class StartBootstraper
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder().Initialize();

            builder.Services.AddModular(builder.Configuration, builder.Environment);

            var app = builder.Build();

            app.UseModular(app.Environment);

            app.Run();
        }
    }
}
