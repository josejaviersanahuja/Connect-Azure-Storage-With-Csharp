using Microsoft.Extensions.Configuration;

// See https://aka.ms/new-console-template for more information
namespace Connect_Azure_Storage_With_Csharp // Note: actual namespace depends on the project name.
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config= new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json", true, true)
                                    .Build();

            string getConectionString = config["connectionstring"];
            Console.WriteLine("Hello World!" + getConectionString);
        }
    }
}