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

        public override bool Equals(object obj)
        {
            if (!(obj is PhraseLocation))
            {
                return false;
            }

            var location = (PhraseLocation)obj;
            return Path == location.Path &&
                   Paragraph == location.Paragraph;
        }

        public override int GetHashCode()
        {
            var hashCode = 1852624543;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Path);
            hashCode = hashCode * -1521134295 + Paragraph.GetHashCode();
            return hashCode;
        }
    }
}