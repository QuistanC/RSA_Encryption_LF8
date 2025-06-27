// See https://aka.ms/new-console-template for more information
using RSA_Encryption;
using System.Diagnostics;

Console.WriteLine("Hello, World!");
//IF YOU WON'T WORK, I WILL CRY

var timer = new Stopwatch();

timer.Start();
var test = new PrimeGenerator().GeneratePrimePair();
timer.Stop();

Console.WriteLine($"{test[0]}, {test[1]} found in {timer.ElapsedMilliseconds}ms");