using System.Security.Cryptography;
using System.Text;

namespace GBBHash;

internal class Program
{
    public static SHA1 Sha1 = SHA1.Create();

    private static void Main(string[] args)
    {
        Console.WriteLine(ObfuscateString(args[0], args[1]));
    }

    protected static string ObfuscateString(string input, string seed)
    {
        var array = Encoding.UTF32.GetBytes(seed + input);
        array = Sha1.ComputeHash(array);
        return Sha1ToBeeStr(array);
    }

    protected static string Sha1ToBeeStr(byte[] hash)
    {
        var stringBuilder = new StringBuilder();
        var num2 = 4;
        var num3 = 0;
        for (var i = 0; i < 6; i++)
        {
            var num4 = (int)hash[i];
            var j = 8;
            while (j > 0)
            {
                if (num2 == 0)
                {
                    stringBuilder.Append(getChar(65 + num3));
                    num3 = 0;
                    num2 = 4;
                }

                var num5 = Math.Min(j, num2);
                var num6 = (int)Math.Pow(2.0, num5);
                num3 += num4 % num6;
                num2 -= num5;
                num3 <<= num2;
                j -= num5;
                num4 >>= num5;
            }
        }

        return stringBuilder.ToString();
    }

    protected static char getChar(int value)
    {
        var num = 0;
        foreach (var num2 in new[] { 33, 35, 36, 37, 38, 47, 92, 95, 46, 0 })
            if (value + num >= num2 && 65 <= num2)
                num++;

        return (char)(value + num);
    }
}