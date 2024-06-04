using System.Security.Cryptography;

namespace MessengerApplication.rsa
{
    public static class RSATools
    {
        public static RSA GetPrivateKey()
        {
            var f = File.ReadAllText("private_key.pem");
            var rsa = RSA.Create();
            rsa.ImportFromPem(f);
            return rsa;
        }
    }
}
