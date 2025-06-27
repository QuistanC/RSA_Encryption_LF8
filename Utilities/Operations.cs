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
    public static long generateModulus(long primeOne, long primeTwo)
    {
        return primeTwo * primeOne;
    }

    public static long generateEulers(long primeOne, long primeTwo)
    {
        return (primeOne-1)*(primeTwo-1);
    }

    public static long generatePrivateKey(long publicKey, long eulers)
    {
        return publicKey * eulers; //TEMP
    }

    private static long findGreatestCommonDividor(long primeOne, long primeTwo)
    {
        if (primeOne == 0 || primeTwo == 0)
            return 0;

        if(primeOne == primeTwo)
        {
            return primeOne;
        }

        if (primeOne > primeTwo)
        {
            return findGreatestCommonDividor(primeOne-primeTwo, primeTwo);
        }

        return findGreatestCommonDividor(primeOne, primeTwo-primeOne);
    }

    private static bool checkIfPrime(long primeOne, long primeTwo)
    {
        return findGreatestCommonDividor(primeOne, primeTwo) == 1;
    }

}
