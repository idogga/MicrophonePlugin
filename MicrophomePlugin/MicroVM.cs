using Prism.Commands;
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
        private const double _gridLenghtConst = 1;
        #endregion

        private string _totalLenght;
        private string _capsuleDiametr;
        private string _handleDiametr;
        private string _handleLenght;
        private string _clipLenght;
        private string _gridLenght;

        private bool? _isEnableBuild = true;
        /// <summary>
        /// Построение можели
        /// </summary>
        public DelegateCommand Build { get; }

        /// <summary>
        /// Установка значений по-умолчанию
        /// </summary>
        public DelegateCommand MakeDefault { get; }

        private Parameters _parameters;

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
                if (!IsNumber(value, out double doble))
                {
                    IsEnableBuild = false;
                    throw new ArgumentException("Недопустимые символы");
                }
                try
                {
                    _parameters.TotalLenght = doble;
                }
                catch(ArgumentException ex)
                {
                    IsEnableBuild = false;
                    throw ex;
                }
                finally
                {
                    IsEnableBuild = true;
                    _totalLenght = value;
                }
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
                if (!IsNumber(value, out double doble))
                {
                    IsEnableBuild = false;
                    throw new ArgumentException("Недопустимые символы");
                }
                try
                {
                    _parameters.CapsuleRadius = doble;
                }
                catch (ArgumentException ex)
                {
                    IsEnableBuild = false;
                    throw ex;
                }
                finally
                {
                    IsEnableBuild = true;
                    _capsuleDiametr = value;
                    RaisePropertyChanged("TotalLenght");
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
                return _handleDiametr;                
            }
            set
            {
                if (!IsNumber(value, out double doble))
                {
                    IsEnableBuild = false;
                    throw new ArgumentException("Недопустимые символы");
                }
                try
                {
                    _parameters.HandleDiametr = doble;
                }
                catch (ArgumentException ex)
                {
                    IsEnableBuild = false;
                    throw ex;
                }
                finally
                {
                    IsEnableBuild = true;
                    _handleDiametr = value;
                    RaisePropertyChanged("CapsuleRadius");
                }
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
                if (!IsNumber(value, out double doble))
                {
                    IsEnableBuild = false;
                    throw new ArgumentException("Недопустимые символы");
                }
                try
                {
                    _parameters.HandleLenght = doble;
                }
                catch (ArgumentException ex)
                {
                    IsEnableBuild = false;
                    throw ex;
                }
                finally
                {
                    IsEnableBuild = true;
                    _handleLenght = value;
                    RaisePropertyChanged("TotalLenght");
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
                return _clipLenght;
            }
            set
            {
                if (!IsNumber(value, out double doble))
                {
                    IsEnableBuild = false;
                    throw new ArgumentException("Недопустимые символы");
                }
                try
                {
                    _parameters.ClipLenght = doble;
                }
                catch (ArgumentException ex)
                {
                    IsEnableBuild = false;
                    throw ex;
                }
                finally
                {
                    IsEnableBuild = true;
                    _clipLenght = value;
                    RaisePropertyChanged("TotalLenght");
                }
            }
        }

        /// <summary>
        /// Толщина сетки
        /// </summary>
        public string GridLenght
        {
            get
            {
                return _gridLenght;
            }
            set
            {
                if (!IsNumber(value, out double doble))
                {
                    IsEnableBuild = false;
                    throw new ArgumentException("Недопустимые символы");
                }
                try
                {
                    _parameters.GridLenght = doble;
                }
                catch (ArgumentException ex)
                {
                    IsEnableBuild = false;
                    throw ex;
                }
                finally
                {
                    IsEnableBuild = true;
                    _gridLenght = value;
                }
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
            _parameters = new Parameters();
            Build = new DelegateCommand(async () =>
            {
                var waitWindow = new WaitWindow();
                waitWindow.Show();
                await Task.Factory.StartNew(() =>
                {
                    using (var builder = new Builder(_parameters))
                    {
                    }
                });
                waitWindow.Close();
            });
            SetDefaultProperties();
            MakeDefault = new DelegateCommand(() =>
            {
                SetDefaultProperties();
            });
        }

        private void SetDefaultProperties()
        {
            TotalLenght = _totalLenghtConst.ToString("0.##");
            HandleLenght = _handleLenghtConst.ToString("0.##");
            ClipLenght = _clipLenghtConst.ToString("0.##");
            HandleDiametr = _handleDiametrConst.ToString("0.##"); ;
            CapsuleRadius = _capsuleDiametrConst.ToString("0.##");
            GridLenght = _gridLenghtConst.ToString("0.##");
            Debug.WriteLine("Установить значение по-умолчанию");
        }

        private bool IsNumber(string str, out double number)
        {
            return double.TryParse(str.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out number)
                    || double.IsNaN(number)
                    || double.IsInfinity(number);
        }

        private double Parse(string str)
        {
            return double.Parse(str.Replace(',', '.'),
                        System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
