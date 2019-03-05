using System;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace FileStreams
{
    class Sync
    {
        public struct DirectoryStruct
        {
            public Dictionary<string, string> dict;
            public DirectoryInfo dir;
        };
        public DirectoryStruct dir1;
        public DirectoryStruct dir2;
        public Sync(DirectoryInfo dir1, DirectoryInfo dir2)
        {
            this.dir1.dir = dir1;
            this.dir2.dir = dir2;
            this.dir1.dict = GetDictonary(dir1);
            this.dir2.dict = GetDictonary(dir2);
        }
        private static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        private string getMD5ofFile(FileInfo f)
        {
            MD5 md5 = MD5.Create();
            var stream = File.OpenRead(f.FullName);
            byte[] pathBytes = Encoding.UTF8.GetBytes(Path.GetFileName(f.FullName));
            md5.TransformBlock(pathBytes, 0, pathBytes.Length, pathBytes, 0);
            byte[] content = ReadFully(stream);
            md5.TransformFinalBlock(content, 0, content.Length);
            stream.Close();
            return BitConverter.ToString(md5.Hash).Replace("-", "").ToLowerInvariant();
        }
        private string getMD5ofDir(DirectoryInfo d)
        {
            string path = d.FullName;
            var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                           .OrderBy(p => p).ToList();

            MD5 md5 = MD5.Create();
           
            for (int i = 0; i < files.Count; i++)
            {
                string file = files[i];
                string relativePath = file.Substring(path.Length + 1);
                byte[] pathBytes = Encoding.UTF8.GetBytes(relativePath.ToLower());
                md5.TransformBlock(pathBytes, 0, pathBytes.Length, pathBytes, 0);
                byte[] contentBytes = File.ReadAllBytes(file);
                md5.TransformBlock(contentBytes, 0, contentBytes.Length, contentBytes, 0);
            }
            byte[] pathBytesName = Encoding.UTF8.GetBytes(Path.GetFileName(d.FullName));
            md5.TransformFinalBlock(pathBytesName, 0, pathBytesName.Length);
            return BitConverter.ToString(md5.Hash).Replace("-", "").ToLowerInvariant();
        }
        private Dictionary<string, string> GetDictonary(DirectoryInfo dir)
        {
            FileInfo[] files = dir.GetFiles();
            DirectoryInfo[] dirs = dir.GetDirectories();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach(var f in files)
                dict.Add(getMD5ofFile(f), f.FullName);
            foreach (var d in dirs)
                dict.Add(getMD5ofDir(d), d.FullName);
            return dict;
        }
        private void CopyDirectorie(string from, string to)
        {
            foreach (string dirPath in Directory.GetDirectories(from, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(from, to));
            foreach (string newPath in Directory.GetFiles(from, ".", SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(from, to), true);
        }
        private void PrintBytes(byte[] bytes)
        {
            foreach (var b in bytes)
                Console.Write(b);
            Console.WriteLine();
        }
        private void AddFilesAndDirectory(DirectoryStruct dir1, DirectoryStruct dir2)
        {
            foreach (var d in dir1.dict)
            {
                if (!dir2.dict.ContainsKey(d.Key))
                {
                    FileAttributes attr = File.GetAttributes(d.Value);
                    if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        Directory.CreateDirectory(dir2.dir.FullName + "/" + Path.GetFileName(d.Value));
                        CopyDirectorie(d.Value, dir2.dir.FullName + "/" + Path.GetFileName(d.Value));
                    }
                    else
                        File.Copy(d.Value, dir2.dir.FullName + "/" + Path.GetFileName(d.Value), true);
                    dir2.dict.Add(d.Key, d.Value);
                }
            }
        }
        private void RemoveFilesAndDirectory(DirectoryStruct dir1, DirectoryStruct dir2)
        {
            var itemToRemove = dir2.dict.Where(x => !dir1.dict.ContainsKey(x.Key)).ToDictionary(x => x.Key, x => x.Value);
            foreach (var item in itemToRemove)
            {
                FileAttributes attr = File.GetAttributes(item.Value);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                    Directory.Delete(item.Value, true);
                else
                    File.Delete(item.Value);
                dir2.dict.Remove(item.Key);
            }
        }
        
        public void Syncronize(DirectoryStruct dir1, DirectoryStruct dir2)
        {
            RemoveFilesAndDirectory(dir1, dir2);
            AddFilesAndDirectory(dir1, dir2);
        }
    }
    class MyWatcher
    {
        private FileSystemWatcher watcher;
        public DirectoryInfo Dir1 { get; private set; }
        public DirectoryInfo Dir2 { get; private set; }
        public MyWatcher(DirectoryInfo dir1, DirectoryInfo dir2)
        {
            Dir1 = dir1;
            Dir2 = dir2;
            watcher = SetWatcher(Dir1);
        }
        private FileSystemWatcher SetWatcher(DirectoryInfo dir)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = dir.FullName;
            watcher.NotifyFilter = NotifyFilters.Size |
                                    NotifyFilters.LastWrite |
                                    NotifyFilters.LastAccess |
                                    NotifyFilters.CreationTime |
                                    NotifyFilters.FileName |
                                    NotifyFilters.DirectoryName;
            watcher.Filter = "*.*";
            watcher.IncludeSubdirectories = true;
            watcher.Changed += OnChanged;
            watcher.Renamed += OnRenamed;
            watcher.Created += OnCreated;
            watcher.Deleted += OnDeleted;
            return watcher;
        }
        private void CopyDirectorie(string from, string to)
        {
            foreach (string dirPath in Directory.GetDirectories(from, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(from, to));
            foreach (string newPath in Directory.GetFiles(from, ".", SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(from, to), true);
        }
        static public void CopyFolder(string sourceFolder, string destFolder)
        {
            if (!Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);
            string[] files = Directory.GetFiles(sourceFolder);
            foreach (string file in files)
            {
                string name = Path.GetFileName(file);
                string dest = Path.Combine(destFolder, name);
                File.Copy(file, dest);
            }
            string[] folders = Directory.GetDirectories(sourceFolder);
            foreach (string folder in folders)
            {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(destFolder, name);
                CopyFolder(folder, dest);
            }
        }
        private bool is_dir(string path)
        {
            FileAttributes attr = File.GetAttributes(path);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                return true;
            return false;
        }
        private void OnChanged(object o, FileSystemEventArgs e)
        {
            string str = e.FullPath.Replace(Dir1.FullName, Dir2.FullName);
            if (Directory.Exists(str) || File.Exists(str))
            {
                if (is_dir(str))
                {
                    //CopyFolder(e.FullPath, str);
                   // Directory.Delete(str, true);
                }
                else
                {
                    File.Delete(str);
                    File.Copy(e.FullPath, str);
                }
            }
        }
        private void OnRenamed(object o, RenamedEventArgs e)
        {
            string str = e.FullPath.Replace(Dir1.FullName, Dir2.FullName);
            string old = e.OldFullPath.Replace(Dir1.FullName, Dir2.FullName);
            // if (Directory.Exists(str))

            //if (is_dir(old))
            // {
            // Directory.Delete(old, true);
            // Console.WriteLine(str);
            //CopyDirectorie(e.FullPath, str);
            // }
            // else
            // {
            if (File.Exists(old))
            {
                File.Delete(old);
                File.Copy(e.FullPath, str);
            }
               // }
            
        }
        private void OnCreated(object o, FileSystemEventArgs e)
        {
            string str = e.FullPath.Replace(Dir1.FullName, Dir2.FullName);
            if (is_dir(e.FullPath))
                Directory.CreateDirectory(str);
            else
                File.Copy(e.FullPath, str);
        }
        private void OnDeleted(object o, FileSystemEventArgs e)
        {
            string str = e.FullPath.Replace(Dir1.FullName, Dir2.FullName);
            if (is_dir(str))
                Directory.Delete(str, true);
            else
                File.Delete(str);
        }
        public void Run()
        {
            watcher.EnableRaisingEvents = true;
        }
        public void Stop()
        {
            watcher.EnableRaisingEvents = false;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DirectoryInfo dir1 = new DirectoryInfo("../../../../../Testing1");
                DirectoryInfo dir2 = new DirectoryInfo("../../../../../Testing2");
                Sync sync = new Sync(dir1, dir2);
                sync.Syncronize(sync.dir1, sync.dir2);
                MyWatcher watcher = new MyWatcher(dir1, dir2);
                watcher.Run();
                while (Console.Read() != 'q');
                watcher.Stop();

            }
            catch (Exception e)
            {
                 Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
