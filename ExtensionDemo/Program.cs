using System;

namespace ExtensionDemo
{
    //Writing multiple classes in the same file for sake of having everything on one screen for the demo.
    //OPEN FOR EXTENSION CLOSED FOR MODIFICATION
    class Program
    {
        static void Main(string[] args)
        {
            #region String extension Demo
            //Since we extended the string type we now see our new method.
            //Be careful extending microsoft types
            string demo = "This is a Demo.";
            demo.PrintToConsole();

            "This is a Demo".PrintToConsole();
            #endregion

            #region SimpleLogger Extension Demo
            ISimpleLogger logger = new SimpleLogger();

            logger.Log("ERror", "This is a normal Error with the default library where mistakes with logging type can happen");
            //Use our extension to consistently/cleanly log
            logger.LogError("This is an Error");
            logger.LogWarning("This is a Warning");
            #endregion
        }
    }

    //Has to be static. Normally would place this is some kind of toolbox folder
    public static class Extensions
    {
        public static void PrintToConsole(this string message)
        {
            Console.WriteLine(message);
        }
    }

    public static class ExtendSimpleLogger
    {
        //With our new extension method we can provide consistency in the poorly written 3rd party class.
        public static void LogError(this ISimpleLogger logger, string message)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            logger.Log(message, "Error");
            Console.ForegroundColor = defaultColor;
        }

        public static void LogWarning(this ISimpleLogger logger, string message)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            logger.Log(message, "Warning");
            Console.ForegroundColor = defaultColor;
        }
    }

    //Simulate a 3rd party logging class that we DO NOT have access to modify directly.
    //So instead we will be writing extensions for use in our product.
    //Extend anything that implements ISimpleLogger
    public class SimpleLogger : ISimpleLogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        public void Log(string message, string messageType)
        {
            Log($"{messageType}: {message}");
        }
    }
}
