using GuiAutomationFramework.Framework.Environment;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiAutomationFramework.Framework.DataSource
{
    /// <summary>
    /// 
    /// </summary>
    public class JSONTestDataProviderHelper
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Model"></typeparam>
        /// <param name="fileName">JSON File name to parse</param>
        /// <returns></returns>
        public static Model Load<Model>(string fileName)
        {
            try
            {
                return JsonConvert.DeserializeObject<Model>(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory +  fileName));
            }
            catch (JsonReaderException ex)
            {
                throw new JsonReaderException(ex.Message);
            }
        }


    }
}
