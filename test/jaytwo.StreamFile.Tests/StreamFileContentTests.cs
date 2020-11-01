using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Moq;
using Xunit;

namespace jaytwo.StreamFile.Tests
{
    public class StreamFileContentTests
    {
        [Fact]
        public void Dispose_calls_stream_dispose()
        {
            // arrange
            var stream = new MemoryStream();

            var streamFileContent = new StreamFileContent()
            {
                Content = stream,
            };

            // act
            streamFileContent.Dispose();

            // assert
            Assert.False(stream.CanRead);
        }

        [Fact]
        public void Constructor_with_stream_sets_content_and_length_and_file_name()
        {
            // arrange
            var content = Encoding.UTF8.GetBytes("helloWorld");
            var stream = new MemoryStream(content);
            var fileName = "myfile";

            // act
            using (var streamFileContent = new StreamFileContent(stream, fileName))
            {
                // assert
                Assert.Equal(content.Length, streamFileContent.Length);
                Assert.Equal(fileName, streamFileContent.FileName);
                Assert.Same(stream, streamFileContent.Content);
            }
        }

        [Fact]
        public void Constructor_with_bytes_sets_content_and_length_and_file_name()
        {
            // arrange
            var content = Encoding.UTF8.GetBytes("helloWorld");
            var fileName = "myfile";

            // act
            using (var streamFileContent = new StreamFileContent(content, fileName))
            {
                // assert
                Assert.Equal(content.Length, streamFileContent.Length);
                Assert.Equal(fileName, streamFileContent.FileName);
                Assert.Equal(content.Length, streamFileContent.Content.Length);
            }
        }
    }
}
