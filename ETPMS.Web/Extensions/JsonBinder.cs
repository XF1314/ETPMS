
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Mvc;


namespace ETPMS.Web.Extensions
{
    public class JsonBinder : IModelBinder
    {

        /// <summary>
        /// 使用指定的控制器上下文和绑定上下文将模型绑定到一个值。
        /// </summary>
        /// <param name="controllerContext">控制器上下文。</param><param name="bindingContext">绑定上下文。</param>
        /// <returns></returns>
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var modelType = bindingContext.ModelType;
            var json = controllerContext.HttpContext.Request.Form[bindingContext.ModelName] ?? controllerContext.HttpContext.Request.QueryString[bindingContext.ModelName];

            if (string.IsNullOrEmpty(json)) { return null; }
            var serializer = new JsonSerializer();
            var jsonBody = JObject.Parse(json);
            return serializer.Deserialize(jsonBody.CreateReader(), modelType);
            //if (json.StartsWith("[") && json.EndsWith("]"))
            //{
            //    var list = new List<T>();
            //    var jsonArray = JArray.Parse(json);
            //    if (jsonArray != null)
            //    {
            //        list.AddRange(jsonArray.Select(jobj => serializer.Deserialize(jobj.CreateReader(), typeof(T))).Select(obj => (T)obj));
            //    }
            //    return list;
            //}
        }
    }
}