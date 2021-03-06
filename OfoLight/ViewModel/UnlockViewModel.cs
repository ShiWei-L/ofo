﻿using OfoLight.Entity;
using OfoLight.View;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OfoLight.ViewModel
{
    /// <summary>
    /// 解锁页VM
    /// </summary>
    public class UnlockViewModel : BaseViewModel
    {
        private string _carNumber;

        public string CarNumber
        {
            get { return _carNumber; }
            set
            {
                _carNumber = value;
                NotifyPropertyChanged("CarNumber");
            }
        }

        private bool _isEnableUnlock = true;

        public bool IsEnableUnlock
        {
            get { return _isEnableUnlock; }
            set
            {
                _isEnableUnlock = value;
                NotifyPropertyChanged("IsEnableUnlock");
            }
        }

        /// <summary>
        /// 解锁命令
        /// </summary>
        public ICommand UnlockCarCommand { get; set; }

        /// <summary>
        /// 解锁页VM
        /// </summary>
        public UnlockViewModel()
        {
            UnlockCarCommand = new RelayCommand(UnlockCar);
        }

        /// <summary>
        /// 解锁车辆
        /// </summary>
        /// <param name="state"></param>
        private void UnlockCar(object state)
        {
            IsEnableUnlock = false;
            Task.Run(async () =>
            {
                try
                {
                    var unlockCarResult = await OfoApi.UnlockCarAsync(CarNumber);
                    if (await CheckOfoApiResult(unlockCarResult))
                    {
                        if (unlockCarResult.Data.Password.Length > 8)  //此处为加密的密码？
                        {
                            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
                            {
                                ContentPageArgs args = new ContentPageArgs()
                                {
                                    Name = "ofo共享单车",
                                    ContentElement = new WebPageContentView(Global.MAIN_WEBPAGE_URL)
                                };

                                TryReplaceNavigate(typeof(ContentPageView), args);
                            });
                        }
                        else
                        {
                            Global.AppConfig.LastOrderNum = unlockCarResult.Data.OrderNumber;
                            Global.AppConfig.LastOrderPwd = unlockCarResult.Data.Password;
                            Global.SaveAppConfig();
                            await TryReplaceNavigateAsync(typeof(UnlockedView), unlockCarResult.Data);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        IsEnableUnlock = true;
                    });
                }
            });
        }
    }
}
