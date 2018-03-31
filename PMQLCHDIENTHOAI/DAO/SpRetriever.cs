using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DAO
{
    public class SpRetriever
    {
        public static SpRetrieverSection _Config =(SpRetrieverSection) ConfigurationManager.GetSection("SpConfig");

        public static SpElementCollection GetFiles()
        {
            return _Config.Files;
        }

        public static string GetFile(string name)
        {
            var source = string.Empty;
            foreach (SpElement item in GetFiles())
            {
                if (item.Name.ToLower() == name.ToLower())
                {
                    source = item.Source;
                    break;
                }
            }
            if (string.IsNullOrEmpty(source))
                throw new Exception("SP Not Found");
            return source;
        }
    }

    public class SpRetrieverSection : ConfigurationSection
    {
        [ConfigurationProperty("files")]
        public SpElementCollection Files
        {
            get { return (SpElementCollection)this["files"]; }
        }
    }

    [ConfigurationCollection(typeof(SpElement))]
    public class SpElementCollection : ConfigurationElementCollection
    {
        public SpElement this[int index]
        {
            get { return (SpElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);

                BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new SpElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SpElement)element).Name;
        }
    }

    public class SpElement : ConfigurationElement
    {
        public SpElement()
        {
        }

        [ConfigurationProperty("name", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("source", DefaultValue = "", IsRequired = true)]
        public string Source
        {
            get { return (string)this["source"]; }
            set { this["source"] = value; }
        }
    }
}
