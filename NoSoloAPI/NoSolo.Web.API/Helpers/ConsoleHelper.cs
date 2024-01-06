using System.Reflection;

namespace NoSolo.Web.API.Helpers;

public static class ConsoleHelper
{
    private static DateTime startedTime = DateTime.Now;
    
    public static void ShowStartAppTitle()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{startedTime} : App execution started");
        Console.ResetColor();
    }

    public static void ShowEndAppTitle()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n{DateTime.Now} : App execution finished");
        Console.WriteLine($"{(DateTime.Now - startedTime).TotalSeconds}:seconds\n\n");
        Console.ResetColor();
    }
    
    public static void ShowInfo(string context, WebApplicationBuilder builder)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;

        Console.WriteLine("\n\n");
        Console.WriteLine($"> DB context: {context}");
        Console.WriteLine($"> Environment: {builder.Environment.EnvironmentName}");
        Console.WriteLine($"> API Version: {DateTime.Now.Year - 2023}.{DateTime.Now.Month}.{DateTime.Now.Day}");
        Console.WriteLine($"> Assembly Version: {Assembly.GetExecutingAssembly().GetName().Version}");
        Console.WriteLine($"> Swagger: https://localhost:5000/swagger/index.html");
        Console.WriteLine("\n\n");
        Console.ResetColor();
    }
    
    public static void ShowMessage(string message, ConsoleColor color = ConsoleColor.Green)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void ShowError(string error)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(error);
        Console.ResetColor();
    }

    public static void ShowError(object error)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(error);
        Console.ResetColor();
    }
}