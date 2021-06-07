using CourseManager.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManager.ViewModel
{
    public class HomePageViewModel: INotifyBase
    {
        private int _instrumentValue = 0;

        public int InstrumentValue
        {
            get { return _instrumentValue; }
            set { _instrumentValue = value;this.DoNotify(); }
        }

        public HomePageViewModel()
        {
            //在构造函数中启动另一个线程去死循环 仪表器
            this.RefreshInstrumentValue();
        }

        Random random = new Random();

        bool taskSwitch = true;

        //使用List的原因是，当我调用Dispose()方法时，可能已经是在While循环里面时调用的，
        //而while里面则使用了停顿，所以会一直卡在那里。通过List的方式在调用Dispose()方法中，再次释放所有的线程
        List<Task> taskList = new List<Task>();

        public void RefreshInstrumentValue()
        {
            //启动一个线程
            var task =  Task.Factory.StartNew(new Action(async () =>
            {
                while (taskSwitch)
                {
                    InstrumentValue = random.Next(Math.Max(InstrumentValue - 5,0),Math.Min(InstrumentValue + 5,100));
                    //每停顿一秒刷新一次
                    await Task.Delay(1000);
                }
            }));
            taskList.Add(task);
        }

        /// <summary>
        /// 释放线程
        /// </summary>
        public void Dispose()
        {
            try
            {

                taskSwitch = false;
                Task.WaitAll(taskList.ToArray());
            }
            catch
            {

            }
        }
    }
}
