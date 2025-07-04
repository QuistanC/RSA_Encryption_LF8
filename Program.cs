using RSA_Encryption.RSA;
using RSA_Encryption.Utilities;

var keys = KeyGenerator.Generate();

RSAEncrypt encrypt = new(keys.N, keys.E);
RSADecrypt decrypt = new(keys.D, keys.N);

Console.WriteLine("Input message to encode:");
var input = Console.ReadLine();
var test = encrypt.Encrypt(input ?? "dummy message");

Console.Write("Encrypted to:\t");
foreach (var entry in test)
    Console.Write($"{entry}\t");
Console.WriteLine();

var decryptTest = decrypt.Decrypt(test);

Console.WriteLine($"decrypted to {decryptTest}");
