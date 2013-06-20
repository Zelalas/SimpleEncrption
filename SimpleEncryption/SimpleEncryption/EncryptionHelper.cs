using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEncrption
{
    public class EncryptionHelper
    {
        private static const String SBOX_LOCATION = @"C:\SBox\";
        internal static bool Validate(SBox box)
        {
            for (int i = 0; i < box.sbox.Length; i++)
            {
                if (i != box.rsbox[box.sbox[i]])
                {
                    return false;
                }
            }
            return true;
        }

        public static byte[] CreateReverseSBox(byte[] sbox)
        {
            byte[] rsbox = new byte[256];
            for (byte i = 0; i < Byte.MaxValue; i++)
            {
                rsbox[sbox[i]] = i;
            }
            rsbox[sbox[Byte.MaxValue]] = Byte.MaxValue;
            return rsbox;
        }

        public static byte[] ReadSBoxFromFile(String fileName)
        {
            return File.ReadAllBytes(SBOX_LOCATION + fileName);
        }

        public static void WriteSBoxToFile(String fileName,byte[] sbox)
        {
            if(!Directory.Exists(SBOX_LOCATION))
            {
                Directory.CreateDirectory(SBOX_LOCATION);
            }
            File.WriteAllBytes(SBOX_LOCATION + fileName, sbox);
        }

        public static byte[] CreateSBox(byte seed = 0)
        {
            Random rnd = (seed > 0)? new Random(seed) : new Random();
            byte[] sbox = new byte[256];
            List<Byte> pool = CreatePoolList();
            for (int i = 0; i < 256; i++)
            {
                int next = rnd.Next(pool.Count);
                sbox[i]  = pool[next];
                pool.RemoveAt(next);
            }
            return sbox;
        }

        private static List<Byte> CreatePoolList()
        {
            List<Byte> pool = new List<Byte>();
            for (byte b = 0; b < Byte.MaxValue; b++)
            {
                pool.Add(b);
            }
            pool.Add(255);
            return pool;
        }
    }
}
