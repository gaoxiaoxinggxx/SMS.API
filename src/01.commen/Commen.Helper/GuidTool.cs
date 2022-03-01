using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Commen.Helper
{
   public class GuidTool
    {
        private static readonly RandomNumberGenerator RandomNumberGenerator = RandomNumberGenerator.Create();

        public static Guid GenerateSequentialGuid()
        {
            byte[] array = new byte[10];
            RandomNumberGenerator.GetBytes(array);
            long value = DateTime.UtcNow.Ticks / 10000;
            byte[] bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse((Array)bytes);
            }

            byte[] array2 = new byte[16];
            Buffer.BlockCopy(array, 0, array2, 0, 10);
            Buffer.BlockCopy(bytes, 2, array2, 10, 6);
            return new Guid(array2);
        }
    }
}
