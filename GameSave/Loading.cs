using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.GameSave
{
    internal class Loading
    {
        /// <summary>
        /// 完全加载一个存档。
        /// </summary>
        /// <param name="path">存档文件的路径。</param>
        /// <param name="data">存档文件中存储的所有内容。</param>
        /// <returns></returns>
        public static bool Load(string path, out GameSave data)
        {
            try
            {
                byte[] bd;
                using (var fs = File.OpenRead(path))
                {
                    bd = new byte[fs.Length];
                    fs.Read(bd, 0, bd.Length);
                    fs.Close();
                }
                byte[] meta = (byte[])bd.Take(sizeof(long) + sizeof(byte));
                var savingtime = DateTime.FromBinary(BitConverter.ToInt64((byte[])meta.Take(sizeof(long)), 0));
                var savingtype = (SaveType)((byte[])meta.Skip(sizeof(long)))[0];
                var datastirng = Encoding.UTF8.GetString((byte[])bd.Skip(sizeof(long) + sizeof(byte)));
                data = GameSave.FromExistingData(savingtype, datastirng, savingtime);
            }
            catch
            {
                data = null;
                return false;
            }
            return true;
        }
        /// <summary>
        /// 获取指定.\Saves文件夹下的所有存档的元数据。
        /// </summary>
        /// <returns>返回所有存档的元数据（包括保存时间、保存类型），Data将为string.Empty。</returns>
        public static GameSave[] GetAllSavesMeta()
        {
            if (!Directory.Exists(".\\Saves"))
                return new GameSave[0];
            var files = Directory.GetFiles(".\\Saves");
            var saves = new GameSave[files.Length];
            for (var i = 0; i < files.Length; i++)
            {
                var file = files[i];
                using (var fs = File.OpenRead(file))
                {
                    var bd = new byte[fs.Length];
                    fs.Read(bd, 0, bd.Length);
                    byte[] meta = (byte[])bd.Take(sizeof(long) + sizeof(byte));
                    var savingtime = DateTime.FromBinary(BitConverter.ToInt64((byte[])meta.Take(sizeof(long)), 0));
                    var savingtype = (SaveType)((byte[])meta.Skip(sizeof(long)))[0];
                    saves[i] = GameSave.FromExistingData(savingtype, string.Empty, savingtime);
                    fs.Close();
                }
            }
            return saves;
        }
    }
}
