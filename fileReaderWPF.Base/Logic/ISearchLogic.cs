﻿using fileReaderWPF.Base.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace fileReaderWPF.Base.Logic
{
    public interface ISearchLogic
    {
        /// <summary>
        /// Works asynchronous and uses Parallel foreach to execute search logic on each of the files
        /// </summary>
        /// <param name="extensions">IEnumerable of file extensions that we want to retrieve from folderPath</param>
        /// <param name="phrase">Sentence that we want to look for</param>
        /// <param name="folderPath">Folder in which we want to look for files (with correct extension)</param>
        /// <exception cref="System.ArgumentNullException">Throws when one of the parameters is null, white space or extensions are empty</exception>
        /// <exception cref="System.InvalidOperationException">Throws when directory does not exist</exception>
        /// <exception cref="fileReaderWPF.Base.Exceptions.EmptyFolderException">Throws when directory does exist, but there are no matching files</exception>
        /// <returns>Returns IEnumerable of PhraseLocation with given parameters</returns>
        Task<IEnumerable<PhraseLocation>> SearchWordsInFilesAsync(IEnumerable<string> extensions, string phrase, string folderPath);
    }
}
