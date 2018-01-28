using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronunciapp
{
    class WaveConverter
    {
        public static void ConvertMp3ToWav(string _inPath_, string _outPath_)
        {
            using (Mp3FileReader mp3 = new Mp3FileReader(_inPath_))
            {
                using (WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(mp3))
                {
                    WaveFileWriter.CreateWaveFile(_outPath_, pcm);
                    pcm.Close();
                }
            }
        }

        public static void ConvertA4MToWav(string inPath, string outPath)
        {

            using (var mm = new MediaFoundationReader(inPath))
            {
                using (WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(mm))
                {
                    WaveFileWriter.CreateWaveFile(outPath, pcm);
                    pcm.Close();
                }
            }
        }

        public static void ConvertMp3ToWav(Stream audioFromGoogle, string _outPath_)
        {
            using (Mp3FileReader mp3 = new Mp3FileReader(audioFromGoogle))
            {
                using (WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(mp3))
                {
                    WaveFileWriter.CreateWaveFile(_outPath_, pcm);
                    pcm.Close();
                }
            }
        }
    }
}
