﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

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
        private string _totalLenght = _totalLenghtConst.ToString("0.00");
        private string _capsuleDiametr = _capsuleDiametrConst.ToString("0.00");
        private string _handleDiametr = _handleDiametrConst.ToString("0.00");
        private string _handleLenght = _handleLenghtConst.ToString("0.00");
        private string _clipLenght = _clipLenghtConst.ToString("0.00");
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
                return _totalLenght;
            }
            set
            {
                if(!double.TryParse(value.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double doble))
                {
                    IsEnableBuild = false;
                    throw new ArgumentException("Недопустимые символы");
                }
                if (doble <= double.Parse(_capsuleDiametr) + double.Parse(_handleLenght) + double.Parse(_clipLenght))
                {
                    IsEnableBuild = false;
                    throw new ArgumentException("Общая длина должна быть больше суммы диаметра капсюли, длины ручки и длины зажима");
                }
                IsEnableBuild = true;
                _totalLenght = value;
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
                return _capsuleDiametr;
            }
            set
            {
                if (!double.TryParse(value.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double doble))
                {
                    IsEnableBuild = false;
                    throw new ArgumentException("Недопустимые символы");
                }
                if (doble <= double.Parse(_handleDiametr) * 1.5)
                {
                    IsEnableBuild = false;
                    throw new ArgumentException("Диаметр капсюли должен быть в 1.5 раза больше диаметра ручки");
                }
                if (doble > 120)
                {
                    IsEnableBuild = false;
                    throw new ArgumentException("Диаметр капсюли должен быть меньше 120 мм");
                }
                IsEnableBuild = true;
                _capsuleDiametr = value;
                RaisePropertyChanged("TotalLenght");
            }
        }

        /// <summary>
        /// диаметр ручки
        /// </summary>
        public string HandleDiametr
        {
            get
            {
                return _handleDiametr;                
            }
            set
            {
                if (!double.TryParse(value.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double doble))
                {
                    IsEnableBuild = false;
                    throw new ArgumentException("Недопустимые символы");
                }
                if (doble <= 30)
                {
                    IsEnableBuild = false;
                    throw new ArgumentException("Диаметр ручки должен быть больше 30 мм");
                }
                if (doble > 80)
                {
                    IsEnableBuild = false;
                    throw new ArgumentException("Диаметр ручки должен быть меньше 80 мм");
                }
                IsEnableBuild = true;
                _handleDiametr = value;
                RaisePropertyChanged("CapsuleRadius");
            }
        }

        /// <summary>
        /// длина ручки
        /// </summary>
        public string HandleLenght
        {
            get
            {
                        return _handleLenght;
            }
            set
            {
                if (!double.TryParse(value.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double doble))
                {
                    IsEnableBuild = false;
                    throw new ArgumentException("Недопустимые символы");
                }
                if (doble <= 110)
                {
                    IsEnableBuild = false;
                    throw new ArgumentException("Длина ручки должна быть больше 110 мм");
                }
                IsEnableBuild = true;
                _handleLenght = value;
                RaisePropertyChanged("TotalLenght");
            }
        }

        /// <summary>
        /// Длина зажима для капсюли
        /// </summary>
        public string ClipLenght
        {
            get
            {
                return _clipLenght;
            }
            set
            {
                if (!double.TryParse(value.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double doble))
                {
                    IsEnableBuild = false;
                    throw new ArgumentException("Недопустимые символы");
                }
                if (doble <= 5)
                {
                    IsEnableBuild = false;
                    throw new ArgumentException("Длина зажима для капсюли должна быть больше 5 мм");
                }
                if (doble > 20)
                {
                    IsEnableBuild = false;
                    throw new ArgumentException("Длина зажима для капсюли должна быть меньше 20 мм");
                }
                _clipLenght = value;
                IsEnableBuild = true;
                RaisePropertyChanged("TotalLenght");
            }
        }

        /// <summary>
        /// Доступность построения
        /// </summary>
        public bool? IsEnableBuild
        {
            get
            {
                return _isEnableBuild;
            }
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
            Build = new DelegateCommand(async () =>
            {
                var waitWindow = new WaitWindow();
                waitWindow.Show();
                await Task.Factory.StartNew(() =>
                {
                    using (var builder = new Builder(double.Parse(_capsuleDiametr.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture), 
                        double.Parse(_clipLenght.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture), 
                        double.Parse(_handleDiametr.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture), 
                        double.Parse(_handleLenght.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture), 
                        double.Parse(_totalLenght.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture)))
                    {
                    }
                });
                waitWindow.Close();
            });

            MakeDefault = new DelegateCommand(() =>
            {
                TotalLenght = _totalLenghtConst.ToString("0.##");
                HandleLenght = _handleLenghtConst.ToString("0.##");
                ClipLenght = _clipLenghtConst.ToString("0.##");
                HandleDiametr = _handleDiametrConst.ToString("0.##"); ;
                CapsuleRadius = _capsuleDiametrConst.ToString("0.##");
                Debug.WriteLine("Установить значение по-умолчанию");
            });
        }
    }
}
