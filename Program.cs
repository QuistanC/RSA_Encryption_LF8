using RSA_Encryption;
using RSA_Encryption.models;
using RSA_Encryption.RSA;
using RSA_Encryption.Utilities;
using System.Diagnostics;
using System.Text;


//var timer = new Stopwatch();

//timer.Start();
//var test = new PrimeGenerator().GeneratePrimePair();
//timer.Stop();

//Console.WriteLine($"{test[0]}, {test[1]} found in {timer.ElapsedMilliseconds}ms");

//var numOne = 3;
//var numTwo = 11;
//var testGCD = Operations.FindGreatestCommonDenominator(numOne, numTwo);

//Console.WriteLine($"GCD for {numOne} and {numTwo} is {testGCD}");

var keys = KeyGenerator.Generate();

Console.WriteLine($"Keyset: e = {keys.E}, d = {keys.D}, n = {keys.N}");

var encrypt = new RSAEncrypt(keys.N, keys.E);
var decrypt = new RSADecrypt(keys.D, keys.N);

var inputText = "Hallo, guck mal wie toll ich en- und de-crypten kann! :)";
var inputBytes = Encoding.ASCII.GetBytes(inputText);

var encrypted = encrypt.Encrypt(inputBytes);

Console.WriteLine($"{inputText}\nencrypted to: {encrypted}");

var decrypted = decrypt.Decrypt(encrypted);
var asString = Encoding.ASCII.GetString(decrypted);

Console.WriteLine($"After decrypting: '{asString}'");