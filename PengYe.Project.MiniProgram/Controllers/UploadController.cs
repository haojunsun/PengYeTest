using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using PengYe.Project.Log;

namespace PengYe.Project.MiniProgram.Controllers
{
    public class UploadController : ApiController
    {
        private readonly ILogService _log;
        //public UploadController()
        //{

        //}


        public UploadController(ILogService logService)
        {
            _log = logService;
        }

        [System.Web.Http.HttpPost]
        public async Task<ApiResponse> FilePost()
        {
            var _result = new ApiResponse()
            {
                IsSuccessful = false,
                Message = ""
            };
            //_result.StatusCode = this.StatusCode(200);
            _log.Debug("上传文件");
            // 检查是否是 multipart/form-data
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            //HttpResponseMessage response = null;
            try
            {
                var img = "";
                var root = HttpContext.Current.Server.MapPath("~/App_Data");
                // 设置上传目录
                var provider = new MultipartFormDataStreamProvider(root);
                await Request.Content.ReadAsMultipartAsync(provider);
                foreach (MultipartFileData file in provider.FileData)
                {
                    string filename = file.Headers.ContentDisposition.Name.Replace("\"", "");//获取控件 id
                    string name = file.Headers.ContentDisposition.FileName.Replace("\"", "");
                    string type = name.Substring(name.LastIndexOf(".") + 1).ToLower();
                    _log.Debug("控件id：" + filename);
                    _log.Debug("名称：" + name);
                    _log.Debug("类型：" + type);
                    img = Path.GetFileName(file.LocalFileName) + "." + type;//新文件名
                    var Img_Base_Domain = "D:\\uploadFile";
                    File.Copy(file.LocalFileName, Img_Base_Domain + "\\" + img.Replace("BodyPart_", ""));
                    img = img.Replace("BodyPart_", "");
                    _log.Debug(img);
                    File.Delete(file.LocalFileName);
                }
                _result.Message = img;
                // 接收数据，并保存文件
                //var bodyparts = await Request.Content.ReadAsMultipartAsync(provider);
                //response = Request.CreateResponse(HttpStatusCode.Accepted);
                //foreach (var file in HttpContext.Current.Request.Files)
                //{
                //    string name = file.GetType().ToString();
                //}
                //var file = HttpContext.Current.Request.Files[0];
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
               // throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            //return response;

            return _result;
        }

        [System.Web.Http.HttpGet]
        public ReturnData GetData()
        {
            var model = new ReturnData();
            model.data=new ReturnModel();
            model.errcode = 0;
            model.errmsg = "success";
            model.data.receiving_address=new receivingaddress();

            model.data.user_id = "2342";
            model.data.nickname = "just for test";
            model.data.sex = 0;
            model.data.icon_url = "https://www.baidu.com/img/bd_logo1.png";
            model.data.birthday = "2017.12.1 ";
            model.data.tel = "18634302121";
            model.data.money = "321";

            model.data.receiving_address.rec_user = "小三";
            model.data.receiving_address.rec_tel = "178493482392";
            model.data.receiving_address.rec_area = "北京市朝阳区";
            model.data.receiving_address.rec_address = "西三旗海淀黄庄";
            model.data.receiving_address.is_default = 0;
            return model;
            //return null;
        }

    }

    public class ReturnData
    {
        public ReturnModel data { get; set; }
        public int errcode { get; set; }
        public string errmsg { get; set; }   
    }

    public class ReturnModel
    {
        public string user_id { get; set; }
        public string nickname { get; set; }
        public int sex { get; set; }
        public string icon_url { get; set; }
        public string birthday { get; set; }
        public string tel { get; set; }
        public string money { get; set; }
        public receivingaddress receiving_address { get; set; }
        public string pay_password { get; set; }

    }

    public class receivingaddress
    {
        public string rec_user { get; set; }
        public string rec_tel { get; set; }
        public string rec_area { get; set; }

        public string rec_address { get; set; }
        public int is_default { get; set; }
    }

    public interface IResponse
    {

    }

    [DataContract(Name = "result")]
    public class ApiResponse : IResponse
    {
        [DataMember(Name = "isSuccessful", Order = 1)]
        public bool IsSuccessful { get; set; }

        [DataMember(Name = "message", Order = 3)]
        public string Message { get; set; }

        [DataMember(Name = "statusCode", Order = 2)]
        public StatusCode StatusCode { get; set; }
    }

    [DataContract(Name = "statusCode")]
    public enum StatusCode
    {
        [EnumMember]
        UnKnow = 0,
        [EnumMember]
        Success = 200,
        [EnumMember]
        ClientError = 400,
        [EnumMember]
        Unauthorized = 401,
        [EnumMember]
        NotFound = 404,
        [EnumMember]
        RequestTimeout = 408,
        [EnumMember]
        BlacklistUser = 409,
        [EnumMember]
        InternalServerError = 500,
        [EnumMember]
        ServiceUnavailable = 503,
    }
}
