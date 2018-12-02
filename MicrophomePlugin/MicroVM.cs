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
        private const double _totalLenghtConst = 320;
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

        /// <summary>
        /// Построение можели
        /// </summary>
        public DelegateCommand Build { get; }

        /// <summary>
        /// Установка значений по-умолчанию
        /// </summary>
        public DelegateCommand MakeDefault { get; }
        
        private void PropertyChange() { }

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
                _totalLenght = double.Parse(value);
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
                    throw new ArgumentException("Недопустимые символы");
                }
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
                _handleDiametr = double.Parse(value);
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
                    throw new ArgumentException("Недопустимые символы");
                }
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
        public bool IsEnableBuild { get; set; } = true;

        /// <summary>
        /// Конструктор
        /// </summary>
        public MicroVM()
        {
            Build = new DelegateCommand(() =>
            {
                Debug.WriteLine("Построить");
            });

            MakeDefault = new DelegateCommand(() =>
            {
                TotalLenght = _totalLenghtConst.ToString("0.##");
                RaisePropertyChanged("TotalLenght");
                HandleLenght = _handleLenghtConst.ToString("0.##");
                RaisePropertyChanged("HandleLenght");
                ClipLenght = _clipLenghtConst.ToString("0.##");
                RaisePropertyChanged("ClipLenght");
                HandleDiametr = _handleDiametrConst.ToString("0.##"); ;
                RaisePropertyChanged("HandleDiametr");
                CapsuleRadius = _capsuleDiametrConst.ToString("0.##");
                RaisePropertyChanged("CapsuleRadius");
                Debug.WriteLine("Установить значение по-умолчанию");
            });
        }
    }
}
