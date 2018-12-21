using System;

namespace MicrophonePlugin
{
    /// <summary>
    /// Параметры модели
    /// </summary>
    public class Parameters
    {
        private double _totalLenght ;
        private double _capsuleDiametr;
        private double _handleDiametr;
        private double _handleLenght ;
        private double _clipLenght;
        private double _gridLenght;

        /// <summary>
        /// Общая длина микрофона
        /// </summary>
        public double TotalLenght
        {
            get
            {
                return _totalLenght;
            }
            set
            {
                if (value <= _capsuleDiametr + _handleLenght + _clipLenght)
                {
                    throw new ArgumentException("Общая длина должна быть больше суммы диаметра капсюли, длины ручки и длины зажима");
                }
                _totalLenght = value;
            }
        }

        /// <summary>
        /// Радиус капсюли
        /// </summary>
        /// <remarks>
        /// набалдшника
        /// </remarks>
        public double CapsuleRadius
        {
            get
            {
                return _capsuleDiametr;
            }
            set
            {
                if (value <= _handleDiametr * 1.5)
                {
                    throw new ArgumentException("Диаметр капсюли должен быть в 1.5 раза больше диаметра ручки");
                }
                if (value > 120)
                {
                    throw new ArgumentException("Диаметр капсюли должен быть меньше 120 мм");
                }
                _capsuleDiametr = value;
            }
        }

        /// <summary>
        /// диаметр ручки
        /// </summary>
        public double HandleDiametr
        {
            get
            {
                return _handleDiametr;
            }
            set
            {
                if (value <= 30)
                {
                    throw new ArgumentException("Диаметр ручки должен быть больше 30 мм");
                }
                if (value > 80)
                {
                    throw new ArgumentException("Диаметр ручки должен быть меньше 80 мм");
                }
                _handleDiametr = value;
            }
        }

        /// <summary>
        /// длина ручки
        /// </summary>
        public double HandleLenght
        {
            get
            {
                return _handleLenght;
            }
            set
            {
                if (value <= 110)
                {
                    throw new ArgumentException("Длина ручки должна быть больше 110 мм");
                }
                _handleLenght = value;
            }
        }

        /// <summary>
        /// Длина зажима для капсюли
        /// </summary>
        public double ClipLenght
        {
            get
            {
                return _clipLenght;
            }
            set
            {
                if (value <= 5)
                {
                    throw new ArgumentException("Длина зажима для капсюли должна быть больше 5 мм");
                }
                if (value > 20)
                {
                    throw new ArgumentException("Длина зажима для капсюли должна быть меньше 20 мм");
                }
                _clipLenght = value;
            }
        }

        /// <summary>
        /// Толщина сетки
        /// </summary>
        public double GridLenght
        {
            get
            {
                return _gridLenght;
            }
            set
            {
                if (value <= 0.01)
                {
                    throw new ArgumentException("Толщина сетки должна быть больше 0.01 мм");
                }
                if (value > 2)
                {
                    throw new ArgumentException("Толщина сетки должна быть меньше 2 мм");
                }
                _gridLenght = value;
            }
        }

        
    }
}
