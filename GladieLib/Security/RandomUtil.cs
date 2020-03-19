using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace GladieLib.security
{
    public sealed class RandomUtils
    {
        private static readonly char[] SafeChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();

        public static readonly RandomUtils Instance = new RandomUtils();

        //private readonly Random _fastRandom;
        private readonly RNGCryptoServiceProvider _securedRandom;

        private RandomUtils()
        {
            //_fastRandom = new Random();
            _securedRandom = new RNGCryptoServiceProvider();
        }

        public int SecuredInt()
        {
            var rndBytes = new byte[4];
            _securedRandom.GetBytes(rndBytes);
            return System.BitConverter.ToInt32(rndBytes, 0);
        }

        public int FastInt()
        {
            return Random.Range(int.MinValue, int.MaxValue);
        }

        public int FastInt(int maxValue)
        {
            return Random.Range(int.MinValue, int.MaxValue);
        }

        public int FastInt(int minInclusiveValue, int maxExclusiveValue)
        {
            return Random.Range(minInclusiveValue, maxExclusiveValue - 1);
        }

        public System.String FastString(int size)
        {
            var buffer = new StringBuilder(size);
            var safeLengh = SafeChars.Length - 1;

            while (buffer.Length < size)
            {
                buffer.Append(SafeChars[Random.Range(0, safeLengh)]);
            }
            return buffer.ToString();
        }
    }
}