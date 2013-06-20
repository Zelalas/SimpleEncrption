using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEncrption
{
    public class SBox
    {
        internal byte[] sbox;
        internal byte[] rsbox;
        public SBox(byte[] newSbox)
        {
            sbox = newSbox;
            rsbox = EncryptionHelper.CreateReverseSBox(sbox);
        }

        public byte Decode(byte encoded)
        {
            return rsbox[encoded];
        }

        public byte Encode(byte plain)
        {
            return sbox[plain];
        }
    }
}
