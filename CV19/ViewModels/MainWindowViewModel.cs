using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region TestDataPoints
        
        /// <summary>
        /// Test data for plot
        /// </summary>
        private IEnumerable<DataPoint> _TestDataPoints;

        /// <summary>
        /// Test data for plot
        /// </summary>
        public IEnumerable<DataPoint> TestDataPoints
        {
            get => _TestDataPoints;
            set => Set(ref _TestDataPoints, value);
        }

        #endregion

        #region Window Title

        private string _Title = "Анализ статистики CV19";
        
        /// <summary>
        /// Window title
        /// </summary>
        public string Title
        {
            get => _Title;
            //set
            //{
            //if (Equals(_Title, value))
            //{
            //    return;
            //}
            //_Title = value;
            //OnPropertyChanged();

            //Set(ref _Title, value);
            //}
            set => Set(ref _Title, value);
        }

        #endregion

        #region App Status

        /// <summary>
        /// App Status
        /// </summary>
        private string _Status = "Ready!";

        /// <summary>
        /// App Status
        /// </summary>
        public string Status
        {
            get => _Status;
            set => Set(ref _Title, value);
        }

        #endregion

        #region Commands

        #region CloseAppCommand

        public ICommand CloseAppCommand
        {
            get;
        }

        private bool CanCloseAppCommandExecute(object p) => true;

        private void OnCloseAppCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        #endregion

        #endregion

        public MainWindowViewModel()
        {

            #region Commands

            CloseAppCommand = new LambdaCommand(OnCloseAppCommandExecuted, CanCloseAppCommandExecute);

            #endregion

            var data_points = new List<DataPoint>((int)(360 / 0.1));
            for (var x = 0d; x <= 360; x += 0.1)
            {
                const double to_rad = Math.PI / 180;
                double y = Math.Sin(x * to_rad);

                data_points.Add(new DataPoint { XValue = x , Yvalue = y }); 
            }

            TestDataPoints = data_points;
        }
    }
}
