using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LaunchKey.SDK.Rest.Tests
{
	[TestClass]
	public class LaunchKeyClientTests
	{
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestLoadInvalidPrivateKey()
		{
			var client = new LaunchKeyRestClient("1234567", "otherness", "mumbojumbo");
		}

		[TestMethod]
		public void TestLoadValidPrivateKeyFromString()
		{
			var client = new LaunchKeyRestClient("1234567", "otherness", @"-----BEGIN RSA PRIVATE KEY-----
MIIEogIBAAKCAQEAlm8f27xSM+aNUIohrjJmLc80+0dxqesufU3Imkp2Y1gRrobp
ZRJ0RWVAJd7N9mJTVWHHYk1jzLnRkN0smsHNX/QSnDRghJBug4XVHkIeGcUGeQPm
3HLOS8xve48qsNtNkMgCGX91G84hkfv7oEfg7IeLCqcQc+jQiDieV5KaeJ/p0SPG
uedY8yOxt7GjyylqETeIxHSepYpQrURtucGAyUu8H4PedyZhlycg/DvHXHZJCtHa
iNXxzSrxUtchzugbgDETQeJbPEPB0g//S3dAwEIXanlK5cPxUUPgYG9gUbWRW+fz
FAPNX9wNhrKGh7w41dLFhWgRreY1oHQi02VbgQIDAQABAoIBAETg1o5uuJio7HU+
+dLfoqdA43pWKPXrB9s5nSsAAxrTfS0S+pRbkb8gb/GnBVjVhmBmp7sbwiE/x3Nz
wXOOXvGbOFYrKTIpS2CPC5iSH2QfMSqBnde7/Tb5gOXrFFnr36T13vg7wqwbgoVs
AbdOke7a1M/o1ICtzaVBf0aLiozFv7zly8vsXS7uRLtU0SJrYnMb56h5zA3Ed2P2
pge2vwgNuKzkkUBM7NozAHuVzK1SVkJJJ/7z4ztC892/394F04hyEBGlR4+nP1u1
5pMDCagpadTKWf9nrp6FcxSZ0/vNFHkZ0DhIDcwf4SQs/CX1dtXCJr4z8J85chU0
1LQtkkECgYEAxKa4AuinLC4xGkdMstjRlxShCHHS2hlzkRE9KmKCPJ2u8eaSd00V
xybi4WTWTCgtW6taFCB3hF7KEiLRoGLHLq2ZG7LyP5Xn2YpR0K3W9f4tm/h0i9PO
PfvELnlTCI0p02GYFmbv5ajQ6KWSnC8S3IN3sBdjVM4KbfmqFP7jOykCgYEAw9Wq
gcu0NXF0v6RjXhGsnBmvbox6o3U2Pq4ffftvSpiemdPRo7q85HbEyvixp8eLVIuw
2OlHR8XFm9xbWef0g8Pug1r805Pfi726qi0IZPBpwvY+h+z2SfqLqvU0jMAkpaLl
urh0y4AWXnlZLM5UQyKhr1CqNGWsC14lR8q/AJkCgYBbY8OWjnrKfHkTZllNsJfO
56Zi30sHUtazb1iGrxspgnqALxCYYvmhAQBlkVBKOk3CEWMegWwp10d/oDDdoapB
linWhpYwWMFUBwU4vuFslwCpQJEhPoDVq4CjvHyLN9ddRjjBCHuqtobH59K3xOS2
GF7yNhxSmCKes22Irl/56QKBgAmnnAtgHpdP66CTGy8FEgx0G7kIhfdPDQiUDwtn
/Pmhgpg04C4BQ9TDojobfq614MGfTW20OoZoUrC3cW2CSQTCAR1KvBI2RRwfk3dW
Vwaa76vj9fs/jHjjOEWKZzfVNTlRSaAl/eQWgYPQ2buixHa221/IS0vgA1ZgIthi
P35pAoGAVWcHrX+PrEhpdgI89Fa56rzeH/hLyQa/Krhq/k1ZzTmMJBKUA5tW6A4G
3taaOY4x2eyMonSxUfnTPlut2CC/1UtlcynuRP7cjVW+ktgXMfH4IGq4cJ4ZbvKl
dxbH194Ss/6UQe6NocCAc1pAR/81MK/2PUTOyEfaXi6qfX3LvPU=
-----END RSA PRIVATE KEY-----@");
		}


	}
}
