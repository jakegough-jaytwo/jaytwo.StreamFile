using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace jaytwo.StreamFile.Tests.AspNetCore
{
    public class IFormFileExtensionsTests
    {
        [Fact]
        public void Dispose_calls_stream_dispose()
        {
            // arrange
            var content = new MemoryStream();
            var fakeFormFile = new FakeFormFile("MyContentType", 123, "MyFileName.txt", content);

            // act
            using (var streamFileContent = fakeFormFile.ToStreamFileContent())
            {
                // assert
                Assert.Equal(fakeFormFile.FileName, streamFileContent.FileName);
                Assert.Equal(fakeFormFile.ContentType, streamFileContent.MediaType);
                Assert.Equal(fakeFormFile.Length, streamFileContent.Length);
                Assert.Same(fakeFormFile.OpenReadStream(), streamFileContent.Content);
            }
        }

        private class FakeFormFile : IFormFile
        {
            private readonly Stream _content;

            public FakeFormFile(string contentType, int length, string fileName, Stream content)
            {
                ContentType = contentType;
                Length = length;
                FileName = fileName;
                _content = content;
            }

            public string ContentType { get; }

            public string ContentDisposition => throw new NotImplementedException();

            public IHeaderDictionary Headers => throw new NotImplementedException();

            public long Length { get; }

            public string Name => throw new NotImplementedException();

            public string FileName { get; }

            public void CopyTo(Stream target)
            {
                throw new NotImplementedException();
            }

            public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
            {
                throw new NotImplementedException();
            }

            public Stream OpenReadStream() => _content;
        }
    }
}
