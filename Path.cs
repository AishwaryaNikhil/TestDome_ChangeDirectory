//Write a function that provides change directory (cd) function for an abstract file system.
//    Notes:
//Root path is '/'.
//    Path separator is '/'.
//    Parent directory is addressable as "..".
//    Directory names consist only of English alphabet letters (A-Z and a-z).

using System;
using System.Text.RegularExpressions;

namespace ChangeDirectory
{
    public class Path
    {
        public string CurrentPath { get; set; }

        public Path(string path)
        {
            this.CurrentPath = path;
        }

        public Path Cd(string newPath)
        {
            var partsOfNewPath = newPath.Split("/");
            foreach (var part in partsOfNewPath)
            {
                var operation = Regex.IsMatch(part, @"^[a-zA-Z]+$") ? "stepIn" :
                    part == ".." ? "stepOut" : "invalid";
                switch (operation)
                {
                    case "stepOut":
                        var lastPathSeparatorIndex = this.CurrentPath.LastIndexOf('/');
                        this.CurrentPath = this.CurrentPath.Substring(0, lastPathSeparatorIndex);
                        break;
                    case "stepIn":
                        this.CurrentPath = this.CurrentPath + '/' + part;
                        break;
                    case "invalid":
                        throw new InvalidOperationException(part);
                }
            }

            return new Path(this.CurrentPath); 
        }

        public static void Main(string[] args)
        {
            try
            {
                Path path = new Path("/a/b/c/d");
                //Console.WriteLine(path.Cd("../x/../y/z").CurrentPath);
                //Console.WriteLine(path.Cd("../../x/../yz/z").CurrentPath);
                //Console.WriteLine(path.Cd("../../x/../yz/z").CurrentPath);
                Console.WriteLine(path.Cd("../.././x/../yz/z").CurrentPath);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Some Invalid characters in the path {ex}");
            }
           
        }
    }
}
