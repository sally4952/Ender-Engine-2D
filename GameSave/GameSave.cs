using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.GameSave
{
    /// <summary>
    /// 确定这个存档是手动保存的还是自动保存的。
    /// </summary>
    enum SaveType : byte
    {
        /// <summary>
        /// 此存档的手动保存的。
        /// </summary>
        Manual,
        /// <summary>
        /// 此存档是自动保存的。
        /// </summary>
        Automatic,
    }

    /// <summary>
    /// 定义存档类。
    /// </summary>
    internal class GameSave
    {
        /// <summary>
        /// 存档保存时的时间。
        /// </summary>
        public DateTime SavingTime { get; }
        /// <summary>
        /// 存档是手动保存的还是自动保存的。
        /// </summary>
        public SaveType Type { get; }
        /// <summary>
        /// 存档需要存储的数据。
        /// </summary>
        public string Data { get; }
        /// <summary>
        /// 初始化一个存档。
        /// </summary>
        /// <param name="type">确定存档的保存方式。</param>
        /// <param name="data">设置存档需要保存的数据。</param>
        public GameSave(SaveType type, string data)
        {
            Type = type;
            Data = data;
            SavingTime = DateTime.Now;
        }
        private GameSave(SaveType type, string data, DateTime saveTime)
        {
            Type = type;
            Data = data;
            SavingTime = saveTime;
        }
        /// <summary>
        /// 从已知的数据来加载一个存档。
        /// </summary>
        /// <param name="type">保存方式。</param>
        /// <param name="data">保存的数据。</param>
        /// <param name="saveTime">保存的时间。</param>
        /// <returns></returns>
        public static GameSave FromExistingData(SaveType type, string data, DateTime saveTime)
        {
            return new GameSave(type, data, saveTime);
        }
    }
}
