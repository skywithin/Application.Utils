using System;
using System.Text;

namespace Application.Utils
{
    public static class RandomIdGenerator
    {
        private static Random _random = new Random();
        private static char[] _baseChars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        public static string GetRandomId(int length = 15)
        {
            var sb = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                sb.Append(_baseChars[_random.Next(_baseChars.Length)]);
            }

            return sb.ToString();
        }
    }
}
