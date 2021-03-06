﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsrlFirmware.FileSystem
{
    public class JsrlProgram
    {
        public string Name { get; protected set; }
        public string Path { get; protected set; }

        public JsrlProgram(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public static JsrlProgram fromPath(string path)
        {
            int indexSlash = path.LastIndexOf('/') + 1;
            int indexDot = path.LastIndexOf('.');
            return new JsrlProgram(path.Substring(indexSlash, indexDot - indexSlash), path);
        }
    }
}
