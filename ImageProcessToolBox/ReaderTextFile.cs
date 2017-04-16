using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class ReaderTextFile
    {
        public ReaderTextFile()
        {
            readeConent(@"C:\Users\lungyu\Downloads\download_2017-02-21_19-07-16\TestFile.txt");
        }

        private string readeConent(string fileFullPath)
        {
            StringBuilder sb = new StringBuilder();
            // Open the text file using a stream reader.
            using (StreamReader sr = new StreamReader(fileFullPath))
            {
                // Read the stream to a string, and write the string to the console.
                String line = sr.ReadToEnd();
                sb.Append(line);
            }
            return sb.ToString();
        }
    }
}
