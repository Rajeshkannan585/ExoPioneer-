using UnityEngine;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class EncryptedSaveSystem
{
    private static readonly string key = "EXOPIONEER_256_KEY"; // Must be 16/24/32 chars for AES
    private static readonly string savePath = Application.persistentDataPath + "/save.dat";

    public static void SaveData(string jsonData)
    {
        byte[] encrypted = EncryptStringToBytes_Aes(jsonData, key);
        File.WriteAllBytes(savePath, encrypted);
        Debug.Log("💾 Encrypted save complete.");
    }

    public static string LoadData()
    {
        if (!File.Exists(savePath)) return null;

        byte[] encrypted = File.ReadAllBytes(savePath);
        string decrypted = DecryptStringFromBytes_Aes(encrypted, key);
        return decrypted;
    }

    public static void DeleteData()
    {
        if (File.Exists(savePath))
            File.Delete(savePath);
        Debug.Log("🧹 All local save data deleted.");
    }

    private static byte[] EncryptStringToBytes_Aes(string plainText, string key)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key.PadRight(32));
            aes.IV = new byte[16];

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (StreamWriter sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            return ms.ToArray();
            }
        }
    }

    private static string DecryptStringFromBytes_Aes(byte[] cipherText, string key)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key.PadRight(32));
            aes.IV = new byte[16];

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using (MemoryStream ms = new MemoryStream(cipherText))
            using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (StreamReader sr = new StreamReader(cs))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
