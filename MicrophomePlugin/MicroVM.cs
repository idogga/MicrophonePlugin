using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
                switch(_totalLenght * 1000 % 10)
                {
                    case 1:
                        _totalLenght -= 0.001;
                        return _totalLenght.ToString("0.##") + ",";
                    case 2:
                        _totalLenght -= 0.002;
                        return _totalLenght.ToString("0.##") + ",0";
                    case 3:
                        _totalLenght -= 0.003;
                        return _totalLenght.ToString("0.##") + ",00";
                    default:
                        return _totalLenght.ToString("0.##");
                }
            }
            set
            {
                var watch = new Stopwatch();
                watch.Start();
                try
                {
                    _totalLenght = double.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
                    if (value.EndsWith(".") | value.EndsWith(","))
                        _totalLenght += 0.001;
                    if (value.EndsWith(",0"))
                        _totalLenght += 0.002;
                    if (value.EndsWith(",00"))
                        _totalLenght += 0.003;
                }
                catch
                {
                    IsEnableBuild = false;
                    Debug.WriteLine("Parse {0} for {1}", value, watch.ElapsedMilliseconds);
                    throw new ArgumentException("Недопустимые символы");
                }
                Debug.WriteLine("Parse {0} for {1}", value, watch.ElapsedMilliseconds);
                if (_totalLenght <= _capsuleDiametr + _handleLenght + _clipLenght)
                {
                    IsEnableBuild = false;
                    Debug.WriteLine("validate {0} for {1}", value, watch.ElapsedMilliseconds);
                    throw new ArgumentException("Общая длина должна быть больше суммы диаметра капсюли, длины ручки и длины зажима");
                }
                IsEnableBuild = true;
                Debug.WriteLine("validate {0} for {1}", value, watch.ElapsedMilliseconds);
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
                switch (_capsuleDiametr * 1000 % 10)
                {
                    case 1:
                        _capsuleDiametr -= 0.001;
                        return _capsuleDiametr.ToString("0.##") + ",";
                    case 2:
                        _capsuleDiametr -= 0.002;
                        return _capsuleDiametr.ToString("0.##") + ",0";
                    case 3:
                        _capsuleDiametr -= 0.003;
                        return _capsuleDiametr.ToString("0.##") + ",00";
                    default:
                        return _capsuleDiametr.ToString("0.##");
                }
            }
            set
            {
                try
                {
                    _capsuleDiametr = Convert.ToDouble(value.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
                    if (value.EndsWith(".") | value.EndsWith(","))
                        _capsuleDiametr += 0.001;
                    if (value.EndsWith(",0"))
                        _capsuleDiametr += 0.002;
                    if (value.EndsWith(",00"))
                        _capsuleDiametr += 0.003;
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
                switch (_handleDiametr * 1000 % 10)
                {
                    case 1:
                        _handleDiametr -= 0.001;
                        return _handleDiametr.ToString("0.##") + ",";
                    case 2:
                        _handleDiametr -= 0.002;
                        return _handleDiametr.ToString("0.##") + ",0";
                    case 3:
                        _handleDiametr -= 0.003;
                        return _handleDiametr.ToString("0.##") + ",00";
                    default:
                        return _handleDiametr.ToString("0.##");
                }
            }
            set
            {
                try
                {
                    _handleDiametr = Convert.ToDouble(value.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
                    if (value.EndsWith(".") | value.EndsWith(","))
                        _handleDiametr += 0.001;
                    if (value.EndsWith(",0"))
                        _handleDiametr += 0.002;
                    if (value.EndsWith(",00"))
                        _handleDiametr += 0.003;
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
                switch (_handleLenght * 1000 % 10)
                {
                    case 1:
                        _handleLenght -= 0.001;
                        return _handleLenght.ToString("0.##") + ",";
                    case 2:
                        _handleLenght -= 0.002;
                        return _handleLenght.ToString("0.##") + ",0";
                    case 3:
                        _handleLenght -= 0.003;
                        return _handleLenght.ToString("0.##") + ",00";
                    default:
                        return _handleLenght.ToString("0.##");
                }
            }
            set
            {
                try
                {
                    _handleLenght = Convert.ToDouble(value.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
                    if (value.EndsWith(".") | value.EndsWith(","))
                        _handleLenght += 0.001;
                    if (value.EndsWith(",0"))
                        _handleLenght += 0.002;
                    if (value.EndsWith(",00"))
                        _handleLenght += 0.003;
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
                switch ((int)(_clipLenght * 1000 % 10))
                {
                    case 1:
                        _clipLenght -= 0.001;
                        return _clipLenght.ToString("0.##") + ",";
                    case 2:
                        _clipLenght -= 0.002;
                        return _clipLenght.ToString("0.##") + ",0";
                    case 3:
                        _clipLenght -= 0.003;
                        return _clipLenght.ToString("0.##") + ",00";
                    default:
                        return _clipLenght.ToString("0.##");
                }
            }
            set
            {
                try
                {
                    _clipLenght = Convert.ToDouble(value.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
                    if (value.EndsWith(".") | value.EndsWith(","))
                        _clipLenght += 0.001;
                    if (value.EndsWith(",0"))
                        _clipLenght += 0.002;
                    if (value.EndsWith(",00"))
                        _clipLenght += 0.003;
                }
                catch
                {
                    IsEnableBuild = false;
                    throw new ArgumentException("Недопустимые символы");
                }
                if (_clipLenght <= 5)
                {
                    IsEnableBuild = false;
                    throw new ArgumentException("Длина зажима для капсюли должна быть больше 5 мм");
                }
                if (_clipLenght > 20)
                {
                    IsEnableBuild = false;
                    throw new ArgumentException("Длина зажима для капсюли должна быть меньше 20 мм");
                }
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
                    using (var builder = new Builder(_capsuleDiametr, _clipLenght, _handleDiametr, _handleLenght, _totalLenght))
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
