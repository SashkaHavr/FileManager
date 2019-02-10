using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace FileManager
{
    public static class Extention
    {
        public static List<T> LeaveRange<T>(this List<T> l, int start, int end)
        {
            List<T> res = new List<T>(end - start);
            for (int i = start; i < end; i++)
                try { res.Add(l[i]); }
                catch (ArgumentOutOfRangeException) { break; }
            return res;
        }

        public static FileInfo ToFileInfo(this FileSystemInfo fsi) => new FileInfo(fsi.FullName);

        public static DirectoryInfo ToDirectoryInfo(this FileSystemInfo fsi) => new DirectoryInfo(fsi.FullName);

        public static DriveInfo ToDriveInfo(this FileSystemInfo fsi) => new DriveInfo(fsi.FullName);

        public static List<string> ToStringList(this List<DirectoryInfo> l)
        {
            List<string> res = new List<string>(l.Count);
            foreach (var i in l)
                res.Add(i.FullName);
            return res;
        }

        public static bool ContainsPath(this IEnumerable<FileSystemInfo> fileSystemInfos, string path)
        {
            foreach (var i in fileSystemInfos)
                if (i.FullName == path)
                    return true;
            return false;
        }

        public static bool ContainsPath(this IEnumerable<DriveInfo> driveInfos, string path)
        {
            foreach (var i in driveInfos)
                if (i.Name == path)
                    return true;
            return false;
        }

        static public string GetSize(this FileInfo f)
        {
            if (f.Exists)
            {
                return f.ConvertSize(f.Length);
            }
            return String.Empty;
        }

        public static List<FileSystemInfo> Search(this FileSystemInfo d, string param)
        {
            List<FileSystemInfo> res = new List<FileSystemInfo>();
            foreach (var dir in d.ToDirectoryInfo().GetDirectories())
            {
                try
                {
                    if (dir.Name.ToLower().Contains(param.ToLower()))
                        res.Add(dir);
                    res.AddRange(Search(dir, param));
                }
                catch { continue; }
            }
            foreach (var file in d.ToDirectoryInfo().GetFiles())
            {
                if (file.Name.ToLower().Contains(param.ToLower()))
                    res.Add(file);
            }
            return res;
        }

        public static List<T> Inverse<T>(this List<T> list) => list.ToArray().Reverse().ToList();

        public static void CopyTo(this DirectoryInfo d, string destPath)
        {
            destPath = Path.Combine(destPath, Path.GetFileName(d.FullName));
            if (!Directory.Exists(destPath))
                Directory.CreateDirectory(destPath);
            foreach (var i in d.GetDirectories("*", SearchOption.AllDirectories))
                Directory.CreateDirectory(i.FullName.Replace(d.FullName, destPath));
            foreach (var i in d.GetFiles("*", SearchOption.AllDirectories))
                try
                {
                    File.Copy(i.FullName, i.FullName.Replace(d.FullName, destPath));
                }
                catch
                {
                    continue;
                }
        }

        public static string GetSize(this FileSystemInfo fsi)
        {
            if (fsi.Is() == FileSystemEntry.Directory)
                return fsi.ConvertSize(fsi.ToDirectoryInfo().CountSize());
            else
                return fsi.ToFileInfo().GetSize();
        }

        static double CountSize(this DirectoryInfo d)
        {
            double size = 0;
            foreach (var i in d.GetDirectories())
                try
                {
                    size += i.CountSize();
                }
                catch { continue; }
            foreach (var i in d.GetFiles())
                size += i.Length;
            return size;
        }

        static public string ConvertSize(this FileSystemInfo fsi, double size)
        {
            int c = 0;
            while (size > 10000)
            {
                size /= 1024;
                c++;
            }
            string res = Math.Round(size, 2).ToString();
            switch (c)
            {
                case 0:
                    res += " b";
                    break;
                case 1:
                    res += " KB";
                    break;
                case 2:
                    res += " MB";
                    break;
                case 3:
                    res += " GB";
                    break;
            }
            return res;
        }

        public static bool ContainsOneOf(this string str, IEnumerable<char> array)
        {
            foreach (var i in array)
                if (str.Contains(i))
                    return true;
            return false;
        }

        public static bool EqualToOneOf(this char s, IEnumerable<char> array)
        {
            foreach (var i in array)
                if (s == i)
                    return true;
            return false;
        }

        public static Dictionary<FileSystemEntry, int> GetAllDirAndFilesCount(this DirectoryInfo d)
        {
            var res = new Dictionary<FileSystemEntry, int>();
            res.Add(FileSystemEntry.Directory, 0);
            res.Add(FileSystemEntry.File, 0);
            foreach (var i in d.GetDirectories())
                try
                {
                    res[FileSystemEntry.Directory]++;
                    var t = i.GetAllDirAndFilesCount();
                    res[FileSystemEntry.Directory] += t[FileSystemEntry.Directory];
                    res[FileSystemEntry.File] += t[FileSystemEntry.File];
                }
                catch { continue; }
            foreach (var i in d.GetFiles())
                res[FileSystemEntry.File]++;
            return res;
        }

        public static FileSystemEntry Is(this FileSystemInfo fsi)
        {
            if (DriveInfo.GetDrives().ContainsPath(fsi.FullName))
                return FileSystemEntry.Drive;
            else if (fsi.ToDirectoryInfo().Exists)
                return FileSystemEntry.Directory;
            else
                return FileSystemEntry.File;
        }

    }

    public enum FileSystemEntry { Directory, File, Drive }
}
