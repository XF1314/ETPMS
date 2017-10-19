using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;
using System.Text;
using System.Web.Mvc;

namespace ETPMS.Web.Extensions
{
    public sealed class JsonNetResult : JsonResult
    {
        public JsonNetResult()
        {
            Settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Error
            };
        }

        public JsonNetResult(object data, JsonRequestBehavior behavior = JsonRequestBehavior.AllowGet, string contentType = null, Encoding contentEncoding = null)
        {
            Data = data;
            JsonRequestBehavior = behavior;
            ContentEncoding = contentEncoding;
            ContentType = contentType;
        }

        private JsonSerializerSettings _settings;
        public JsonSerializerSettings Settings
        {
            get
            {
                _settings = _settings ?? new JsonSerializerSettings();
                _settings.Converters.Add(new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
                //_settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                return _settings;
            }
            private set { _settings = value; }
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("JSON GET is not allowed");
            var response = context.HttpContext.Response;
            response.ContentType = string.IsNullOrEmpty(ContentType) ? "application/json" : ContentType;

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;
            if (Data == null)
                return;
            var scriptSerializer = JsonSerializer.Create(Settings);
            using (var sw = new StringWriter())
            {
                scriptSerializer.Serialize(sw, Data);
                response.Write(sw.ToString());
            }
        }
    }
}