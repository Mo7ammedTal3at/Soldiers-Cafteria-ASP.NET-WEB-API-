
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace Soldiers_Cafteria.Models
{
    public static class ModelStateExtension
    {
        public static string GetErrorMessages(this ModelStateDictionary msd)
        {
            string responseMessage = "";
            foreach (var temp in msd)
            {
                foreach (var error in temp.Value.Errors)
                {
                    responseMessage += error.ErrorMessage + "\n\r";
                }
            }
            return responseMessage;
        }
    }
}