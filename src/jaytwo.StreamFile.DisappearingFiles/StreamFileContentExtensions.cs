using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using jaytwo.DisappearingFiles;

namespace jaytwo.StreamFile
{
    public static class StreamFileContentExtensions
    {
        public static T CacheToDisk<T>(this T streamFileContent)
            where T : StreamFileContent
        {
            var oldStream = streamFileContent.Content;
            var newStream = DisappearingFile.CreateTempFile();

            try
            {
                oldStream.CopyTo(newStream);
                newStream.Position = 0;
            }
            catch
            {
                newStream.Dispose();
                throw;
            }

            streamFileContent.Content = newStream;
            oldStream.Dispose();

            return streamFileContent;
        }

        public static async Task<T> CacheToDiskAsync<T>(this T streamFileContent)
            where T : StreamFileContent
        {
            var oldStream = streamFileContent.Content;
            var newStream = DisappearingFile.CreateTempFile();

            try
            {
                await oldStream.CopyToAsync(newStream);
                newStream.Position = 0;
            }
            catch
            {
                newStream.Dispose();
                throw;
            }

            streamFileContent.Content = newStream;
            oldStream.Dispose();

            return streamFileContent;
        }
    }
}
