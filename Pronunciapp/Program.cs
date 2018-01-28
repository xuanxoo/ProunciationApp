using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronunciapp
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathWave = @"C:\temp\google.wav";

            //google traductor
            var textospeech = new TextToSpeech("en");
            using (Task<Stream> audioStream = textospeech.DownloadAsStream("This is my tshirt"))
            {
                audioStream.Wait();
                if (audioStream.IsCompleted)
                {
                    using (audioStream.Result)
                    {
                        //stream from google to wav file
                        WaveConverter.ConvertMp3ToWav(audioStream.Result, pathWave); 
                    }
                }
            }

            //audio movil
            string pathAudioMovilWave = @"C:\temp\Tshirt.wav";
            string pathAudioMovil = @"C:\temp\Tshirt.m4a";
            WaveConverter.ConvertA4MToWav(pathAudioMovil, pathAudioMovilWave);

            var m_Wavefile = new WaveSound(pathWave); //wave from google
            m_Wavefile.ReadWaveFile();
            var waveFile2 = new WaveSound(pathAudioMovilWave); //wave from user
            waveFile2.ReadWaveFile();
            var result = m_Wavefile.Compare(waveFile2);

            Utils.CleanFiles(pathWave, pathAudioMovilWave);

            Console.WriteLine("Similar:" + result.ToString() + " %");
            Console.ReadLine();
        }
    }
}
