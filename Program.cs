
namespace CustomerAPI
{
    public static class Program
    {

        /// <summary> Defines whether this application will run post setup or not </summary>
        public static bool Start { get; set; } = true;


        public static int Main(string[] args)
        {
            //Initialise the host
            var host = CreateHostBuilder(args).Build();


            //Start the host
            host.Run();

            //Return success!
            return 0;
        }

        /// <summary> Initialize the host </summary>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            //Initialize the builder
            var builder = Host.CreateDefaultBuilder(args);

            //Configure startup
            builder.ConfigureWebHostDefaults(x => x.UseStartup<Startup>());

            //Return the builder
            return builder;
        }
    }
}

