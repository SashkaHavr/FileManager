using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace FileManager
{
    public class XmlLogger : ILogger
    {
        public void ClearLog()
        {
            if (File.Exists("Log.xml"))
                File.Delete("Log.xml");
        }

        public void Log(string actionType, string msg)
        {
            bool notEx = true;
            if (File.Exists("Log.xml"))
                notEx = false;
            FileStream fs = new FileStream("Log.xml", FileMode.OpenOrCreate);
            using (XmlTextWriter writer = new XmlTextWriter(fs, Encoding.Default))
            {
                if (notEx)
                {
                    writer.WriteStartDocument();
                    writer.WriteWhitespace(Environment.NewLine);
                    writer.WriteStartElement("Log");
                    writer.WriteWhitespace(Environment.NewLine);
                }
                else
                    writer.BaseStream.Seek(-Encoding.ASCII.GetBytes("</Log>").Length, SeekOrigin.End);
                writer.WriteWhitespace("    ");
                writer.WriteStartElement(actionType);
                writer.WriteAttributeString("Time", DateTime.Now.ToString());
                writer.WriteString(msg);
                writer.WriteEndElement();
                writer.WriteWhitespace(Environment.NewLine);
                writer.Flush();
                if (!notEx)
                {
                    fs.Seek(0, SeekOrigin.End);
                    fs.Write(Encoding.ASCII.GetBytes("</Log>"), 0, Encoding.ASCII.GetBytes("</Log>").Length);
                }
            }
        }

        public List<FileSystemInfo> History()
        {
            if (File.Exists("Log.xml"))
            {
                FileStream fs = new FileStream("Log.xml", FileMode.Open);
                using (XmlTextReader reader = new XmlTextReader(fs))
                {
                    List<FileSystemInfo> res = new List<FileSystemInfo>();
                    while(reader.ReadToFollowing("Move_In"))
                    {
                        reader.Read();
                        if(!res.ContainsPath(reader.Value))
                             res.Add(new DirectoryInfo(reader.Value));
                    }
                    return res;
                }
            }
            else
                throw new Exception("Log is empty");
        }
    }
}
