using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Asn1.Pkcs;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// 授权文件辅助类
    /// </summary>
    public static class LicenseFileHelper
    {
        #region 属性变量

        #endregion

        #region 获取内容

        #endregion

        #region 扩展方法

        #endregion

        #region 验证判断

        #endregion

        #region 辅助操作(GetByXXX,GetToXXX,GetByXXXXToXXX,SetXXX,......)

        /// <summary>
        /// 获取公/私密钥文件内容(PEM进行base64)
        /// </summary>
        /// <param name="publicByte"></param>
        /// <param name="privateByte"></param>
        public static void GetToPemKeyBase64Content(out string publicContent, out string privateContent)
        {
            // 生成密钥对
            RsaKeyPairGenerator rsaKeyPairGenerator = new RsaKeyPairGenerator();
            RsaKeyGenerationParameters rsaKeyGenerationParameters = new RsaKeyGenerationParameters(BigInteger.ValueOf(3), new Org.BouncyCastle.Security.SecureRandom(), 1024, 25);
            rsaKeyPairGenerator.Init(rsaKeyGenerationParameters);//初始化参数  
            AsymmetricCipherKeyPair keyPair = rsaKeyPairGenerator.GenerateKeyPair();
            AsymmetricKeyParameter publicKey = keyPair.Public;//公钥  
            AsymmetricKeyParameter privateKey = keyPair.Private;//私钥  

            SubjectPublicKeyInfo subjectPublicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKey);
            PrivateKeyInfo privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(privateKey);

            Asn1Object asn1ObjectPublic = subjectPublicKeyInfo.ToAsn1Object();
            byte[] publicByte = asn1ObjectPublic.GetEncoded();

            Asn1Object asn1ObjectPrivate = privateKeyInfo.ToAsn1Object();
            byte[] privateByte = asn1ObjectPrivate.GetEncoded();

            //这里可以将密钥对保存到本地  
            //Console.WriteLine("PublicKey:\n" + Convert.ToBase64String(publicInfoByte));
            //Console.WriteLine("PrivateKey:\n" + Convert.ToBase64String(privateInfoByte));

            //生成公钥
            //string content = AESHelper.AESEncrypt(Convert.ToBase64String(publicInfoByte));
            publicContent = Convert.ToBase64String(publicByte);

            //生成私钥
            //string content = AESHelper.AESEncrypt(Convert.ToBase64String(privateInfoByte));
            privateContent = Convert.ToBase64String(privateByte);
        }

        /// <summary>
        /// 保存公/私密钥文件
        /// </summary>
        /// <param name="publicFilePath"></param>
        /// <param name="privateFilePath"></param>
        public static bool SavePemKeyFile(string publicFilePath, string privateFilePath, bool isAES = true)
        {
            string publicContent = string.Empty, privateContent = string.Empty;
            GetToPemKeyBase64Content(out publicContent, out privateContent);
            publicContent.CheckEmpty();
            privateContent.CheckEmpty();

            if (isAES)
            {
                publicContent = AESHelper.AESEncrypt(publicContent);
                privateContent = AESHelper.AESEncrypt(privateContent);
            }

            //保存公钥
            FilesHelper.SaveFile(Path.GetDirectoryName(publicFilePath), Path.GetFileName(publicFilePath), publicContent);

            //保存私钥
            FilesHelper.SaveFile(Path.GetDirectoryName(privateFilePath), Path.GetFileName(privateFilePath), privateContent);

            return true;
        }

        /// <summary>
        /// 生成授权文件内容
        /// </summary>
        /// <param name="model"></param>
        /// <param name="keyFilePath"></param>
        /// <returns></returns>
        public static string GetPemLicenseContent(LicenseModel model, string keyFilePath, Encoding encoding = null, bool isAES = true)
        {
            model.CheckNull();
            keyFilePath.CheckEmpty();
            encoding = encoding.IsNull() ? Encoding.UTF8 : encoding;

            //注册文件内容
            string encrytText = "";

            //密钥文件内容(公/私密钥文件)
            StreamReader sr = new StreamReader(keyFilePath, encoding);
            string keyContent = isAES ? AESHelper.AESDecrypt(sr.ReadToEnd()) : sr.ReadToEnd();
            byte[] keyBytes = Convert.FromBase64String(keyContent);
            sr.Close();

            //信息内容
            byte[] contentBytes = encoding.GetBytes(JsonHelper.Serialize(model));

            //私钥加密 
            Asn1Object keyObj = Asn1Object.FromByteArray(keyBytes);
            AsymmetricKeyParameter keyObjParameter = PrivateKeyFactory.CreateKey(PrivateKeyInfo.GetInstance(keyObj));

            //PublicKeyFactory,SubjectPublicKeyInfo

            //非对称加密算法，加解密用  
            IAsymmetricBlockCipher engine = new RsaEngine();
            engine.Init(true, keyObjParameter);

            #region 内容超出长度

            int bufferSize = engine.GetOutputBlockSize();//(engine.GetInputBlockSize() / 8) - 11;
            byte[] buffer = new byte[bufferSize];
            MemoryStream msInput = new MemoryStream(contentBytes);
            MemoryStream msOuput = new MemoryStream();
            int readLen = msInput.Read(buffer, 0, bufferSize);
            while (readLen > 0)
            {
                byte[] dataToEnc = new byte[readLen];
                Array.Copy(buffer, 0, dataToEnc, 0, readLen);
                byte[] encData = engine.ProcessBlock(dataToEnc, 0, dataToEnc.Length);
                msOuput.Write(encData, 0, encData.Length);
                readLen = msInput.Read(buffer, 0, bufferSize);
            }
            msInput.Close();
            byte[] result = msOuput.ToArray();
            msOuput.Close();

            encrytText = Convert.ToBase64String(result, 0, result.Length);

            #endregion

            return encrytText;
        }

        /// <summary>
        /// 保存PEM授权文件
        /// </summary>
        /// <param name="model"></param>
        /// <param name="licenseFilePath"></param>
        /// <param name="keyFilePath"></param>
        /// <param name="encoding"></param>
        /// <param name="isAES"></param>
        /// <returns></returns>
        public static bool SavePemLicenseFile(LicenseModel model, string licenseFilePath, string keyFilePath, Encoding encoding = null, bool isAES = true)
        {
            string licenseFileContent = GetPemLicenseContent(model, keyFilePath, encoding: encoding, isAES: isAES);

            //保存授权文件
            FilesHelper.SaveFile(Path.GetDirectoryName(licenseFilePath), Path.GetFileName(licenseFilePath), licenseFileContent);

            return true;
        }

        /// <summary>
        /// 获取授权文件信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="keyFilePath"></param>
        /// <returns></returns>
        public static LicenseModel GetPemLicense(string licenseFilePath, string keyFilePath, Encoding encoding = null, bool isAES = true)
        {
            licenseFilePath.CheckEmpty();
            keyFilePath.CheckEmpty();
            encoding = encoding.IsNull() ? Encoding.UTF8 : encoding;

            //读取注册数据文件
            StreamReader sr = new StreamReader(licenseFilePath);
            string encrytText = sr.ReadToEnd().Replace("\r", "").Replace("\n", "");
            sr.Close();
            byte[] licenseBytets = System.Convert.FromBase64CharArray(encrytText.ToCharArray(), 0, encrytText.Length);

            //读取公钥
            StreamReader srPublickey = new StreamReader(keyFilePath, encoding);
            string publicKey = isAES ? AESHelper.AESDecrypt(srPublickey.ReadToEnd()) : srPublickey.ReadToEnd();
            byte[] publicKeyBytes = Convert.FromBase64String(publicKey);
            srPublickey.Close();

            //注册内容
            LicenseModel model = null;

            try
            {
                string decryptText = "";

                #region PEM 格式解密

                //私钥加密 
                Asn1Object keyObj = Asn1Object.FromByteArray(publicKeyBytes);
                AsymmetricKeyParameter keyObjParameter = PublicKeyFactory.CreateKey(SubjectPublicKeyInfo.GetInstance(keyObj));

                //非对称加密算法，加解密用  
                IAsymmetricBlockCipher engine = new RsaEngine();
                engine.Init(false, keyObjParameter); //false表示解密  

                #region 注册内容未超出长度

                //decryptText = Encoding.UTF8.GetString(engine.ProcessBlock(licenseBytets, 0, licenseBytets.Length));

                #endregion

                #region 注册内容超出长度

                int bufferSize = engine.GetInputBlockSize();//(engine.GetInputBlockSize() / 8) - 11;
                byte[] buffer = new byte[bufferSize];
                MemoryStream msInput = new MemoryStream(licenseBytets);
                MemoryStream msOuput = new MemoryStream();
                int readLen = msInput.Read(buffer, 0, bufferSize);
                while (readLen > 0)
                {
                    byte[] dataToDec = new byte[readLen];
                    Array.Copy(buffer, 0, dataToDec, 0, readLen);
                    byte[] decData = engine.ProcessBlock(dataToDec, 0, dataToDec.Length);
                    msOuput.Write(decData, 0, decData.Length);
                    readLen = msInput.Read(buffer, 0, bufferSize);
                }

                msInput.Close();
                byte[] result = msOuput.ToArray();    //得到解密结果
                msOuput.Close();

                decryptText = encoding.GetString(result);

                #endregion

                #endregion

                #region XML 格式解密

                #region 注册内容未超出长度

                ////用公钥初化始RSACryptoServiceProvider类实例crypt。
                //RSACryptoServiceProvider crypt = new RSACryptoServiceProvider();
                //crypt.FromXmlString(AESHelper.AESDecrypt(publicKey));
                //byte[] decryptByte;

                //byte[] newBytes;
                //newBytes = System.Convert.FromBase64CharArray(encrytText.ToCharArray(), 0, encrytText.Length);
                //decryptByte = crypt.Decrypt(newBytes, false);
                //decryptText = enc.GetString(decryptByte);

                #endregion

                #region 注册内容超出长度

                ////用公钥初化始RSACryptoServiceProvider类实例crypt。
                //RSACryptoServiceProvider crypt = new RSACryptoServiceProvider();
                //crypt.LoadPublicKeyPEM(publicKey);

                //int keySize = crypt.KeySize / 8;
                //byte[] buffer = new byte[keySize];
                //MemoryStream msInput = new MemoryStream(encrytBytes);
                //MemoryStream msOuput = new MemoryStream();
                //int readLen = msInput.Read(buffer, 0, keySize);
                //while (readLen > 0)
                //{
                //    byte[] dataToDec = new byte[readLen];
                //    Array.Copy(buffer, 0, dataToDec, 0, readLen);
                //    byte[] decData = crypt.Decrypt(dataToDec, false);
                //    msOuput.Write(decData, 0, decData.Length);
                //    readLen = msInput.Read(buffer, 0, keySize);
                //}

                //msInput.Close();
                //byte[] result = msOuput.ToArray();    //得到解密结果
                //msOuput.Close();
                //crypt.Clear();

                //decryptText = enc.GetString(result);

                #endregion

                #endregion

                if (!decryptText.IsEmpty())
                {
                    model = JsonHelper.Deserialize<LicenseModel>(decryptText);
                }
            }
            catch (Exception ex)
            {

            }

            return model;
        }


        /// <summary>
        /// 获取公/私密钥文件内容(XML)
        /// </summary>
        /// <param name="publicContent"></param>
        /// <param name="privateContent"></param>
        public static void GetToXmlKeyContent(out string publicContent, out string privateContent)
        {
            RSACryptoServiceProvider crypt = new RSACryptoServiceProvider();
            publicContent = crypt.ToXmlString(true);
            privateContent = crypt.ToXmlString(false);
            crypt.Clear();
        }

        /// <summary>
        /// 注册文件内容(XML,调验证)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="keyFilePath"></param>
        /// <returns></returns>
        public static string GetXmlLicenseContent(LicenseModel model, string keyFilePath, Encoding encoding = null, bool isAES = true)
        {
            model.CheckNull();
            keyFilePath.CheckEmpty();
            encoding = encoding.IsNull() ? Encoding.UTF8 : encoding;

            //注册文件内容
            string encrytText = "";

            //密钥文件内容(公/私密钥文件)
            StreamReader sr = new StreamReader(keyFilePath, encoding);
            string keyXmlContent = isAES ? AESHelper.AESDecrypt(sr.ReadToEnd()) : sr.ReadToEnd();
            //byte[] keyBytes = Convert.FromBase64String(keyContent);
            sr.Close();

            //信息内容
            byte[] contentBytes = encoding.GetBytes(JsonHelper.Serialize(model));

            //用私钥参数初始化RSACryptoServiceProvider类的实例crypt。
            RSACryptoServiceProvider rsaPro = new RSACryptoServiceProvider();
            rsaPro.FromXmlString(keyXmlContent);

            #region 注册内容未超出长度

            //contentBytes = rsaPro.Encrypt(contentBytes, false);
            //encrytText = Convert.ToBase64String(contentBytes, 0, contentBytes.Length);

            #endregion

            #region 注册内容超出长度

            int keySize = rsaPro.KeySize / 8;
            int bufferSize = keySize - 11;
            byte[] buffer = new byte[bufferSize];
            MemoryStream msInput = new MemoryStream(contentBytes);

            MemoryStream msOuput = new MemoryStream();
            int readLen = msInput.Read(buffer, 0, bufferSize);

            while (readLen > 0)
            {
                byte[] dataToEnc = new byte[readLen];
                Array.Copy(buffer, 0, dataToEnc, 0, readLen);
                byte[] encData = rsaPro.Encrypt(dataToEnc, false);
                msOuput.Write(encData, 0, encData.Length);
                readLen = msInput.Read(buffer, 0, bufferSize);
            }
            msInput.Close();
            byte[] result = msOuput.ToArray();
            msOuput.Close();
            rsaPro.Clear();

            encrytText = Convert.ToBase64String(result, 0, result.Length);

            #endregion

            return encrytText;
        }


        /// <summary>
        /// 是否过期
        /// </summary>
        /// <param name="expireValue">过期值(时间/整数天数)</param>
        /// <param name="startDt">开始/安装 时间</param>
        /// <param name="currentDt">当前时间,考虑从服务器传入防止客户端更新</param>
        /// <returns></returns>
        public static bool IsExpire(string expireValue, DateTime startDt, DateTime? currentDt = null)
        {
            if (expireValue.Trim() == "0" || DateTimeHelper.Get(expireValue, DateTime.MinValue) == DateTimeHelper.DefaultDateTime)
            {
                return false;  //不进行过期校验
            }

            if (currentDt == null)
            {
                currentDt = DateTime.Now;
            }

            DateTime endDt = DateTimeHelper.Get(expireValue, DateTime.MinValue);
            if (endDt != DateTime.MinValue)
            {
                return currentDt.Value > DateTimeHelper.GetEnd(endDt);
            }

            int expDays = IntHelper.Get(expireValue, -1);
            if (expDays > -1)
            {
                return currentDt.Value > DateTimeHelper.GetEnd(startDt.AddDays(expDays));
            }

            return true;
        }

        #endregion
    }
}
