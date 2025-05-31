using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.GameSave
{
    internal class Saving
    {
        /// <summary>
        /// 将一个存档作为一个文件保存到本地。
        /// </summary>
        /// <param name="data">要保存的存档。</param>
        /// <returns>是否保存成功。</returns>
        public static bool SaveGame(GameSave data)
        {
            var databytes = Encoding.UTF8.GetBytes(data.Data);
            var timevalue = data.SavingTime.ToBinary();
            var bd = new byte[sizeof(long) + sizeof(byte) + databytes.Length];
            
            try
            {
                Buffer.BlockCopy(BitConverter.GetBytes(timevalue), 0, bd, 0, sizeof(long));

                Buffer.BlockCopy(new[] { (byte)data.Type }, 0, bd, sizeof(long), sizeof(byte));

                Buffer.BlockCopy(databytes, 0, bd, sizeof(long) + sizeof(byte), databytes.Length);

                if (!Directory.Exists(".\\Saves"))
                    Directory.CreateDirectory(".\\Saves");

                using (var fs = File.Create($".\\Saves\\{data.SavingTime.ToBinary()}.save"))
                {
                    fs.Write(bd, 0, bd.Length);
                    fs.Close();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
