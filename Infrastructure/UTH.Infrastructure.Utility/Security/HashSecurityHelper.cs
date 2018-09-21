using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Runtime.CompilerServices;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// Hash算法加/解密
    /// Rfc2898算法(eg:asp.net core identity)
    /// </summary>
    public static class HashSecurityHelper
    {
        /* =======================
         * HASHED str FORMATS
         * =======================
         * 
         * Version 2:
         * PBKDF2 with HMAC-SHA1, 128-bit salt, 256-bit subkey, 1000 iterations.
         * (See also: SDL crypto guidelines v5.1, Part III)
         * Format: { 0x00, salt, subkey }
         *
         * Version 3:
         * PBKDF2 with HMAC-SHA256, 128-bit salt, 256-bit subkey, 10000 iterations.
         * Format: { 0x01, prf (UInt32), iter count (UInt32), salt length (UInt32), salt, subkey }
         * (All UInt32s are stored big-endian.)
         */

        /// <summary>
        /// 获取加密结果
        /// </summary>
        /// <param name="str">待加密字符串</param>
        /// <returns></returns>
        public static string GetSecurity(string str, HashSecurityCompatibilityMode mode = HashSecurityCompatibilityMode.V3)
        {
            str.CheckNull();

            RandomNumberGenerator _rng = RandomNumberGenerator.Create();

            if (mode == HashSecurityCompatibilityMode.V2)
            {
                return Convert.ToBase64String(GetSecurityV2(str, _rng));
            }
            else
            {
                return Convert.ToBase64String(GetSecurityV3(str, _rng));
            }
        }

        private static byte[] GetSecurityV2(string str, RandomNumberGenerator rng)
        {
            const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA1; // default for Rfc2898DeriveBytes
            const int Pbkdf2IterCount = 1000; // default for Rfc2898DeriveBytes
            const int Pbkdf2SubkeyLength = 256 / 8; // 256 bits
            const int SaltSize = 128 / 8; // 128 bits

            // Produce a version 2 (see comment above) text hash.
            byte[] salt = new byte[SaltSize];
            rng.GetBytes(salt);
            byte[] subkey = KeyDerivation.Pbkdf2(str, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength);

            var outputBytes = new byte[1 + SaltSize + Pbkdf2SubkeyLength];
            outputBytes[0] = 0x00; // format marker
            Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, Pbkdf2SubkeyLength);
            return outputBytes;
        }

        private static byte[] GetSecurityV3(string str, RandomNumberGenerator rng)
        {
            //获取或设置使用PBKDF2的密码时使用的迭代次数。默认是10000。
            int _iterCount = 10000;

            return GetSecurityV3(str, rng,
                prf: KeyDerivationPrf.HMACSHA256,
                iterCount: _iterCount,
                saltSize: 128 / 8,
                numBytesRequested: 256 / 8);
        }

        private static byte[] GetSecurityV3(string str, RandomNumberGenerator rng, KeyDerivationPrf prf, int iterCount, int saltSize, int numBytesRequested)
        {
            // Produce a version 3 (see comment above) text hash.
            byte[] salt = new byte[saltSize];
            rng.GetBytes(salt);
            byte[] subkey = KeyDerivation.Pbkdf2(str, salt, prf, iterCount, numBytesRequested);

            var outputBytes = new byte[13 + salt.Length + subkey.Length];
            outputBytes[0] = 0x01; // format marker
            WriteNetworkByteOrder(outputBytes, 1, (uint)prf);
            WriteNetworkByteOrder(outputBytes, 5, (uint)iterCount);
            WriteNetworkByteOrder(outputBytes, 9, (uint)saltSize);
            Buffer.BlockCopy(salt, 0, outputBytes, 13, salt.Length);
            Buffer.BlockCopy(subkey, 0, outputBytes, 13 + saltSize, subkey.Length);
            return outputBytes;
        }

        private static uint ReadNetworkByteOrder(byte[] buffer, int offset)
        {
            return ((uint)(buffer[offset + 0]) << 24)
                | ((uint)(buffer[offset + 1]) << 16)
                | ((uint)(buffer[offset + 2]) << 8)
                | ((uint)(buffer[offset + 3]));
        }


        /// <summary>
        /// 校验加密(eg: providedStr[加密后] == hashedStr)
        /// </summary>
        /// <param name="hashedStr">己加密字符串</param>
        /// <param name="providedStr">待验证字符串</param>
        /// <returns></returns>
        public static bool VerifySecurity(string hashedStr, string providedStr)
        {
            hashedStr.CheckNull();
            providedStr.CheckNull();

            byte[] decodedHashedStr = Convert.FromBase64String(hashedStr);

            if (decodedHashedStr.Length == 0)
            {
                return false;
            }

            switch (decodedHashedStr[0])
            {
                case 0x00:
                    if (VerifySecurityV2(decodedHashedStr, providedStr))
                    {
                        return true;
                        // This is an old str hash format - the caller needs to rehash if we're not running in an older compat mode.
                        //return (_compatibilityMode == strHasherCompatibilityMode.IdentityV3) ? strVerificationResult.SuccessRehashNeeded : strVerificationResult.Success;
                    }
                    else
                    {
                        return false;
                    }

                case 0x01:
                    int embeddedIterCount;
                    if (VerifySecurityV3(decodedHashedStr, providedStr, out embeddedIterCount))
                    {
                        return true;
                        // If this hasher was configured with a higher iteration count, change the entry now.
                        //return (embeddedIterCount < _iterCount) ? strVerificationResult.SuccessRehashNeeded : strVerificationResult.Success;
                    }
                    else
                    {
                        return false;
                    }

                default:
                    return false;
            }
        }

        private static bool VerifySecurityV2(byte[] hashedStr, string str)
        {
            const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA1; // default for Rfc2898DeriveBytes
            const int Pbkdf2IterCount = 1000; // default for Rfc2898DeriveBytes
            const int Pbkdf2SubkeyLength = 256 / 8; // 256 bits
            const int SaltSize = 128 / 8; // 128 bits

            // We know ahead of time the exact length of a valid hashed str payload.
            if (hashedStr.Length != 1 + SaltSize + Pbkdf2SubkeyLength)
            {
                return false; // bad size
            }

            byte[] salt = new byte[SaltSize];
            Buffer.BlockCopy(hashedStr, 1, salt, 0, salt.Length);

            byte[] expectedSubkey = new byte[Pbkdf2SubkeyLength];
            Buffer.BlockCopy(hashedStr, 1 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

            // Hash the incoming str and verify it
            byte[] actualSubkey = KeyDerivation.Pbkdf2(str, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength);
            return ByteArraysEqual(actualSubkey, expectedSubkey);
        }

        private static bool VerifySecurityV3(byte[] hashedStr, string str, out int iterCount)
        {
            iterCount = default(int);

            try
            {
                // Read header information
                KeyDerivationPrf prf = (KeyDerivationPrf)ReadNetworkByteOrder(hashedStr, 1);
                iterCount = (int)ReadNetworkByteOrder(hashedStr, 5);
                int saltLength = (int)ReadNetworkByteOrder(hashedStr, 9);

                // Read the salt: must be >= 128 bits
                if (saltLength < 128 / 8)
                {
                    return false;
                }
                byte[] salt = new byte[saltLength];
                Buffer.BlockCopy(hashedStr, 13, salt, 0, salt.Length);

                // Read the subkey (the rest of the payload): must be >= 128 bits
                int subkeyLength = hashedStr.Length - 13 - salt.Length;
                if (subkeyLength < 128 / 8)
                {
                    return false;
                }
                byte[] expectedSubkey = new byte[subkeyLength];
                Buffer.BlockCopy(hashedStr, 13 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

                // Hash the incoming str and verify it
                byte[] actualSubkey = KeyDerivation.Pbkdf2(str, salt, prf, iterCount, subkeyLength);
                return ByteArraysEqual(actualSubkey, expectedSubkey);
            }
            catch
            {
                // This should never occur except in the case of a malformed payload, where
                // we might go off the end of the array. Regardless, a malformed payload
                // implies verification failed.
                return false;
            }
        }

        private static void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
        {
            buffer[offset + 0] = (byte)(value >> 24);
            buffer[offset + 1] = (byte)(value >> 16);
            buffer[offset + 2] = (byte)(value >> 8);
            buffer[offset + 3] = (byte)(value >> 0);
        }


        //比较两个字节数组的相等性。该方法是专门编写的，这样循环就不会被优化。
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (a == null && b == null)
            {
                return true;
            }
            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }
            var areSame = true;
            for (var i = 0; i < a.Length; i++)
            {
                areSame &= (a[i] == b[i]);
            }
            return areSame;
        }

    }

    public enum HashSecurityCompatibilityMode
    {
        /// <summary>
        /// Indicates hashing strs in a way that is compatible with ASP.NET Identity versions 1 and 2.
        /// </summary>
        V2,

        /// <summary>
        /// Indicates hashing strs in a way that is compatible with ASP.NET Identity version 3.
        /// </summary>
        V3
    }
}
