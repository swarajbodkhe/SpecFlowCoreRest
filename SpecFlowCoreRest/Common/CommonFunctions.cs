using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.IO;
using TechTalk.SpecFlow;

namespace SpecFlowCoreRest
{
    public class CommonFunctions
    {
        private static Dictionary<string, string> GlobalDictionary;

        private static IConfiguration _config;

        public static void initGlobalDictionary()
        {
            GlobalDictionary  = new Dictionary<string, string>();
        }
        public static void setGlobalValue(string key,string value)
        {
            if (GlobalDictionary.ContainsKey(key))
            {
                GlobalDictionary[key] = value;
            }
            else
            {
                GlobalDictionary.Add(key, value);

            }
        }


        public static string getGlobalValue(string key)
        {
            return GlobalDictionary[key];
        }

        public static void setProjectConfiguration(IConfiguration config)
        {
            _config = config;
        }


        public static String getJsonString(Object obj)
        {
            String json = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
            return json;
        }


        public static String getAppSettingValues(String pathTokeyOrKeyItself)
        {
            return _config.GetValue<String>("RestSettings:"+ pathTokeyOrKeyItself);
        }


        public static Dictionary<string,object> getModelDataFromTable(Table table)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            foreach (TableRow row in table.Rows)
            {
                string newValue = row[1];
                if (row[1].Contains("!"))
                {
                    newValue = getGlobalValue(row[1].Replace("!",""));
                }
                if (row[2].ToLower().Equals("int"))
                {
                    dict.Add(row[0],Int32.Parse(newValue));
                }
                if (row[2].ToLower().Equals("bool") || row[2].ToLower().Equals("boolean"))
                {
                    dict.Add(row[0], newValue.ToLower().ToString().Equals("true") ? true:false);
                }
            }
            return dict;
        }
    }
}
