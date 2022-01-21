using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32.SafeHandles;

namespace MarieGold.WebAPI.Controllers {
    [ApiController]
    [Route("api/")]
    public class ApiController : Controller {
        private static readonly List<string> Tokens = new() {"e7da2aff-67d8-4142-bde3-1803452b6389"};
        private static bool IsMatchedToken(string token) => Tokens.Contains(token);

        [HttpPost]
        [Route("post")]
        public async Task<ActionResult> Post(string token) {
            if (!IsMatchedToken(token))
                return new JsonResult(new {
                    Status = 403,
                    Result = "Token is not matched"
                });

            if (Request.ContentLength == 0) {
                return new JsonResult(new {
                    Status = 400,
                    Result = "Content is not found"
                });
            }

            if (Request.ContentType != null && Request.ContentType.StartsWith("image")) {
                await using (var body = Request.Body) {
                    await using (var output = System.IO.File.Create("AA.jpg")) {
                        var buffer = new byte[32 * 1024];

                        int read;
                        while ((read = await body.ReadAsync(buffer.AsMemory(0, buffer.Length))) > 1) {
                            output.Write(buffer, 0, read);
                        }
                    }
                }

                return new JsonResult(new {
                    Status = 200,
                    Result = "OK!"
                });
            }
            else {
                return new JsonResult(new {
                    Status = 400,
                    Result = "Content Type is not Image"
                });
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResult> Delete(string token) {
            return Ok();
        }
    }
}