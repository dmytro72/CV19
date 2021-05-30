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
        #region SelectedPageIndex

        /// <summary>
        /// Taken Tab number
        /// </summary>
        private int _SelectedPageIndex;

        /// <summary>
        /// Taken Tab number
        /// </summary>
        public int SelectedPageIndex
        {
            get => _SelectedPageIndex;
            set => Set(ref _SelectedPageIndex, value);
        }

        #endregion

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

        #region ChangeTabIndexCommand

        public ICommand ChangeTabIndexCommand
        {
            get;
        }

        private bool CanChangeTabIndexCommandExecute(object p) => _SelectedPageIndex >= 0;

        private void OnChangeTabIndexCommandExecuted(object p)
        {
            if (p is null)
            {
                return;
            }
            SelectedPageIndex += Convert.ToInt32(p);
        }

        #endregion

        #endregion

        public MainWindowViewModel()
        {

            #region Commands

            CloseAppCommand = new LambdaCommand(OnCloseAppCommandExecuted, CanCloseAppCommandExecute);
            ChangeTabIndexCommand = new LambdaCommand(OnChangeTabIndexCommandExecuted, CanChangeTabIndexCommandExecute);

            #endregion

            var data_points = new List<DataPoint>((int)(360 / 0.1));
            for (var x = 0d; x <= 360; x += 0.1)
            {
                const double to_rad = Math.PI / 180;
                double y = Math.Sin(x * to_rad);

                data_points.Add(new DataPoint { XValue = x , YValue = y }); 
            }

            TestDataPoints = data_points;
        }
    }
}
