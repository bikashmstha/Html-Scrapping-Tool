using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace AllTextExtractor_HtmlScrapping.Helpers
{
    class PageLinks
    {
        private readonly List<string> _onPageLinksList1;
        private readonly List<string> _onPageLinksList2;
        private Dictionary<string, string> _newFileContentsList;

        private string url;

        public PageLinks(string url)
        {
            this.url = url;
            this._newFileContentsList = new Dictionary<string, string>();
            this._onPageLinksList2 = new List<string>();
            this._onPageLinksList1 = new List<string>();

            GetOnPageLinksInitial();
            GetAllLinks();
            AddinListObject();
        }


        private void AddinListObject()
        {
            foreach (var item in _onPageLinksList2.Distinct())
            {
                Console.WriteLine("All Page Links :=> " + item);
                _newFileContentsList.Add(item, new ScrapHtmlFile(item).GetScrapedHtmlContents());
            }

        }
        
        private void GetOnPageLinksInitial()
        {
            Console.WriteLine("Getting Page Links ");

            var doc = new HtmlWeb().Load(url);
            var linkedPages = doc.DocumentNode.Descendants("a")
                .Select(a => a.GetAttributeValue("href", null))
                .Where(u => !String.IsNullOrEmpty(u));
            
            foreach (var item in linkedPages.Distinct())
            {
                if (item == "#") continue;



                if (!(item.StartsWith(url) || item.StartsWith("http://")))
                {
                    if (item.StartsWith(url))
                    {
                        _onPageLinksList1.Add(item);
                    }
                    _onPageLinksList1.Add(url + item);
                }
            }
        }


        private void GetOnPageLinksRecursive(string newUrl)
        {
            var doc = new HtmlWeb().Load(newUrl);
            var linkedPages = doc.DocumentNode.Descendants("a")
                .Select(a => a.GetAttributeValue("href", null))
                .Where(u => !string.IsNullOrEmpty(u));



            foreach (var item in linkedPages.Distinct())
            {
                if (item == "#") continue;

                if (!(item.StartsWith(url) || item.StartsWith("http://")))
                {
                    if (item.StartsWith(url))
                    {
                        _onPageLinksList2.Add(item);
                    }
                    _onPageLinksList2.Add(url + item);
                }
            }
        }


        private void GetAllLinks()
        {
            foreach (var item in _onPageLinksList1)
            {
                GetOnPageLinksRecursive(item);
            }
        }

        public Dictionary<string, string> GetUrlAndContentAsDictionary() => _newFileContentsList;
    }
}
