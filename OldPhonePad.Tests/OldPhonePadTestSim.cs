using System;
using System.Text;

public class OldPhonePadTestSim
{
    private readonly string[] keyMap = {
        "", "", "ABC", "DEF", "GHI", "JKL", "MNO", "PQRS", "TUV", "WXYZ"
    };

    public string Simulate(string sequence)
    {
        StringBuilder result = new();
        char lastKey = '\0';
        int pressCount = 0;

        foreach (char key in sequence)
        {
            if (key == '#')
            {
                Commit(ref result, keyMap, lastKey, pressCount);
                break;
            }
            else if (key == '*')
            {
                if (pressCount > 0) { pressCount = 0; lastKey = '\0'; }
                else if (result.Length > 0) result.Length--;
            }
            else if (key == ' ')
            {
                Commit(ref result, keyMap, lastKey, pressCount);
                pressCount = 0;
                lastKey = '\0';
            }
            else if (char.IsDigit(key))
            {
                if (key == lastKey) pressCount++;
                else
                {
                    Commit(ref result, keyMap, lastKey, pressCount);
                    lastKey = key;
                    pressCount = 1;
                }
            }
        }

        return result.ToString();
    }

    private void Commit(ref StringBuilder result, string[] map, char key, int count)
    {
        if (key == '\0' || count == 0) return;
        int digit = key - '0';
        string letters = map[digit];
        if (letters.Length == 0) return;
        int index = (count - 1) % letters.Length;
        result.Append(letters[index]);
    }
}
