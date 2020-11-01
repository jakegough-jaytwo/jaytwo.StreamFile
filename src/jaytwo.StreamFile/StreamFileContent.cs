using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace jaytwo.StreamFile
{
    public class StreamFileContent : IDisposable
    {
        public StreamFileContent()
        {
        }

        public StreamFileContent(string filePath)
            : this(new FileInfo(filePath))
        {
        }

        public StreamFileContent(FileInfo file)
        {
            file.Refresh();

            FileName = file.Name;
            Length = file.Length;
            Content = file.Open(FileMode.Open);
        }

        public StreamFileContent(Stream content, string fileName)
        {
            Content = content;
            FileName = fileName;
            Length = content.Length;
        }

        public StreamFileContent(byte[] bytes, string fileName)
        {
            Content = new MemoryStream(bytes);
            FileName = fileName;
            Length = bytes.Length;
        }

        public Stream Content { get; set; }

        public long? Length { get; set; }

        public string FileName { get; set; }

        public void Dispose()
        {
            Content?.Dispose();
        }
    }
}
