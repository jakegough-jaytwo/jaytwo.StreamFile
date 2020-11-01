using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jaytwo.StreamFile.AspNetCore;

namespace Microsoft.AspNetCore.Http
{
    public static class IFormFileExtensions
    {
        public static StreamFileContentWithMediaType ToStreamFileContent(this IFormFile formFile)
        {
            return new StreamFileContentWithMediaType()
            {
                FileName = formFile.FileName,
                Length = formFile.Length,
                MediaType = formFile.ContentType,
                Content = formFile.OpenReadStream(),
            };
        }
    }
}
