using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RayPI.Treasury.Helpers
{
   public class FileHelper
    {
        /// <summary>
        /// 保存为不带Bom的文件
        /// </summary>
        /// <param name="TxtStr"></param>
        /// <param name="tempDir">格式:a/b.htm,相对根目录</param>
        public static void SaveFile(string TxtStr, string tempDir)
        {
            SaveFile(TxtStr, tempDir, true);
        }
        /// <summary>
        /// 保存文件内容,自动创建目录
        /// </summary>
        /// <param name="TxtStr"></param>
        /// <param name="tempDir">格式:a/b.htm,相对根目录</param>
        /// <param name="noBom"></param>
        public static void SaveFile(string TxtStr, string tempDir, bool noBom)
        {
            try
            {
             //   CreateDir(GetFolderPath(true, tempDir));
                System.IO.StreamWriter sw;
                if (noBom)
                    sw = new System.IO.StreamWriter(tempDir, false, new System.Text.UTF8Encoding(false));
                else
                    sw = new System.IO.StreamWriter(tempDir, false, System.Text.Encoding.UTF8);
                sw.Write(TxtStr);
                sw.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="file">格式:a/b.htm,相对根目录</param>
        /// <returns></returns>
        public static bool FileExists(string file)
        {
            if (File.Exists(file))
                return true;
            else
                return false;
        }
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="dir">物理路径</param>
        public static void CreateDir(string dir)
        {
            if (dir.Length == 0) return;
            if (!System.IO.Directory.Exists(dir))
                System.IO.Directory.CreateDirectory(dir);
        }
        /// <summary>
        /// 创建目录路径
        /// </summary>
        /// <param name="folderPath">物理路径</param>
        public static void CreateFolder(string folderPath)
        {
            if (!System.IO.Directory.Exists(folderPath))
                System.IO.Directory.CreateDirectory(folderPath);
        }
        /// <summary>
        /// 复制文件        
        /// </summary>
        /// <param name="file1"></param>
        /// <param name="file2"></param>
        /// <param name="overwrite">如果已经存在是否覆盖？</param>
        public static void CopyFile(string file1, string file2, bool overwrite)
        {
            if (System.IO.File.Exists(file1))
            {
                if (overwrite)
                    System.IO.File.Copy(file1, file2, true);
                else
                {
                    if (!System.IO.File.Exists(file2))
                        System.IO.File.Copy(file1, file2);
                }
            }
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="FilePath">文件物理路径</param>
        public static void DelFile(string FilePath)
        {
            if (System.IO.File.Exists(FilePath))
            {
                System.IO.File.Delete(FilePath);
            }
        }
    }
}
