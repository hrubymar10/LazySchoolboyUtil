namespace LazySchoolboyUtil
{
    public class Format
    {
        protected string formatName;
        protected string mime;
        protected int offset;
        protected byte[] magicNumbers;

        public Format(string formatName, string mime, int offset, byte[] magicNumbers)
        {
            this.formatName = formatName;
            this.mime = mime;
            this.offset = offset;
            this.magicNumbers = magicNumbers;
        }

        public string GetFormatName()
        {
            return formatName;
        }

        public string GetMime()
        {
            return mime;
        }

        public int GetOffset()
        {
            return offset;
        }

        public byte[] GetMagicNumbers()
        {
            return magicNumbers;
        }
    }
}
