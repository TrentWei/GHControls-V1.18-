using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Data;
using DB;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using Controls;
using System.IO;
using SqlFactory;
using System.Runtime;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Share;

namespace DadayBabyMainCtrl
{
    public partial class MainFrm : Form
    {
        #region Memeber

        FPControl frmDlgFP = null;
        //WIPControl frmDlgWIP = null;

        private ArrayList aFxMachNos = null;
        private Hashtable hStnInfos = null;
        private int nLogCnt = 0 ;
        private int nTimerIn = 0;
        public object obj = new object();
        
        private delegate void RunInfoEvent(object sender, EventArgs e);
        private event RunInfoEvent OnRunInfoEvent;
        public StringBuilder strRunInf = new StringBuilder();
        private string sOutStn = string.Empty;
        private DataTable dtTrace;
        private DataTable dtCmdQuery;
        private DataTable dtEquCmd;

        #endregion

        #region 命令查询

        private void CmdQueryBtn_Click(object sender, EventArgs e)
        {
            string strWhe = string.Empty;

            try
            {
                string strSql = "select Cmd_Sno, Cmd_Mode, Cmd_Sts, Stn_No, Loc, New_Loc, Height, substring(Crt_Date,1,10) as Crt_Dte, substring(Crt_Date,12,8) as Crt_Tim," +
                    "User_Id, Plt_ID, Trace  from Cmd_Mst where Cmd_Sts in ('0','1') ";
                if (cmbCmdMode.SelectedIndex != 0)
                    strWhe += string.Format(" and Cmd_Mode='{0}'", cmbCmdMode.Text.Split("-".ToCharArray())[0]);
                if (CmdSnoCtl.Text != "命令号")
                    strWhe += string.Format(" and Cmd_Sno='{0}'", GCtrl.C5Sno(int.Parse(CmdSnoCtl.Text)));
                strSql = strSql + strWhe;
                dtCmdQuery.Clear();
                Globals.DB.ExecGetTable(strSql, ref dtCmdQuery);
                dgvCmdMst.ClearSelection();
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
            }
        }

        private void CmdSnoCtl_Enter(object sender, EventArgs e)
        {
            CmdSnoCtl.Text = string.Empty;
        }

        private void CmdSnoCtl_Leave(object sender, EventArgs e)
        {
            try
            {
                CmdSnoCtl.Text = GCtrl.C5Sno(int.Parse(CmdSnoCtl.Text));
            }
            catch (Exception)
            {
                CmdSnoCtl.Text = "命令号";
            }
        }

        #endregion

        #region 命令维护

        private void cmbCmdSno_DropDown(object sender, EventArgs e)
        {
            string strObj = string.Empty;
            DataTable dtCmd = new DataTable();

            try
            {
                string sSql = "select Cmd_Sno from Cmd_Mst where Cmd_Sts in ('0','1')";
                Globals.DB.ExecGetTable(sSql, ref dtCmd);

                cmbCmdSno.Items.Clear();
                cmbCmdSno.Items.Add("");
                foreach (DataRow drCmd in dtCmd.Rows)
                {
                    strObj = drCmd[0].ToString();
                    if (!cmbCmdSno.Items.Contains(strObj))
                        cmbCmdSno.Items.Add(strObj);
                }
            }
            catch(Exception ex)
            {
                Log.WriteLog(ex);
            }
        }

        private void cmbCmdSno_TextChanged(object sender, EventArgs e)
        {
            DataRow drCmd = null;

            try
            {
                if (cmbCmdSno.Text.Trim().Length < 1)
                {
                    Globals.CtlClear(cmbCSts, cStnNoCtl, cTraceCtl, cPriCtl);
                    return;
                }

                string sSql = string.Format(@"select * from Cmd_Mst where Cmd_Sts in ('0','1') and Cmd_Sno='{0}'", cmbCmdSno.Text);
                if (Globals.DB.ExecGetRow(sSql, ref drCmd) && drCmd != null)
                {
                    cmbCSts.SelectedIndex = int.Parse(drCmd["Cmd_Sts"].ToString());
                    cStnNoCtl.Text = drCmd["Stn_No"].ToString();
                    cTraceCtl.Text = drCmd["trace"].ToString();
                    cPriCtl.Text = drCmd["Prty"].ToString();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
            }
        }

        private void modCmdBtn_Click(object sender, EventArgs e)
        {
            DataRow drCmd = null;

            //if (!Globals.bLogin) { MsgBox.Warning("请先到用户登录模块登录,谢谢!"); return; }
            if (!Globals.CtlValidate(cStnNoCtl, cTraceCtl, cPriCtl,cmbCSts))
            {
                MsgBox.Warning(StrTables.MSG_NOT_FULL); 
                return; 
            }
            if (MsgBox.Question("是否确认执行?") == DialogResult.No) return;

            try
            {
                string sSql = string.Format("select * from Cmd_Mst where Cmd_Sno='{0}'", GCtrl.C5Sno(cmbCmdSno.Text));
                if (!Globals.DB.ExecGetRow(sSql, ref drCmd) || drCmd == null)
                {
                    MsgBox.Warning("查无记录!");
                    return;
                }

                MakeSql mSql = new MakeSql("Cmd_Mst", SqlType.Update);
                mSql.AddStrWhere(drCmd, "Cmd_Sno");
                mSql.SetStrValue("Cmd_Sts", cmbCSts.Text.Split("-".ToCharArray())[0]);
                mSql.SetStrValue("Stn_No", cStnNoCtl.Text.Trim());
                mSql.SetStrValue("Trace", cTraceCtl.Text.Trim());
                mSql.SetStrValue("Prty", cPriCtl.Text.Trim());
                sSql = mSql.GetSql();

                if (Globals.DB.ExecSql(sSql)) MsgBox.Information("操作成功!");
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
            }
            finally
            {
                flushCmdBtn_Click(null, null);
            }
        }

        private void flushCmdBtn_Click(object sender, EventArgs e)
        {
            DataTable dtCmd = new DataTable();

            Globals.CtlClear(cStnNoCtl, cTraceCtl, cPriCtl);
            string sSql = "select * from Cmd_Mst where Cmd_Sts in('0','1')";
            Globals.DB.ExecGetTable(sSql, ref dtCmd);

            cmbCmdSno.Items.Clear();
            cmbCmdSno.Items.Add("");
            cmbCSts.SelectedIndex = -1;

            foreach (DataRow dr in dtCmd.Rows)
                cmbCmdSno.Items.Add(dr["Cmd_Sno"]);
            if (cmbCmdSno.Items.Count > 0)
                cmbCmdSno.SelectedIndex = 0;
        }

        #endregion

        #region 设备命令

        private void cmbEquCmdSno_DropDown(object sender, EventArgs e)
        {
            string strObj = string.Empty;
            DataTable dtTemp = new DataTable();

            string strSql = "select CmdSno,EquNo,CmdMode,CmdSts,Source,Destination,CompleteCode from EquCmd where CmdSts in ('0','1')";
            Globals.DB.ExecGetTable(strSql, ref dtTemp);

            cmbEquCmdSno.Items.Clear();
            cmbEquCmdSno.Items.Add("");
            foreach (DataRow drCmd in dtTemp.Rows)
            {
                strObj = drCmd["CmdSno"].ToString();
                if (!cmbEquCmdSno.Items.Contains(strObj))
                    cmbEquCmdSno.Items.Add(strObj);
            }
        }

        private void cmbEquCmdSno_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtEquCmd.Rows.Clear();
        }

        private void btnEquQuery_Click(object sender, EventArgs e)
        {
            QueryEquCmd();
        }

        private void QueryEquCmd()
        {
            string strSql = "select CmdSno,EquNo,CmdMode,CmdSts,Source,Destination,CompleteCode from EquCmd where CmdSts in ('0','1')";
            if (cmbEquCmdSno.Text.Length > 0)
                strSql += string.Format(" and CmdSno='{0}'", cmbEquCmdSno.Text);
            Globals.DB.ExecGetTable(strSql, ref dtEquCmd);
        }

        private void btnEquFin_Click(object sender, EventArgs e)
        {
            //if (!Globals.bLogin) { MsgBox.Warning("请先到用户登录模块登录,谢谢!"); return; }
            if (MsgBox.Question("是否确认执行?") == DialogResult.No) { return; }

            try
            {
                if (dgvCraneRgv.SelectedRows.Count > 0)
                {
                    string strSql = string.Format("update EquCmd set CmdSts='{0}',CompleteCode='PC',ENDDT=getdate() where CmdSno ='{1}'",
                        cEquSts.FFin, cmbEquCmdSno.Text.Trim());
                    if (Globals.DB.ExecSql(strSql))
                        MsgBox.Information("执行成功!");
                }
            }
            catch (Exception)
            { 
            }
            finally 
            { 
                QueryEquCmd(); 
            }
        }

        private void btnEquCancel_Click(object sender, EventArgs e)
        {
            //if (!Globals.bLogin) { MsgBox.Warning("请先到用户登录模块登录,谢谢!"); return; }
            if (MsgBox.Question("是否确认执行?") == DialogResult.No) { return; }

            try
            {
                if (dgvCraneRgv.SelectedRows.Count > 0)
                {
                    string strSql = string.Format("update EquCmd set CmdSts='{0}',CompleteCode='PC',ENDDT=getdate() where CmdSno ='{1}'",
                        cEquSts.FCanCel, cmbEquCmdSno.Text.Trim());
                    if (Globals.DB.ExecSql(strSql)) 
                        MsgBox.Information("执行成功!");
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                QueryEquCmd(); 
            }
        }
        #endregion

        #region 站口维护

        

        

       

        

        #endregion

        #region 用户登录

        

        private void LogOutBtn_Click(object sender, EventArgs e)
        {
            Globals.bLogin = false;
            Globals.USER_ID = string.Empty;
            Globals.USER_NAME = string.Empty;
            UsrInfCtl.Text = string.Empty;
            Log.WriteLog(cLog.UsrLogOut, Globals.USER_ID);
        }
        #endregion

        #region 流程码

        private void DataTrace()
        {
            dtTrace.Columns.Add("作业");
            dtTrace.Columns.Add("流程码");
            dtTrace.Columns.Add("说明");

            dtTrace.Rows.Add("公用", cTrace.A00, "待执行");

            dtTrace.Rows.Add(dtTrace.NewRow());
            dtTrace.Rows.Add("入库", cTrace.I11, "写资料到入库站口");
            dtTrace.Rows.Add("入库", cTrace.I12, "下SLV入库命令");
            dtTrace.Rows.Add("入库", cTrace.I13, "SLV入库命令完成");
            dtTrace.Rows.Add("入库", cTrace.I14, "下CRN入库命令");
            dtTrace.Rows.Add("入库", cTrace.I15, "CRN入库命令完成");

            dtTrace.Rows.Add(dtTrace.NewRow());
            dtTrace.Rows.Add("出库", cTrace.O21, "写资料到出库站口");
            dtTrace.Rows.Add("出库", cTrace.O22, "下CRN出库命令");
            dtTrace.Rows.Add("出库", cTrace.O23, "CRN出库命令完成");
            dtTrace.Rows.Add("出库", cTrace.O24, "下SLV出库命令");
            dtTrace.Rows.Add("出库", cTrace.O25, "SLV出库命令完成");
            dtTrace.Rows.Add("出库", cTrace.O26, "货物到达目的站口");

            dtTrace.Rows.Add(dtTrace.NewRow());
            dtTrace.Rows.Add("库对库", cTrace.L51, "下CRN库对库命令");
            dtTrace.Rows.Add("库对库", cTrace.L52, "CRN库对库命令完成");
        }

        #endregion

        #region Event

        private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MsgBox.Question(StrTables.MSG_EXIT) == DialogResult.Yes)
            {
                timer1.Enabled = false;
                Thread.Sleep(500);
#if !DEBUG
            PLC.ClosePlc();
            //Device.ClosePorts();
#endif
                Log.WriteLog(cLog.Exit, "...");
                Log.WriteLogClose(cLog.NextLine);

                string strSql = string.Format("update ctrlhs set hs='0' where EquNo='{0}'", Globals.strEquNo);
                Globals.DB.ExecSql(strSql);

                System.GC.Collect();
            }
            else
                e.Cancel = true;
        }

        private void MainFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.GC.Collect();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimerPro();
        }

        #endregion

        #region Function

        public MainFrm()
        {
            InitializeComponent();
            Init(); 
        }

        private void Init()
        {
            InitData();
            InitCtl();
            InitCrane();
            GCtrl.GetCrnInfo();
            string strSql = string.Format("select * from ctrlhs where EquNo='{0}'", Globals.strEquNo);
            if (!Globals.DB.ExecQuery(strSql))
            {
                strSql = string.Format("insert into ctrlhs(HS,EquNo,trndt) values ('1','{0}','2015-10-10 12:00:00')", Globals.strEquNo);
                Globals.DB.ExecSql(strSql);
            }
            
            Log.WriteLogClose(cLog.Start,"...");
        }

        private void InitCrane()
        {
            GCtrl.pCrnInfs = new CrnInfo[GCtrl.nMaxCrn];

            string strObj = string.Empty;
            for (int i = 0; i < GCtrl.nMaxCrn; i++)
            {
                GCtrl.pCrnInfs[i] = new CrnInfo();
            }
        }

        private void InitData()
        {
            dtTrace = new DataTable();
            dtCmdQuery = new DataTable();
            dtEquCmd = new DataTable();

            GCtrl.dtWrtPlc = new DataTable();
            GCtrl.dtWrtPlc.Columns.Add("Station");
            GCtrl.dtWrtPlc.Columns.Add("CmdSno");
            GCtrl.dtWrtPlc.Columns.Add("PalletNo");
            GCtrl.dtWrtPlc.Columns.Add("CmdMode");
            GCtrl.dtWrtPlc.Columns.Add("Destnation");
            GCtrl.dtWrtPlc.Columns.Add("PCConform");

            //dgvTrace.DataSource = dtTrace;

            DataTrace();
            UsrInfCtl.Text = "";
            CmdSnoCtl.Text = "命令号";
            OnRunInfoEvent = new RunInfoEvent(RunInfo);
            if (!Directory.Exists(Log.strLogPath))
                Directory.CreateDirectory(Log.strLogPath);
            Log.strLogName = Globals.GetCurDate(Globals.DB) + ".Log";

            string strSql = "select Cmd_Sno, Cmd_Mode, Cmd_Sts, Stn_No, Loc, New_Loc, Height, substring(Crt_Date,1,10) as Crt_Dte, substring(Crt_Date,12,8) as Crt_Tim," +
                            "User_Id, Plt_ID, Trace from Cmd_Mst where Cmd_Sts in ('0','1') and 0=1 ";
            Globals.DB.ExecGetTable(strSql, ref dtCmdQuery);
            dgvCmdMst.DataSource = dtCmdQuery;

            strSql = "select CmdSno,EquNo,CmdMode,CmdSts,Source,Destination,CompleteCode from EquCmd where 0=1";
            Globals.DB.ExecGetTable(strSql, ref dtEquCmd);
            dgvCraneRgv.DataSource = dtEquCmd;

            cmbCmdMode.SelectedIndex = 0;
        }

        private void MaintainLog()
        {
            try
            {
                #region 日志文件
                foreach (string sPath in Directory.GetDirectories(Log.strLogPath))
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

                string sComA = Globals.GetDteFrmCtl(DateTime.Now.AddMonths(-1));
                string sComC = Globals.GetDteFrmCtl(DateTime.Now);
                string sDate = string.Empty,sDFile = string.Empty;
                foreach (string sFile in Directory.GetFiles(Log.strLogPath))
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
                        if( sDate.Length != 12)
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
        
        private void InitCtl()
        {
            this.Text = "Warehouse Control System (V.1.1.0)";
            ComNameCtl.Text = "盟立自动化科技(上海)有限公司承制";
            timInfCtl.Text = "2011-01-01 09:00:02";
            
            if( Globals.AppFlag == cWarehouse.FWH)
            {
                SysNameCtl.Text = "中山港航自动仓主控系统(WCS)";
                this.Text = "中山港航自动仓主控系统(WCS)) V" + Application.ProductVersion;
                frmDlgFP = new FPControl();
                frmDlgFP.MdiParent = this;
                frmDlgFP.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                frmDlgFP.Show();
               // cmbArea.Items.Add("D区");
                //cmbArea.Items.Add("E区");
               // cmbArea.Items.Add("F区");
                hStnInfos = frmDlgFP.hFStnInfos;
               // cmbArea.SelectedIndex = 0;
            }

            timer1.Interval = 500;
            timer1.Enabled = true;            
        }

        private void RunInfo(object sender, EventArgs e)
        {
            try
            {
                if (strRunInf.Length < 1) return;
                Monitor.Enter(obj);

                string[] sInfs = OntimInfCtl.Lines;
                if (sInfs.Length > 60)
                {
                    int nLine = 20;
                    OntimInfCtl.Clear();
                    for (nLine = 20; nLine < sInfs.Length - 1; nLine++)
                        OntimInfCtl.AppendText(sInfs[nLine] + "\r\n");
                }

                OntimInfCtl.AppendText(strRunInf.ToString());
                strRunInf.Remove(0, strRunInf.Length);
                OntimInfCtl.Select(OntimInfCtl.TextLength - 1, 0);
                OntimInfCtl.ScrollToCaret();
                Monitor.Exit(obj);
            }
            catch (Exception ex)
            {
                Log.WriteLog(cLog.Exception, ex.Message);
            }
        }
        
        private void SetRunInfo(string strInf)
        {
            try
            {
                Monitor.Enter(obj);
                strRunInf.Append(Globals.GetTimFrmCtl(DateTime.Now) + "  " + strInf + "\r\n");
                Monitor.Exit(obj);
            }
            catch (Exception ex)
            {
                Log.WriteLog(cLog.Exception, ex.Message);
            }
        }
        
        private void TimerPro()
        {
            try
            {
                timer1.Stop();
                timer1.Enabled = false;

                timInfCtl.Text = Globals.GetDteTimFrmCtlL(DateTime.Now);
                OnRunInfoEvent.Invoke(null, null);
                if (nTimerIn++ > 4) nTimerIn = 0; else return;

                string strSql = string.Format("update CtrlHs set Hs='1',TrnDt={0} where EquNo='{1}'", Globals.sEquDt, Globals.strEquNo);
                Globals.DB.ExecSql(strSql);

                Log.strLogName = Globals.GetDteFrmCtl(DateTime.Now) + ".Log";
                
                if (!Directory.Exists(Log.strLogPath))
                    Directory.CreateDirectory(Log.strLogPath);
                Log.MaintainLog();
                if (Globals.bLogin && Globals.IS_SUPER_USER == false)
                {
                    if (nLogCnt++ > 100)
                    {
                        Globals.bLogin = false;
                        nLogCnt = 0;
                        OntimInfCtl.AppendText("用户" + Globals.USER_NAME + "时间到,强制登出\n\r");
                        Log.WriteLog(cLog.LogOuted, Globals.USER_NAME);
                        UsrInfCtl.Text = "";
                    }
                }

                GC.Collect();
                if ( !Globals.DB.IsConnect())
                {
                    SetRunInfo("ASRS数据库失去连接");
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
            }
            finally
            {
                timer1.Enabled = true;
                timer1.Start();
            }          
        }
                
        #endregion
    }
}
