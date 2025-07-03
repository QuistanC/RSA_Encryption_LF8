using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA_Encryption.Utilities;

public class FileReader
{
    public static string ReadFile(string filePath)
    {
        string fileText = "";
        if(File.Exists(filePath))
        {
            fileText = File.ReadAllText(filePath);

        }
        return fileText;
    }
}
