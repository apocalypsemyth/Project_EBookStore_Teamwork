using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace EBookStore.Helpers
{
    public class FileHelper
    {
        private static string[] _imageFileExtArr =
       {
            ".jpg", ".bmp", ".png"
        };

        private static int _uploadMB = 10;
        private static int _uploadBytes = _uploadMB * 1024 * 1024;

        public static string[] ImageFileExtArr
        {
            get
            {
                return _imageFileExtArr;
            }
        }

        public static int UploadMB
        {
            get
            {
                return _uploadMB;
            }
        }

        /// <summary> 檢查檔案副檔名是否為圖片 </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool ValidImageExtension(string fileName)
        {
            return ValidFileExtension(fileName, _imageFileExtArr);
        }

        /// <summary> 檢查檔案副檔名是否在允許清單中 </summary>
        /// <param name="fileName"></param>
        /// <param name="avaiExts"></param>
        /// <returns></returns>
        public static bool ValidFileExtension(string fileName, params string[] avaiExts)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return false;

            string ext = Path.GetExtension(fileName);   // 含有 . 號

            // 檢查是否包含於清單中
            if (avaiExts.Contains(ext.ToLower()))
                return true;
            else
                return false;
        }


        /// <summary> 驗證檔案容量是否在允許範圍 </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static bool ValidFileLength(byte[] bytes)
        {
            if (bytes == null)
                return false;

            int fileLength = bytes.Length;

            if (fileLength > _uploadBytes)
                return false;
            else
                return true;
        }
    }
}