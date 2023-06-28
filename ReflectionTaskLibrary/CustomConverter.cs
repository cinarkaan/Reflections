
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using static System.Collections.Specialized.BitVector32;

namespace ReflectionTaskLibrary
{
    public class CustomConverter
    {
        public string Serialize(object model)
        {
            string output = "[section.begin]\r\n";
            if (model.GetType().ToString() == "ReflectionTask.Config")
            {
                var opt = new JsonSerializerOptions()
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                };
                var data = JsonSerializer.Serialize(model, opt);
                using (var jsonDoc = JsonDocument.Parse(data))
                {
                    JsonElement jsonElement = jsonDoc.RootElement;
                    var items = jsonElement.EnumerateObject();
                    foreach (var item in items)
                    {
                        if (item.Name.Contains("notSerializableProperty"))
                            continue;
                        else if (item.Name.Contains("innerConfig1"))
                        {
                            output += "subConfigFirst = \r\n" + "          [section.begin]\r\n";
                            var doc = JsonDocument.Parse(JsonSerializer.Serialize(item.Value, opt));
                            JsonElement root = doc.RootElement;
                            var childs = root.EnumerateObject();
                            foreach (var c in childs)
                            {
                                if (c.Name.Equals("subName"))
                                {
                                    output += "          " + "SubName" + " = " + c.Value + "\r\n";
                                    continue;
                                }
                                output += "          " + c.Name + " = " + c.Value + "\r\n";
                            }
                            output += "          [section.end]\r\n";
                            continue;
                        }
                        else if (item.Name.Contains("innerConfig2"))
                        {
                            output += "subConfigLast = \r\n" + "          [section.begin]\r\n";
                            var doc = JsonDocument.Parse(JsonSerializer.Serialize(item.Value, opt));
                            JsonElement root = doc.RootElement;
                            var childs = root.EnumerateObject();
                            foreach (var c in childs)
                            {
                                if (c.Name.Equals("subName"))
                                {
                                    output += "          " + "SubName" + " = " + c.Value + "\r\n";
                                    continue;
                                }
                                output += "          " + c.Name + " = " + c.Value + "\r\n";
                            }
                            output += "          [section.end]\r\n";
                            continue;
                        }
                        else if (item.Name.Contains("folderInfo"))
                        {
                            output += "folderInfo = \r\n" + "          [section.begin]\r\n";
                            var doc = JsonDocument.Parse(JsonSerializer.Serialize(item.Value, opt));
                            JsonElement root = doc.RootElement;
                            var childs = root.EnumerateObject();
                            output += "          folderName = " + childs.ToArray()[0].Value + "\r\n";
                            output += "          folderInfoInnerEntity = \r\n" + "                    [section.begin]\r\n";
                            doc = JsonDocument.Parse(JsonSerializer.Serialize(childs.ToArray()[2].Value, opt));
                            root = doc.RootElement;
                            childs = root.EnumerateObject();
                            foreach (var ce in childs)
                                output += "                    " + ce.Name + " = " + ce.Value + "\r\n";
                            output += "                    [section.end]\r\n";
                            output += "          [section.end]\r\n";
                            continue;
                        }
                        else if (Regex.Match(item.Value.ToString(), "\\d+.\\d+").Success)
                        {
                            output += item.Name + " = " + item.Value.ToString().Replace(".", ",") + "\r\n";
                            continue;
                        }
                        output += item.Name + " = " + item.Value + "\r\n";
                    }
                }
                output += "[section.end]";
            }
            else
            {
                output = model.ToString();
                if (model.GetType() == typeof(System.Double))
                    output = output.Replace(".", ",");
            }
            return output;
        }
    }


}
