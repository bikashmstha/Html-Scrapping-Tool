using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using NUglify;


namespace AllTextExtractor_HtmlScrapping.Helpers
{
    internal class ScrapHtmlFile
    {
        private HtmlWeb hw = new HtmlWeb();
        private readonly HtmlDocument _htmlDoc;

        private string _newFileContents;

        private readonly string _bodyTagContents;

        public ScrapHtmlFile(string urlLink)
        {
          //  _bodyTagContents = Uglify.Html(htmlFileContents).ToString();

            try
            {
                _htmlDoc = hw.Load(urlLink);
                _bodyTagContents = Uglify.Html(_htmlDoc.ParsedText).ToString();
                //       _htmlDoc.LoadHtml(_bodyTagContents);
                RemoveComments();
                RemoveTagElements();
            }
            catch (Exception )
            {
            }
        
      
           
        }
        
        private void RemoveTagElements()
        {
            var newFileContents = Regex.Replace(_bodyTagContents, @"<script[^>]*>[\s\S]*?</script>", Environment.NewLine);
            newFileContents = Regex.Replace(newFileContents, @"<style[^>]*>[\s\S]*?</style>", Environment.NewLine);
            newFileContents = Regex.Replace(newFileContents, "<.*?>", Environment.NewLine);
            newFileContents = Regex.Replace(newFileContents, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);

            using (var reader = new StringReader(newFileContents))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Last() == '>' || line.First() == '<') continue;

                    _newFileContents = _newFileContents != string.Empty
                        ? _newFileContents + Environment.NewLine + line.Trim()
                        : line.Trim();
                }
            }
        }
        
        private void RemoveComments()
        {
            try
            {
                // get all comment nodes using XPATH
                if (_htmlDoc.DocumentNode == null) return;

                foreach (var comment in _htmlDoc.DocumentNode.SelectNodes("//comment()"))
                {
                    comment.ParentNode.RemoveChild(comment);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public string GetScrapedHtmlContents() =>_newFileContents;

    }
}
