using Prism.Commands;
using Prism.Mvvm;
using System.Diagnostics;

namespace MicrophonePlugin
{
    /// <summary>
    /// Модель микрофона
    /// </summary>
    public class MicroVM : BindableBase
    {
        private double _totalLenght = 80;
        private double _capsuleRadius = 80;
        private double _handleDiametr = 80;
        private double _handleLenght = 80;
        private double _clipLenght = 80;

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
                return _capsuleRadius.ToString("0.##");
            }
            set
            {
                _capsuleRadius = double.Parse(value);
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
                _handleLenght = double.Parse(value);
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
                Debug.WriteLine("Установить значение по-умолчанию");
            });
        }
    }
}
