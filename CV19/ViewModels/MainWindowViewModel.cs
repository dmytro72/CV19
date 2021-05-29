using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
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
    }
}
