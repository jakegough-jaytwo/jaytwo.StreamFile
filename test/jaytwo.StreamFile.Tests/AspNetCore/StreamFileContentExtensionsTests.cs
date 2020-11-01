using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using jaytwo.StreamFile.AspNetCore;
using Xunit;

namespace jaytwo.StreamFile.Tests.AspNetCore
{
    public class StreamFileContentExtensionsTests
    {
        [Theory]
        [InlineData("myfile.txt", "text/plain")]
        [InlineData("myfile.pdf", "application/pdf")]
        [InlineData("myfile", null)]
        [InlineData(null, null)]
        public void WithMediaType_adds_default_media_type(string fileName, string expectedMediaType)
        {
            // arrange
            using (var stream = new MemoryStream())
            using (var content = new StreamFileContent(stream, fileName))
            {
                // act
                var withMediaType = content.WithMediaType();

                // assert
                Assert.Equal(expectedMediaType, withMediaType.MediaType);
            }
        }

        [Theory]
        [InlineData("text/plain")]
        [InlineData("foo")]
        [InlineData(null)]
        public void WithMediaType_adds_specified_media_type(string mediaType)
        {
            // arrange
            using (var stream = new MemoryStream())
            using (var content = new StreamFileContent(stream, "myfile.txt"))
            {
                // act
                var withMediaType = content.WithMediaType(mediaType);

                // assert
                Assert.Equal(mediaType, withMediaType.MediaType);
            }
        }

        [Fact]
        public void ToFileStreamResult_preserves_content_filename()
        {
            // arrange
            using (var stream = new MemoryStream())
            using (var original = new StreamFileContent(stream, "myfile.txt"))
            {
                // act
                var fileStreamResult = original.ToFileStreamResult();

                // assert
                Assert.Same(original.Content, fileStreamResult.FileStream);
                Assert.Equal(original.FileName, fileStreamResult.FileDownloadName);
            }
        }

        [Fact]
        public void ToFileStreamResult_preserves_content_filename_mediatype()
        {
            // arrange
            var mediaType = "foo/bar";
            using (var stream = new MemoryStream())
            using (var original = new StreamFileContent(stream, "myfile.txt").WithMediaType(mediaType))
            {
                // act
                var fileStreamResult = original.ToFileStreamResult();

                // assert
                Assert.Same(original.Content, fileStreamResult.FileStream);
                Assert.Equal(original.FileName, fileStreamResult.FileDownloadName);
                Assert.Equal(mediaType, fileStreamResult.ContentType);
            }
        }

        [Theory]
        [InlineData("myfile.txt", "text/plain")]
        [InlineData("myfile.pdf", "application/pdf")]
        public void ToFileStreamResult_adds_default_media_type(string fileName, string expectedMediaType)
        {
            // arrange
            using (var stream = new MemoryStream())
            using (var original = new StreamFileContent(stream, fileName))
            {
                // act
                var fileStreamResult = original.ToFileStreamResult();

                // assert
                Assert.Same(original.Content, fileStreamResult.FileStream);
                Assert.Equal(original.FileName ?? string.Empty, fileStreamResult.FileDownloadName);
                Assert.Equal(expectedMediaType, fileStreamResult.ContentType);
            }
        }

        [Fact]
        public void AsStreamFileContentWithMediaType_passes_through_when_already_StreamFileContentWithMediaType()
        {
            // arrange
            using (var stream = new MemoryStream())
            using (var original = new StreamFileContentWithMediaType(stream, "myfile.txt"))
            {
                // act
                var withMediaType = original.AsStreamFileContentWithMediaType();

                // assert
                Assert.Same(original, withMediaType);
            }
        }

        [Fact]
        public void AsStreamFileContentWithMediaType_preserves_content_filename_length()
        {
            // arrange
            using (var stream = new MemoryStream())
            using (var original = new StreamFileContent(stream, "myfile.txt"))
            {
                // act
                var withMediaType = original.AsStreamFileContentWithMediaType();

                // assert
                Assert.Same(original.Content, withMediaType.Content);
                Assert.Equal(original.FileName, withMediaType.FileName);
                Assert.Equal(original.Length, withMediaType.Length);
            }
        }
    }
}
