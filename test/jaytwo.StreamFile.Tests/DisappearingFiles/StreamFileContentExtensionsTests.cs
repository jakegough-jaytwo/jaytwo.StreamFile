using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using jaytwo.DisappearingFiles;
using Xunit;

namespace jaytwo.StreamFile.Tests.DisappearingFiles
{
    public class StreamFileContentExtensionsTests
    {
        [Fact]
        public void CacheToDisk_preserves_original_object()
        {
            // arrange
            using (var stream = new MemoryStream())
            using (var original = new StreamFileContent(stream, "myfile.txt"))
            {
                // act
                var cached = original.CacheToDisk();

                // assert
                Assert.Same(original, cached);
            }
        }

        [Fact]
        public void CacheToDisk_stream_is_DisappearingFileStream()
        {
            // arrange
            using (var stream = new MemoryStream())
            using (var streamFileContent = new StreamFileContent(stream, "myfile.txt"))
            {
                // act
                streamFileContent.CacheToDisk();

                // assert
                Assert.IsType<DisappearingFileStream>(streamFileContent.Content);
            }
        }

        [Fact]
        public void CacheToDisk_disposes_original_stream()
        {
            // arrange
            using (var originalStream = new MemoryStream())
            using (var streamFileContent = new StreamFileContent(originalStream, "myfile.txt"))
            {
                // act
                streamFileContent.CacheToDisk();

                // assert
                Assert.False(originalStream.CanRead);
            }
        }

        [Fact]
        public async Task CacheToDiskAsync_preserves_original_object()
        {
            // arrange
            using (var stream = new MemoryStream())
            using (var original = new StreamFileContent(stream, "myfile.txt"))
            {
                // act
                var cached = await original.CacheToDiskAsync();

                // assert
                Assert.Same(original, cached);
                Assert.NotSame(stream, original.Content);
            }
        }

        [Fact]
        public async Task CacheToDiskAsync_stream_is_DisappearingFileStream()
        {
            // arrange
            using (var stream = new MemoryStream())
            using (var streamFileContent = new StreamFileContent(stream, "myfile.txt"))
            {
                // act
                await streamFileContent.CacheToDiskAsync();

                // assert
                Assert.IsType<DisappearingFileStream>(streamFileContent.Content);
            }
        }

        [Fact]
        public async Task CacheToDiskAsync_disposes_original_stream()
        {
            // arrange
            using (var originalStream = new MemoryStream())
            using (var streamFileContent = new StreamFileContent(originalStream, "myfile.txt"))
            {
                // act
                await streamFileContent.CacheToDiskAsync();

                // assert
                Assert.False(originalStream.CanRead);
            }
        }
    }
}
