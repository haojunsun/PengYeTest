using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace PengYe.Project.MiniProgram.Controllers
{
    public class UploadController : ApiController
    {
        public async Task<HttpResponseMessage> Post()
        {
            // 检查是否是 multipart/form-data
            if (!Request.Content.IsMimeMultipartContent("form-data"))
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            HttpResponseMessage response = null;
            try
            {
                string root = HttpContext.Current.Server.MapPath("~/App_Data");
                // 设置上传目录
                var provider = new MultipartFormDataStreamProvider(root);
                // 接收数据，并保存文件
                var bodyparts = await Request.Content.ReadAsMultipartAsync(provider);
                response = Request.CreateResponse(HttpStatusCode.Accepted);
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            return response;
        }
    }
}
