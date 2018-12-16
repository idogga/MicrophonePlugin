using MicrophonePlugin;
using NUnit.Framework;
using System;
using System.Reflection;

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
        
        [TestCase("500", nameof(MicroVM.TotalLenght))]
        [TestCase("600", nameof(MicroVM.TotalLenght))]
        [TestCase("100", nameof(MicroVM.CapsuleRadius))]
        [TestCase("100.1", nameof(MicroVM.CapsuleRadius))]
        [TestCase("100,1", nameof(MicroVM.CapsuleRadius))]
        [TestCase("50", nameof(MicroVM.HandleDiametr))]
        [TestCase("50.1", nameof(MicroVM.HandleDiametr))]
        [TestCase("50,1", nameof(MicroVM.HandleDiametr))]
        [TestCase("120", nameof(MicroVM.HandleLenght))]
        [TestCase("120.1", nameof(MicroVM.HandleLenght))]
        [TestCase("120,1", nameof(MicroVM.HandleLenght))]
        [TestCase("10", nameof(MicroVM.ClipLenght))]
        [TestCase("10.1", nameof(MicroVM.ClipLenght))]
        [TestCase("10,1", nameof(MicroVM.ClipLenght))]
        [Test, Description("Позитивный тест")]
        public void SetPositive(string arg, string typeName)
        {
            try
            {
                Type type = _microVM.GetType();
                PropertyInfo prop = type.GetProperty(typeName);

                prop.SetValue(_microVM, arg, null);
            }
            catch
            {
                Assert.Fail("Получена не понятная ошибка");
            }
        }



        [TestCase("-1", "Общая длина должна быть больше суммы диаметра капсюли, длины ручки и длины зажима", nameof(MicroVM.TotalLenght))]
        [TestCase("200", "Общая длина должна быть больше суммы диаметра капсюли, длины ручки и длины зажима", nameof(MicroVM.TotalLenght))]
        [TestCase("-1", "Диаметр капсюли должен быть в 1.5 раза больше диаметра ручки", nameof(MicroVM.CapsuleRadius))]
        [TestCase("1", "Диаметр капсюли должен быть в 1.5 раза больше диаметра ручки", nameof(MicroVM.CapsuleRadius))]
        [TestCase("200", "Диаметр капсюли должен быть меньше 120 мм", nameof(MicroVM.CapsuleRadius))]
        [TestCase("asdfgh", "Недопустимые символы", nameof(MicroVM.CapsuleRadius))]
        [TestCase("-1", "Диаметр ручки должен быть больше 30 мм", nameof(MicroVM.HandleDiametr))]
        [TestCase("1", "Диаметр ручки должен быть больше 30 мм", nameof(MicroVM.HandleDiametr))]
        [TestCase("80.1", "Диаметр ручки должен быть меньше 80 мм", nameof(MicroVM.HandleDiametr))]
        [TestCase("asdfgh", "Недопустимые символы", nameof(MicroVM.HandleDiametr))]
        [TestCase("-1", "Длина ручки должна быть больше 110 мм", nameof(MicroVM.HandleLenght))]
        [TestCase("109", "Длина ручки должна быть больше 110 мм", nameof(MicroVM.HandleLenght))]
        [TestCase("asdfgh", "Недопустимые символы", nameof(MicroVM.HandleLenght))]
        [TestCase("-1", "Длина зажима для капсюли должна быть больше 5 мм", nameof(MicroVM.ClipLenght))]
        [TestCase("4", "Длина зажима для капсюли должна быть больше 5 мм", nameof(MicroVM.ClipLenght))]
        [TestCase("21", "Длина зажима для капсюли должна быть меньше 20 мм", nameof(MicroVM.ClipLenght))]
        [TestCase("asdfgh", "Недопустимые символы", nameof(MicroVM.ClipLenght))]
        [Test, Description("Негативный тест")]
        public void SetNegative(string arg, string mess, string typeName)
        {
            try
            {
                _microVM.GetType().InvokeMember(
                        typeName,
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty,
                        Type.DefaultBinder, 
                        _microVM, 
                        new object[] { arg });
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.InnerException.Message,
                    mess,
                    "Текст ошибки не соответствует сообщению");
            }
        }
    }
}
