﻿using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace NoSolo.Web.API.Helpers;

[ExcludeFromCodeCoverage]
public static class ConsoleHelper
{
    private static readonly DateTime StartedTime = DateTime.Now;
    
    public static void ShowStartAppTitle()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{StartedTime} : App execution started");
        Console.ResetColor();
    }

    public static void ShowEndAppTitle()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n{DateTime.Now} : App execution finished");
        Console.WriteLine($"{(DateTime.Now - StartedTime).TotalSeconds}:seconds\n\n");
        Console.ResetColor();
    }
    
    public static void ShowInfo(string context, WebApplicationBuilder builder)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;

        Console.WriteLine("\n\n");
        Console.WriteLine($"> DB context: {context}");
        Console.WriteLine($"> Environment: {builder.Environment.EnvironmentName}");
        Console.WriteLine($"> API Version: {DateTime.UtcNow.Year - 2023}.{DateTime.UtcNow.Month}.{DateTime.UtcNow.Day}");
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