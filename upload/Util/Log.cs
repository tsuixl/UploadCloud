using System;

namespace Util
{
    public static class Log
    {
        public static ArgsData Content;

        public static void l (object o)
        {
            Debug(o.ToString(), ConsoleColor.White);
        } 


        public static void w (object o)
        {
            Debug (o.ToString(), ConsoleColor.Yellow);
        }


        public static void e (object o)
        {
            Debug (o.ToString(), ConsoleColor.Red);
        }

        public static void Debug (string str, ConsoleColor c)
        {
            str = ConvertStr (str, c);
            Console.ForegroundColor = c;
            Console.WriteLine (str);
            Console.ForegroundColor = ConsoleColor.White;
        }


        public static void Kill(string str)
        {
            if (Content.Jenkins)
            {   
                str = ConvertStr (str, ConsoleColor.Red);
            }
            
            throw new System.Exception (str);
        }

        public static string ConvertStr (string str, ConsoleColor c)
        {
            if (Content.Jenkins)
            {
                var j = GetJenkinsColor (c);
                str = string.Format(j, str);
            }
            return str;
        }


        public static string GetJenkinsColor (ConsoleColor color)
        {
            int c = 37;
            if (color == ConsoleColor.Red)
            {
                c = 31;
            }
            else if (color == ConsoleColor.Yellow)
            {
                c = 33;
            }
            else if (color == ConsoleColor.Green)
            {
                c = 32;
            }
            else if (color == ConsoleColor.Blue)
            {
                c = 34;
            }
            else
            {
                c = 37;
            }

            return string.Format("\u001b[{0}m{{0}}\u001b[0m", c);
        }

    }
}