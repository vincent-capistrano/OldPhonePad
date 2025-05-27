using System;
using System.Text;
using System.Threading;

public class OldPhonePad
{
    private readonly string[] keyMap = {
        "", "", "ABC", "DEF", "GHI", "JKL", "MNO", "PQRS", "TUV", "WXYZ"
    };

    private StringBuilder result = new StringBuilder();
    private char lastKey = '\0';
    private int pressCount = 0;
    private DateTime lastPressTime = DateTime.MinValue;
    private readonly TimeSpan maxDelay = TimeSpan.FromSeconds(1);

    public string GetInput()
    {
        Console.WriteLine("Press keys (end with #, backspace with *):");

        while (true)
        {
            if (Console.KeyAvailable)
            {
                var keyInfo = Console.ReadKey(true);
                char keyChar = keyInfo.KeyChar;

                if (keyChar == '#') break;
                HandleKeyPress(keyChar);

                Console.Clear();
                Console.WriteLine("Current: " + result.ToString());
                if (pressCount > 0 && char.IsDigit(lastKey))
                    Console.WriteLine("Typing: " + GetCurrentChar());
            }
            else
            {
                if (ShouldCommit()) CommitBuffer();
                Thread.Sleep(50);
            }
        }

        CommitBuffer(); // Final flush
        return result.ToString();
    }

    private void HandleKeyPress(char keyChar)
    {
        if (keyChar == '*')
        {
            if (pressCount > 0) { pressCount = 0; lastKey = '\0'; }
            else if (result.Length > 0) result.Length--;
        }
        else if (keyChar == ' ')
        {
            CommitBuffer();
        }
        else if (char.IsDigit(keyChar))
        {
            var now = DateTime.Now;
            if (keyChar == lastKey && (now - lastPressTime) < maxDelay) pressCount++;
            else { CommitBuffer(); lastKey = keyChar; pressCount = 1; }

            lastPressTime = now;
        }
    }

    private bool ShouldCommit() =>
        lastKey != '\0' && (DateTime.Now - lastPressTime) > maxDelay;

    private void CommitBuffer()
    {
        if (lastKey != '\0' && char.IsDigit(lastKey))
        {
            result.Append(GetCurrentChar());
            pressCount = 0;
            lastKey = '\0';
        }
    }

    private char GetCurrentChar()
    {
        int digit = lastKey - '0';
        string letters = keyMap[digit];
        if (letters.Length == 0) return ' ';
        int index = (pressCount - 1) % letters.Length;
        return letters[index];
    }
}
