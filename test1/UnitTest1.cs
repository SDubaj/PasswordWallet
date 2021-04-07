using NUnit.Framework;
using PasswordWallet_console.Entities;
using PasswordWallet_console.Services;

namespace test1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var haslo = "haslo123";
            var service = new PasswordService(null,null);
           service.EncryptPassword("b14ca5898a4e4133bbce2ea231511111", haslo, out string encryptedPassword);
            Assert.AreEqual("ag8PD0WdxZSYWluDBHzqbg==", encryptedPassword);
        }

        [Test]
        public void Test2()
        {
            var haslo = "haslo123";

            var password = new Password();
            password.LoginPassword = "ag8PD0WdxZSYWluDBHzqbg==";
            password.Key = "b14ca5898a4e4133bbce2ea231511111";
            var service = new PasswordService(null, null);
            service.DecryptPass(password);
            Assert.AreEqual( haslo, password.LoginPassword);
        }

    }
}