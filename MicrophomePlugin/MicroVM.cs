﻿using Prism.Commands;
using Prism.Mvvm;

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
        public DelegateCommand Build;

        /// <summary>
        /// Установка значений по-умолчанию
        /// </summary>
        public DelegateCommand MakeDefault;
        
        private void PropertyChange() { }

        /// <summary>
        /// Общая длина микрофона
        /// </summary>
        public string TotalLenght
        {
            get;
            set;
        }

        /// <summary>
        /// Радиус капсюли
        /// </summary>
        /// <remarks>
        /// набалдшника
        /// </remarks>
        public string CapsuleRadius { get; set; }

        /// <summary>
        /// диаметр ручки
        /// </summary>
        public string HandleDiametr { get; set; }

        /// <summary>
        /// длина ручки
        /// </summary>
        public string HandleLenght { get; set; }

        /// <summary>
        /// Длина задвижки
        /// </summary>
        public string ClipLenght { get; set; }

        /// <summary>
        /// Доступность построения
        /// </summary>
        public bool IsEnableBuild { get; set; }

    }
}