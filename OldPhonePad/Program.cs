using System;

class Program
{
    static void Main()
    {
        var pad = new OldPhonePad();
        string output = pad.GetInput();

        Console.WriteLine($"\nFinal Output: {output}");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}