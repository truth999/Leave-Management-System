using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace LeaveBalanceScheduler
{
    public partial class Service1 : ServiceBase
    {

        public Timer timer = null;
        public bool hasRun = false;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Log.WriteEventLog("Leave Scheduler");
            timer = new Timer();
            this.timer.Interval = 86400000;    //1 day
            this.timer.Start();
            this.timer.Elapsed += new ElapsedEventHandler(this.timer_tick);
            this.timer.Enabled = true;
            
            Log.WriteEventLog("Leave Scheduler Started");
        }

        private void timer_tick(object sender, ElapsedEventArgs e)
        {

            Log.WriteEventLog("Timer Ticked");

            DateTime temp = DateTime.Now;
            temp = temp.AddDays(1);                 // Push date to tomorrow
            string day = Convert.ToString(temp.Day);
            Log.WriteEventLog(day);

            if (temp.Day == 1)         // If tomorrow is first day then today is last day of month
            {
                Log.WriteEventLog("API calling");
                string url = "http://localhost:50404/api/EmployeeWebApi/UpdateLeaveBalance";
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "Get";
                request.ContentType = "text";
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                string result = responseString;
                Log.WriteEventLog("Leave Balance Incremented");
                hasRun = true;
            }
            else
            {
                hasRun = false;
            }

            Log.WriteEventLog("Leave Scheduler Done");
        }

        protected override void OnStop()
        {
            Log.WriteEventLog("Event OnStop");
            Log.WriteEventLog("ShutDown Service");
            timer.Stop();
            timer = null;
            Log.WriteEventLog("Service Shutdown Successfully");
        }
    }
}
