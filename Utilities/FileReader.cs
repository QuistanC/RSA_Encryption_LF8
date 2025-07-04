
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
