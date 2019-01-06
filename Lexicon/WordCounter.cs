using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexicon
{
    public class WordCounter
    {
        public string word;
        public int Count;

        public WordCounter(string word)
        {
            this.word = word;
            Count = 1;
        }
    }
}
