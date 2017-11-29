using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AllTextExtractor_HtmlScrapping.Helpers;
using HtmlAgilityPack;

namespace AllTextExtractor_HtmlScrapping
{
    class Program
    {

 
        static void Main(string[] args)
        {

            Console.WriteLine("Specify Path => ");
            var url = Console.ReadLine();
            Console.WriteLine("URL :=> " + url);


            var newFileContentsList = new PageLinks(url).GetUrlAndContentAsDictionary();
            
            CreateAndSave.AsTextFile("D:\\ScrappedHtmlData\\"+ url.Replace("http://", string.Empty), newFileContentsList);

            Console.WriteLine(""); Console.WriteLine("");
            Console.WriteLine("!.....!.....!.....!.....!___________D___________O___________N___________E___________!.....!.....!.....!.....!");
            Console.WriteLine("");
            Console.WriteLine("PRESS ANY KEY TO EXIT ...!!!");
            Console.ReadLine();
        }





    }
}
