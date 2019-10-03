using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResizer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string sourcePath = Path.Combine(Environment.CurrentDirectory, "images");
            string destinationPath = Path.Combine(Environment.CurrentDirectory, "output"); ;

            ImageProcess imageProcess = new ImageProcess();

            imageProcess.Clean(destinationPath);

            Stopwatch sw1 = new Stopwatch();
            sw1.Start();
            imageProcess.ResizeImages(sourcePath, destinationPath, 2.0);
            sw1.Stop();

            imageProcess.Clean(destinationPath);

            Stopwatch sw2 = new Stopwatch();
            sw2.Start();
            imageProcess.ResizeImagesWait(sourcePath, destinationPath, 2.0);
            sw2.Stop();

            imageProcess.Clean(destinationPath);

            Stopwatch sw3 = new Stopwatch();
            sw3.Start();
            await imageProcess.ResizeImagesAsync(sourcePath, destinationPath, 2.0);
            sw3.Stop();

            Console.WriteLine($"花費時間          : {sw1.ElapsedMilliseconds} ms");

            double percent1 = Math.Round((sw1.ElapsedMilliseconds - sw2.ElapsedMilliseconds) * 1.00 / sw1.ElapsedMilliseconds * 100.0, 1);
            Console.WriteLine($"花費時間 WaitAll  : {sw2.ElapsedMilliseconds} ms, 提升: {percent1} %");

            double percent2 = Math.Round((sw1.ElapsedMilliseconds - sw3.ElapsedMilliseconds) * 1.00 / sw1.ElapsedMilliseconds * 100.0, 1);
            Console.WriteLine($"花費時間 WhenAll  : {sw3.ElapsedMilliseconds} ms, 提升: {percent2} %");
        }
    }
}
