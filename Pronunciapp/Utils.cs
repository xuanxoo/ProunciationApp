using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronunciapp
{
    public class Utils
    {
        public static void CleanFiles(string fileSource, string fileCompared)
        {
            FileInfo fiWave = new FileInfo(fileSource);
            if (fiWave.Exists) fiWave.Delete();

            FileInfo fiDat = new FileInfo(fileSource + ".fft.dat");
            if (fiDat.Exists) fiDat.Delete();

            FileInfo fcWave = new FileInfo(fileCompared);
            if (fcWave.Exists) fcWave.Delete();

            FileInfo fcDat = new FileInfo(fileCompared + ".fft.dat");
            if (fcDat.Exists) fcDat.Delete();
        }
    }
}
