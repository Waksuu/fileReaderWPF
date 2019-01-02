using System.Collections.Generic;

namespace fileReaderWPF.Base.Model
{
    public class PhraseLocation
    {
        public string Path { get; set; }
        public int Paragraph { get; set; }
        public string Sentence { get; set; }

        public override bool Equals(object obj)
        {
            var location = obj as PhraseLocation;
            return location != null &&
                   Path == location.Path &&
                   Paragraph == location.Paragraph;
        }

        public override int GetHashCode()
        {
            var hashCode = -1277029107;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Path);
            hashCode = hashCode * -1521134295 + Paragraph.GetHashCode();
            return hashCode;
        }
    }
}