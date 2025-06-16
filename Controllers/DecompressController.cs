using System.IO.Compression;
using Microsoft.AspNetCore.Mvc;

namespace GzDecompressApi.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class DecompressController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Decompress()
        {
            using var inMs = new MemoryStream();
            await Request.Body.CopyToAsync(inMs);
            inMs.Seek(0, SeekOrigin.Begin);

            using var outMs = new MemoryStream();
            using var gzip = new GZipStream(inMs, CompressionMode.Decompress);
            await gzip.CopyToAsync(outMs);
            outMs.Seek(0, SeekOrigin.Begin);

            return File(outMs.ToArray(), "application/octet-stream", "decompressed");
        }
    }
}
