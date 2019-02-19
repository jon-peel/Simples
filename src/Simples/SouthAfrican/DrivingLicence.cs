using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.OpenSsl;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Simples.SouthAfrican
{
    public class DrivingLicence
    {
        private const string longKey = @"-----BEGIN RSA PUBLIC KEY-----
MIGWAoGBAMqfGO9sPz+kxaRh/qVKsZQGul7NdG1gonSS3KPXTjtcHTFfexA4MkGA
mwKeu9XeTRFgMMxX99WmyaFvNzuxSlCFI/foCkx0TZCFZjpKFHLXryxWrkG1Bl9+
+gKTvTJ4rWk1RvnxYhm3n/Rxo2NoJM/822Oo7YBZ5rmk8NuJU4HLAhAYcJLaZFTO
sYU+aRX4RmoF
-----END RSA PUBLIC KEY-----";

        public DrivingLicence(byte[] code)
        {
            // string input = "019b0945000060e9a27a1e475f89af17cb3a5ae86bd91152d22fbca1f462ba8bd39e9341bb26ad82e3ab1b4e68069247b75286edb64fed476c1a444bba7ffeca8ac9ddcf629ac582a865b8eefec682f33de40e68a6fc08265df86ad058376db56e4a9d2a2da285d11f408493fb4706abc346632a156b517c87b58372a3afda8be11dbae4a8f5a3d1881559ce0c69acd026770fd9e75c28c4abd0b2796e70eca894d8546bd8456bdcd9af0aa112f78b56523471b46805ae844a66108ba2f12a28180e5ddded48436239eae212c6193d351201e6a24613d2482ddae552bf7e21729246667a9cd5c730ba4b80736586f5f90894b1017fcea7b299cd6241a266c4b7b28b704de1f0266025358d134686e1cac2ba9af74ea16b9524f4fca20792d2618beec8499dbab596f5a56531f26462c996dad5e1278400a5266c5b5aed2dd3cae135821148bdfd3d564ac127c90d3a125031cb263e63879dfdd10725dc80225a700e5d259a5a1d2340d7bd2d3fecc9b078e1726661e2c51b945b56d762fa3067f2b926dd2707a74e1535abf1a7ee09b0ec40689cd3b3b9995dfdda9b5f33482f1441afa48304a68b24b942464d0334d4b20c17a6f09d5db119257212f7b2d48860e22c64924dc53c03557624a020b175ef47553946ce2e60c3f2ba95865d73b2d440c2fd3eb3c9850447c249c869444dbc103d8197c5556355e6c46455b61aa0b176bcc73290b532726d4339bb35b144fecf5c5d4c5e90aef23fc9c9375ae7206f9ea5a22255365f7e0728e3360cb78562c805a4f9a2b4a674eb5af30b598b98b16227a223505e2e013e426296e8f3d1ff03d6506fa431162438afabbc4e8b9626e8821d6bd264ff9d35a2df90e2be27e0297c162a0a6bbc937f349d31413484b52ca27255b2263abde2793cb946387685de9662777cdf6808a18c3a25759ed4e6ab6410f7881b655421be87e3f3df100062f12ae806059ca3e11248557080a64e4ec6e0a46697cae4c5c5197e3defdc";

            using (var reader = new StringReader(longKey))
            {
                var pem = new PemReader(reader);
                var o = (RsaKeyParameters)pem.ReadObject();

                var br = new BinaryReader(new MemoryStream(code));

                br.ReadBytes(6);

                // for (var i = 0; i < 3; i++)
                // {
                var data = br.ReadBytes(128);

                var decrypted = EncryptValue(data, o.Exponent, o.Modulus, 128);
                // string base64 = Convert.ToBase64String(decrypted);
                var ascii = Encoding.ASCII.GetString(decrypted).Split('?');
                var i = Array.IndexOf(ascii, string.Empty) + 2;
                Surname = ascii[i];
                Initials = ascii[i + 1];
                IdNumber = ascii[i + 8].Substring(0, 13);
                // Console.WriteLine("As base64: " + base64);
                Console.WriteLine("As ASCII: " + ascii);
                // }
            }
        }

        public string Surname { get; }
        public string Initials { get; }
        public IdNumber IdNumber { get; }

        public static DrivingLicence FromHex(string code) => new DrivingLicence(HexToByteArray(code));

        public static byte[] HexToByteArray(string hex) => Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();

        private static byte[] EncryptValue(byte[] rgb, BigInteger e, BigInteger n, int size)
        {
            var input = new BigInteger(rgb);
            var output = input.ModPow(e, n);
            var result = GetPaddedValue(output, size);
            return result;
        }

        private static byte[] GetPaddedValue(BigInteger value, int length)
        {
            var result = value.ToByteArrayUnsigned();
            var padded = new byte[length];
            Buffer.BlockCopy(result, 0, padded, (length - result.Length), result.Length);
            return padded;
        }
    }
}