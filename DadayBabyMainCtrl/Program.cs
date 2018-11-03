using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Share;

namespace DadayBabyMainCtrl
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                string strSql;
                if (!Globals.InitApp())
                    return;
                
                bool isCreated;
                Mutex m = null;
                if (Globals.AppFlag == "A")
                {
                    Globals.strEquNo = "21";
                    m = new Mutex(false, "DadayBabyMainCtrl--FPControl", out isCreated);
                    if (!isCreated)
                    {
                        Log.WriteLogClose(cLog.RunLog, "程序已经运行");
                        return;
                    }
                }
                else if (Globals.AppFlag == "B")
                {
                    Globals.strEquNo = "22";
                    m = new Mutex(false, "DadayBabyMainCtrl--WIPControl", out isCreated);
                    if (!isCreated)
                    {
                        Log.WriteLogClose(cLog.RunLog, "程序已经运行");
                        return;
                    }
                }
                else
                    return;

                strSql = string.Format("select * from CtrlHs where EquNo='{0}' and Hs='1'", Globals.strEquNo);
                if (Globals.DB.ExecQuery(strSql))
                {
                    strSql = string.Format("update CtrlHs set Hs='0' where EquNo='{0}'", Globals.strEquNo);
                    Globals.DB.ExecSql(strSql);
                    MsgBox.Warning("网络内已经有程序在运行");
                    Log.WriteLogClose(cLog.RunLog, "网络已经有程序在运行");
                    return;
                }
                
                Application.Run(new MainFrm());
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
                Log.CloseLog();
            }
        }
    }
}
