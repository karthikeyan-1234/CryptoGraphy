using Microsoft.AspNetCore.Mvc;

using System.Security.Cryptography;
using System.Text;

namespace CryptoGraphy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CryptoGraphyController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<CryptoGraphyController> _logger;

        public CryptoGraphyController(ILogger<CryptoGraphyController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetCryptoGraphyWithPass")]
        public IActionResult GetCryptoGraphyWithPass()
        {
            // Load the public key for encryption
            string publicKeyPath = "public_key.pem";
            string publicKey = System.IO.File.ReadAllText(publicKeyPath);
            string textToEncrypt = "This is a secret message.";

            // Encrypt the text
            byte[] encryptedData = Encrypt(textToEncrypt, publicKey);
            string encryptedText = Convert.ToBase64String(encryptedData);
            Console.WriteLine("Encrypted Text: " + encryptedText);

            // Load the private key for decryption
            string privateKeyPath = "private_key.pem";
            string privateKey = System.IO.File.ReadAllText(privateKeyPath);

            // Decrypt the text
            string decryptedText = Decrypt(encryptedData, privateKey, "12345678");
            Console.WriteLine("Decrypted Text: " + decryptedText);

            return Ok();
        }

        [HttpGet("GetCryptoGraphyWithOutPass")]
        public IActionResult GetCryptoGraphyWithOutPass()
        {
            // Load the public key for encryption
            string publicKeyPath = "public_key_wop.pem";
            string publicKey = System.IO.File.ReadAllText(publicKeyPath);
            string textToEncrypt = "This is a secret message.";

            // Encrypt the text
            byte[] encryptedData = Encrypt(textToEncrypt, publicKey);
            string encryptedText = Convert.ToBase64String(encryptedData);
            Console.WriteLine("Encrypted Text: " + encryptedText);

            // Load the private key for decryption
            string privateKeyPath = "private_key_wop.pem";
            string privateKey = System.IO.File.ReadAllText(privateKeyPath);

            // Decrypt the text
            string decryptedText = Decrypt(encryptedData, privateKey, null);
            Console.WriteLine("Decrypted Text: " + decryptedText);

            return Ok();
        }

        public static byte[] Encrypt(string text, string publicKey)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportFromPem(publicKey.ToCharArray());
                byte[] dataToEncrypt = Encoding.UTF8.GetBytes(text);
                byte[] encryptedData = rsa.Encrypt(dataToEncrypt, RSAEncryptionPadding.Pkcs1);
                return encryptedData;
            }
        }

        public static string Decrypt(byte[] encryptedData, string privateKey,string? password)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                if(password == null)
                    rsa.ImportFromPem(privateKey.ToCharArray());
                else
                    rsa.ImportFromEncryptedPem(privateKey.ToCharArray(), password);
                byte[] decryptedData = rsa.Decrypt(encryptedData, RSAEncryptionPadding.Pkcs1);
                return Encoding.UTF8.GetString(decryptedData);
            }
        }


    }
}