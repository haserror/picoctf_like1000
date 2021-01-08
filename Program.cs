using ICSharpCode.SharpZipLib.Tar;
using System.IO;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            // picoctf like1000 

            const string BASE_DIR = @"C:\tmp";

            string dir = BASE_DIR;
            while (true)
            {
                string[] files = Directory.GetFiles(dir, "*.tar");
                if (files.Length == 0)
                {
                    break;
                }

                string tarFile = files[0];
                string outDir = Path.Combine(BASE_DIR, Path.GetFileNameWithoutExtension(tarFile));
                Extract(tarFile, outDir);

                if (dir != BASE_DIR)
                {
                    Directory.Delete(dir, true);
                }
                dir = outDir;
            }
        }

        /// <summary>
        /// https://github.com/icsharpcode/SharpZipLib
        /// </summary>
        /// <param name="tarFile"></param>
        /// <param name="outDir"></param>
        /// <returns></returns>
        static void Extract(string tarFile, string outDir)
        {
            using (Stream inStream = File.OpenRead(tarFile))
            {
                using (TarArchive tarArchive = TarArchive.CreateInputTarArchive(inStream))
                {
                    tarArchive.ExtractContents(outDir);
                    tarArchive.Close();
                }
                inStream.Close();
            }
        }
    }
}
