using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RSA_Encryption.Utilities;

public class Operations
{
    public static long generateModulus(int primeOne, int primeTwo)
    {
        long modulus = primeTwo * primeOne;
        return modulus;
    }

    public static int generateEulers(int primeOne, int primeTwo)
    {
        return (primeOne-1)*(primeTwo-1);
    }
}
