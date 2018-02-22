   using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager
{
    public class ProjectReference
    {
        public string CurrentName { get; set; }
        public string CurrentProTagPath { get; set; }
        public ProjectReference()
        {
            
        }
        public ProjectReference(string name,string proTagPath)
        {
            CurrentName = name;
            CurrentProTagPath = proTagPath;           
        }
    }
}
