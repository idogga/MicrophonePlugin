using System;

namespace MicrophonePlugin
{
    /// <summary>
    /// Строитель модели
    /// </summary>
    public class Builder : IDisposable
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="capsuleDiametr">диаметр капсюли</param>
        /// <param name="clipLenght">длина зажима</param>
        /// <param name="handleDiametr">диаметр ручки</param>
        /// <param name="handleLenght">длина ручки</param>
        /// <param name="totalLenght">общая длина микрофона</param>
        public Builder(double capsuleDiametr, double clipLenght, double handleDiametr, double handleLenght, double totalLenght)
        {
            using (var connector = new CadConnect())
            {

            }
        }

        /// <summary>
        /// очистка данных
        /// </summary>
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
