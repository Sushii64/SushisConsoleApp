using System;

namespace $safeprojectname$
{
    class Helpers
    {
    /// <summary>
    /// sparkly Console.ReadLine
    /// </summary>
    /// <remarks>
    /// yes, it actually works with (mostly) any key
    /// </remarks>
    public static void Pause()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("\nPress any key to continue...");
        Console.ResetColor();
        Console.ReadKey();
    }

    /// <summary>
    /// Console.WriteLine but red
    /// </summary>
    /// <param name="error">the error to write</param>
    /// <remarks>
    /// for use when something goes wrong but you can still continue
    /// </remarks>
    public static void WriteError(string error)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(error);
        Console.ResetColor();
    }

    /// <summary>
    /// writes an error and quits
    /// </summary>
    /// <param name="error">the error to write</param>
    /// <remarks>
    /// for use when something goes wrong but you can't continue
    /// </remarks>
    public static void Die(string error)
    {
        WriteError(error);
        Pause();
        Environment.Exit(0);
    }

    /// <summary>
    /// gets user input for a string and assigns it to a variable
    /// </summary>
    /// <param name="prompt">the string to print when asking for input</param>
    /// <returns>
    /// the string that the user input
    /// </returns>
    public static string ReadString(string prompt)
    {
        bool valid = false;
        string str = "";
        while (!valid)
        {
            Console.Write(prompt);
            str = Console.ReadLine();
            if (str != "" || str != null) { WriteError("Invalid string!"); }
            else
            {
                break;
            }
        }

        return str;
    }

    /// <summary>
    /// gets user input for an integer and assigns it to a variable
    /// </summary>
    /// <param name="num">the variable being assigned</param>
    /// <param name="prompt">the string to print when asking for input</param>
    /// <param name="min">the minimum value, if any</param>
    /// <param name="max">the maximum value, if any</param>
    /// <returns>
    /// the integer that the user input
    /// </returns>
    /// <remarks>
    ///     <para>
    ///         num must be preceded with out (e.g. int x = ReadInt(out x, "? ");)
    ///     </para>
    ///     <para>
    ///         min and max are optional
    ///     </para>
    /// </remarks>
    public static int ReadInt(out int num, string prompt, int min = int.MinValue, int max = int.MaxValue)
    {
        while (true)
        {
            Console.Write(prompt);
            
            if (int.TryParse(Console.ReadLine(), out num))
            {
                if (num >= min && num <= max)
                {
                    return num;
                }
            }

            WriteError("Invalid number!");
        }
    }


    /// <summary>
    /// gets user input for a double and assigns it to a variable
    /// </summary>
    /// <param name="num">the variable being assigned</param>
    /// <param name="prompt">the string to print when asking for input</param>
    /// <param name="min">the minimum value, if any</param>
    /// <param name="max">the maximum value, if any</param>
    /// <returns>
    /// the double that the user input
    /// </returns>
    /// <remarks>
    ///     <para>
    ///         num must be preceded with out (e.g. double x = ReadDouble(out x, "? ");)
    ///     </para>
    ///     <para>
    ///         min and max are optional
    ///     </para>
    /// </remarks>
    public static double ReadDouble(out double num, string prompt, double min = double.MinValue, double max = double.MaxValue)
    {
        while (true)
        {
            Console.Write(prompt);

            if (double.TryParse(Console.ReadLine(), out num))
            {
                if (num >= min && num <= max)
                {
                    return num;
                }
            }

            WriteError("Invalid number!");
        }
    }

    /// <summary>
    /// gets user input for a true or false value and assigns it to a variable
    /// </summary>
    /// <param name="prompt">the string to print when asking for input</param>
    /// <param name="posValue">the value that returns true, defaults to "y"</param>
    /// <param name="negValue">the value that returns false, defaults to "n"</param>
    /// <returns>
    /// true or false, depending on what you input
    /// </returns>
    /// <remarks>
    ///     <para>
    ///         posValue and negValue are optional
    ///     </para>
    ///     <para>
    ///         technically, everything other than the positive value will return false
    ///     </para>
    /// </remarks>
    public static bool ReadBool(string prompt, string posValue = "y", string negValue = "n")
    {
        bool val;
        Console.Write("\n" + prompt + $" {posValue}/{negValue} ");
        string s = Console.ReadLine();
        if (s == posValue)
        {
            val = true;
        }
        else
        {
            val = false;
        }

        return val;
    }

    /// <summary>
    /// a pretty 'do it again' loop!
    /// </summary>
    /// <param name="program">the code to run in the loop.</param>
    /// <param name="posValue">the value that does 'it' again, defaults to "y"</param>
    /// <param name="negValue">the value that breaks the loop, defaults to "n"</param>
    /// <remarks>
    ///     <para>
    ///         posValue and negValue are optional
    ///     </para>
    ///     <para>
    ///         technically, everything other than the positive value will return break the loop
    ///     </para>
    ///     <para>
    ///         the syntax for writing a function like this is () => { code goes here }
    ///     </para>
    /// </remarks>
    public static void DoItAgain(Action program, string posValue = "y", string negValue = "n")
    {
        bool again = true;
        while (again)
        {
            program();
            again = ReadBool("Do it again?");
            Console.Clear();
        }
    }

    /// <summary>
    /// creates a menu where you can type a number to pick an option
    /// </summary>
    /// <param name="choices">the choices</param>
    /// <returns>
    /// an integer, the index of the choice
    /// </returns>
    /// <remarks>
    ///     <para>
    ///         see https://www.w3schools.com/cs/cs_arrays.php
    ///     </para>
    ///     <para>
    ///         you CAN put the array inside of the function instead of just a variable that is an array
    ///     </para>
    ///     <para>
    ///         you just won't be able to access the actual text of the option you picked, since the method returns an int
    ///     </para>
    /// </remarks>
    public static int MenuChoice(string[] options)
    {
        int i = 0;
        foreach (string option in options)
        {
            i++;
            Console.WriteLine($"{i}: {option}");
        }
        ReadInt(out int choice, "Select an option: ", options.Length, 1);
        return choice - 1;
    }

    /// <summary>
    /// Console.Write but with a colour
    /// </summary>
    /// <param name="color">the colour to write the text in</param>
    /// <param name="text">the text to write</param>
    public static void ColorWrite(ConsoleColor color, string text) {
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ResetColor();
    }

    /// <summary>
    /// Console.WriteLine but with a colour
    /// </summary>
    /// <param name="color">the colour to write the text in</param>
    /// <param name="text">the text to write</param>
    public static void ColorWriteLine(ConsoleColor color, string text) {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }
    }
}
