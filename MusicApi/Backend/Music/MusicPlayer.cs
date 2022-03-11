using NAudio.Wave;
using System.IO;
using System.Net;

namespace MusicApi.Backend.Music
{
    public class MusicPlayer
    {
        protected WaveOut waveOut;

        public MusicPlayer()
        {

        }


        public bool PlayMp3FromBytes(byte[] bytes)
        {
            using (Stream ms = new MemoryStream())
            {
                ms.Write(bytes, 0, bytes.Length);
                return PlayMp3FromStream(ms);
            }
        }


        public bool PlayMp3FromStream(Stream stream)
        {
            using (stream)
            {
                stream.Position = 0;
                using (WaveStream blockAlignedStream =
                    new BlockAlignReductionStream(
                        WaveFormatConversionStream.CreatePcmStream(
                            new Mp3FileReader(stream))))
                {
                    using (waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback()))
                    {
                        waveOut.Init(blockAlignedStream);
                        waveOut.Play();
                        while (waveOut.PlaybackState == PlaybackState.Playing)
                        {
                            System.Threading.Thread.Sleep(100);
                        }
                        waveOut.Stop();
                    }
                    waveOut = null;
                }
            }
            return true;
        }

        
        public bool PlayMp3FromUrl(string url)
        {
            using (Stream ms = new MemoryStream())
            {
                using (Stream stream = WebRequest.Create(url)
                    .GetResponse().GetResponseStream())
                {
                    byte[] buffer = new byte[32768];
                    int read;
                    while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                }
                return PlayMp3FromStream(ms);
            }
        }

        public bool Pause()
        {
            using(waveOut)
            {
                if (waveOut != null)
                {
                    if (waveOut.PlaybackState == PlaybackState.Playing)
                    {
                        waveOut.Pause();
                        return true;
                    }
                }
            }
            return false;
        }

        public bool Resume()
        {
            using (waveOut)
            {
                if (waveOut != null)
                {
                    if (waveOut.PlaybackState == PlaybackState.Paused)
                    {
                        waveOut.Resume();
                        return true;
                    }
                }
            }
            return false;
        }

        public bool Stop()
        {
            using (waveOut)
            {
                if (waveOut != null)
                {
                    if (waveOut.PlaybackState != PlaybackState.Stopped)
                    {
                        waveOut.Stop();
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
