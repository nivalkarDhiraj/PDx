using NUnit.Framework;
using HCA.PatientDigital.Identity;

namespace HCA.PatientDigital.Identity.Test
{
    public class Identity_UnitTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void EncrypString()
        {
            string value = "Qb8wznqrty";
            string key = "E546C8DF278CD5931069B522E695D4F2";
            var encrypctedString = "AcImJHkBG0SpyKm+lIUMIcxTaVc+ANFD2zGL1qX9Sfo=";
            var encryptionString = Encryption.EncryptString(value, key);
            var result = Encryption.DecryptString(encryptionString, key);
            var resultExpected = Encryption.DecryptString(encrypctedString, key);            
            Assert.IsTrue(result == resultExpected);
            
        }        
    }
}