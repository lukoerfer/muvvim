using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvvmUtil.Dialogs
{
    public class ExtensionFilter
    {
        public string Description { get; set; }

        public List<string> Extensions { get; set; }

        public ExtensionFilter(string description, params string[] extensions)
        {
            Description = description;
            Extensions = new List<string>(extensions);
        }
    }
}
