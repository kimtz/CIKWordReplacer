using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Raven.Client.Document;

namespace CIK.WordReplacer
{
    public class WordReplacer
    {
        public IStorage documentStore;

        public WordReplacer(IStorage storage)
        {
            documentStore = storage;
        }

        public string Replace(string illegalWord, string validWord, string text)
        {
            return text.Replace(illegalWord,validWord);
        }

        public string ReplaceWordsInText(string textPath, IEnumerable<ReplacementPair> rules)
        {
            var textToReplace = File.ReadAllText(textPath);
            var linesToReplace = textToReplace.Split('.');
            var stringBuilder = new StringBuilder();
            foreach (var line in linesToReplace)
            {
                var count = 0;
                var result = line;
                foreach (var replacementPair in rules)
                {
                    result = Replace(replacementPair.illegalWord, replacementPair.validWord, result);
                }
                if (result != line)
                {
                    stringBuilder.Append(result);
                    stringBuilder.Append(".");
                    count++;
                }
                if (count == 0)
                {
                    stringBuilder.Append(line);
                    stringBuilder.Append(".");
                }
            }
            if (stringBuilder.ToString().EndsWith(".."))
            {
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
            }
            return stringBuilder.ToString();
        }
    }
}
