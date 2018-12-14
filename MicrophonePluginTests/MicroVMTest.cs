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
        [Test, Description("Не гативный тест")]
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

        [TestCase("100")]
        [TestCase("100.1")]
        [TestCase("100,1")]
        [Test, Description("Позитивный тест")]
        public void SetCapsuleRadiusPositive(string arg)
        {
            try
            {
                _microVM.CapsuleRadius = arg;
            }
            catch
            {
                Assert.Fail("Получена не понятная ошибка");
            }
        }

        [TestCase("-1", "Диаметр капсюли должен быть в 1.5 раза больше диаметра ручки")]
        [TestCase("1", "Диаметр капсюли должен быть в 1.5 раза больше диаметра ручки")]
        [TestCase("200", "Диаметр капсюли должен быть меньше 120 мм")]
        [TestCase("asdfgh", "Недопустимые символы")]
        [Test, Description("Негативный тест")]
        public void SetCapsuleRadiusNegative(string arg, string mess)
        {
            try
            {
                _microVM.CapsuleRadius = arg;
                Assert.Fail("значение было установлено");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(ex.Message,
                    mess, 
                    "Текст ошибки не соответствует сообщению");
            }
            catch
            {
                Assert.Fail("Получена не понятная ошибка");
            }
        }

        [TestCase("50")]
        [TestCase("50.1")]
        [TestCase("50,1")]
        [Test, Description("Позитивный тест")]
        public void SetHandleDiametrPositive(string arg)
        {
            try
            {
                _microVM.HandleDiametr = arg;
            }
            catch
            {
                Assert.Fail("Получена не понятная ошибка");
            }
        }

        [TestCase("-1", "Диаметр ручки должен быть больше 30 мм")]
        [TestCase("1", "Диаметр ручки должен быть больше 30 мм")]
        [TestCase("80.1", "Диаметр ручки должен быть меньше 80 мм")]
        [TestCase("asdfgh", "Недопустимые символы")]
        [Test, Description("Негативный тест")]
        public void SetHandleDiametrNegative(string arg, string mess)
        {
            try
            {
                _microVM.HandleDiametr = arg;
                Assert.Fail("значение было установлено");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(ex.Message,
                    mess,
                    "Текст ошибки не соответствует сообщению");
            }
            catch
            {
                Assert.Fail("Получена не понятная ошибка");
            }
        }

        [TestCase("120")]
        [TestCase("120.1")]
        [TestCase("120,1")]
        [Test, Description("Позитивный тест")]
        public void SetHandleLenghtPositive(string arg)
        {
            try
            {
                _microVM.HandleLenght = arg;
            }
            catch(Exception ex)
            {
                Assert.Fail("Получена ошибка : " + ex.Message);
            }
        }

        [TestCase("-1", "Длина ручки должна быть больше 110 мм")]
        [TestCase("109", "Длина ручки должна быть больше 110 мм")]
        [TestCase("asdfgh", "Недопустимые символы")]
        [Test, Description("Негативный тест")]
        public void SetHandleLenghtNegative(string arg, string mess)
        {
            try
            {
                _microVM.HandleLenght = arg;
                Assert.Fail("значение было установлено");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(ex.Message,
                    mess,
                    "Текст ошибки не соответствует сообщению");
            }
            catch
            {
                Assert.Fail("Получена не понятная ошибка");
            }
        }
        [TestCase("10")]
        [TestCase("10.1")]
        [TestCase("10,1")]
        [Test, Description("Позитивный тест")]
        public void SetClipLenghtPositive(string arg)
        {
            try
            {
                _microVM.ClipLenght = arg;
            }
            catch
            {
                Assert.Fail("Получена не понятная ошибка");
            }
        }

        [TestCase("-1", "Длина зажима для капсюли должна быть больше 5 мм")]
        [TestCase("4", "Длина зажима для капсюли должна быть больше 5 мм")]
        [TestCase("21", "Длина зажима для капсюли должна быть меньше 20 мм")]
        [TestCase("asdfgh", "Недопустимые символы")]
        [Test, Description("Негативный тест")]
        public void SetCClipLenghtNegative(string arg, string mess)
        {
            try
            {
                _microVM.ClipLenght = arg;
                Assert.Fail("значение было установлено");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(ex.Message,
                    mess,
                    "Текст ошибки не соответствует сообщению");
            }
            catch
            {
                Assert.Fail("Получена не понятная ошибка");
            }
        }
    }
}
