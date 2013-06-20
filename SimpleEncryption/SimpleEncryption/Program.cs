using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEncrption
{
    public class Program
    {
        public static void Main(string[] args)
        {
            EncryptionHelper.WriteSBoxToFile("SBOX",EncryptionHelper.CreateSBox());
            byte[] sboxArray = EncryptionHelper.ReadSBoxFromFile("SBOX");
            SBox sbox = new SBox(sboxArray);
            if (EncryptionHelper.Validate(sbox))
            {
                byte[] input = File.ReadAllBytes(@"C:\in\btm.png");
                for (long i = 0L; i < input.LongLength; i++)
                {
                    input[i] = sbox.Encode(input[i]);
                }
                File.WriteAllBytes(@"C:\out\encFile.png", input);

                byte[] input2 = File.ReadAllBytes(@"C:\out\encFile.png");
                DecodeFile(sbox, input2);
                File.WriteAllBytes(@"C:\out\decFile.png", input2);
            }
        }

        private static void DecodeFile(SBox sbox, byte[] input2)
        {
            for (long i = 0L; i < input2.LongLength; i++)
            {
                input2[i] = sbox.Decode(input2[i]);
            }
        }

    }
}
