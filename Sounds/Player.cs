using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnderEngine2D.Sounds
{
    /// <summary>
    /// 一个声音播放器。
    /// </summary>
    internal class Player
    {
        /// <summary>
        /// 表示这个播放器是否正在异步播放音乐。
        /// </summary>
        public bool IsPlaying { get; private set; } = false;
        /// <summary>
        /// 表示这个播放器是否正在循环播放音乐。
        /// </summary>
        public bool IsLooping { get; private set; } = false;
        private Thread mLoopingThread = null;
        private Thread mAsyncPlayingThread = null;
        /// <summary>
        /// 用于存储音频的流。
        /// </summary>
        public Stream Data { get; private set; }
        /// <summary>
        /// 设置音频原文件。
        /// </summary>
        public string SourceFile
        {
            set
            {
                if (!File.Exists(value))
                    throw new FileNotFoundException();
                var databytes = File.ReadAllBytes(value);
                Data = new MemoryStream(databytes);
            }
        }
        /// <summary>
        /// 在这个线程上播放音乐。
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void Play()
        {
            if (IsPlaying)
                throw new InvalidOperationException("正在执行异步播放。");
            if (IsLooping)
                throw new InvalidOperationException("正在循环播放。");

            using (WaveStream waveStream = new Mp3FileReader(Data))
            {
                using (WaveOutEvent waveOut = new WaveOutEvent())
                {
                    waveOut.Init(waveStream);

                    waveOut.Play();

                    while (waveOut.PlaybackState == PlaybackState.Playing) ;
                }
            }
        }
        /// <summary>
        /// 异步地循环播放音乐。
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void PlayLooping()
        {
            if (IsPlaying)
                throw new InvalidOperationException("正在执行异步播放。");
            if (IsLooping)
                throw new InvalidOperationException("正在循环播放。");
            IsLooping = true;
            if (mLoopingThread == null)
            {
                mLoopingThread = new Thread(() =>
                {
                    using (WaveStream waveStream = new Mp3FileReader(Data))
                    {
                        using (WaveOutEvent waveOut = new WaveOutEvent())
                        {
                            waveOut.Init(waveStream);

                            while (true)
                            {
                                waveOut.Play();

                                while (waveOut.PlaybackState == PlaybackState.Playing)
                                {
                                    if (!IsLooping)
                                    {
                                        break;
                                    }
                                }

                                waveOut.Stop();

                                while (!IsLooping) ;
                            }
                        }
                    }
                });
                mLoopingThread.Start();
            }
        }
        /// <summary>
        /// 停止循环播放。
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void StopLooping()
        {
            if (!IsLooping)
                throw new InvalidOperationException("未在循环播放。");
            IsLooping = false;
        }
        /// <summary>
        /// 开始异步播放音乐。
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void BeginPlaying()
        {
            if (Data == null)
                throw new InvalidOperationException("Data未定义。");
            if (IsLooping)
                throw new InvalidOperationException("正在循环播放。");
            if (IsPlaying)
                return;

            if (mAsyncPlayingThread == null)
            {
                mAsyncPlayingThread = new Thread(() =>
                {
                    Start:
                    using (WaveStream waveStream = new Mp3FileReader(Data))
                    {
                        using (WaveOutEvent waveOut = new WaveOutEvent())
                        {
                            waveOut.Init(waveStream);

                            waveOut.Play();

                            while (waveOut.PlaybackState == PlaybackState.Playing)
                            {
                                if (!IsPlaying)
                                    goto Stop;
                            }
                            IsPlaying = false;
                            goto Next;
                            Stop:
                            waveOut.Stop();
                            Next:
                            while (!IsPlaying) ;
                            goto Start;
                        }
                    }
                });
                mAsyncPlayingThread.Start();
            }
            IsPlaying = true;
        }
        /// <summary>
        /// 停止异步播放音乐。
        /// </summary>
        public void StopPlaying()
        {
            if (!IsPlaying)
                return;

            IsPlaying = false;
        }
        /// <summary>
        /// 从已加密的资源文件导入音频。
        /// </summary>
        /// <param name="file">已加密的资源文件。</param>
        /// <param name="password">解密的密码。</param>
        /// <param name="iv">解密的维度（相当于第二个密码）。</param>
        /// <returns>是否成功导入。</returns>
        public bool FromProtectedFile(string file, byte[] password, byte[] iv)
        {
            var dec = ResourcesProtectDll.Decryption.GetBytes(file, password, iv);
            if (dec == null)
            {
                return false;
            }
            Data = new MemoryStream(dec);
            return true;
        }
    }
}
