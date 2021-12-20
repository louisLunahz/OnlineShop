using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LouigisSP.BO
{
   

        public abstract class Fileable
        {
            public virtual string ToCsv()
            {
                string output = "";

            Type type1 = GetType();
            var declaredProperties = type1.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            var inheritedProps = type1.BaseType.GetProperties();
            PropertyInfo[] properties = inheritedProps.Concat(declaredProperties).ToArray();

            for (var i = 0; i < properties.Length; i++)
                {
                    if (properties[i].PropertyType.IsSubclassOf(typeof(Fileable)))
                    {
                        var m = properties[i].PropertyType
                                .GetMethod("ToCsv", new Type[0]);
                        
                        output += m.Invoke(properties[i].GetValue(this),
                                            new object[0]);                        
                    }
                    else
                    {
                        output += PreProcess(properties[i]
                                            .GetValue(this).ToString());
                    }
                    if (i != properties.Length - 1)
                    {
                        output += ",";
                    }
                }

                return output;
            }

       

            public virtual string ToCsv(string[] propertyNames, bool isIgnore)
            {
                string output = "";
                bool isFirstPropertyWritten = false;


                var properties = GetType().GetProperties();

                for (var i = 0; i < properties.Length; i++)
                {
                    if (isIgnore)
                    {
                        if (!propertyNames.Contains(properties[i].Name))
                        {
                            if (isFirstPropertyWritten)
                            {
                                output += ",";
                            }

                            if (properties[i].PropertyType
                                .IsSubclassOf(typeof(Fileable)))
                            {
                                var m = properties[i].PropertyType
                                .GetMethod("ToCsv", new Type[0]);
                                output += m.Invoke(properties[i].GetValue(this),
                                                    new object[0]);
                            }
                            else
                            {
                                output += PreProcess(properties[i]
                                            .GetValue(this).ToString());
                            }

                            if (!isFirstPropertyWritten)
                            {
                                isFirstPropertyWritten = true;
                            }
                        }
                    }
                    else
                    {
                        if (propertyNames.Contains(properties[i].Name))
                        {
                            if (isFirstPropertyWritten)
                            {
                                output += ",";
                            }

                            if (properties[i].PropertyType
                            .IsSubclassOf(typeof(Fileable)))
                            {
                                var m = properties[i].PropertyType
                                        .GetMethod("ToCsv", new Type[0]);
                                output += m.Invoke(properties[i].GetValue(this),
                                                    new object[0]);
                            }
                            else
                            {
                                output += PreProcess(properties[i]
                                            .GetValue(this).ToString());
                            }

                            if (!isFirstPropertyWritten)
                            {
                                isFirstPropertyWritten = true;
                            }
                        }
                    }
                }

                return output;
            }

            public virtual string ToCsv(int[] propertyIndexes, bool isIgnore)
            {
                string output = "";

                bool isFirstPropertyWritten = false;

                var properties = GetType().GetProperties();

                for (var i = 0; i < properties.Length; i++)
                {
                    if (isIgnore)
                    {
                        if (!propertyIndexes.Contains(i))
                        {
                            if (isFirstPropertyWritten)
                            {
                                output += ",";
                            }

                            if (properties[i].PropertyType
                                .IsSubclassOf(typeof(Fileable)))
                            {
                                var m = properties[i].PropertyType
                                        .GetMethod("ToCsv", new Type[0]);
                                output += m.Invoke(properties[i].GetValue(this),
                                                    new object[0]);
                            }
                            else
                            {
                                output += PreProcess(properties[i]
                                            .GetValue(this).ToString());
                            }

                            if (!isFirstPropertyWritten)
                            {
                                isFirstPropertyWritten = true;
                            }
                        }
                    }
                    else
                    {
                        if (propertyIndexes.Contains(i))
                        {
                            if (isFirstPropertyWritten)
                            {
                                output += ",";
                            }

                            if (properties[i].PropertyType
                                .IsSubclassOf(typeof(Fileable)))
                            {
                                var m = properties[i].PropertyType
                                        .GetMethod("ToCsv", new Type[0]);
                                output += m.Invoke(properties[i].GetValue(this),
                                                    new object[0]);
                            }
                            else
                            {
                                output += PreProcess(properties[i]
                                            .GetValue(this).ToString());
                            }

                            if (!isFirstPropertyWritten)
                            {
                                isFirstPropertyWritten = true;
                            }
                        }
                    }

                }

                return output;
            }

        public virtual void AssignValuesFromCsv(string[] propertyValues)
        {
            Type type1 = GetType();
             var declaredProperties = type1.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
             var inheritedProps = type1.BaseType.GetProperties();
            PropertyInfo[] properties = inheritedProps.Concat(declaredProperties).ToArray();

          
            
            
            for (var i = 0; i < properties.Length; i++)
            {
                if (properties[i].PropertyType
                    .IsSubclassOf(typeof(Fileable)))
                {
                    var instance = Activator.CreateInstance(properties[i].PropertyType);
                    var instanceProperties = instance.GetType().GetProperties();
                    var propertyList = new List<string>();

                    for (var j = 0; j < instanceProperties.Length; j++)
                    {
                        propertyList.Add(propertyValues[i + j]);
                    }
                    var m = instance.GetType().GetMethod("AssignValuesFromCsv", new Type[] { typeof(string[]) });
                    m.Invoke(instance, new object[] { propertyList.ToArray() });
                    properties[i].SetValue(this, instance);

                    i += instanceProperties.Length;
                }
                else
                {
                    var type = properties[i].PropertyType.Name;
                    switch (type)
                    {
                        case "Int32":
                            properties[i].SetValue(this, int.Parse(propertyValues[i]));

                            break;
                        case "DateTime":
                            properties[i].SetValue(this, DateTime.Parse(propertyValues[i]));
                            break;
                        case "Single":
                            properties[i].SetValue(this, float.Parse(propertyValues[i]));

                            break;
                       
                        default:
                            properties[i].SetValue(this, propertyValues[i]);
                            break;
                    }
                }
            }
        }

        private string PreProcess(string input)
            {
                string str = "\"\"";
                string str2 = "\"";


                input = input.Replace('ı', 'i')
                    .Replace('ç', 'c')
                    .Replace('ö', 'o')
                    .Replace('ş', 's')
                    .Replace('ü', 'u')
                    .Replace('ğ', 'g')
                    .Replace('İ', 'I')
                    .Replace('Ç', 'C')
                    .Replace('Ö', 'O')
                    .Replace('Ş', 'S')
                    .Replace('Ü', 'U')
                    .Replace('Ğ', 'G')
                    .Replace(str2, str)
                    .Trim();
                if (input.Contains(","))
                {
                    input = '"' + input + '"';
                }
                return input;
            }
        }
}
