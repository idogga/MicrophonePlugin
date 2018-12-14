using MicrophonePlugin;
using NUnit.Framework;
using System;

namespace MicrophonePluginTests
{
    [TestFixture]
    public class MicroVMTest
    {
        private MicroVM _microVM;

        [SetUp]
        public void SetUp()
        {
            _microVM = new MicroVM();
        }

        [TestCase("500")]
        [TestCase("600")]
        [Test, Description("Позитивный тест")]
        public void SetTotalLenght(string arg)
        {
            _microVM.TotalLenght = arg;
            Assert.AreEqual(_microVM.TotalLenght, arg, "данные не занеслись");
        }

        [TestCase("-1")]
        [TestCase("200")]
        [Test, Description("Позитивный тест")]
        public void SetTotalLenghtNegative(string arg)
        {
            try
            {
                _microVM.TotalLenght = arg;
                Assert.Fail("значение было установлено");
            }
            catch(ArgumentException ex)
            {
                Assert.AreEqual(ex.Message, 
                    "Общая длина должна быть больше суммы диаметра капсюли, длины ручки и длины зажима", 
                    "Текст ошибки не соответствует сообщению");
            }
            catch
            {
                Assert.Fail("Получена не понятная ошибка");
            }
        }
    }
}
