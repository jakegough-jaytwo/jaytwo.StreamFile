using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using jaytwo.StreamFile.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace jaytwo.StreamFile
{
    public static class StreamFileContentExtensions
    {
        public static StreamFileContentWithMediaType WithMediaType(this StreamFileContent streamFileContent, string mediaType)
        {
            var result = streamFileContent.AsStreamFileContentWithMediaType();
            result.MediaType = mediaType;

            return result;
        }

        public static StreamFileContentWithMediaType WithMediaType(this StreamFileContent streamFileContent)
        {
            var result = streamFileContent.AsStreamFileContentWithMediaType();

            if (string.IsNullOrEmpty(result.MediaType))
            {
                result.SetDefaultMediaType();
            }

            return result;
        }

        public static FileStreamResult ToFileStreamResult(this StreamFileContent streamFileContent)
            => ToFileStreamResult(streamFileContent, false);

        public static FileStreamResult ToFileStreamResult(this StreamFileContent streamFileContent, bool enableRangeProcessing)
        {
            string contentType = streamFileContent.WithMediaType().MediaType;
            return ToFileStreamResult(streamFileContent, contentType, enableRangeProcessing);
        }

        public static FileStreamResult ToFileStreamResult(this StreamFileContent streamFileContent, string contentType, bool enableRangeProcessing)
        {
            return new FileStreamResult(streamFileContent.Content, contentType)
            {
                FileDownloadName = streamFileContent.FileName,
                EnableRangeProcessing = enableRangeProcessing,
            };
        }

        internal static StreamFileContentWithMediaType AsStreamFileContentWithMediaType(this StreamFileContent streamFileContent)
        {
            var result = streamFileContent as StreamFileContentWithMediaType;

            if (result == null)
            {
                result = new StreamFileContentWithMediaType()
                {
                    Content = streamFileContent.Content,
                    FileName = streamFileContent.FileName,
                    Length = streamFileContent.Length,
                };

                result.SetDefaultMediaType();
            }

            return result;
        }
    }
}
