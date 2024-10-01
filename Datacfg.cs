using System;
using System.IO;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using Crypto = System.Security.Cryptography;

namespace LuaDecryptor
{
    public class Datacfg
    {
        public static void DecryptDatacfg()
        {
            string datacfgPath = "Datacfg";
            List<string> datacfgList = Directory.GetFiles(datacfgPath, "*.dat*", SearchOption.AllDirectories).ToList();
            foreach (string datacfg in datacfgList)
            {
                AESDecrypt(datacfg);
            }
        }

        public static void AESDecrypt(string datacfg)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes("@_#*&Reverse2806                ");
            byte[] ivBytes = Encoding.UTF8.GetBytes("!_#@2022_Skyfly)");
            using (Crypto.Aes aesDec = Crypto.Aes.Create())
            {
                aesDec.Key = keyBytes;
                aesDec.IV = ivBytes;

                ICryptoTransform decryptor = aesDec.CreateDecryptor(aesDec.Key, aesDec.IV);

                using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(datacfg)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            Console.WriteLine($"Decrypting {Path.GetFileName(datacfg)}...");
                            File.WriteAllText(datacfg, srDecrypt.ReadToEnd());
                        }
                    }
                }
            }            
        }

    }

}

