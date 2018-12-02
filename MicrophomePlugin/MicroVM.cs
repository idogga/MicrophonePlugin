using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Diagnostics;
using System.Windows.Controls;

namespace MicrophonePlugin
{
    /// <summary>
    /// Модель микрофона
    /// </summary>
    public class MicroVM : BindableBase
    {
        #region const
        private const double _totalLenghtConst = 400;
        private const double _capsuleDiametrConst = 80;
        private const double _handleDiametrConst = 50;
        private const double _handleLenghtConst = 250;
        private const double _clipLenghtConst = 15;
        #endregion
        private double _totalLenght = _totalLenghtConst;
        private double _capsuleDiametr = _capsuleDiametrConst;
        private double _handleDiametr = _handleDiametrConst;
        private double _handleLenght = _handleLenghtConst;
        private double _clipLenght = _clipLenghtConst;
        private bool? _isEnableBuild = true;
        /// <summary>
        /// Построение можели
        /// </summary>
        public DelegateCommand Build { get; }

        /// <summary>
        /// Установка значений по-умолчанию
        /// </summary>
        public DelegateCommand MakeDefault { get; }
        

        /// <summary>
        /// Общая длина микрофона
        /// </summary>
        public string TotalLenght
        {
            get
            {
                return _totalLenght.ToString("0.##");
            }
            set
            {
                try
                {
                    _totalLenght = double.Parse(value);
                }
                catch
                {
                    IsEnableBuild = false;
                    throw new ArgumentException("Недопустимые символы");
                }
                if (_totalLenght <= _capsuleDiametr + _handleLenght + _clipLenght)
                {
                    IsEnableBuild = false;
                    throw new ArgumentException("Общая длина должна быть больше суммы диаметра капсюли, длины ручки и длины зажима");
                }
                IsEnableBuild = true;
                PropertyChange();
            }
        }

        /// <summary>
        /// Радиус капсюли
        /// </summary>
        /// <remarks>
        /// набалдшника
        /// </remarks>
        public string CapsuleRadius
        {
            get
            {
                return _capsuleDiametr.ToString("0.##");
            }
            set
            {
                try
                {
                    _capsuleDiametr = double.Parse(value);
                }
                catch
                {
                    IsEnableBuild = false;
                    throw new ArgumentException("Недопустимые символы");
                }
                if (_capsuleDiametr <= _handleDiametr * 1.5)
                {
                    IsEnableBuild = false;
                    throw new ArgumentException("Диаметр капсюли должен быть в 1.5 раза больше диаметра ручки");
                }
                if (_capsuleDiametr > 120)
                {
                    IsEnableBuild = false;
                    throw new ArgumentException("Диаметр капсюли должен быть меньше 120 мм");
                }
                IsEnableBuild = true;
                PropertyChange();
            }
        }

        /// <summary>
        /// диаметр ручки
        /// </summary>
        public string HandleDiametr
        {
            get
            {
                return _handleDiametr.ToString("0.##");
            }
            set
            {
                try
                {
                    _handleDiametr = double.Parse(value);
                }
                catch
                {
                    IsEnableBuild = false;
                    throw new ArgumentException("Недопустимые символы");
                }
                if (_handleDiametr <= 30)
                {
                    IsEnableBuild = false;
                    throw new ArgumentException("Диаметр ручки должен быть больше 30 мм");
                }
                if (_handleDiametr > 80)
                {
                    IsEnableBuild = false;
                    throw new ArgumentException("Диаметр ручки должен быть меньше 80 мм");
                }
                IsEnableBuild = true;
                PropertyChange();
            }
        }

        /// <summary>
        /// длина ручки
        /// </summary>
        public string HandleLenght
        {
            get
            {
                return _handleLenght.ToString("0.##");
            }
            set
            {
                try
                {
                    _handleLenght = double.Parse(value);
                }
                catch
                {
                    IsEnableBuild = false;
                    throw new ArgumentException("Недопустимые символы");
                }
                if (_handleLenght <= 110)
                {
                    IsEnableBuild = false;
                    throw new ArgumentException("Длина ручки должна быть больше 110 мм");
                }
                IsEnableBuild = true;
                PropertyChange();
            }
        }

        /// <summary>
        /// Длина зажима для капсюли
        /// </summary>
        public string ClipLenght
        {
            get
            {
                return _clipLenght.ToString("0.##");
            }
            set
            {
                _clipLenght = double.Parse(value);
            }
        }

        /// <summary>
        /// Доступность построения
        /// </summary>
        public bool? IsEnableBuild
        { get
            { return _isEnableBuild; }
            set
            {
                _isEnableBuild = value;
                RaisePropertyChanged("IsEnableBuild");
            }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public MicroVM()
        {
            Build = new DelegateCommand(() =>
            {
                Debug.WriteLine("Построить");
                using (var builder = new Builder(_capsuleDiametr, _clipLenght, _handleDiametr, _handleLenght, _totalLenght))
                {
                }
            });

            MakeDefault = new DelegateCommand(() =>
            {
                TotalLenght = _totalLenghtConst.ToString("0.##");
                HandleLenght = _handleLenghtConst.ToString("0.##");
                ClipLenght = _clipLenghtConst.ToString("0.##");
                HandleDiametr = _handleDiametrConst.ToString("0.##"); ;
                CapsuleRadius = _capsuleDiametrConst.ToString("0.##");
                PropertyChange();
                Debug.WriteLine("Установить значение по-умолчанию");
            });
        }


        private void PropertyChange()
        {
            RaisePropertyChanged("TotalLenght");
            RaisePropertyChanged("HandleLenght");
            RaisePropertyChanged("ClipLenght");
            RaisePropertyChanged("HandleDiametr");
            RaisePropertyChanged("CapsuleRadius");
        }
    }
}
