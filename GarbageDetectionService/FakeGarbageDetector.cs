using System;
using System.IO;

namespace GarbageDetectionService
{
    public class FakeGarbageDetector:IGarbageDetector
    {
        public bool Check(FileInfo fileInfo)
        {
            return new Random().Next(0, 2).Equals(1);
        }
    }
}