using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDK.ViewModels;
using SDK.ViewModels.ViewModels;

namespace Reminder.ViewModels
{
    /// <summary>
    /// Main page VM
    /// </summary>
    public class MainPageViewModel : ViewModelBase
    {
        #region Private property
        private string _title = "Reminder";
        #endregion

        #region Public property
        public string Title { get => _title; set => SetProperty(ref _title, value); }
        #endregion
    }
}
