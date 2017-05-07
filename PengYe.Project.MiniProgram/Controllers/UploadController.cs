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
using PengYe.Project.Log;

namespace PengYe.Project.MiniProgram.Controllers
{
    public class UploadController : ApiController
    {
        private readonly ILogService _log;
        public UploadController(ILogService logService)
        {
            _log = logService;
        }

        public async Task<HttpResponseMessage> Post()
        {
            // 检查是否是 multipart/form-data
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            HttpResponseMessage response = null;
            try
            {
                var root = HttpContext.Current.Server.MapPath("~/App_Data");
                // 设置上传目录
                var provider = new MultipartFormDataStreamProvider(root);
                foreach (MultipartFileData file in provider.FileData)
                {
                    string filename = file.Headers.ContentDisposition.Name.Replace("\"", "");//获取控件 id
                    string name = file.Headers.ContentDisposition.FileName.Replace("\"", "");
                    string type = name.Substring(name.LastIndexOf(".") + 1).ToLower();
                    _log.Debug("控件id："+filename);
                    _log.Debug("名称：" + name);
                    _log.Debug("类型：" + type);
                    //img = Path.GetFileName(file.LocalFileName) + "." + type;//新文件名
                    //File.Copy(file.LocalFileName, Img_Base_Domain + "\\" + img.Replace("BodyPart_", ""));
                    //img = img.Replace("BodyPart_", "");
                    //File.Delete(file.LocalFileName);
                }
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
