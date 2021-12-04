using System.IO;

namespace GarbageDetectionService
{
    public interface IGarbageDetector
    {
        public bool Check(FileInfo fileInfo);
    }
}