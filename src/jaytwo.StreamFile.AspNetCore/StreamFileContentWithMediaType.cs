using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.StaticFiles;

namespace jaytwo.StreamFile.AspNetCore
{
    public class StreamFileContentWithMediaType : StreamFileContent
    {
        public StreamFileContentWithMediaType()
        {
        }

        public StreamFileContentWithMediaType(string filePath)
            : base(filePath)
        {
            SetDefaultMediaType();
        }

        public StreamFileContentWithMediaType(FileInfo file)
            : base(file)
        {
            SetDefaultMediaType();
        }

        public StreamFileContentWithMediaType(Stream stream, string fileName)
            : base(stream, fileName)
        {
            SetDefaultMediaType();
        }

        public StreamFileContentWithMediaType(byte[] bytes, string fileName)
            : base(bytes, fileName)
        {
            SetDefaultMediaType();
        }

        public string MediaType { get; set; }

        internal void SetDefaultMediaType()
        {
            if (!string.IsNullOrEmpty(FileName)
                && new FileExtensionContentTypeProvider().TryGetContentType(FileName, out string contentType))
            {
                MediaType = contentType;
            }
        }
    }
}
