using Kubis1982.Shared;

namespace Kubis1982
{
    public class StartBootstraper
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();

            builder.Services.AddModular(builder.Configuration, builder.Environment);

            var app = builder.Build();

            app.Run();
        }
    }
}