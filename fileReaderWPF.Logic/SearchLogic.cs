using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using fileReaderWPF.Base.Logic;
using fileReaderWPF.Base.Model;

namespace fileReaderWPF.Base.Logic
{
    public class SearchLogic : ISearchLogic
    {
        public Task<IEnumerable<PhraseLocation>> SearchWords(IEnumerable<string> filePaths, string phrase, object _syncLock)
        {
            return Task.Run(() => {
                List<PhraseLocation> results = new List<PhraseLocation>();
                List<string> lines = new List<string>();

                string line;
                bool contains;

                int lineCount = 0;

                foreach (var item in filePaths)
                {
                    lock (_syncLock)
                    {
                        var liness = File.ReadLines(item);

                        StreamReader sr = new StreamReader(item);
                        while ((line = sr.ReadLine()) != null)
                        {
                            lineCount++;

                            contains = Regex.IsMatch(line, @"\b" + phrase + @"\b");
                            if (contains)
                            {
                                results.Add(new PhraseLocation { Line = lineCount, Path = item, Sentence = line });
                            }
                        }
                        sr.Close();
                        lineCount = 0;
                    }
                }
                return results.AsEnumerable();
            });
        }
    }
}
