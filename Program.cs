using RSA_Encryption.RSA;
using RSA_Encryption.Utilities;
using System.IO;

var keys = KeyGenerator.Generate();

RSAEncrypt encrypt = new(keys.N, keys.E);
RSADecrypt decrypt = new(keys.D, keys.N);

Console.WriteLine("Do you want to encode a file or a single sentence?");
Console.WriteLine("1. Single sentence \n 2. File");
var choice = Console.ReadLine();

if (choice == "1")
{

    Console.WriteLine("Input message to encode:");
    var input = Console.ReadLine();
    var test = encrypt.Encrypt(input ?? "dummy message");

    Console.Write("Encrypted to:\t");
    foreach (var entry in test)
        Console.Write($"{entry}\t");
    Console.WriteLine();

    var decryptTest = decrypt.Decrypt(test);

    Console.WriteLine($"decrypted to {decryptTest}");
}

if(choice == "2")
{
    Console.WriteLine("Paste the path to the file here:");
    var input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input) || !File.Exists(input))
    {
        Console.WriteLine("Invalid file path.");
        return;
    }

    var fileToEncrypt = File.ReadAllText(input!);

    var test = encrypt.Encrypt(fileToEncrypt);

    string directory = Path.GetDirectoryName(input)!;
    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(input);
    string extension = Path.GetExtension(input);

    string newFileNameForEncrypt = Path.Combine(directory, $"{fileNameWithoutExt}_Encrypted{extension}");
    File.WriteAllBytes(newFileNameForEncrypt, test);

    Console.Write("Encrypted to:\t");
    foreach (var entry in test)
        Console.Write($"{entry}\t");
    Console.WriteLine();

    var decryptTest = decrypt.Decrypt(test);

    string newFileName = Path.Combine(directory, $"{fileNameWithoutExt}_Result{extension}");
    File.WriteAllText(newFileName, decryptTest);

    Console.WriteLine($"\nDecrypted to: {decryptTest}");
    Console.WriteLine($"The decrypted file was written to: {newFileName}");

}
