﻿using fileReaderWPF.Base.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace fileReaderWPF.Base.Logic
{
    public interface ISearchLogicService
    {
        /// <summary>
        /// Works asynchronous and uses Parallel foreach to execute search logic on each of the files
        /// </summary>
        /// <param name="extensions">IEnumerable of file extensions that we want to retrieve from folderPath</param>
        /// <param name="phrase">Sentence that we want to look for</param>
        /// <param name="folderPath">Folder in which we want to look for files (with correct extension)</param>
        /// <param name="container">UnityContainer with dependencies</param>
        /// <returns>Returns IEnumerable of PhraseLocation with given parameters</returns>
        Task<IEnumerable<PhraseLocation>> SearchWordsInFilesAsync(IEnumerable<string> extensions, string phrase, string folderPath, IUnityContainer container);
    }
}