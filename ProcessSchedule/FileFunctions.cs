using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ProcessSchedule
{
    public class FileFunctions
    {
        public static string ReadFile()
        {
            string fName;
            Console.WriteLine("Enter the path to your text file \n");
            fName = Console.ReadLine();
            string fileContent =
                File.ReadAllText(fName);
            return fileContent;
        }
    }
}
