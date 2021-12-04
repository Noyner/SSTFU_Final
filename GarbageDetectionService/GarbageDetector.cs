using GarbageDetectionService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.Diagnostics;

namespace GarbageDetectionService
{
    public class GarbageDetector : IGarbageDetector
    {
        private string run_cmd(string cmd, string args)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "C:\\Users\\yurid\\AppData\\Local\\Programs\\Python\\Python310\\python.exe";
            start.Arguments = string.Format("{0} {1}", cmd, args);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            string res = "";
            using Process process = Process.Start(start);
            using StreamReader reader = process.StandardOutput;
            string result = reader.ReadToEnd();
            res += result;

            return res;
        }
        public bool Check(FileInfo fileInfo)
        {
            string cmd = ".\\..\\GarbageDetectionService\\NN\\detect.py";
            string args =
                $"--weights .\\..\\GarbageDetectionService\\NN\\runs\\train\\exp7\\weights\\best.pt --img 320 --nosave --conf 0.4 --save-txt --source {fileInfo.FullName}";
            string s =run_cmd(cmd,args);
            //se.ExecuteFile($"py ./../GarbageDetectionService/NN/detect.py --weights ./../GarbageDetectionService/NN/runs/train/exp7/weights/best.pt --img 320 --nosave --conf 0.4 --save-txt --source {fileInfo.FullName}");

            return true;
        }
    }
}
