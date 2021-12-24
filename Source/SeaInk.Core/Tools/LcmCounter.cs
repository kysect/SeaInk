using System.Linq;

namespace SeaInk.Core.Tools
{
    internal static class LcmCounter
    {
        public static int Count(params int[] values)
        {
            if (!values.Any())
                return 0;

            int ans = values[0];

            for (int i = 1; i < values.Length; i++)
            {
                ans = (values[i] * ans) / Gcd(values[i], ans);
            }

            return ans;
        }

        private static int Gcd(int a, int b)
        {
            while (true)
            {
                if (b is 0)
                    return a;

                int a1 = a;
                a = b;
                b = a1 % b;
            }
        }
    }
}