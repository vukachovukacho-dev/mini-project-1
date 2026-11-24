using System;
using System.Collections.Generic;
using System.Text;

namespace mini_project_1.Models
{
    public class LanguagePair
    {
        public string Name { get; set; }
        public string FileName { get; set; }

        public LanguagePair(string name, string fileName)
        {
            Name = name;
            FileName = fileName;
        }
    }
}
