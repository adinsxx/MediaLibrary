using System;
using NLog.Web;
using System.IO;
using System.Collections.Generic;

namespace MediaLibrary
{
    class MovieFile
    {
        //get and set for C# are generic since all programs require this ability
        //Can be customized later if needed
        public string listing { get; set; }
        public List<Movie> Movies { get; set; }
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();



    }
}