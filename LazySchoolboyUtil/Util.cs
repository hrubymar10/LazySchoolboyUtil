using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace LazySchoolboyUtil
{
    public static class Util
    {
        public static string version = "1.4";
        public static double progress;

        static RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();

        public static void GenerateFile(string path, int size, Format format)
        {
            progress = 0;
            
            byte[] bytesToBeSaved = new byte[size];

            rand.GetBytes(bytesToBeSaved);

            for (int i = format.GetOffset(); i <= format.GetOffset() + format.GetMagicNumbers().Length - 1; i++)
            {
                progress = (double)i / ((double)format.GetMagicNumbers().Length / (double)100);
                bytesToBeSaved[i] = format.GetMagicNumbers()[i - format.GetOffset()];
            }

            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                fs.Write(bytesToBeSaved, 0, bytesToBeSaved.Length);
            }
            
            progress = 100;
        }

        public static List<Format> GetListOfFormats()
        {
            List<Format> toReturn = new List<Format>();
            toReturn.Add(new Format("123", "application/vnd.lotus-1-2-3", 0, new byte[] { 0x00, 0x00, 0x1A, 0x00, 0x05, 0x10, 0x04 }));
            toReturn.Add(new Format("cpl", "application/cpl+xml", 0, new byte[] { 0x4D, 0x5A }));
            toReturn.Add(new Format("epub", "application/epub+zip", 0, new byte[] { 0x50, 0x4B, 0x03, 0x04, 0x0A, 0x00, 0x02, 0x00 }));
            toReturn.Add(new Format("ttf", "application/font-sfnt", 0, new byte[] { 0x00, 0x01, 0x00, 0x00, 0x00 }));
            toReturn.Add(new Format("gz", "application/gzip", 0, new byte[] { 0x1F, 0x8B, 0x08 }));
            toReturn.Add(new Format("tgz", "application/gzip", 0, new byte[] { 0x1F, 0x8B, 0x08 }));
            toReturn.Add(new Format("hqx", "application/mac-binhex40", 0, new byte[] { 0x28, 0x54, 0x68, 0x69, 0x73, 0x20, 0x66, 0x69, 0x6C, 0x65, 0x20, 0x6D, 0x75, 0x73, 0x74, 0x20, 0x62, 0x65, 0x20, 0x63, 0x6F, 0x6E, 0x76, 0x65, 0x72, 0x74, 0x65, 0x64, 0x20, 0x77, 0x69, 0x74, 0x68, 0x20, 0x42, 0x69, 0x6E, 0x48, 0x65, 0x78, 0x20 }));
            toReturn.Add(new Format("doc", "application/msword", 0, new byte[] { 0x0D, 0x44, 0x4F, 0x43 }));
            toReturn.Add(new Format("mxf", "application/mxf", 0, new byte[] { 0x06, 0x0E, 0x2B, 0x34, 0x02, 0x05, 0x01, 0x01, 0x0D, 0x01, 0x02, 0x01, 0x01, 0x02 }));
            toReturn.Add(new Format("lha", "application/octet-stream", 2, new byte[] { 0x2D, 0x6C, 0x68 }));
            toReturn.Add(new Format("lzh", "application/octet-stream", 2, new byte[] { 0x2D, 0x6C, 0x68 }));
            toReturn.Add(new Format("exe", "application/octet-stream", 0, new byte[] { 0x4D, 0x5A }));
            toReturn.Add(new Format("class", "application/octet-stream", 0, new byte[] { 0xCA, 0xFE, 0xBA, 0xBE }));
            toReturn.Add(new Format("dll", "application/octet-stream", 0, new byte[] { 0x4D, 0x5A }));
            toReturn.Add(new Format("img", "application/octet-stream", 0, new byte[] { 0xEB, 0x3C, 0x90, 0x2A }));
            toReturn.Add(new Format("iso", "application/octet-stream", 0, new byte[] { 0x43, 0x44, 0x30, 0x30, 0x31 }));
            toReturn.Add(new Format("ogx", "application/ogg", 0, new byte[] { 0x4F, 0x67, 0x67, 0x53, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }));
            toReturn.Add(new Format("oxps", "application/oxps", 0, new byte[] { 0x50, 0x4B, 0x03, 0x04 }));
            toReturn.Add(new Format("pdf", "application/pdf", 0, new byte[] { 0x25, 0x50, 0x44, 0x46 }));
            toReturn.Add(new Format("p10", "application/pkcs10", 0, new byte[] { 0x64, 0x00, 0x00, 0x00 }));
            toReturn.Add(new Format("pls", "application/pls+xml", 0, new byte[] { 0x5B, 0x70, 0x6C, 0x61, 0x79, 0x6C, 0x69, 0x73, 0x74, 0x5D }));
            toReturn.Add(new Format("eps", "application/postscript", 0, new byte[] { 0x25, 0x21, 0x50, 0x53, 0x2D, 0x41, 0x64, 0x6F, 0x62, 0x65, 0x2D, 0x33, 0x2E, 0x30, 0x20, 0x45, 0x50, 0x53, 0x46, 0x2D, 0x33, 0x20, 0x30 }));
            toReturn.Add(new Format("ai", "application/postscript", 0, new byte[] { 0x25, 0x50, 0x44, 0x46 }));
            toReturn.Add(new Format("rtf", "application/rtf", 0, new byte[] { 0x7B, 0x5C, 0x72, 0x74, 0x66, 0x31 }));
            toReturn.Add(new Format("tsa", "application/tamp-sequence-adjust", 0, new byte[] { 0x47 }));
            toReturn.Add(new Format("msf", "application/vnd.epson.msf", 0, new byte[] { 0x2F, 0x2F, 0x20, 0x3C, 0x21, 0x2D, 0x2D, 0x20, 0x3C, 0x6D, 0x64, 0x62, 0x3A, 0x6D, 0x6F, 0x72, 0x6B, 0x3A, 0x7A }));
            toReturn.Add(new Format("fdf", "application/vnd.fdf", 0, new byte[] { 0x25, 0x50, 0x44, 0x46 }));
            toReturn.Add(new Format("fm", "application/vnd.framemaker", 0, new byte[] { 0x3C, 0x4D, 0x61, 0x6B, 0x65, 0x72, 0x46, 0x69, 0x6C, 0x65, 0x20 }));
            toReturn.Add(new Format("kmz", "application/vnd.google-earth.kmz", 0, new byte[] { 0x50, 0x4B, 0x03, 0x04 }));
            toReturn.Add(new Format("tpl", "application/vnd.groove-tool-template", 0, new byte[] { 0x00, 0x20, 0xAF, 0x30 }));
            toReturn.Add(new Format("kwd", "application/vnd.kde.kword", 0, new byte[] { 0x50, 0x4B, 0x03, 0x04 }));
            toReturn.Add(new Format("wk4", "application/vnd.lotus-1-2-3", 0, new byte[] { 0x00, 0x00, 0x1A, 0x00, 0x02, 0x10, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00 }));
            toReturn.Add(new Format("wk3", "application/vnd.lotus-1-2-3", 0, new byte[] { 0x00, 0x00, 0x1A, 0x00, 0x00, 0x10, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00 }));
            toReturn.Add(new Format("wk1", "application/vnd.lotus-1-2-3", 0, new byte[] { 0x00, 0x00, 0x02, 0x00, 0x06, 0x04, 0x06, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00 }));
            toReturn.Add(new Format("apr", "application/vnd.lotus-approach", 0, new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 }));
            toReturn.Add(new Format("nsf", "application/vnd.lotus-notes", 0, new byte[] { 0x1A, 0x00, 0x00, 0x04, 0x00, 0x00 }));
            toReturn.Add(new Format("ntf", "application/vnd.lotus-notes", 0, new byte[] { 0x4E, 0x49, 0x54, 0x46, 0x30 }));
            toReturn.Add(new Format("org", "application/vnd.lotus-organizer", 0, new byte[] { 0x41, 0x4F, 0x4C, 0x56, 0x4D, 0x31, 0x30, 0x30 }));
            toReturn.Add(new Format("lwp", "application/vnd.lotus-wordpro", 0, new byte[] { 0x57, 0x6F, 0x72, 0x64, 0x50, 0x72, 0x6F }));
            toReturn.Add(new Format("sam", "application/vnd.lotus-wordpro", 0, new byte[] { 0x5B, 0x50, 0x68, 0x6F, 0x6E, 0x65, 0x5D }));
            toReturn.Add(new Format("mif", "application/vnd.mif", 0, new byte[] { 0x56, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x20 }));
            toReturn.Add(new Format("xul", "application/vnd.mozilla.xul+xml", 0, new byte[] { 0x3C, 0x3F, 0x78, 0x6D, 0x6C, 0x20, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3D, 0x22, 0x31, 0x2E, 0x30, 0x22, 0x3F, 0x3E }));
            toReturn.Add(new Format("asf", "application/vnd.ms-asf", 0, new byte[] { 0x30, 0x26, 0xB2, 0x75, 0x8E, 0x66, 0xCF, 0x11, 0xA6, 0xD9, 0x00, 0xAA, 0x00, 0x62, 0xCE, 0x6C }));
            toReturn.Add(new Format("cab", "application/vnd.ms-cab-compressed", 0, new byte[] { 0x49, 0x53, 0x63, 0x28 }));
            toReturn.Add(new Format("xls", "application/vnd.ms-excel", 0, new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 }));
            toReturn.Add(new Format("xla", "application/vnd.ms-excel", 0, new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 }));
            toReturn.Add(new Format("chm", "application/vnd.ms-htmlhelp", 0, new byte[] { 0x49, 0x54, 0x53, 0x46 }));
            toReturn.Add(new Format("ppt", "application/vnd.ms-powerpoint", 0, new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 }));
            toReturn.Add(new Format("pps", "application/vnd.ms-powerpoint", 0, new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 }));
            toReturn.Add(new Format("wks", "application/application/vnd.ms-works", 0, new byte[] { 0xFF, 0x00, 0x02, 0x00, 0x04, 0x04, 0x05, 0x54, 0x02, 0x00 }));
            toReturn.Add(new Format("wpl", "application/vnd.ms-wpl", 84, new byte[] { 0x4D, 0x69, 0x63, 0x72, 0x6F, 0x73, 0x6F, 0x66, 0x74, 0x20, 0x57, 0x69, 0x6E, 0x64, 0x6F, 0x77, 0x73, 0x20, 0x4D, 0x65, 0x64, 0x69, 0x61, 0x20, 0x50, 0x6C, 0x61, 0x79, 0x65, 0x72, 0x20, 0x2D, 0x2D, 0x20 }));
            toReturn.Add(new Format("xps", "application/vnd.ms-xpsdocument", 0, new byte[] { 0x50, 0x4B, 0x03, 0x04 }));
            toReturn.Add(new Format("cif", "application/vnd.multiad.creator.cif", 2, new byte[] { 0x5B, 0x56, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E }));
            toReturn.Add(new Format("odp", "application/vnd.oasis.opendocument.presentation", 0, new byte[] { 0x50, 0x4B, 0x03, 0x04 }));
            toReturn.Add(new Format("odt", "application/vnd.oasis.opendocument.text", 0, new byte[] { 0x50, 0x4B, 0x03, 0x04 }));
            toReturn.Add(new Format("ott", "application/vnd.oasis.opendocument.text-template", 0, new byte[] { 0x50, 0x4B, 0x03, 0x04 }));
            toReturn.Add(new Format("pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation", 0, new byte[] { 0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x06, 0x00 }));
            toReturn.Add(new Format("xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 0, new byte[] { 0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x06, 0x00 }));
            toReturn.Add(new Format("docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", 0, new byte[] { 0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x06, 0x00 }));
            toReturn.Add(new Format("prc", "application/vnd.palm", 0, new byte[] { 0x42, 0x4F, 0x4F, 0x4B, 0x4D, 0x4F, 0x42, 0x49 }));
            toReturn.Add(new Format("pdb", "application/vnd.palm", 0, new byte[] { 0x4D, 0x2D, 0x57, 0x20, 0x50, 0x6F, 0x63, 0x6B, 0x65, 0x74, 0x20, 0x44, 0x69, 0x63, 0x74, 0x69 }));
            toReturn.Add(new Format("qxd", "application/vnd.Quark.QuarkXPress", 0, new byte[] { 0x00, 0x00, 0x4D, 0x4D, 0x58, 0x50, 0x52 }));
            toReturn.Add(new Format("rar", "application/vnd.rar", 0, new byte[] { 0x52, 0x61, 0x72, 0x21, 0x1A, 0x07, 0x01, 0x00 }));
            toReturn.Add(new Format("mmf", "application/vnd.smaf", 0, new byte[] { 0x4D, 0x4D, 0x4D, 0x44, 0x00, 0x00 }));
            toReturn.Add(new Format("cap", "application/vnd.tcpdump.pcap", 0, new byte[] { 0x52, 0x54, 0x53, 0x53 }));
            toReturn.Add(new Format("dmp", "application/vnd.tcpdump.pcap", 0, new byte[] { 0x4D, 0x44, 0x4D, 0x50, 0x93, 0xA7 }));
            toReturn.Add(new Format("wpd", "application/vnd.wordperfect", 0, new byte[] { 0xFF, 0x57, 0x50, 0x43 }));
            toReturn.Add(new Format("xar", "application/vnd.xara", 0, new byte[] { 0x78, 0x61, 0x72, 0x21 }));
            toReturn.Add(new Format("spf", "application/vnd.yamaha.smaf-phrase", 0, new byte[] { 0x53, 0x50, 0x46, 0x49, 0x00 }));
            toReturn.Add(new Format("dtd", "application/xml-dtd", 0, new byte[] { 0x07, 0x64, 0x74, 0x32, 0x64, 0x64, 0x74, 0x64 }));
            toReturn.Add(new Format("zip", "application/zip", 0, new byte[] { 0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x08, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }));
            toReturn.Add(new Format("amr", "application/AMR", 0, new byte[] { 0x23, 0x21, 0x41, 0x4D, 0x52 }));
            toReturn.Add(new Format("au", "audio/basic", 0, new byte[] { 0x2E, 0x73, 0x6E, 0x64 }));
            toReturn.Add(new Format("m4a", "audio/mp4", 0, new byte[] { 0x00, 0x00, 0x00, 0x20, 0x66, 0x74, 0x79, 0x70, 0x4D, 0x34, 0x41, 0x20 }));
            toReturn.Add(new Format("mp3", "audio/mpeg", 0, new byte[] { 0x49, 0x44, 0x33 }));
            toReturn.Add(new Format("oga", "audio/ogg", 0, new byte[] { 0x4F, 0x67, 0x67, 0x53, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }));
            toReturn.Add(new Format("ogg", "audio/ogg", 0, new byte[] { 0x4F, 0x67, 0x67, 0x53, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }));
            toReturn.Add(new Format("qcp", "audio/qcelp", 0, new byte[] { 0x52, 0x49, 0x46, 0x46 }));
            toReturn.Add(new Format("koz", "audio/vnd.audikoz", 0, new byte[] { 0x49, 0x44, 0x33, 0x03, 0x00, 0x00, 0x00 }));
            toReturn.Add(new Format("bmp", "image/bmp", 0, new byte[] { 0x42, 0x4D }));
            toReturn.Add(new Format("dib", "image/bmp", 0, new byte[] { 0x42, 0x4D }));
            toReturn.Add(new Format("emf", "image/emf", 0, new byte[] { 0x01, 0x00, 0x00, 0x00 }));
            toReturn.Add(new Format("fits", "image/fits", 0, new byte[] { 0x53, 0x49, 0x4D, 0x50, 0x4C, 0x45, 0x20, 0x20, 0x3D, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x54 }));
            toReturn.Add(new Format("gif", "image/gif", 0, new byte[] { 0x47, 0x49, 0x46, 0x38, 0x39, 0x61 }));
            toReturn.Add(new Format("jp2", "image/jp2", 0, new byte[] { 0x00, 0x00, 0x00, 0x0C, 0x6A, 0x50, 0x20, 0x20, 0x0D, 0x0A }));
            toReturn.Add(new Format("jpg", "image/jpeg", 0, new byte[] { 0xFF, 0xD8 }));
            toReturn.Add(new Format("jpeg", "image/jpeg", 0, new byte[] { 0xFF, 0xD8 }));
            toReturn.Add(new Format("jpe", "image/jpeg", 0, new byte[] { 0xFF, 0xD8 }));
            toReturn.Add(new Format("jfif", "image/jpeg", 0, new byte[] { 0xFF, 0xD8 }));
            toReturn.Add(new Format("png", "image/png", 0, new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }));
            toReturn.Add(new Format("tiff", "image/tiff", 0, new byte[] { 0x4D, 0x4D, 0x00, 0x2B }));
            toReturn.Add(new Format("tif", "image/tiff", 0, new byte[] { 0x4D, 0x4D, 0x00, 0x2B }));
            toReturn.Add(new Format("psd", "image/vnd.adobe.photoshop", 0, new byte[] { 0x38, 0x42, 0x50, 0x53 }));
            toReturn.Add(new Format("dwg", "image/vnd.dwg", 0, new byte[] { 0x41, 0x43, 0x31, 0x30 }));
            toReturn.Add(new Format("ico", "image/vnd.microsoft.icon", 0, new byte[] { 0x00, 0x00, 0x01, 0x00 }));
            toReturn.Add(new Format("mdi", "image/vnd.ms-modi", 0, new byte[] { 0x45, 0x50 }));
            toReturn.Add(new Format("hdr", "image/vnd.radiance", 0, new byte[] { 0x49, 0x53, 0x63, 0x28 }));
            toReturn.Add(new Format("pcx", "image/vnd.zbrush.pcx", 512, new byte[] { 0x09, 0x08, 0x10, 0x00, 0x00, 0x06, 0x05, 0x00 }));
            toReturn.Add(new Format("wmf", "image/wmf", 0, new byte[] { 0xD7, 0xCD, 0xC6, 0x9A }));
            toReturn.Add(new Format("eml", "message/rfc822", 0, new byte[] { 0x52, 0x65, 0x74, 0x75, 0x72, 0x6E, 0x2D, 0x50, 0x61, 0x74, 0x68, 0x3A, 0x20 }));
            toReturn.Add(new Format("art", "message/rfc822", 0, new byte[] { 0x4A, 0x47, 0x04, 0x0E }));
            toReturn.Add(new Format("manifest", "text/cache-manifest", 0, new byte[] { 0x3C, 0x3F, 0x78, 0x6D, 0x6C, 0x20, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x3D }));
            toReturn.Add(new Format("log", "text/plain", 0, new byte[] { 0x2A, 0x2A, 0x2A, 0x20, 0x20, 0x49, 0x6E, 0x73, 0x74, 0x61, 0x6C, 0x6C, 0x61, 0x74, 0x69, 0x6F, 0x6E, 0x20, 0x53, 0x74, 0x61, 0x72, 0x74, 0x65, 0x64, 0x20 }));
            toReturn.Add(new Format("tsv", "text/tab-separated-values", 0, new byte[] { 0x47 }));
            toReturn.Add(new Format("vcf", "text/vcard", 0, new byte[] { 0x42, 0x45, 0x47, 0x49, 0x4E, 0x3A, 0x56, 0x43, 0x41, 0x52, 0x44, 0x0D, 0x0A }));
            toReturn.Add(new Format("dms", "text/vnd.DMClientScript", 0, new byte[] { 0x44, 0x4D, 0x53, 0x21 }));
            toReturn.Add(new Format("dot", "text/vnd.graphviz", 0, new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 }));
            toReturn.Add(new Format("ts", "text/vnd.trolltech.linguist", 0, new byte[] { 0x47 }));
            toReturn.Add(new Format("3gp", "text/3gpp", 0, new byte[] { 0x00, 0x00, 0x00, 0x20, 0x66, 0x74, 0x79, 0x70, 0x33, 0x67, 0x70 }));
            toReturn.Add(new Format("3g2", "text/3gpp2", 0, new byte[] { 0x00, 0x00, 0x00, 0x20, 0x66, 0x74, 0x79, 0x70, 0x33, 0x67, 0x70 }));           
            toReturn.Add(new Format("mp4", "text/mp4", 4, new byte[] { 0x66, 0x74, 0x79, 0x70, 0x69, 0x73, 0x6F, 0x6D }));
            toReturn.Add(new Format("m4v", "text/mp4", 4, new byte[] { 0x66, 0x74, 0x79, 0x70, 0x6D, 0x70, 0x34, 0x32 }));
            toReturn.Add(new Format("mpeg", "text/mpeg", 0, new byte[] { 0xFF, 0xD8 }));
            toReturn.Add(new Format("mpg", "text/mpeg", 0, new byte[] { 0xFF, 0xD8 }));
            toReturn.Add(new Format("ogv", "text/ogg", 0, new byte[] { 0x4F, 0x67, 0x67, 0x53, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }));
            toReturn.Add(new Format("mov", "text/quicktime", 4, new byte[] { 0x6D, 0x6F, 0x6F, 0x76 }));
            toReturn.Add(new Format("cpt", "application/mac-compactpro", 0, new byte[] { 0x43, 0x50, 0x54, 0x46, 0x49, 0x4C, 0x45 }));
            toReturn.Add(new Format("sxc", "application/vnd.sun.xml.calc", 0, new byte[] { 0x50, 0x4B, 0x03, 0x04 }));
            toReturn.Add(new Format("sxd", "application/vnd.sun.xml.draw", 0, new byte[] { 0x50, 0x4B, 0x03, 0x04 }));
            toReturn.Add(new Format("sxi", "application/vnd.sun.xml.impress", 0, new byte[] { 0x50, 0x4B, 0x03, 0x04 }));
            toReturn.Add(new Format("sxw", "application/vnd.sun.xml.writer", 0, new byte[] { 0x50, 0x4B, 0x03, 0x04 }));
            toReturn.Add(new Format("bz2", "application/x-bzip2", 0, new byte[] { 0x42, 0x5A, 0x68 }));
            toReturn.Add(new Format("vcd", "application/x-cdlink", 0, new byte[] { 0x45, 0x4E, 0x54, 0x52, 0x59, 0x56, 0x43, 0x44, 0x02, 0x00, 0x00, 0x01, 0x02, 0x00, 0x18, 0x58 }));
            toReturn.Add(new Format("csh", "application/x-csh", 0, new byte[] { 0x63, 0x75, 0x73, 0x68, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00 }));
            toReturn.Add(new Format("spl", "application/x-futuresplash", 0, new byte[] { 0x00, 0x00, 0x01, 0x00 }));
            toReturn.Add(new Format("jar", "application/x-java-archive", 0, new byte[] { 0x5F, 0x27, 0xA8, 0x89 }));
            toReturn.Add(new Format("rpm", "application/x-rpm", 0, new byte[] { 0xED, 0xAB, 0xEE, 0xDB }));
            toReturn.Add(new Format("swf", "application/x-shockwave-flash", 0, new byte[] { 0x5A, 0x57, 0x53 }));
            toReturn.Add(new Format("sit", "application/x-stuffit", 0, new byte[] { 0x53, 0x49, 0x54, 0x21, 0x00 }));
            toReturn.Add(new Format("tar", "application/x-tar", 257, new byte[] { 0x75, 0x73, 0x74, 0x61, 0x72 }));
            toReturn.Add(new Format("xpi", "application/x-xpinstall", 0, new byte[] { 0x50, 0x4B, 0x03, 0x04 }));
            toReturn.Add(new Format("xz", "application/x-xz", 0, new byte[] { 0xFD, 0x37, 0x7A, 0x58, 0x5A, 0x00 }));
            toReturn.Add(new Format("mid", "audio/midi", 0, new byte[] { 0x4D, 0x54, 0x68, 0x64 }));
            toReturn.Add(new Format("midi", "audio/midi", 0, new byte[] { 0x4D, 0x54, 0x68, 0x64 }));
            toReturn.Add(new Format("aiff", "audio/x-aiff", 0, new byte[] { 0x46, 0x4F, 0x52, 0x4D, 0x00 }));
            toReturn.Add(new Format("flac", "audio/x-flac", 0, new byte[] { 0x66, 0x4C, 0x61, 0x43, 0x00, 0x00, 0x00, 0x22 }));
            toReturn.Add(new Format("wma", "audio/x-ms-vma", 0, new byte[] { 0x30, 0x26, 0xB2, 0x75, 0x8E, 0x66, 0xCF, 0x11, 0xA6, 0xD9, 0x00, 0xAA, 0x00, 0x62, 0xCE, 0x6C }));
            toReturn.Add(new Format("ram", "audio/x-pn-realaudio", 0, new byte[] { 0x72, 0x74, 0x73, 0x70, 0x3A, 0x2F, 0x2F }));
            toReturn.Add(new Format("rm", "audio/x-pn-realaudio", 0, new byte[] { 0x2E, 0x52, 0x4D, 0x46 }));
            toReturn.Add(new Format("ra", "audio/x-realaudio", 0, new byte[] { 0x2E, 0x72, 0x61, 0xFD, 0x00 }));
            toReturn.Add(new Format("wav", "audio/x-wav", 0, new byte[] { 0x52, 0x49, 0x46, 0x46 }));
            toReturn.Add(new Format("webp", "image/webp", 0, new byte[] { 0x52, 0x49, 0x46, 0x46 }));
            toReturn.Add(new Format("pgm", "image/x-portable-graymap", 0, new byte[] { 0x50, 0x35, 0x0A }));
            toReturn.Add(new Format("rgb", "image/x-rgb", 0, new byte[] { 0x01, 0xDA, 0x01, 0x01, 0x00, 0x03 }));
            toReturn.Add(new Format("webm", "video/webm", 0, new byte[] { 0x1A, 0x45, 0xDF, 0xA3 }));
            toReturn.Add(new Format("flv", "video/x-flv", 0, new byte[] { 0x6, 0x4C, 0x56, 0x01 }));
            toReturn.Add(new Format("mkv", "video/x-matroska", 0, new byte[] { 0x1A, 0x45, 0xDF, 0xA3 }));
            toReturn.Add(new Format("asx", "video/x-ms-asf", 0, new byte[] { 0x3C }));
            toReturn.Add(new Format("wmv", "video/x-ms-wmv", 0, new byte[] { 0x30, 0x26, 0xB2, 0x75, 0x8E, 0x66, 0xCF, 0x11, 0xA6, 0xD9, 0x00, 0xAA, 0x00, 0x62, 0xCE, 0x6C }));
            toReturn.Add(new Format("avi", "video/x-msvideo", 0, new byte[] { 0x52, 0x49, 0x46, 0x46 }));

            return toReturn;
        }
    }
}
