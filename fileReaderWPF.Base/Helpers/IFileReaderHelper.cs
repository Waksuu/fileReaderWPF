using fileReaderWPF.Base.Model;
using System.Collections.Generic;

namespace fileReaderWPF.Base.Helpers
{
    public interface IFileReaderHelper
    {
        /// <summary>
        /// Gets IEnumrable of PhraseLocation from files in a given path
        /// <para></para>
        /// Works in Parallel context, using locks is necessary when needed
        /// </summary>
        /// <param name="filePath">Path where filed are contained</param>
        /// <param name="pattern">Phrase that we want to look for</param>
        /// <returns>Returns IEnumerable of PhraseLocation</returns>
        IEnumerable<PhraseLocation> GetPhraseLocationsFromFile(string filePath, string pattern);
    }
}
