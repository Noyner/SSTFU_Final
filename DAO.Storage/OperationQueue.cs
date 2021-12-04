using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DAO.Storage
{
    public static class OperationQueue
    {
        private static readonly Queue<FileInfo> FilesQueue=new Queue<FileInfo>();

        public static bool QueueEmpty() => !FilesQueue.Any();

        private const string FramesPath = @"./../DAO.Storage/FramesQueue/";
        public static void FillQueue()
        {
            while (Directory.GetFiles(FramesPath).Count()>0)
                EnqueueFrame();
            //foreach(var f in Directory.GetFiles(FramesPath,"*.jpg"))
            //{
            //    EnqueueFrame(new FileInfo(f));
            //}
        }
        public static void EnqueueFrame()
        {
            //FilesQueue.Enqueue(file);


            var fileInfos = new List<FileInfo>();
            Directory
                .GetFiles(FramesPath)
                .ToList()
                .ForEach(x => fileInfos.Add(new FileInfo(x)));
            var orderedEnumerable = fileInfos.OrderBy(x => x.CreationTime);
            FilesQueue.Enqueue(orderedEnumerable.First());
            File.Delete(orderedEnumerable.First().FullName);
        }

        /* public static DeleteLast()
         {
             File.Delete()
         }*/
        public static FileInfo DequeueFrame()
        {
            return FilesQueue.Dequeue();
        }
        
    }
}