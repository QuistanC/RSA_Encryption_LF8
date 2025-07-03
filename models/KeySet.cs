using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RSA_Encryption.models;

public record KeySet(BigInteger N, BigInteger E, BigInteger D);
