using System.Collections.Generic;

namespace fileReaderWPF.Base.Model
{
    /// <summary>
    /// Contains localization and text of a sentence
    /// </summary>
    public struct PhraseLocation
    {
        /// <summary>
        /// Path of the file
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Paragraph (or page in case of pdf files) that informs us where the sentence is
        /// </summary>
        public int Paragraph { get; set; }

        /// <summary>
        /// Block of text with desired sentence
        /// </summary>
        public string Sentence { get; set; }
    }
}