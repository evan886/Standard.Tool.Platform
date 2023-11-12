using System;

namespace Standard.Tool.Platform.Common
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class UIModuleMatadataAttribute : Attribute
    {
        public UIModuleMatadataAttribute(string ver = "", string author = "")
        {
            Version = ver;
            Author = author;
        }

        public String Version
        {
            get;
            set;
        }

        public String Author
        {
            get;
            set;
        }
    }
}
