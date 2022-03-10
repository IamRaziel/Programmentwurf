using NAudio.Wave;
using System.IO;
using System.Net;

namespace MusicApi.Backend.Music
{
    public class MusicPlayer
    {
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
                    using (WaveOut waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback()))
                    {
                        waveOut.Init(blockAlignedStream);
                        waveOut.Play();
                        while (waveOut.PlaybackState == PlaybackState.Playing)
                        {
                            System.Threading.Thread.Sleep(100);
                        }
                    }
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
    }
}
