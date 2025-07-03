using RSA_Encryption;
using RSA_Encryption.models;
using RSA_Encryption.RSA;
using RSA_Encryption.Utilities;
using System.Diagnostics;


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

RSAEncrypt encrypt = new(keys.N, keys.E);

var test = encrypt.Encrypt("C:\\Users\\Lisa.Kortkamp\\Documents\\Test.txt");

Console.Write(test);

//Console.WriteLine($"Keyset: e = {keys.E}, d = {keys.D}, n = {keys.N}");