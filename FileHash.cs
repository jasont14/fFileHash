using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;

namespace FileHashForTracking
{
    class FileHashOps
    {
        internal static List<string> fHOErrorMessages = new List<string>();

        internal FileHashOps()
        {

        }

        internal static string GetSHA1ForFile(string FilePath)
        {
            if (File.Exists(FilePath) && !IsFileLocked(FilePath))
            {
                byte[] start = FileToBytesViaSHA1(FilePath);
                return BytesToString(start);
            }

            return string.Format("ERROR: " + FilePath);

        }
        internal static string GetFileNameFromPath(string filePath)
        {
            System.IO.FileInfo.
            int counter = 0;
            int pointer = 0;
            foreach (char c in filePath)
            {
                if (c == '\\')
                {
                    pointer = counter;
                }
                counter++;
            }

            return filePath.Substring(pointer + 1);
        }

        private static byte[] FileToBytesViaSHA1(string filePath)
        {
            SHA1 sha1hash = SHA1.Create();
            FileStream fs = null;

            try
            {
                fs = File.OpenRead(filePath);
                return sha1hash.ComputeHash(fs);
            }
            catch (System.Exception e)
            {
                fHOErrorMessages.Add(DateTime.Now.ToString() + "," + e.Source + "," + e.Message);
            }
            finally
            {
                if (fs != null) { fs.Close(); }
            }

            return sha1hash.ComputeHash(fs);

            //using (FileStream fs = File.OpenRead(filePath))
            //{

            //}
        }
        private static string BytesToString(byte[] bytes)
        {
            string result = "";
            foreach (byte b in bytes)

            {
                result += b.ToString("X2");
            }
            return result;
        }

        private static bool IsFileLocked(string file)
        {
            FileStream stream = null;

            try
            {
                stream = File.Open(file, FileMode.Open);
            }
            catch (IOException e)
            {
                fHOErrorMessages.Add(DateTime.Now.ToString() + "," + e.Source + "," + e.Message);
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }
    }
}
