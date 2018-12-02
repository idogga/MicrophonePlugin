using System;
using Kompas6API5;

namespace MicrophonePlugin
{
    /// <summary>
    /// Хранение соединения с Кад-системой
    /// </summary>
    public class CadConnect : IDisposable
    {
        private KompasObject _kompasObject;
        /// <summary>
        /// Объект када
        /// </summary>
        public KompasObject Kompas
        {
            get
            {
                return _kompasObject;
            }
            set
            {
                _kompasObject = value;
            }
        }

        /// <summary>
        /// Соединение с кадом
        /// </summary>
        /// <returns></returns>
        public ksDocument3D Connect()
        {
            if (Kompas == null)
            {
                var type = Type.GetTypeFromProgID("KOMPAS.Application.5");
                Kompas = (KompasObject)Activator.CreateInstance(type);
            }

            if (Kompas != null)
            {
                Kompas.Visible = true;
                Kompas.ActivateControllerAPI();
            }
            return (ksDocument3D)_kompasObject.Document3D();
        }

        /// <summary>
        /// Закрытие када
        /// </summary>
        public void Close()
        {
            try
            {
                Kompas.Quit();
            }
            catch
            {
            }
            Kompas = null;
        }
        /// <summary>
        /// Очистка данных
        /// </summary>
        public void Dispose()
        {
            
        }
    }
}
