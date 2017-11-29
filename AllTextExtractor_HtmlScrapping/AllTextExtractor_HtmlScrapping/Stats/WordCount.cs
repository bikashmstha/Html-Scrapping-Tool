using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllTextExtractor_HtmlScrapping.Stats
{
    static class WordCount
    {
        public static int Get(string htmlContents)
        {
            int wordCount = 0, index = 0;

            while (index < htmlContents.Length)
            {
                // check if current char is part of a word
                while (index < htmlContents.Length && !char.IsWhiteSpace(htmlContents[index]))
                    index++;

                wordCount++;

                // skip whitespace until next word
                while (index < htmlContents.Length && char.IsWhiteSpace(htmlContents[index]))
                    index++;
            }

            return wordCount;
        }

    }
}
