using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Collections;
using System.Threading;
using System.Text;

namespace DadayBabyMainCtrl
{
    class Log
    {
        private static Queue strQue = new Queue();
        private static object obj = new object();
        public static string strLogPath = string.Empty;
        public static string strLogName = string.Empty;

        public static void WriteLog(string strDes, string strLog = "")
        {
            try
            {
                if (strLog.Length < 1) return;
                Monitor.Enter(strQue);
                string strTim = GetTimFrmCtl(DateTime.Now);
                strTim = strTim + "  " + strDes + "  " + strLog;
                strQue.Enqueue(strTim);
                Monitor.Exit(strQue);
            }
            catch (Exception)
            {
            }
            if (strQue.Count > 40) CloseLog();
        }

        public static void WriteLogClose(string strDes, string strLog = "")
        {
            try
            {
                Monitor.TryEnter(obj);
                string strTim = GetTimFrmCtl(DateTime.Now);
                strTim = strTim + "  " + strDes + "  " + strLog;
                strQue.Enqueue(strTim);
                Monitor.Exit(obj);
            }
            catch (Exception)
            {
            }
            CloseLog();
        }

        public static void CloseLog()
        {
            try
            {
                Monitor.TryEnter(obj);
                if (strLogName.Trim().Length < 10)
                {
                    strLogName = GetDteFrmCtl(DateTime.Now) + ".Log";
                }
                FileStream lfs = File.Open(strLogPath + strLogName, FileMode.Append);
                StreamWriter sw = new StreamWriter(lfs);
                try
                {
                    Monitor.Enter(strQue);
                    while (strQue.Count > 0)
                        sw.WriteLine(strQue.Dequeue());
                    Monitor.Exit(strQue);
                }
                catch (Exception)
                {
                }
                sw.Close();
                lfs.Close();
                Monitor.Exit(obj);
            }
            catch (Exception)
            {
            }
        }

        public static void WriteLog(Exception ex)
        {
            WriteLog(cLog.Exception, ex.Message + "\r\n" + ex.StackTrace);
        }

        public static void MaintainLog()
        {
            try
            {
                #region 日志文件
                foreach (string sPath in Directory.GetDirectories(strLogPath))
                {
                    // 删除子目录
                    try
                    {
                        Directory.Delete(sPath, true);
                    }
                    catch (Exception)
                    {
                    }
                }

                string sComA = GetDteFrmCtl(DateTime.Now.AddMonths(-1));
                string sComC = GetDteFrmCtl(DateTime.Now);
                string sDate = string.Empty, sDFile = string.Empty;
                foreach (string sFile in Directory.GetFiles(strLogPath))
                {
                    try
                    {
                        // 删除非日志文件
                        if (!sFile.EndsWith(".Log"))
                        {
                            File.Delete(sFile);
                            continue;
                        }
                        sDFile = sFile;
                        sDate = sFile.Substring(sFile.LastIndexOf('\\') + 1);
                        if (sDate.Length != 12)
                            File.Delete(sFile);
                        sDate = sDate.Remove(8);
                        long.Parse(sDate);

                        if (sDate.CompareTo(sComC) > 0)
                            File.Delete(sFile);
                        else if (sDate.CompareTo(sComA) < 0)
                            File.Delete(sFile);
                    }
                    catch (Exception)
                    {
                        File.Delete(sDFile);
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
            }
        }

        private static string GetDteFrmCtl(DateTime dtDt)
        {
            return dtDt.Year.ToString("0000") + dtDt.Month.ToString("00") + dtDt.Day.ToString("00");
        }

        private static string GetTimFrmCtl(DateTime dtDt)
        {
            return dtDt.Hour.ToString("00") + ":" + dtDt.Minute.ToString("00") + ":" + dtDt.Second.ToString("00");
        }

    }
    
    class cLog
    {
        public const string Start = "启动";
        public const string NextLine = "\r\n";
        public const string Exit = "退出";
        
        //初始化
        public const string InitErr = "程序初始化失败";

        //数据库
        public const string ConnDb = "连接数据库";
        public const string SqlCmd = "SQL指令";
        public const string DbErr = "数据库错误";

        //操作
        public const string Exception = "异常";
        public const string DoFailed = "操作失败";

        //用户
        public const string DoUsr = "操作用户";
        public const string UsrLogIn = "用户登入";
        public const string UsrLogOut = "用户登出";
        public const string LogOuted = "用户时间到,自动退出";
        public const string UsrUptStn = "用户修改站信息";
        public const string UsrUptCmd = "用户修改命令信息";

        //写PLC
        public const string NoStnAdd = "没有站地址";
        public const string WritePLC = "写PLC";
        public const string WritePlcErr = "写PLC异常";

        //读取PLC
        public const string RdPlcErr = "读取PLC失败";
        public const string PlcLog = "PLC站口值记录\r\n";

        //条码
        public const string BcrOK = "读码成功";
        public const string BcrErr = "读码失败";

        //信息
        public const string NoLoc = "储位信息有误";
        public const string RunLog = "运行记录";
        public const string IntHis = "接口调用记录";
        public const string IntErr = "调用接口错误";

        public const string ThreadTime = "线程时间";
    }

}
