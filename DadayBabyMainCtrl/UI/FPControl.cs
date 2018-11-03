#define Area_D
#define Area_E
#define Area_F

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using Controls;
using System.Threading;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using DB;
using System.Net.NetworkInformation;
using System.IO.Ports;

namespace DadayBabyMainCtrl
{
    public partial class FPControl : Form
    {
        #region Memeber

        public Hashtable hFStns = null;
        public Hashtable hFStnInfos = null;
        public Hashtable hFMachNos = null;
        public Hashtable hMacAddr = null;
        public StnInfo[] pStnInfos = null;
        public string[] strInStnNo = {"B123","B134","B144","B154","B164"};
       

        public static int nCranForP = 5;
        public static int nCranForE = 4;
        public static bool isShowLocRate = false;
        public static int nShowLocRateTimes = 0;

        private int nCount = 0;
        private DbAccess dbAsrs = null;
        int[] lpPlcData = null;

        private string sLstStn = string.Empty;
        private int nLstCrnHP = 0;
        private int nLstCrnOP = 0;

        private SerialPort hPort1 = null;
        

        #endregion
        
        #region Event

        public FPControl()
        {
            InitializeComponent();
            Init();
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            TimerPro();
        }

        #endregion
        
        #region UI

        /// <summary>
        /// 初始数据
        /// </summary>
        private void Init()
        {
            dbAsrs = Globals.DBF1;
            GCtrl.dbConn = Globals.DBF1;
            hFStns = new Hashtable();
            hFMachNos = new Hashtable();
            hFStnInfos = new Hashtable();
            pStnInfos = new StnInfo[200];
            hMacAddr = new Hashtable();
            lpPlcData = new int[710];

            try
            {
                InitMapStn();
                setMachNo();
                UpdateStn();
                OpenDevice();
                setMacAdd();
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
            }
            timer1.Interval = 500;
            timer1.Enabled = true;
        }

        /// <summary>
        /// 打开设备端口
        /// </summary>
        private void OpenDevice()
        {
#if !DEBUG
#if Area_D
            PLC.OpenPLCs(Plcs.FCJPLC);
            hPort1 = new SerialPort("COM11");
            try
            {
                hPort1.ReadBufferSize = 100;
                hPort1.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show("端口打开失败！");
            }

#endif

#endif
        }

        /// <summary>
        /// 初始站口号
        /// </summary>
        private void InitMapStn()
        {
            int nCnt = 0;
            for (nCnt = 0; nCnt < pStnInfos.Length; nCnt++)
                pStnInfos[nCnt] = new StnInfo();
            nCnt = 0;

            hFStns.Add(C11.Name, C11); hFStnInfos.Add(C11.Name, pStnInfos[nCnt++]);
            hFStns.Add(C21.Name, C21); hFStnInfos.Add(C21.Name, pStnInfos[nCnt++]);
            hFStns.Add(C31.Name, C31); hFStnInfos.Add(C31.Name, pStnInfos[nCnt++]);
            hFStns.Add(C41.Name, C41); hFStnInfos.Add(C41.Name, pStnInfos[nCnt++]);
            hFStns.Add(C51.Name, C51); hFStnInfos.Add(C51.Name, pStnInfos[nCnt++]);

            hFStns.Add(A11.Name, A11); hFStnInfos.Add(A11.Name, pStnInfos[nCnt++]);
            hFStns.Add(A21.Name, A21); hFStnInfos.Add(A21.Name, pStnInfos[nCnt++]);
            hFStns.Add(A31.Name, A31); hFStnInfos.Add(A31.Name, pStnInfos[nCnt++]);
            hFStns.Add(A41.Name, A41); hFStnInfos.Add(A41.Name, pStnInfos[nCnt++]);
            hFStns.Add(A51.Name, A51); hFStnInfos.Add(A51.Name, pStnInfos[nCnt++]);
            hFStns.Add(A12.Name, A12); hFStnInfos.Add(A12.Name, pStnInfos[nCnt++]);
            hFStns.Add(A22.Name, A22); hFStnInfos.Add(A22.Name, pStnInfos[nCnt++]);
            hFStns.Add(A32.Name, A32); hFStnInfos.Add(A32.Name, pStnInfos[nCnt++]);
            hFStns.Add(A42.Name, A42); hFStnInfos.Add(A42.Name, pStnInfos[nCnt++]);
            hFStns.Add(A52.Name, A52); hFStnInfos.Add(A52.Name, pStnInfos[nCnt++]);

            hFStns.Add(B114.Name, B114); hFStnInfos.Add(B114.Name, pStnInfos[nCnt++]);

            hFStns.Add(B121.Name,B121); hFStnInfos.Add(B121.Name, pStnInfos[nCnt++]);
            hFStns.Add(B122.Name, B122); hFStnInfos.Add(B122.Name, pStnInfos[nCnt++]);
            hFStns.Add(B123.Name, B123); hFStnInfos.Add(B123.Name, pStnInfos[nCnt++]);

            hFStns.Add(B131.Name, B131); hFStnInfos.Add(B131.Name, pStnInfos[nCnt++]);
            hFStns.Add(B132.Name, B132); hFStnInfos.Add(B132.Name, pStnInfos[nCnt++]);
            hFStns.Add(B133.Name, B133); hFStnInfos.Add(B133.Name, pStnInfos[nCnt++]);
            hFStns.Add(B134.Name, B134); hFStnInfos.Add(B134.Name, pStnInfos[nCnt++]);

            hFStns.Add(B141.Name, B141); hFStnInfos.Add(B141.Name, pStnInfos[nCnt++]);
            hFStns.Add(B142.Name, B142); hFStnInfos.Add(B142.Name, pStnInfos[nCnt++]);
            hFStns.Add(B143.Name, B143); hFStnInfos.Add(B143.Name, pStnInfos[nCnt++]);
            hFStns.Add(B144.Name, B144); hFStnInfos.Add(B144.Name, pStnInfos[nCnt++]);

            hFStns.Add(B151.Name, B151); hFStnInfos.Add(B151.Name, pStnInfos[nCnt++]);
            hFStns.Add(B152.Name, B152); hFStnInfos.Add(B152.Name, pStnInfos[nCnt++]);
            hFStns.Add(B153.Name, B153); hFStnInfos.Add(B153.Name, pStnInfos[nCnt++]);
            hFStns.Add(B154.Name, B154); hFStnInfos.Add(B154.Name, pStnInfos[nCnt++]);

            hFStns.Add(B161.Name, B161); hFStnInfos.Add(B161.Name, pStnInfos[nCnt++]);
            hFStns.Add(B162.Name, B162); hFStnInfos.Add(B162.Name, pStnInfos[nCnt++]);
            hFStns.Add(B163.Name, B163); hFStnInfos.Add(B163.Name, pStnInfos[nCnt++]);
            hFStns.Add(B164.Name, B164); hFStnInfos.Add(B164.Name, pStnInfos[nCnt++]);


        }

        /// <summary>
        /// 匹配机台号
        /// </summary>
        private void setMachNo()
        {
            hFMachNos.Add("C11", C11.Name); hFMachNos.Add("C21", C21.Name);
            hFMachNos.Add("C31", C31.Name); hFMachNos.Add("C41", C41.Name);
            hFMachNos.Add("C51", C51.Name);

            hFMachNos.Add("A11", A11.Name); hFMachNos.Add("A21", A21.Name);
            hFMachNos.Add("A31", A31.Name); hFMachNos.Add("A41", A41.Name);
            hFMachNos.Add("A51", A51.Name);
            hFMachNos.Add("A12", A12.Name); hFMachNos.Add("A22", A22.Name);
            hFMachNos.Add("A32", A32.Name); hFMachNos.Add("A42", A42.Name);
            hFMachNos.Add("A52", A52.Name);

            hFMachNos.Add("B114", B114.Name);

            hFMachNos.Add("B121", B121.Name); hFMachNos.Add("B122", B122.Name);
            hFMachNos.Add("B123", B123.Name);

            hFMachNos.Add("B131", B131.Name); hFMachNos.Add("B132", B132.Name);
            hFMachNos.Add("B133", B133.Name); hFMachNos.Add("B134", B134.Name);

            hFMachNos.Add("B141", B141.Name); hFMachNos.Add("B142", B142.Name);
            hFMachNos.Add("B143", B143.Name); hFMachNos.Add("B144", B144.Name);

            hFMachNos.Add("B151", B151.Name); hFMachNos.Add("B152", B152.Name);
            hFMachNos.Add("B153", B153.Name); hFMachNos.Add("B154", B154.Name);

            hFMachNos.Add("B161", B161.Name); hFMachNos.Add("B162", B162.Name);
            hFMachNos.Add("B163", B163.Name); hFMachNos.Add("B164", B164.Name);

            foreach (object objMac in hFMachNos.Keys)
            {
                ((StnCtrl)hFStns[hFMachNos[objMac]]).MachNo = objMac.ToString();
            }
        }

        private void setMacAdd()
        {
            hMacAddr.Add(A11.Name, "D1010"); hMacAddr.Add(A21.Name, "D1050");
            hMacAddr.Add(A31.Name, "D1090"); hMacAddr.Add(A41.Name, "D1120");
            hMacAddr.Add(A51.Name, "D1190");

            hMacAddr.Add(A12.Name, "D1020"); hMacAddr.Add(A22.Name, "D1060");
            hMacAddr.Add(A32.Name, "D1100"); hMacAddr.Add(A42.Name, "D1130");
            hMacAddr.Add(A52.Name, "D1200");


            hMacAddr.Add(B114.Name, "D2070");

            hMacAddr.Add(B121.Name,"D2080"); hMacAddr.Add(B122.Name,"D2090");
            hMacAddr.Add(B123.Name,"D2100");

            hMacAddr.Add(B131.Name,"D2110"); hMacAddr.Add( B132.Name,"D2120");
            hMacAddr.Add(B133.Name,"D2130"); hMacAddr.Add( B134.Name,"D2140");

            hMacAddr.Add( B141.Name,"D2150"); hMacAddr.Add( B142.Name,"D2160");
            hMacAddr.Add( B143.Name,"D2170"); hMacAddr.Add( B144.Name,"D2180");

            hMacAddr.Add(B151.Name,"D2190"); hMacAddr.Add( B152.Name,"D2200");
            hMacAddr.Add(B153.Name,"D2210"); hMacAddr.Add( B154.Name,"D2220");

            hMacAddr.Add(B161.Name,"D2230"); hMacAddr.Add(B162.Name,"D2240");
            hMacAddr.Add(B163.Name,"D2250"); hMacAddr.Add( B164.Name,"D2260");

            hMacAddr.Add(C51.Name, "D2320"); hMacAddr.Add(C41.Name, "D2280");
            hMacAddr.Add(C31.Name, "D2290"); hMacAddr.Add(C21.Name, "D2300");
            hMacAddr.Add(C11.Name, "D2310");
        }

        /// <summary>
        /// 读取PLC资料
        /// </summary>
        private void ReadPlc()
        {

#if !DEBUG
            int nRead = 0;
            string sMachNo = string.Empty;
            Array.Clear(lpPlcData, 0, 710);
            #region 原有程序（已注释）
            //if (PLC.ChkNetworkSts(Globals.ipFCJPLC))
            //{
            //    nRead = PLC.ReadPlc(Plcs.FCJPLC, "D1010", 710, out lpPlcData[0]);
            //    if (nRead != 0)
            //    {
            //        Log.WriteLog(cLog.RdPlcErr, "读取PLC失败");
            //        SetRunInfo("D区 读取PLC失败");
            //        PLC.ClosePlc(Plcs.FCJPLC);
            //        PLC.OpenPlc(Plcs.FCJPLC);
            //    }
            //}
            #endregion
            foreach (object objStn in hMacAddr.Keys)
            {
                string strAdrr = hMacAddr[objStn].ToString();
                int nAddr = Int32.Parse(strAdrr.Substring(1));
                setStnInf(lpPlcData, objStn.ToString(), nAddr);
            }
  
#endif
        }

        /// <summary>
        /// 写入PLC
        /// </summary>
        public bool WritePlc(string sStn, object nCmdSno, object nCmdMode, object nDes)
        {
            string sMachNo = string.Empty;
            string sDevice = string.Empty, sPlc = string.Empty;
            int nStn = 0, nDevice = 0, nCnt = 0;
            bool bWrtOK = false;

            try
            {
                StnCtrl stn = (StnCtrl)hFStns[sStn];
                sMachNo = stn.MachNo;
                nStn = int.Parse(sMachNo.Substring(1));
                sPlc = Plcs.FCJPLC;

                sDevice = hMacAddr[sStn].ToString();
                nDevice = Int32.Parse(sDevice.Substring(1));
#if !DEBUG
                bWrtOK = PLC.WriteBlock(sPlc, sDevice, 4, new int[] { int.Parse(nCmdSno.ToString()),
                    int.Parse(nDes.ToString()),int.Parse(nCmdMode.ToString())});
#endif
                Log.WriteLog(cLog.WritePLC, string.Format("Stn:{0} Adr-{1} CmdSno-{2} CmdMode-{3} Destnation-{4}",
                    sStn, nDevice, nCmdSno, nCmdMode, nDes));
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
                bWrtOK = false;
            }
            return bWrtOK;
        }

        /// <summary>
        /// 更新站口资料
        /// </summary>
        private void UpdateStn()
        {
            foreach (object obj in hFStns.Keys)
            {
                StnCtrl pStn = (StnCtrl)hFStns[obj];
                pStn.setStnInf((StnInfo)hFStnInfos[obj]);
            }
        }

        /// <summary>
        /// 更新主机资料
        /// </summary>
        private void setCrnInfo()
        {
            try
            {
                Crane05.setCrnInfo(GCtrl.pCrnInfs[4]);
                Crane04.setCrnInfo(GCtrl.pCrnInfs[3]);
                Crane03.setCrnInfo(GCtrl.pCrnInfs[2]);
                Crane02.setCrnInfo(GCtrl.pCrnInfs[1]);
                Crane01.setCrnInfo(GCtrl.pCrnInfs[0]);

            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
            }
        }

        /// <summary>
        /// 更新站口资料
        /// </summary>
        /// <param name="lpData"></param>
        /// <param name="sMachNo"></param>
        /// <param name="nAdd"></param>
        private void setStnInf(int[] lpData, string sMachNo, int nAdd)
        {
            
            int[] lpdate = new int[10];
            string  sAddr ="D"+ nAdd.ToString();
            int nRead = PLC.ReadPlc(Plcs.FCJPLC, sAddr, 10, out lpdate[0]);

            #region 确认与PLC连接成功
            int[] lpResponse = new int[1];
            if (nRead == 0)//连接成功成功
            {
                lpResponse[0] = 1;
                PLC.WriteBlock(Plcs.FCJPLC, "D11", 1, lpResponse);
            }
            else if (nRead != 0)
            {
                lpResponse[0] = 2;
                PLC.WriteBlock(Plcs.FCJPLC, "D11", 1, lpResponse);
            }
            #endregion

            int nCur = 0;
            StnInfo stnInf = (StnInfo)hFStnInfos[hFMachNos[sMachNo]];

            stnInf.nCmdSno = lpdate[nCur++];                            //母托盘号（栈板编号）
            stnInf.nDesStn = lpdate[nCur++];                            //目的站
            stnInf.nCmdMode = (byte)lpdate[nCur++];                     //模式
            stnInf.nErr = lpdate[nCur++];                             //异常码
            stnInf.nACK1 = lpdate[nCur++];
            stnInf.nACk2 = lpdate[nCur++];

            int nStatus = lpdate[nCur++];                               //设备状态
            stnInf.bAuto = GCtrl.BitChk(nStatus, 0);                    //自动
            stnInf.bLoad = GCtrl.BitChk(nStatus, 1);                    //荷有
      
            stnInf.bFixed = GCtrl.BitChk(nStatus, 2);                   //定位
            stnInf.bError = GCtrl.IntChk(nStatus, 0xFF00);              //异常
            ((StnCtrl)hFStns[hFMachNos[sMachNo]]).setStnInf(stnInf);

            stnInf.nPalletStatus = lpdate[nCur++];                    //子托盘状态
            stnInf.nLocType = lpdate[nCur++];                         //储位类型
            stnInf.nRemark = lpdate[nCur];                            //预留
        }

        /// <summary>
        /// 即时信息
        /// </summary>
        /// <param name="strInf"></param>
        private void SetRunInfo(string strInf)
        {
            try
            {
                if (strInf.Length < 1) return;
                MainFrm parFrm = (MainFrm)this.ParentForm;
                Monitor.Enter(parFrm.obj);
                parFrm.strRunInf.Append(Globals.GetTimFrmCtl(DateTime.Now) + "  " + strInf + "\r\n");
                Monitor.Exit(parFrm.obj);
            }
            catch (Exception ex)
            {
                Log.WriteLog(cLog.Exception, ex.Message);
            }
        }

        /// <summary>
        /// Timer处理
        /// </summary>
        private void TimerPro()
        {
            try
            {
                timer1.Stop(); 
                timer1.Enabled = false;

                ReadPlc();
                UpdateStn();

                ChkCrnOutStn();
                DoCrnOutStn();

                DoBcrInStn("B114");
                GetEmpetPallet();
                DoCrnInStn();

                DoEmptyCrnOutStn();
                DoWithCycleIn();
                DoWithCycleOut();

                //Globals.DealWithCycle("01350");

                DoWithL2L();
                ChkS2SStn();
                DoWithS2S();


                GetLocRateTvinfo();
                GCtrl.TestEquFin();

                if (nCount++ > 3)
                {
                    nCount = 0;
                    GCtrl.GetCrnInfo();
                    setCrnInfo();
                    GCtrl.MoveCmd();
                }
                Log.CloseLog();
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

        #region Control

        /// <summary>
        /// 入库口读取托盘条码(下入库命令)
        /// </summary>
        private void DoBcrInStn(string strStnNo)
        {
            string strSql = string.Empty;
            string strCmdSno = string.Empty;
            string strBcr = string.Empty;
            string strInStn = string.Empty;
            string strLocSize = string.Empty;
            DataTable dtPalletInfo = new DataTable();
            DataTable dtCmd = new DataTable();
            string sRunInfo = string.Empty;
            try
            {
                StnInfo stnInf = (StnInfo)hFStnInfos[strStnNo];
                if (stnInf.bAuto && stnInf.bLoad && stnInf.nCmdMode == 1 && stnInf.nDesStn == 0) //自动、荷有、命令模式1
                {
                    strLocSize = GetLocHeight(stnInf.nLocType.ToString());
                    if (strLocSize == "0")
                    {
                        Log.WriteLog("站B11-4 获取货物高度信息失败！");
                        SetRunInfo("站B11-4 获取货物高度信息失败！");
                        return;//获取储位类型资料失败

                    }
                    strLocSize = strLocSize.Trim();
                    strBcr = ReadBcr();
                    sRunInfo = string.Format("站B11-4读取条码值：{0}", strBcr);
                    SetRunInfo(sRunInfo);
                    Log.WriteLog("条码读取值："+ strBcr);
                    
                    #region 空栈板入库
                    if (stnInf.nCmdSno == 29999) 
                    {
                        //获取入库站口信息，是否满足入库条件
                        strInStn = GetCmdStnNo("1", true, false, strLocSize);
                        if (strInStn.Length == 0)
                        {
                            sRunInfo = string.Format("空栈版入库:{0}，查无可用站口",strBcr);
                            Log.WriteLog(sRunInfo); SetRunInfo(sRunInfo);
                            throw new Exception("空栈版入库，查无可用站口");
                        }
                        //创建对应空栈板入库命令
                        if (strBcr == "")
                        {
                            DataTable dtEm = new DataTable();
                            strSql = string.Format("select * from cmd_mst where cmd_sts <'9' and cmd_sno = '{0}' and cmd_mode = '1'", stnInf.CmdSno);
                            Globals.DB.ExecGetTable(strSql, ref dtEm);
                            if (dtEm.Rows.Count > 0)
                            {
                                strSql = string.Format("update cmd_mst set cmd_sts = 'E'  where cmd_sts <'9' and cmd_sno = '{0}' and cmd_mode = '1'", stnInf.CmdSno);
                                Globals.DB.ExecSql(strSql);
                                //return;
                            }
                               
                            //空栈板入库读码失败，下达站对站命令
                            strInStn = GetCmdStnNo("1",true, true, strLocSize);
                            if (GCtrl.CrtCmdMst("29998", "0", "5", strInStn, cCmdMode.S2S, cIoType.S2S, "", "", "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "wcs", "wcs", "dfd", strLocSize))
                            {
                                int[] lpData = new int[2];
                                lpData[0] = 29998;
                                lpData[1] = 1;
                                sRunInfo = string.Format("空栈板入库读码失败,下达站对站指令");
                                Log.WriteLog(sRunInfo); SetRunInfo(sRunInfo);
                                if (!PLC.WriteBlock(Plcs.FCJPLC, "D2070", 2, lpData))
                                    throw new Exception("空栈板入库读码失败，站对站写入目的站PLC失败！");
                            }
                            else throw new Exception(strBcr + "产生站对站命令失败！");
                            #region 原有程序备份
                            //if (GCtrl.CrtCmdMst(stnInf.CmdSno, "0", "5", strInStn, cCmdMode.In, cIoType.EptPltIn, "", "", "", stnInf.CmdSno, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "wcs", "wcs", "dfd", strLocSize))
                            //{
                            //    //写入目的站

                            //    int[] lpData = new int[1];
                            //    if (strBcr == "")
                            //    {
                            //        lpData[0] = stnInf.nCmdSno;
                            //    }
                            //    else
                            //    {
                            //        lpData[0] = Int32.Parse(strBcr.Substring(1));
                            //    }

                            //    if (!PLC.WriteBlock(Plcs.FCJPLC, "D2070", 1, lpData))
                            //        throw new Exception("空栈板入库，写入目的站PLC失败！");
                            //    lpData[0] = Int32.Parse(Globals.GetDestinateStn(strInStn));
                            //    sRunInfo = string.Format("空栈版入库:{0}，下达指令", strBcr);
                            //    Log.WriteLog(sRunInfo); SetRunInfo(sRunInfo);
                            //    if (!PLC.WriteBlock(Plcs.FCJPLC, "D2071", 1, lpData))
                            //        throw new Exception("空栈板入库，写入目的站PLC失败！");
                            //}
                            //else throw new Exception(strBcr + "产生入库命令失败！");
                            #endregion
                        }
                        else
                        {
                            if (GCtrl.CrtCmdMst(strBcr.Substring(1), "0", "5", strInStn, cCmdMode.In, cIoType.EptPltIn, "", "", "", strBcr.Substring(1), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "wcs", "wcs", "dfd", strLocSize))
                            {
                                //写入目的站

                                int[] lpData = new int[1];
                                if (strBcr == "")
                                {
                                    lpData[0] = stnInf.nCmdSno;
                                }
                                else
                                {
                                    lpData[0] = Int32.Parse(strBcr.Substring(1));
                                }

                                if (!PLC.WriteBlock(Plcs.FCJPLC, "D2070", 1, lpData))
                                    throw new Exception("空栈板入库，写入目的站PLC失败！");
                                lpData[0] = Int32.Parse(Globals.GetDestinateStn(strInStn));
                                sRunInfo = string.Format("空栈版入库:{0}，下达指令", strBcr);
                                Log.WriteLog(sRunInfo); SetRunInfo(sRunInfo);
                                if (!PLC.WriteBlock(Plcs.FCJPLC, "D2071", 1, lpData))
                                    throw new Exception("空栈板入库，写入目的站PLC失败！");
                            }
                            else throw new Exception(strBcr + "产生入库命令失败！");
                        }
                        
                    }
                    //else if (stnInf.nCmdSno == 29999 && strBcr.Length == 0)
                    //{
                    //    //空栈板入库读码失败，下达站对站命令
                    //    //strInStn = GetCmdStnNo("1",true, true, strLocSize);
                    //    //if (GCtrl.CrtCmdMst("29999", "0", "5", strInStn, cCmdMode.S2S, cIoType.S2S, "", "", "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "wcs", "wcs", "dfd", strLocSize))
                    //    //{
                    //    //    int[] lpData = new int[1];
                    //    //    lpData[0] = 1;
                    //    //    sRunInfo = string.Format("空栈板入库读码失败,下达站对站指令");
                    //    //    Log.WriteLog(sRunInfo); SetRunInfo(sRunInfo);
                    //    //    if (!PLC.WriteBlock(Plcs.FCJPLC, "D2071", 1, lpData))
                    //    //        throw new Exception("空栈板入库读码失败，站对站写入目的站PLC失败！");
                    //    //}
                    //    //else throw new Exception(strBcr + "产生站对站命令失败！");
                    //}
                    #endregion
                    #region 正常成品入库
                    else if (strBcr.Length > 0 && stnInf.CmdSno != "29999")
                    {
                        //读取到的条码信息与库存进行比对，若存在则设置为异常库位
                        DataTable dtTem = new DataTable();
                        strSql = string.Format("select * from loc_mst where PLT_ID='{0}'", strBcr.Substring(1));
                        Globals.DB.ExecGetTable(strSql, ref dtTem);
                        if (dtTem.Rows.Count > 0)
                        {
                            strSql = string.Format("update loc_mst set loc_sts = 'A' where PLT_ID = '{0}'", strBcr);
                            Globals.DB.ExecSql(strSql);
                            sRunInfo = (strBcr + "该栈板已在库存中，下达站对站命令");
                            SetRunInfo(sRunInfo);
                            Log.WriteLog(strBcr + "该栈板已在库存中，下达站对站命令");
                            //下达站对站命令
                            strInStn = GetCmdStnNo("1", false, true, strLocSize);
                            //20170603 by hupei
                            //if (GCtrl.CrtCmdMst(strBcr.Substring(1), "0", "5", strInStn, cCmdMode.S2S, cIoType.S2S, "", "", "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "wcs", "wcs", "dfd", strLocSize))
                            //{
                            //    int[] lpData = new int[1];
                            //    lpData[0] = 1;
                            //    if (!PLC.WriteBlock(Plcs.FCJPLC, "D2071", 1, lpData))
                            //        throw new Exception("该栈板已在库存中，站对站写入目的站PLC失败！");
                            //}
                            if (GCtrl.CrtCmdMst("29998", "0", "5", strInStn, cCmdMode.S2S, cIoType.S2S, "", "", "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "wcs", "wcs", "dfd", strLocSize))
                            {
                                int[] lpData = new int[2];
                                lpData[0] = 29998;
                                lpData[1] = 1;
                                sRunInfo = string.Format("该栈板已在库存中，站对站写入目的站PLC失败！");
                                Log.WriteLog(sRunInfo); SetRunInfo(sRunInfo);
                                if (!PLC.WriteBlock(Plcs.FCJPLC, "D2070", 2, lpData))
                                    throw new Exception("该栈板已在库存中，站对站写入目的站PLC失败！");
                                return;
                            }
                            else throw new Exception(strBcr + "产生站对站命令失败！");

                        }

                        //读取到的条码信息与palletInfo 进行比对
                        //// 20170511 by hupei
                        strSql = string.Format("select * from cmd_mst where cmd_sno = '{0}' and cmd_sts = '0'", strBcr.Substring(1));
                        Globals.DB.ExecGetTable(strSql, ref dtCmd);
                        if (dtCmd.Rows.Count <= 0) //查无命令数据，下达站对站命令
                        {
                            strInStn = GetCmdStnNo("1",false,true, strLocSize);
                            if (GCtrl.CrtCmdMst(strBcr.Substring(1), "0", "5", strInStn, cCmdMode.S2S, cIoType.S2S, "", "", "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "wcs", "wcs", "dfd", strLocSize))
                            {
                                int[] lpData = new int[1];
                                lpData[0] = 1;
                                if (!PLC.WriteBlock(Plcs.FCJPLC, "D2071", 1, lpData))
                                    throw new Exception("成品入库查无对应命令信息，站对站写入目的站PLC失败！");
                                sRunInfo = strBcr.Substring(1) + "查无命令数据，下达站对站命令";
                                SetRunInfo(sRunInfo); Log.WriteLog(sRunInfo);
                                return;
                            }
                            else throw new Exception(strBcr + "产生站对站命令失败！");
                        }
                        else //有绑定数据，正常入库
                        {
                            strInStn = GetCmdStnNo("1", false, false, strLocSize);
                            if (strInStn.Length == 0)
                            {
                                sRunInfo = string.Format("{0}:入库，查无可用站口", strBcr);
                                Log.WriteLog(sRunInfo); SetRunInfo(sRunInfo);
                                throw new Exception("入库，查无可用站口");
                            } 
                            strSql = string.Format("update cmd_mst set stn_no = '{0}',height = '{1}' where cmd_sno = '{2}'", strInStn,strLocSize, stnInf.CmdSno);
                            if (Globals.DB.ExecSql(strSql))
                            {
                                int[] lpData = new int[1];
                                lpData[0] = Int32.Parse(Globals.GetDestinateStn(strInStn));
                                if (!PLC.WriteBlock(Plcs.FCJPLC, "D2071", 1, lpData))
                                    throw new Exception("空栈板入库，写入目的站PLC失败！");
                            }
                            else throw new Exception(strBcr + "产生入库命令失败！");
                        }
                    }
                    else if(stnInf.CmdSno !="29999" && strBcr.Length == 0)
                    {
                        DataTable dtTemCmd = new DataTable();
                        strSql = string.Format("select * from cmd_mst where cmd_sno = '{0}' and cmd_sts = '0'", stnInf.CmdSno);
                        Globals.DB.ExecGetTable(strSql, ref dtTemCmd);
                        if (dtTemCmd.Rows.Count <= 0) //读码失败，且没有入库命令
                        {
                            strInStn = GetCmdStnNo("1", false, true, strLocSize);
                            if (GCtrl.CrtCmdMst(stnInf.CmdSno, "0", "5", strInStn, cCmdMode.S2S, cIoType.S2S, "", "", "", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "wcs", "wcs", "dfd", strLocSize))
                            {
                                int[] lpData = new int[1];
                                lpData[0] = 1;
                                if (!PLC.WriteBlock(Plcs.FCJPLC, "D2071", 1, lpData))
                                    throw new Exception("成品入库没有绑定子托盘号，站对站写入目的站PLC失败！");
                                sRunInfo = string.Format("{0}:成品入库没有绑定子托盘号,下达站对站指令", stnInf.CmdSno);
                                Log.WriteLog(sRunInfo); SetRunInfo(sRunInfo);
                            }
                            else throw new Exception(strBcr + "产生站对站命令失败！");
                        }
                        else
                        {
                            strInStn = GetCmdStnNo("1", false, false, strLocSize);
                            if (strInStn.Length == 0)
                            {
                                sRunInfo = string.Format("{0}:入库，查无可用站口", stnInf.CmdSno);
                                Log.WriteLog(sRunInfo); SetRunInfo(sRunInfo);
                                throw new Exception("入库，查无可用站口");
                            }                               
                            strSql = string.Format("update cmd_mst set stn_no = '{0}',height = '{1}' where cmd_sno = '{2}'", strInStn, strLocSize, stnInf.CmdSno);
                            if (Globals.DB.ExecSql(strSql))
                            {
                                int[] lpData = new int[1];
                                lpData[0] = Int32.Parse(Globals.GetDestinateStn(strInStn));
                                if (!PLC.WriteBlock(Plcs.FCJPLC, "D2071", 1, lpData))
                                    throw new Exception("空栈板入库，写入目的站PLC失败！");
                            }
                            else throw new Exception(strBcr + "产生入库命令失败！");
                        }
                        
                    }
                    #endregion
                    
                }                
            }
            catch (Exception ex)
            {
                Log.WriteLog("DoBcrInStn:" + ex.Message);
                SetRunInfo("DoBcrInStn:" + ex.Message);
            }
        }

        /// <summary>
        /// CRN入库口处理(下CRN入库命令)
        /// </summary>
        private void DoCrnInStn()
        {
            string sSql = string.Empty,sCrnStn = string.Empty;
            string sStnIdx = string.Empty, sEquNo = string.Empty;
            string sLoc = string.Empty, sNewLoc = string.Empty;
            string sCmdSno = string.Empty, sRunInfo = string.Empty;

            DataTable dtStn = new DataTable();
            DataRow drCmd = null;

            try
            {
                sSql = string.Format("select * from stn_inf where STN_MODE ='1' and can_use = 'Y'");
                Globals.DB.ExecGetTable(sSql, ref dtStn);
                foreach(DataRow drStn in dtStn.Rows)
                {
                    try
                    {
                        
                        sEquNo = drStn["Equ_No"].ToString();
                        sCrnStn = drStn["Stn_No"].ToString();
                        sStnIdx = drStn["Stn_Idx"].ToString();
                        StnInfo stnInf = (StnInfo)hFStnInfos[sCrnStn];
                        // 检查当前主机入库站口是否可用
                        if (!GCtrl.ChkCrnInStn(int.Parse(sEquNo), int.Parse(sStnIdx), sCrnStn, stnInf))
                            continue;
                        ////20170511 by hupei
                        if (!GCtrl.ChkCrnInStnForOnlyOne(int.Parse(sEquNo), int.Parse(sStnIdx), sCrnStn, stnInf))
                            continue;

                        // 自动、荷有、有资料下CRN命令
                        if (stnInf.bAuto && stnInf.bLoad && stnInf.nCmdSno > 0 && stnInf.nCmdMode == int.Parse(cCmdMode.In))
                        {
                            sCmdSno = stnInf.CmdSno;

                            // 查找是否存在符合条件的命令记录
                            sSql = string.Format(@"select * from Cmd_Mst where Cmd_Sno='{0}' and Cmd_Mode='{1}' and Cmd_Sts='{2}' ",
                                sCmdSno, cCmdMode.In, "0");
                            if (dbAsrs.ExecGetRow(sSql, ref drCmd) == false || drCmd == null)
                            {
                                //sRunInfo = string.Format("站口:{0} 命令:{1}入库,查无符合条件的命令记录!", sCrnStn, sCmdSno);
                                Log.WriteLog(cLog.RunLog, sRunInfo); SetRunInfo(sRunInfo);
                                continue;
                            }

                            if (drCmd["Loc"].ToString().Length != 7)
                            {
                                // 找可用空储位
                                if (drCmd["Io_Type"].ToString() == cIoType.EptPltIn)

                                    sLoc = GCtrl.GetNewInLoc(int.Parse(sEquNo), drCmd["Height"], cItmType.EptPlt, drCmd["Plt_Id"], out sRunInfo);
                                else
                                    sLoc = GCtrl.GetNewInLoc(int.Parse(sEquNo), drCmd["Height"], cItmType.Stock, drCmd["Plt_id"], out sRunInfo);

                                if (sLoc.Length <=0)
                                {
                                    sRunInfo = string.Format("站口:{0} 托盘:{1} {2}", sCrnStn, drCmd["Plt_ID"], sRunInfo);
                                    Log.WriteLog(cLog.RunLog, sRunInfo); SetRunInfo(sRunInfo);
                                    continue;
                                }

                                sRunInfo = string.Format("站口:{0} 托盘:{1} {2}:{3}", sCrnStn, drCmd["Plt_id"], sRunInfo, sLoc);
                                Log.WriteLog(cLog.RunLog, sRunInfo); SetRunInfo(sRunInfo);

                                dbAsrs.BeginTran(true);
                                // =========================================================================================================================
                                // 更新储位状态
                                sSql = string.Format(@"update Loc_Mst set old_sts=Loc_Sts, Loc_Sts='{0}', Trn_Time='{1}',plt_id ='{4}' where Loc='{2}'" +
                                    " and Loc_Sts='{3}'", eLocSts.I, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),  sLoc, eLocSts.N,stnInf.CmdSno);
                                if (!dbAsrs.ExecSql(sSql))
                                    throw new Exception(dbAsrs.ErrorInfo);
                                Log.WriteLog(cLog.RunLog, "更新库位状态为预约" + sSql);
                                // 下主机入库命令
                                if (!GCtrl.CrtEquCmd(sCmdSno, cCmdMode.In, sEquNo, "2", sLoc, drCmd["Prty"]))
                                    throw new Exception(dbAsrs.ErrorInfo);
                                Log.WriteLog(cLog.RunLog, "下主机命令成功：" + sCmdSno);
                                // 更新命令流程码
                                sSql = string.Format(@"update Cmd_Mst set CMD_STS = '1', Exp_DAte='{0}',loc = '{1}' where Cmd_Sno='{2}' ",
                                     System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),sLoc, sCmdSno);
                                if (!dbAsrs.ExecSql(sSql))
                                    throw new Exception(dbAsrs.ErrorInfo);
                                Log.WriteLog(cLog.RunLog, "更新命令状态" + sSql);
                                sRunInfo = string.Format("站口:{0} 托盘:{1} 下主机入库命令成功,命令号:{2}", sCrnStn, drCmd["Plt_id"], sCmdSno);
                                Log.WriteLog(cLog.RunLog, sRunInfo); SetRunInfo(sRunInfo);
                                // =========================================================================================================================
                                dbAsrs.CommitTran();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        dbAsrs.RollbackTran();
                        Log.WriteLog(cLog.RunLog, ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex); 
            }
        }

        
        /// <summary>
        /// CRN出库口处理(写资料)
        /// </summary>
        private void ChkCrnOutStn()
        {
            string sSql = string.Empty, sDesArea = string.Empty;
            string sCmdSno = string.Empty, sLoc = string.Empty; 
            string sCrnStn = string.Empty, sCmdStn = string.Empty;
            string sEquNo = string.Empty, sCrnStnIdx = string.Empty;
            string sStnArea = string.Empty, sRunInfo = string.Empty;

            DataTable dtStn = new DataTable();
            DataTable dtCmd = new DataTable();

            try
            {
                sSql = string.Format(@"select * from Stn_Inf where Area_No in('{0}') and Equipment='{1}' and Can_Use='Y'",
                    "A", cEquType.Crane);
                dbAsrs.ExecGetTable(sSql, ref dtStn);

                foreach (DataRow drStn in dtStn.Rows)
                {
                    try
                    {
                        sEquNo = drStn["Equ_No"].ToString();
                        sCrnStn = drStn["Stn_No"].ToString();
                        sCrnStnIdx = drStn["Stn_Idx"].ToString();
                        sStnArea = drStn["Area_No"].ToString();
                        StnInfo stnInf = (StnInfo)hFStnInfos[sCrnStn];

                        //20170808 by hupei
                        sSql = string.Format("select  * from  CMD_MST where  Cmd_Mode = '5' and Cmd_Sts in('0','1','3') and Loc in (select loc from  LOC_MST where Row_X>=({0}-1)*4+1 and Row_X <={1}*4)", int.Parse(sEquNo), int.Parse(sEquNo));
                        DataTable dtCmdS2SCmd = new DataTable();
                        Globals.DB.ExecGetTable(sSql, ref dtCmdS2SCmd);

                        // 出库口自动、荷无、无资料
                        if (stnInf.bAuto && stnInf.bLoad == false && stnInf.nCmdSno < 1 && stnInf.nCmdMode < 1)
                        {
                            // 查找符合条件的出库命令
                            sSql = string.Format(@"select * from Cmd_Mst where Cmd_Mode='{0}' and Cmd_Sts in('{1}','{2}') order by Prty, Crt_Date",
                                cCmdMode.Out, cCmdSts.Wait,"1");
                            dbAsrs.ExecGetTable(sSql, ref dtCmd);

                            foreach (DataRow drCmd in dtCmd.Rows)
                            {
                                sCmdSno = drCmd["Cmd_Sno"].ToString();
                                sCmdStn = drCmd["Stn_No"].ToString();
                                sLoc = drCmd["Loc"].ToString();
                                int nCrn = GCtrl.GetEquFLoc(sLoc);

                                // 判断是否是同一巷道
                                if (nCrn != int.Parse(sEquNo)) continue;
                                //20170808 by hupei
                                sSql = string.Format("select  * from  CMD_MST where  Cmd_Mode = '5' and Cmd_Sts in('0','1','3') and Loc in (select loc from  LOC_MST where Row_X>=({0}-1)*4+1 and Row_X <={1}*4)", int.Parse(sEquNo), int.Parse(sEquNo));
                                DataTable dtCmdS2SCmd1 = new DataTable();
                                Globals.DB.ExecGetTable(sSql, ref dtCmdS2SCmd1);
                                // 判断是否具备出库条件（如果是外储位且内储位有货物要下库对库命令）
                                if (dtCmdS2SCmd.Rows.Count > 0) continue;
                                if (!GCtrl.GetReadyOut(sLoc)) continue;

                                //20180113 by hupei
                                DataTable dtEqucmd = new DataTable();
                                sSql = string.Format("select * from equcmd where cmdsno = '{0}'",sCmdSno);
                                Globals.DB.ExecGetTable(sSql, ref dtEqucmd);
                                if (dtEqucmd.Rows.Count > 0) continue;
                                    
                                dbAsrs.BeginTran(true);
                                // ==========================================================================================================
                                // 更新命令流程码
                                sSql = string.Format(@"update Cmd_Mst set Cmd_Sts='{0}', Trace='{1}' where Cmd_Sno='{2}' ",
                                    cCmdSts.Proc, cTrace.O21, sCmdSno);
                                if (drCmd["cmd_sts"].ToString() == "0")
                                {
                                    if (!dbAsrs.ExecSql(sSql))
                                    {
                                        throw new Exception("更新命令流程码失败！" + sSql);
                                    }
                                }
                                else
                                {
                                    Log.WriteLog(cLog.RunLog, sCmdSno + ":命令状态为1，继续出库！");
                                }
                                // 写PLC
                                int nDes = 0;
                                string strMode = string.Empty;
                                string strCmd = string.Empty;
                                if (drCmd["io_type"].ToString() == "25")
                                {
                                    strMode ="2";
                                    nDes = 7;
                                    strCmd ="29999";

                                }
                                else
                                {
                                    nDes =Int32.Parse(drCmd["stn_no"].ToString().Substring(1, 1));
                                    strMode = cCmdMode.Out;
                                    strCmd = drCmd["cmd_sno"].ToString();
                                }
                                if (!WritePlc(sCrnStn, strCmd, cCmdMode.Out, nDes))
                                    throw new Exception(string.Format("站口:{0} 命令:{1} 出库,写入PLC失败!", sCrnStn, sCmdSno));
                                else
                                {
                                    Log.WriteLog(cLog.RunLog, sCrnStn + "--" + strCmd +"--"+ "2"+"--"+ nDes.ToString());
                                }
                                // ==========================================================================================================
                                dbAsrs.CommitTran();
                                break;
                                }
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        dbAsrs.RollbackTran();
                        Log.WriteLog(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
            }
        }

        /// <summary>
        /// CRN出库口处理(下CRN出库命令)
        /// </summary>
        private void DoCrnOutStn()
        {
            string sSql = string.Empty, sCrnStn = string.Empty;
            string sEquNo = string.Empty, sCrnStnIdx = string.Empty;
            string sRunInfo = string.Empty;

            DataTable dtStn = new DataTable();
            DataRow drCmd = null;

            try
            {
                sSql = string.Format(@"select * from Stn_Inf where Area_No in('A') and Equipment='{0}' and Can_Use='Y'",
                    cEquType.Crane);
                dbAsrs.ExecGetTable(sSql, ref dtStn);

                foreach (DataRow drStn in dtStn.Rows)
                {
                    try
                    {
                        sEquNo = drStn["Equ_No"].ToString();
                        sCrnStn = drStn["Stn_No"].ToString();
                        sCrnStnIdx = drStn["Stn_Idx"].ToString();
                        StnInfo stnInf = (StnInfo)hFStnInfos[sCrnStn];

                        // 检查当前主机出库站口是否可用
                        if (!GCtrl.ChkCrnOutStn(int.Parse(sEquNo), int.Parse(sCrnStnIdx), sCrnStn, stnInf))
                            continue;
                        //// 20170511 by hupei
                        if (!GCtrl.ChkCrnOutStnForOnlyOne(int.Parse(sEquNo), int.Parse(sCrnStnIdx), sCrnStn, stnInf))
                            continue;
                        
                        // 出库口自动、荷无、有资料
                        if (stnInf.bAuto && stnInf.bLoad == false && stnInf.nCmdSno > 0 && stnInf.nCmdMode > 0)
                        {
                            // 查找是否存在符合条件的命令记录
                            sSql = string.Format(@"select * from Cmd_Mst where Cmd_Sno='{0}' and Cmd_Mode='{1}' and Cmd_Sts='{2}' and io_type !='25' ",
                                stnInf.CmdSno, cCmdMode.Out, cCmdSts.Proc);
                            if (dbAsrs.ExecGetRow(sSql, ref drCmd) == false || drCmd == null)
                            {
                                sRunInfo = string.Format("站口:{0} 命令:{1}出库,查无符合条件的命令记录!", sCrnStn, stnInf.CmdSno);
                                Log.WriteLog(cLog.RunLog, sRunInfo); SetRunInfo(sRunInfo);
                                continue;
                            }

                            dbAsrs.BeginTran(true);
                            // ==========================================================================================================================
                            // 下主机出库命令
                            if (!GCtrl.CrtEquCmd(drCmd["Cmd_Sno"], cCmdMode.Out, sEquNo, drCmd["Loc"], sCrnStnIdx, drCmd["Prty"]))
                                throw new Exception(dbAsrs.ErrorInfo);

                            // 更新命令流程码
                            sSql = string.Format(@"update Cmd_Mst set Exp_DAte='{0}' where Cmd_Sno='{1}' ",
                                System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), drCmd["Cmd_Sno"]);
                            if (!dbAsrs.ExecSql(sSql))
                                throw new Exception(dbAsrs.ErrorInfo);

                            sRunInfo = string.Format("站口:{0} 托盘:{1} 下主机出库命令成功,命令号:{2}", sCrnStn, drCmd["Plt_id"], drCmd["Cmd_Sno"]);
                            Log.WriteLog(cLog.RunLog, sRunInfo); SetRunInfo(sRunInfo);
                            // ==========================================================================================================================
                            dbAsrs.CommitTran();
                        }
                    }
                    catch (Exception ex)
                    {
                        dbAsrs.RollbackTran();
                        Log.WriteLog(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
            }

        }

        private void DoEmptyCrnOutStn()
        {
            string sSql = string.Empty, sCrnStn = string.Empty;
            string sEquNo = string.Empty, sCrnStnIdx = string.Empty;
            string sRunInfo = string.Empty;

            DataTable dtStn = new DataTable();
            DataRow drCmd = null;

            try
            {
                sSql = string.Format(@"select * from Stn_Inf where Area_No in('A') and Equipment='{0}' and Can_Use='Y'",
                    cEquType.Crane);
                dbAsrs.ExecGetTable(sSql, ref dtStn);

                foreach (DataRow drStn in dtStn.Rows)
                {
                    try
                    {
                        sEquNo = drStn["Equ_No"].ToString();
                        sCrnStn = drStn["Stn_No"].ToString();
                        sCrnStnIdx = drStn["Stn_Idx"].ToString();
                        StnInfo stnInf = (StnInfo)hFStnInfos[sCrnStn];

                        // 检查当前主机出库站口是否可用
                        if (!GCtrl.ChkCrnOutStn(int.Parse(sEquNo), int.Parse(sCrnStnIdx), sCrnStn, stnInf))
                            continue;

                        // 出库口自动、荷无、有资料
                        if (stnInf.bAuto && stnInf.bLoad == false && stnInf.nCmdSno > 0 && stnInf.nCmdMode > 0)
                        {
                            // 查找是否存在符合条件的命令记录
                            sSql = string.Format(@"select * from Cmd_Mst where  Cmd_Mode='{0}' and io_type = '25' and Cmd_Sts='{1}' and stn_no = '{2}' ",
                                 cCmdMode.Out, cCmdSts.Proc,drStn["stn_no"]);
                            if (dbAsrs.ExecGetRow(sSql, ref drCmd) == false || drCmd == null)
                            {
                                sRunInfo = string.Format("站口:{0} 命令:{1}出库,查无符合条件的命令记录!", sCrnStn, stnInf.CmdSno);
                                Log.WriteLog(cLog.RunLog, sRunInfo); SetRunInfo(sRunInfo);
                                continue;
                            }

                            dbAsrs.BeginTran(true);
                            // ==========================================================================================================================
                            // 下主机出库命令
                            if (!GCtrl.CrtEquCmd(drCmd["Cmd_Sno"], cCmdMode.Out, sEquNo, drCmd["Loc"], sCrnStnIdx, drCmd["Prty"]))
                                throw new Exception(dbAsrs.ErrorInfo);

                            // 更新命令流程码
                            sSql = string.Format(@"update Cmd_Mst set Exp_DAte='{0}' where Cmd_Sno='{1}' ",
                                System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), drCmd["Cmd_Sno"]);
                            if (!dbAsrs.ExecSql(sSql))
                                throw new Exception(dbAsrs.ErrorInfo);

                            sRunInfo = string.Format("站口:{0} 托盘:{1} 下主机出库命令成功,命令号:{2}", sCrnStn, drCmd["Plt_id"], drCmd["Cmd_Sno"]);
                            Log.WriteLog(cLog.RunLog, sRunInfo); SetRunInfo(sRunInfo);
                            // ==========================================================================================================================
                            dbAsrs.CommitTran();
                        }
                    }
                    catch (Exception ex)
                    {
                        dbAsrs.RollbackTran();
                        Log.WriteLog(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
            }

        }


        ///<summary>
        ///站对站写资料
        ///</summary>
        private void ChkS2SStn()
        {
            string strSql = string.Empty;
            string sRunInfo = string.Empty;
            DataTable dtStn = new DataTable();
            DataTable dtCmd = new DataTable();
            try
            {
                strSql = string.Format("select  * from STN_INF where Area_No  = 'B' and STN_MODE = '1' and Can_Use = 'Y'");
                Globals.DB.ExecGetTable(strSql, ref dtStn);
                foreach (DataRow ds in dtStn.Rows)
                {
                    StnInfo stnInf = (StnInfo)hFStnInfos[ds["stn_no"]];
                    if (stnInf.bAuto && stnInf.bLoad && stnInf.nCmdMode == 1 && stnInf.nCmdSno > 0)
                    {
                        strSql = string.Format("select * from cmd_mst where cmd_sts = '0' and cmd_mode = '4' and cmd_sno = '{0}'", stnInf.CmdSno);
                        Globals.DB.ExecGetTable(strSql, ref dtCmd);
                        if (dtCmd.Rows.Count <= 0)
                        {
                            continue;
                        }
                        else
                        {
                            foreach(DataRow dc in dtCmd.Rows)
                            {
                                string strDesStn = GetS2SStnNo(ds["stn_no"].ToString());
                            StnInfo stnInfDes = (StnInfo)hFStnInfos[strDesStn];
                            if(stnInfDes.bAuto&& stnInfDes.bLoad == false && stnInfDes.bError ==false && stnInfDes.nCmdSno < 1 && stnInfDes.nCmdMode<1)
                            {
                                Globals.DB.BeginTran(true);
                                strSql = string.Format("update cmd_mst set cmd_sts = '1' where cmd_sno = '{0}' and cmd_sts = '0'", dc["cmd_sno"]);
                                if (!Globals.DB.ExecSql(strSql))
                                {
                                    throw new Exception(dc["cmd_sn"].ToString() + "更新命令状态信息错误！");
                                }
                                if (!WritePlc(strDesStn, dc["cmd_sno"], "4", 6))
                                    throw new Exception(string.Format("站口:{0} 命令:{1} 出库,写入PLC失败!", strDesStn, dc["cmd_sno"]));
                                sRunInfo = string.Format("站对站写入PLC命令信息：{0}  {1}  {2}",dc["cmd_sno"],strDesStn,"6");
                                Log.WriteLog(sRunInfo); SetRunInfo(sRunInfo);
                                Globals.DB.CommitTran();
                                
                            }
                            }
                            
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex.Message);
                Globals.DB.RollbackTran();
            }
        }

        /// <summary>
        /// 库对库处理(下CRN库对库命令)
        /// </summary>
        private void DoWithL2L()
        {
            string sSql = string.Empty, sLoc = string.Empty;
            string sRunInfo = string.Empty;
            DataTable dtCmd = new DataTable();
            int nCrn = 0;

            try
            {
                // 查找符合条件的库对库命令记录
                sSql = string.Format(@"select * from Cmd_Mst where Cmd_Mode='{0}' and Cmd_Sts='{1}' and Len(New_Loc)=7 " +
                    " order by Prty,Crt_Date", cCmdMode.L2L, cCmdSts.Wait);
                dbAsrs.ExecGetTable(sSql, ref dtCmd);

                foreach (DataRow drCmd in dtCmd.Rows)
                {
                    try
                    {
                        sLoc = drCmd["Loc"].ToString();
                        nCrn = GCtrl.GetEquFLoc(sLoc);

                        // 判断是否满足库对库条件
                        if (!GCtrl.GetReadyOut(sLoc)) continue;

                        // 检查是否已经下过库对库命令
                        sSql = string.Format(@"select CmdSno from EquCmd where CmdSno='{0}'", drCmd["Cmd_Sno"]);
                        if (dbAsrs.ExecQuery(sSql)) return;

                        dbAsrs.BeginTran(true);
                        // ============================================================================================================================
                        // 下主机库对库命令
                        if (!GCtrl.CrtEquCmd(drCmd["Cmd_Sno"], cCmdMode.L2L, nCrn, drCmd["Loc"], drCmd["New_Loc"], drCmd["prty"]))
                            throw new Exception(dbAsrs.ErrorInfo);
                        // 更新命令状态信息
                        sSql = string.Format("update cmd_mst set cmd_sts = '1',exp_date = '{0}' where cmd_sno = '{1}' and cmd_sts = '0'", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), drCmd["cmd_sno"]);
                        if (!dbAsrs.ExecSql(sSql))
                            throw new Exception(dbAsrs.ErrorInfo);

                        sRunInfo = string.Format("储位:{0} 托盘:{1} 下主机库对库命令成功,命令号:{2}", sLoc, drCmd["Plt_id"], drCmd["Cmd_Sno"]);
                        Log.WriteLog(cLog.RunLog, sRunInfo); SetRunInfo(sRunInfo);
                        // ============================================================================================================================
                        dbAsrs.CommitTran();
                    }
                    catch (Exception ex)
                    {
                        dbAsrs.RollbackTran();
                        Log.WriteLog(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
            }
        }

        ///<summary>
        ///站对站命令
        ///</summary>
        private void DoWithS2S()
        {
            string strSql = string.Empty;
            DataTable dtCmd = new DataTable();
            DataTable dtStn = new DataTable();
            
            try
            {
                strSql = string.Format("select  * from STN_INF where Area_No  = 'B' and STN_MODE = '1' and Can_Use = 'Y'");
                Globals.DB.ExecGetTable(strSql, ref dtStn);
                foreach (DataRow dr in dtStn.Rows)
                {
                    strSql = string.Format("select  * from CMD_MST where  CMD_MODE = '4' and CMD_STS = '1'  and stn_no = '{0}' ", dr["stn_no"]);
                    Globals.DB.ExecGetTable(strSql, ref dtCmd);
                    foreach (DataRow dw in dtCmd.Rows)
                    {
                        StnInfo stnInf = (StnInfo)hFStnInfos[dr["stn_no"]];
                        if (stnInf.bAuto && stnInf.bLoad && stnInf.nCmdSno > 1 && stnInf.nCmdMode == 1)
                        {
                            string strDesStn = GetS2SStnNo(dr["stn_no"].ToString());
                            StnInfo stnInfDes = (StnInfo)hFStnInfos[strDesStn];
                            if (stnInfDes.bAuto && stnInfDes.bLoad == false && stnInfDes.nCmdMode == 4 && stnInfDes.CmdSno == dw["cmd_sno"].ToString())
                            {
                                Globals.DB.BeginTran(true);
                                //该站口存在站对站命令，创建主机命令
                                if (!GCtrl.CrtEquCmd(dw["cmd_sno"], "4", dr["equ_no"], "2", "1", "1"))
                                    throw new Exception(dr["stn_no"].ToString() + "创建站对站命令失败！");
                                strSql = string.Format("update cmd_mst set cmd_sts = '{0}' where cmd_sno = '{1}'", cCmdSts.Proc, dw["cmd_sno"]);
                                if (!Globals.DB.ExecSql(strSql))
                                    throw new Exception(dw["cmd_sno"] + " 更新命令状态失败！");
                                Globals.DB.CommitTran();
                            }
                           
                        }
                       
                    }
                }
            }
            catch (Exception ex)
            {
                Globals.DB.RollbackTran();
                Log.WriteLog(ex.Message);
                return;
            }
        }

        /// <summary>
        /// 空栈板出库
        /// </summary>
        private void GetEmpetPallet()
        {
            bool isAsking = false; int nAnswer = 0;
            string strSql = string.Empty;
            DataTable dtLoc = new DataTable();
            DataTable dtCmd = new DataTable();
            DataTable dtStn = new DataTable();
            string sAddrAsk = "D1909";//空托盘请求地址；
            string sAddrAnswer = "D1904"; 
            try
            {
                int[] lpdate = new int[10];
                int nRead = PLC.ReadPlc(Plcs.FCJPLC, sAddrAsk, 10, out lpdate[0]);
                int nStatus = lpdate[0];                               //设备状态
                isAsking = GCtrl.BitChk(nStatus, 0);
                string strDTNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                if (isAsking) //存在空栈板出库请求
                {
                   nAnswer= PLC.ReadDevice(Plcs.FCJPLC, sAddrAnswer);
                   if (nAnswer == 0)
                   {
                       //判断是否有正在出库的空栈板
                       strSql = string.Format("select * from cmd_mst where cmd_mode = '2' and io_type = '25' and cmd_sts in ('0','1')");
                       Globals.DB.ExecGetTable(strSql, ref dtCmd);
                       if (dtCmd.Rows.Count > 0) return;
                       //查找满足出库条件的站口信息，并下达出库命令
                       strSql = string.Format("select * from stn_inf where stn_mode = '2' and can_use = 'Y' and stn_no !='B123'");
                       Globals.DB.ExecGetTable(strSql, ref dtStn);
                       foreach (DataRow drStn in dtStn.Rows)
                       {
                           StnInfo stnInf = (StnInfo)hFStnInfos[drStn["stn_no"].ToString()];
                           if (stnInf.bAuto && stnInf.bLoad == false && stnInf.nCmdSno < 1)
                           {
                               int nRow = int.Parse(Globals.GetDestinateStn(drStn["stn_no"].ToString()));
                               strSql = string.Format("select * from loc_mst where loc_sts = 'E' and crn_no = {0}", nRow);
                               Globals.DB.ExecGetTable(strSql, ref dtLoc);
                               foreach (DataRow drLoc in dtLoc.Rows)
                               {
                                   //创建出库命令
                                   strSql = string.Format("update loc_mst set old_sts = loc_sts ,loc_sts = 'O' where loc = '{0}'",drLoc["Loc"]);
                                   Globals.DB.ExecSql(strSql);
                                   bool bCrt = GCtrl.CrtCmdMst(drLoc["Plt_id"], "0", "2", drStn["Stn_no"], "2", "25", drLoc["Loc"], "", "", drLoc["Plt_id"], strDTNow, "WCS", "WCS", "", drLoc["Loc_size"]);
                                   int[] lpData = new int[1];
                                   lpData[0] = 1;
                                   PLC.WriteBlock(Plcs.FCJPLC, sAddrAnswer, 1, lpData);
                                   if (bCrt) return;
                               }
                                   
                           }
                       }
                   }
                }
            }
            catch (Exception ex)
            {
 
            }
                
        }

        private void DoWithCycleOut()
        {
            string strSql = string.Empty;
            string strEquNo = string.Empty; 
            DataTable dtStn = new DataTable();
            DataTable dtCmd = new DataTable();
            try
            {
                strSql = string.Format("select * from cmd_mst where cmd_mode = '3' and io_type = '32' and cmd_sts = '0'");
                Globals.DB.ExecGetTable(strSql,ref dtCmd);
                foreach (DataRow dr in dtCmd.Rows)
                {
                    StnInfo stnInf = (StnInfo)hFStnInfos[dr["stn_no"].ToString()];
                    string sLoc = dr["Loc"].ToString();
                    if (stnInf.bAuto && stnInf.bLoad == false)
                    {
                        if (!GCtrl.GetReadyOut(sLoc)) continue;
                        strEquNo = Globals.GetDestinateStn(dr["stn_no"].ToString());

                        if(!GCtrl.ChkCrnOutStnForOnlyOneForCycle(int.Parse(strEquNo),3, dr["stn_no"].ToString(), stnInf)) continue;   
                        GCtrl.CrtEquCmd(dr["cmd_sno"], "2", strEquNo, dr["Loc"], "3", "5");
                       // 更新命令流程码
                       strSql = string.Format(@"update Cmd_Mst set Exp_DAte='{0}',cmd_sts = '1' where Cmd_Sno='{1}' ",
                             System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), dr["Cmd_Sno"]);
                      if (!dbAsrs.ExecSql(strSql))
                           throw new Exception(dbAsrs.ErrorInfo);
                    }
                }
            }
            catch (Exception ex)
            { 
            }
        }

        private void DoWithCycleIn()
        {
            string strSql = string.Empty;
            string strEqu = string.Empty;
            string sLoc = string.Empty;
            string sRunInfo = string.Empty;
            DataTable dtCmd = new DataTable();
            try
            {
                strSql = string.Format("select * from cmd_mst where cmd_sts = '0' and cmd_mode = '3' and io_type = '31'");
                Globals.DB.ExecGetTable(strSql, ref dtCmd);
                foreach (DataRow dr in dtCmd.Rows)
                {
                    StnInfo stnInf = (StnInfo)hFStnInfos[dr["stn_no"].ToString()];
                    if (stnInf.bLoad && stnInf.bAuto)
                    {
                        strEqu = Globals.GetDestinateStn(dr["stn_no"].ToString());
                        if (dr["Loc"].ToString().Length != 6)
                        {
                            // 找可用空储位
                            if (dr["Io_Type"].ToString() == cIoType.EptPltIn)

                                sLoc = GCtrl.GetNewInLoc(int.Parse(strEqu), dr["Height"], cItmType.EptPlt, dr["Plt_Id"], out sRunInfo);
                            else
                                sLoc = GCtrl.GetNewInLoc(int.Parse(strEqu), dr["Height"], cItmType.Stock, dr["Plt_id"], out sRunInfo);

                            if (sLoc.Length <= 0)
                            {
                                sRunInfo = string.Format("站口:{0} 托盘:{1} {2}", dr["stn_no"], dr["Plt_ID"], sRunInfo);
                                Log.WriteLog(cLog.RunLog, sRunInfo); SetRunInfo(sRunInfo);
                                continue;
                            }
                        }
                        dbAsrs.BeginTran(true);
                        // =========================================================================================================================
                        // 更新储位状态
                        strSql = string.Format(@"update Loc_Mst set old_sts=Loc_Sts, Loc_Sts='{0}', Trn_Time='{1}',plt_id ='{4}' where Loc='{2}'" +
                            " and Loc_Sts='{3}'", eLocSts.I, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), sLoc, eLocSts.N, stnInf.CmdSno);
                        if (!dbAsrs.ExecSql(strSql))
                            throw new Exception(dbAsrs.ErrorInfo);
                        Log.WriteLog("更新库位状态为预约"+ strSql);
                        // 下主机入库命令
                        if (!GCtrl.CrtEquCmd(dr["cmd_sno"], cCmdMode.In, strEqu, "3", sLoc, dr["Prty"]))
                            throw new Exception(dbAsrs.ErrorInfo);

                        // 更新命令流程码
                        strSql = string.Format(@"update Cmd_Mst set CMD_STS = '1', Exp_DAte='{0}',loc = '{1}' where Cmd_Sno='{2}' ",
                             System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), sLoc, dr["cmd_sno"]);
                        if (!dbAsrs.ExecSql(strSql))
                            throw new Exception(dbAsrs.ErrorInfo);

                        sRunInfo = string.Format("站口:{0} 托盘:{1} 下主机入库命令成功,命令号:{2}", dr["stn_no"], dr["Plt_id"], dr["cmd_sno"]);
                        Log.WriteLog(cLog.RunLog, sRunInfo); SetRunInfo(sRunInfo);
                        // =========================================================================================================================
                        dbAsrs.CommitTran();

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        
        ///<summary>
        ///读取条码信息
        ///</summary>
        private string ReadBcr()
        {
            string strBcr = string.Empty;
            try
            {
                if (hPort1.BytesToRead > 1)
                {
                    strBcr = hPort1.ReadExisting();
                    if (strBcr.Length != 6) throw new Exception("读取条码信息失败！");
                }
                return strBcr;
            }
            catch(Exception ex)
            {
                Log.WriteLog(ex.Message);
                return "";
            }
            finally
            {
                hPort1.DiscardInBuffer();
            }
        }

        private string GetInStnNo(string strCmdMode,string strBcr)
        {
            DataTable dtStn = new DataTable();
            string strSql = string.Empty;
            string strStnRuslt = string.Empty;
            try
            {
                strSql = string.Format("select stn_no from STN_INF where stn_mode = '1' and CAN_USE = 'Y'");
                if (strCmdMode == "4") strSql += "and STN_NO not in ('B123','B164') order by stn_no desc"; //空托盘入库，优先入较远的巷道，且不如1、5号线
                Globals.DB.ExecGetTable(strSql, ref dtStn);
                foreach (DataRow dr in dtStn.Rows)
                {
                    StnInfo stnInf = (StnInfo)hFStnInfos[dr["stn_no"].ToString()];
                    if (stnInf.bAuto && stnInf.bLoad && stnInf.nCmdSno < 1)
                    {
                        //存在满足条件的站口，更新站口状态
                        strSql = string.Format("update stn_inf  set stn_sts = '1' where stn_no = '{0}'", dr["stn_no"]);
                        Globals.DB.ExecSql(strSql);
                        strStnRuslt = dr["stn_no"].ToString();
                        break;
                    }
                }
                return strStnRuslt;

            }
            catch (Exception ex)
            {
                Log.WriteLog("获取站口信息：" + ex.Message);
                return strStnRuslt;
            }
           
        }

        ///<summary>
        /// 1、查找所有可用站口号
        /// 2、判断是否为模式信息
        /// 3、区分是否为空展板信息
        /// 4、区分站对站
        /// 5、判断该站口是否有满足条件的库位信息
        ///</summary>
        private  string GetCmdStnNo(string sCmdMode, bool isEPallet, bool isS2S, string sLocSize)
        {
            int nRow = 0;
            string strSql = string.Empty;
            string strResult = string.Empty;
            DataTable dtStnInfo = new DataTable();
            bool isOK = false;
            if (isS2S)
            {
                strResult = "B123";
            }
            else
            {
                strSql = string.Format("select * from stn_inf where stn_mode = '{0}' and can_use = 'Y'", sCmdMode);
                if (isEPallet)
                {
                    int nTem = 0;
                    while (true)
                    {
                        string strDes = string.Empty;
                        switch (nCranForE)
                        { 
                            case 2:
                                strDes = "B134";
                                nTem++;
                                break;
                            case 3:
                                 strDes = "B144";
                                 nTem++;
                                break;
                            case 4:
                                 strDes = "B154";
                                 nTem++;
                                break;
                            case 5:
                                 strDes = "B164";
                                 nTem++;
                                break;
                            default:
                                nTem++;
                                break;
                        }
                        StnInfo stnInf2 = (StnInfo)hFStnInfos[strDes];
                        if (stnInf2.bAuto )
                        {
                            strSql += string.Format(" and STN_NO not in ('B123','B164')");
                            strSql += string.Format(" and equ_no = '{0}'", nCranForE.ToString());
                            isOK = true;
                            if (nCranForE > 2) nCranForE = nCranForE - 1;
                            else if (nCranForE == 2) nCranForE = 4;
                            break;
                        }
                        if (nTem > 50)
                        {
                           if (nCranForE > 2) nCranForE = nCranForE - 1;
                           else if (nCranForE == 2) nCranForE = 4;
                           break;
                        }
                        if (nCranForE > 2) nCranForE = nCranForE - 1;
                        else if (nCranForE == 2) nCranForE = 4;      
                    }
                    if (isOK == false) return string.Empty;
                   
                }
                else
                {
                    int nTem = 0;
                    while (true)
                    {
                        string strDes = string.Empty;
                        switch (nCranForP)
                        {
                            case 1:
                                strDes = "B123";
                                nTem++;
                                break;
                            case 2:
                                strDes = "B134";
                                nTem++;
                                break;
                            case 3:
                                strDes = "B144";
                                nTem++;
                                break;
                            case 4:
                                strDes = "B154";
                                nTem++;
                                break;
                            case 5:
                                strDes = "B164";
                                nTem++;
                                break;
                            default:
                                nTem++;
                                break;
                        }
                        StnInfo stnInf2 = (StnInfo)hFStnInfos[strDes];
                        if (stnInf2.bAuto)
                        {
                            strSql += string.Format(" and equ_no = '{0}'", nCranForP.ToString());
                            isOK = true;
                            if (nCranForP > 1) nCranForP = nCranForP - 1;
                            else if (nCranForP == 1) nCranForP = 5;
                            break;
                        }
                        if (nTem > 50)
                        {
                            if (nCranForP > 1) nCranForP = nCranForP - 1;
                            else if (nCranForP == 1) nCranForP = 5;
                            break;
                        }
                        if (nCranForP > 1) nCranForP = nCranForP - 1;
                        else if (nCranForP == 1) nCranForP = 5;
                    }
                    if (isOK == false) return string.Empty;
                   
                }
                Globals.DB.ExecGetTable(strSql, ref dtStnInfo);
                foreach (DataRow dr in dtStnInfo.Rows)
                {
                    StnInfo stnInf = (StnInfo)hFStnInfos[dr["stn_no"].ToString()];
                    StnInfo stnInf1 = (StnInfo)hFStnInfos["B121"];
                    string strSno = GetPreStnNo(dr["stn_no"].ToString());
                    StnInfo stnInf2 = (StnInfo)hFStnInfos[strSno];
                    //if (stnInf.bAuto && stnInf1.bAuto&& stnInf2.bLoad==false &&stnInf2.bAuto&& stnInf1.bLoad==false&&stnInf1.nCmdSno<1)
                    //if (stnInf.bAuto && stnInf1.bAuto && stnInf2.bLoad == false && stnInf2.bAuto )
                    if (stnInf.bAuto  && stnInf1.bAuto && stnInf2.bAuto)
                    {
                        DataTable dtLoc = new DataTable();
                        nRow = int.Parse(Globals.GetDestinateStn(dr["stn_no"].ToString()));
                        //if (nRow == 5 && sLocSize.Trim() == "L") continue;
                        strSql = string.Format("select * from loc_mst where loc_sts = 'N' and crn_no = {0} ", nRow);
                        if (sLocSize.Trim() == "L")
                        {
                            strSql = strSql + string.Format("and LOC_SIZE in ('L','M')");
                        }
                        else if (sLocSize.Trim() == "M")
                        {
                            strSql = strSql + string.Format("and LOC_SIZE in ('M')");
                        }
                        else if (sLocSize.Trim() == "H")
                        {
                            strSql = strSql + string.Format("and LOC_SIZE in ('H')");
                        }
                        Globals.DB.ExecGetTable(strSql, ref dtLoc);
                        if (dtLoc.Rows.Count > 10)
                        {
                            strResult = dr["stn_no"].ToString();
                            break;
                        }
                    }
                }
            }

            return strResult;
        }
        private string GetS2SStnNo(string strSourceStn)
        {
            string strResult = string.Empty;
            switch (strSourceStn)
            {
                case "B123":
                    strResult = "A51";
                    break;
                case "B134":
                    strResult = "A41";
                    break;
                case "B144":
                    strResult = "A31";
                    break;
                case "B154":
                    strResult = "A21";
                    break;
                case "B164" :
                    strResult = "A11";
                    break;
                    
            }
            return strResult;
        }
        private string GetPreStnNo(string strSourceStn)
        {
            string strResult = string.Empty;
            switch (strSourceStn)
            {
                case "B123":
                    strResult = "B122";
                    break;
                case "B134":
                    strResult = "B133";
                    break;
                case "B144":
                    strResult = "B143";
                    break;
                case "B154":
                    strResult = "B153";
                    break;
                case "B164":
                    strResult = "B163";
                    break;

            }
            return strResult;
        }
        private void UpdateStnInfo()
        {
            string strSql = string.Empty;
            DataTable dtStnNo = new DataTable();
            try
            {
                strSql = string.Format("select * from stn_inf where area_no in('B','A') and can_use = 'Y'");
                Globals.DB.ExecGetTable(strSql,ref dtStnNo);
                foreach (DataRow dr in dtStnNo.Rows)
                {
                    StnInfo stnInf = (StnInfo)hFStnInfos[dr["stn_no"].ToString()];
                    //自动、荷无、有命令号（清楚站口命令信息）
                    if (stnInf.bAuto && stnInf.bLoad == false && stnInf.nCmdSno > 0)
                    {
                        DataTable dtCmd = new DataTable();
                        strSql = string.Format("select * from cmd_mst where cmd_sts in ('0','1') and cmd_sno = '{0}'",stnInf.CmdSno);
                        Globals.DB.ExecGetTable(strSql, ref dtCmd);
                        if (dtCmd.Rows.Count <= 0)
                        {
                            WritePlc(dr["stn_no"].ToString(), 0, 0, 0);
                            Log.WriteLog(dr["snt_no"].ToString() + "站口状态异常，清除命令：" + stnInf.nCmdSno.ToString().PadLeft('0'));
                        }
                       
                    }
                }
            }
            catch
            {
 
            }
        }

        private string  GetLocHeight(string sType)
        {
            string strRes = string.Empty;
            switch (sType)
            {
                case "0":
                    strRes = "0";
                    break;
                case "1":
                    strRes = "L";
                    break;
                case "2":
                    strRes = "M";
                    break;
                case "3":
                    strRes = "H";
                    break;
                default:
                    strRes = "0";
                    break;
            }
            return strRes;
        }

        private void GetLocRateTvinfo()
        {
            try
            {
                double dRate;
                int nLocCountForAll = 8740;
                int  nLocCountForUsing = 0;
                string strLocCountUsing = string.Empty;
                string strSql = string.Empty;
                strSql = string.Format("select count(*) from loc_mst where loc_sts !='N'");
                Globals.DB.ExecGetValue(strSql, out strLocCountUsing);
                if (strLocCountUsing.Length > 0)
                {
                    nLocCountForUsing = int.Parse(strLocCountUsing);
                    dRate = Convert.ToDouble (nLocCountForUsing) /Convert.ToDouble( nLocCountForAll)*100;
                    string strMessage = dRate.ToString().Substring(0,5) + "%";
                    if (DateTime.Now.Hour >= 8 && DateTime.Now.Hour <= 9 && isShowLocRate == false)
                    {
                        if (isShowLocRate == false)
                        {
                            //insert into KanbanInfo (InfoType,KanbanType,Msg,DetailMsg,UpdatedPerson,CreatedDate,Status,STN_NO) values('ddd','60006','库位使用率','目前使用率为12','WCS','2017-01-23 10:50:05','0','11')
                            strSql = string.Format("update KanbanInfo set infoType =N'{0}',kanbantype = '{1}',msg = N'{2}',detailMsg =N'{3}',UpdatedPerson = '{4}',CreatedDate = '{5}',Status = '{6}' where kanbantype = '00000' and Status = '2' and stn_no = '{7}'",
                                "库存", "60006", "库位使用率", "库位使用率为：" + strMessage, "WCS", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "0", "11");
                            if (Globals.DB.ExecSql(strSql)) isShowLocRate = true;
                        }
                    }
                    if (DateTime.Now.Hour >= 10 && isShowLocRate == true) 
                    {
                        isShowLocRate = false;
                    }
                    //else if(isShowLocRate== true)
                    //{
                        
                    //    string  strCreatDate = string.Empty;
                    //    strSql = string.Format("select  CreatedDate from kanbaninfo where infoType = N'{0}'  and kanbantype = '60006' and status = '2'","库存");
                    //    Globals.DB.ExecGetValue(strSql, out strCreatDate);
                    //    if (strCreatDate.Length > 0)
                    //    {
                    //        DateTime dt = DateTime.Parse(strCreatDate);
                    //        if (DateTime.Now.Subtract(dt).TotalMilliseconds > 3)
                    //        {
                    //            strSql = string.Format("Update kanbaninfo set infoType = '',kanbantype = '00000',status = '2' where kanbantype = '60006' and status = '2' and stn_no ='11'");
                    //            Globals.DB.ExecSql(strSql);
                    //            Log.WriteLog("清除库存使用率显示信息！");
                    //        }
                    //    }

                    //}
     
                }

            }
            catch
            {
 
            }
        }

        #endregion
    }
}
