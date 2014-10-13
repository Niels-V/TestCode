using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordHash
{
    /// <summary>
    /// This class demoes the prefered way with native .NET classes to 
    /// generate password hashes. The <see cref="Rfc2898DeriveBytes"/> class
    /// implements the PBKDF2 algorithm. Some internet posts suggest the use
    /// of BCRYPT, but as there is no native implemented .NET method for this
    /// and you should 'trust' a NuGet implementation in stead of the native 
    /// one.
    /// Further note that the keySize is low because Rfc2898DeriveBytes uses SHA-1 
    /// which has a hash size of 20. As it is cryptografically better to not
    /// increase the keySize larger than this hashsize, and increase the iterations
    /// instead to increase the complexity of calculating hashes
    /// see http://security.stackexchange.com/questions/35250/hmacsha512-versus-rfc2898derivebytes-for-password-hash
    /// 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            List<long> ms = new List<long>();
            for (int i = 1; i <= 10; i++)
            {
                Stopwatch w = Stopwatch.StartNew();
                int saltSize = 64;
                int iterations = 64000;
                int keySize = 20; //Set to hash size of used SHA-1 
                var deriveBytes = new Rfc2898DeriveBytes("Passw0rd", saltSize, iterations);
                byte[] salt = deriveBytes.Salt;
                byte[] key = deriveBytes.GetBytes(keySize);
                w.Stop();

                Console.WriteLine(salt.Length);
                Console.WriteLine(key.Length);
                Console.WriteLine(Convert.ToBase64String(salt));
                ms.Add(w.ElapsedMilliseconds);
                Console.WriteLine(w.ElapsedMilliseconds);
            }
            Console.WriteLine();
            Console.WriteLine(ms.Sum() / ms.Count);
            Console.WriteLine(ms.Max() + " -  " + ms.Min());
            Console.ReadKey();
        }
    }
}
