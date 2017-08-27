using System.Linq;
using System.Threading;
using CSCore;
using CSCore.Codecs;
using CSCore.CoreAudioAPI;
using CSCore.SoundOut;

namespace SoundDataUtils
{
    public static class Player
    {
        public static void Play(string filePath)
        {
            using (var enumerator = new MMDeviceEnumerator())
            using (var device = enumerator.EnumAudioEndpoints(DataFlow.Render, DeviceState.Active).Last())
            using (var source =
                CodecFactory.Instance.GetCodec(filePath)
                    .ToSampleSource()
                    .ToMono()
                    .ToWaveSource())

            using (
                var soundOut = new WasapiOut() { Latency = 100, Device = device })
            {
                soundOut.Initialize(source);
                soundOut.Play();
                Thread.Sleep(source.GetLength());
                soundOut.Stop();
            }
        }
    }
}