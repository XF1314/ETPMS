using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace ETPMS.Infrastructure.Extensions
{
    public static class ModelStateExtensions
    {
        public static string GetAllErrorMessage(this ModelStateDictionary modelState)
        {
            var stringBuilder = new StringBuilder();

            foreach (var error in modelState.Values.SelectMany(value => value.Errors))
            {
                stringBuilder.AppendFormat("{0}<br/>", error.ErrorMessage);
            }

           return stringBuilder.ToString();
        }
    }
}
