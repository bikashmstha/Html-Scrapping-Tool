using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AllTextExtractor_HtmlScrapping.Helpers
{
    internal static class CreateAndSave
    {
        public static bool AsTextFile(string filePath, Dictionary<string, string> newFileContents)
        {
            try
            {
                var directoryInfo = Directory.CreateDirectory(filePath);

                foreach (var item in newFileContents.Keys)
                {
                    var newFilePath = directoryInfo.FullName + "\\" + item.Replace("http://", String.Empty).Replace("/", "_") + ".txt";
                    File.WriteAllText(newFilePath, newFileContents[item]);
                    Console.WriteLine(newFilePath);
                }
                
              
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }






    }
}
