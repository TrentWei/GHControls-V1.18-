using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using DB;
using System.Data;
using SqlFactory;
using Controls;
using System.Text;
using System.Net.NetworkInformation;

namespace DadayBabyMainCtrl
{
    public class GCtrl
    {
        public static int nMaxCrn = 5;
        public static CrnInfo[] pCrnInfs;
        public static DataTable dtWrtPlc = null;
        public static string sRunInfo = string.Empty;
        public static DbAccess dbConn = null;

        public static bool BitChk(int nErr, int nBit)
        {
            int nB = (int)(Math.Pow(2, nBit));
            return (nErr & nB) > 0 ? true : false;
        }

        public static bool IntChk(int nErr, int nVal)
        {
            return (nErr & nVal) > 0 ? true : false;
        }

        public static string C5Sno(object objSno)
        {
            int nCnt = 0;
            if (objSno.ToString().Length == 0)
                nCnt = 0;
            else
                nCnt = int.Parse(objSno.ToString());
            return nCnt.ToString("00000");
        }



        public static bool CrtCmdMst(object scmdsno, object scmdsts, object sprty, object sStnNo, object scmdmode, object sIoType,
            object sLoc, object sNewLoc, object sTrace, object sPletNo, object sCrtDate, object sProgId, object sProgName, object sUserId, object sHeight)
        {
            string strSql = string.Empty;
            DataTable dtCmd = new DataTable();
            //将已经存在的命令号，移入历史纪录中，并删除
            strSql = string.Format("select * from cmd_mst where cmd_sno = '{0}'", scmdsno);
            Globals.DB.ExecGetTable(strSql, ref dtCmd);
            if (dtCmd.Rows.Count > 0)
            {
                strSql = string.Format("insert into CMD_MST_Log select * from CMD_MST where CMD_SNO = '{0}'", scmdsno);
                if (Globals.DB.ExecSql(strSql))
                {
                    Log.WriteLog("命令表中已经存在命令：" + scmdsno + " 删除！");
                    strSql = string.Format("delete from cmd_mst where cmd_sno = '{0}'", scmdsno);
                    Globals.DB.ExecSql(strSql);
                }
            }
            strSql = string.Format("INSERT INTO CMD_MST (CMD_SNO,CMD_STS,PRTY,STN_NO,CMD_MODE,IO_TYPE,LOC,NEW_LOC,TRACE,PLT_ID,CRT_DATE,PROG_ID,PROG_NAME,USER_ID,Height,MIX_QTY,avail) values(");
            strSql += string.Format("'{0}',", scmdsno);
            strSql += string.Format("'{0}',", scmdsts);
            strSql += string.Format("'{0}',", sprty);
            strSql += string.Format("'{0}',", sStnNo);
            strSql += string.Format("'{0}',", scmdmode);
            strSql += string.Format("'{0}',", sIoType);
            strSql += string.Format("'{0}',", sLoc);
            strSql += string.Format("'{0}',", sNewLoc);
            strSql += string.Format("'{0}',", sTrace);
            strSql += string.Format("'{0}',", sPletNo);
            strSql += string.Format("'{0}',", sCrtDate);
            strSql += string.Format("'{0}',", sProgId);
            strSql += string.Format("'{0}',", sProgName);
            strSql += string.Format("'{0}',", sUserId);
            strSql += string.Format("'{0}',0,0)", sHeight);
            return Globals.DB.ExecSql(strSql);

        }

        public static bool CrtEquCmd(object sCmdSno, object sMode, object sEqu,
            object sSrc, object sDes, object strPri)
        {
            MakeSql mSql = new MakeSql("EquCmd", SqlType.Insert);
            mSql.SetStrValue("CmdSno", sCmdSno.ToString());
            mSql.SetStrValue("EquNo", sEqu.ToString());
            mSql.SetStrValue("CmdMode", sMode.ToString());
            mSql.SetStrValue("CmdSts", "0");
            string sSr = string.Empty;
            switch (sMode.ToString())
            {
                case cCmdMode.In:
                    mSql.SetStrValue("Source", sSrc.ToString());
                    sSr = GetCrnLoc(sDes);
                    //sSr = sSr.Insert(2, "0");
                    mSql.SetStrValue("Destination", sSr);
                    break;
                case cCmdMode.Out:
                    sSr = GetCrnLoc(sSrc);
                    //sSr = sSr.Insert(2, "0");
                    mSql.SetStrValue("Source", sSr);
                    mSql.SetStrValue("Destination", sDes.ToString());
                    break;
                case cCmdMode.S2S:
                    mSql.SetStrValue("Source", sSrc.ToString());
                    mSql.SetStrValue("Destination", sDes.ToString());
                    break;
                case cCmdMode.L2L:
                    sSr = GetCrnLoc(sSrc);
                    //sSr = sSr.Insert(2, "0");
                    mSql.SetStrValue("Source", sSr);

                    sSr = GetCrnLoc(sDes);
                    //sSr = sSr.Insert(2, "0");
                    mSql.SetStrValue("Destination", sSr);
                    break;
                case cCmdMode.Move:
                    mSql.SetStrValue("Source", "1");
                    sSr = GetCrnLoc(sDes);
                    mSql.SetStrValue("Destination", sSr);
                    break;
                case cCmdMode.Pick:
                    sSr = GetCrnLoc(sSrc);
                    //sSr = sSr.Insert(2, "0");
                    mSql.SetStrValue("Source", sSr);
                    mSql.SetStrValue("Destination", "1");
                    break;
                case cCmdMode.Put:
                    mSql.SetStrValue("Source", "1");
                    sSr = GetCrnLoc(sDes);
                    //sSr = sSr.Insert(2, "0");
                    mSql.SetStrValue("Destination", sSr);
                    break;
                default:
                    return false;
            }
            mSql.SetStrValue("LocSize", "0");
            mSql.SetStrValue("Priority", strPri.ToString());
            mSql.SetNumValue("RcvDT", Globals.sEquDt);

            if (dbConn.ExecSql(mSql.GetSql()))
                return true;
            else
                return false;
        }

        public static bool CrtRgvCmd(object sCmdSno, object sMode, object sEqu,
            int sSrc, int sDes, object strPri)
        {
            int nStn = 0;
            MakeSql mSql = new MakeSql("EquCmd", SqlType.Insert);
            nStn = int.Parse(sCmdSno.ToString());
            mSql.SetStrValue("CmdSno", nStn.ToString("00000"));
            mSql.SetStrValue("EquNo", sEqu.ToString());
            mSql.SetStrValue("CmdMode", sMode.ToString());
            mSql.SetStrValue("CmdSts", "0");
            mSql.SetStrValue("Source", sSrc.ToString());
            mSql.SetStrValue("Destination", sDes.ToString());
            mSql.SetStrValue("LocSize", "0");
            mSql.SetStrValue("Priority", strPri.ToString());
            mSql.SetNumValue("RcvDT", Globals.sEquDt);

            if (dbConn.ExecSql(mSql.GetSql()))
                return true;
            else
                return false;
        }

        public static bool CrtSlvCmd(object sCmdSno, object sEqu, object sPalletNo,
            int sSrc, int sDes, object strPri)
        {
            int nStn = 0;
            MakeSql mSql = new MakeSql("Slv_Cmd", SqlType.Insert);
            nStn = int.Parse(sCmdSno.ToString());
            mSql.SetStrValue("PalletNo", sPalletNo.ToString());
            mSql.SetStrValue("CmdSno", nStn.ToString("00000"));
            mSql.SetStrValue("EquNo", sEqu.ToString());
            mSql.SetStrValue("CmdSts", "0");
            mSql.SetStrValue("Fr_Stn", sSrc.ToString());
            mSql.SetStrValue("To_Stn", sDes.ToString());
            mSql.SetNumValue("CarNo", "0");
            mSql.SetNumValue("CreateDate", Globals.sEquDt);

            if (dbConn.ExecSql(mSql.GetSql()))
                return true;
            else
                return false;
        }

        public static void SetStnLoad(string strArea, Hashtable hTvStns)
        {
            DataTable dtStn = new DataTable();
            string sStnName = string.Empty;

            try
            {
                string strSql = string.Format("select Station_Name from Wcs_Information where Area_No in ({0})", strArea);
                dbConn.ExecGetTable(strSql, ref dtStn);

                foreach (DataRow dr in dtStn.Rows)
                {
                    sStnName = dr["Station_Name"].ToString();
                    if (hTvStns.Contains(sStnName))
                    {
                        StnInfo stnInf = (StnInfo)hTvStns[sStnName];
                        strSql = string.Format("update Wcs_Information set BLoad ='{0}' where Station_Name ='{1}'", stnInf.bLoad ? "1" : "0", sStnName);
                        dbConn.ExecSql(strSql);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(cLog.Exception, "setStnLoad\n\r" + dbConn.ErrorInfo);
                Log.WriteLog(ex);
            }
        }

        public static void GetCrnInfo()
        {
            string sSql = string.Empty;
            string strData = string.Empty;
            int nStatus = 0;
            DataTable dtData = new DataTable();

            for (int nCrn = 1; nCrn < nMaxCrn + 1; nCrn++)
            {
                sSql = string.Format(@"select * from EquPlcData where EquNo='{0}' and SerialNo='1-3'", nCrn);
                dtData.Clear();
                try
                {
                    strData = string.Empty;
                    if (dbConn.ExecGetTable(sSql, ref dtData) && dtData.Rows.Count > 0)
                    {
                        strData = dtData.Rows[0]["EquPlcData"].ToString();
                    }

                    string[] strValues = strData.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    if (strValues.Length > 72)
                    {
                        nStatus = int.Parse(strValues[58]);
                        if ((nStatus & 0x03) < 0x03)
                        {
                            pCrnInfs[nCrn - 1].sStatus = "X";
                            if ((nStatus & 0x04) > 0)
                                pCrnInfs[nCrn - 1].bError = true;
                            else
                                pCrnInfs[nCrn - 1].bError = false;
                        }
                        else
                        {
                            if ((nStatus & 0x08) > 0)
                                pCrnInfs[nCrn - 1].sStatus = "R";
                            if ((nStatus & 0x0F) > 0)
                                pCrnInfs[nCrn - 1].sStatus = "A";
                            if ((nStatus & 0x04) > 0)
                                pCrnInfs[nCrn - 1].bError = true;
                            else
                                pCrnInfs[nCrn - 1].bError = false;
                        }

                        pCrnInfs[nCrn - 1].CmdSno = strValues[68];
                        pCrnInfs[nCrn - 1].nCmdMode = byte.Parse(strValues[69]);
                    }
                    else
                    {
                        pCrnInfs[nCrn - 1].bError = false;
                        pCrnInfs[nCrn - 1].sStatus = "X";
                        pCrnInfs[nCrn - 1].CmdSno = "0";
                        pCrnInfs[nCrn - 1].nCmdMode = 0;
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteLog(ex);
                }
            }
        }

        public static string GetCrnLoc(object sLoc)
        {
            string strLoc = sLoc.ToString();
            int nRow = 0;
            nRow = int.Parse(strLoc.Substring(0, 2));
            nRow = nRow % 4;
            if (nRow == 0) nRow = 4;
            strLoc = nRow.ToString("00") + strLoc.Substring(2, 5);
            return strLoc;
        }

        public static int GetEquFLoc(string strLoc)
        {
            int nLoc = int.Parse(strLoc.Substring(0, 2));
            nLoc = (nLoc % 4 > 0 ? 1 : 0) + nLoc / 4;
            return nLoc;
        }

        public static bool ChkCrnCanUse(int nCrn)
        {
            if (pCrnInfs[nCrn - 1].sStatus == "A" && pCrnInfs[nCrn - 1].bError == false)
                return true;
            else
                return false;
        }

        public static bool ChkCrnInStn(int nCrn, int nSrc, string sStn, StnInfo stnInfo)
        {
            bool bStnCanUse = true;
            DataTable dtEquCmd = new DataTable();

            try
            {
                if (stnInfo.bAuto && stnInfo.bLoad && stnInfo.nCmdSno > 0 && stnInfo.nCmdMode > 0)
                {
                    string strSql = string.Format(@"select * from EquCmd where EquNo='{0}' and Source='{1}' and CmdSts <= '1'", nCrn, nSrc);
                    dbConn.ExecGetTable(strSql, ref dtEquCmd);

                    if (dtEquCmd.Rows.Count > 0)
                    {
                        if (dtEquCmd.Rows[0]["CmdSno"].ToString() != stnInfo.CmdSno)
                        {
                            sRunInfo = string.Format("站口:{0} 命令:{1} 等待主机执行命令:{2}", sStn, stnInfo.CmdSno, dtEquCmd.Rows[0]["CmdSno"].ToString());
                            Log.WriteLog(cLog.RunLog, sRunInfo);
                        }
                        bStnCanUse = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
            }
            return bStnCanUse;
        }
        public static bool ChkCrnInStnForOnlyOne(int nCrn, int nSrc, string sStn, StnInfo stnInfo)
        {
            bool bStnCanUse = true;
            DataTable dtEquCmd = new DataTable();

            try
            {
                if (stnInfo.bAuto && stnInfo.bLoad && stnInfo.nCmdSno > 0 && stnInfo.nCmdMode > 0)
                {
                    string strSql = string.Format(@"select * from EquCmd where EquNo='{0}'  and CmdSts <= '1'", nCrn);
                    dbConn.ExecGetTable(strSql, ref dtEquCmd);

                    if (dtEquCmd.Rows.Count > 0)
                    {
                        if (dtEquCmd.Rows[0]["CmdSno"].ToString() != stnInfo.CmdSno)
                        {
                            sRunInfo = string.Format("站口:{0} 命令:{1} 等待主机执行命令:{2}", sStn, stnInfo.CmdSno, dtEquCmd.Rows[0]["CmdSno"].ToString());
                            Log.WriteLog(cLog.RunLog, sRunInfo);
                        }
                        bStnCanUse = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
            }
            return bStnCanUse;
        }

        public static bool ChkCrnOutStn(int nCrn, int nDes, string sStn, StnInfo stnInfo)
        {
            bool bStnCanUse = true;
            DataTable dtEquCmd = new DataTable();

            try
            {
                if (stnInfo.bAuto && stnInfo.bLoad == false && stnInfo.nCmdSno > 0 && stnInfo.nCmdMode > 0)
                {
                    string strSql = string.Format(@"select * from EquCmd where EquNo='{0}' and Destination='{1}' and CmdSts <= '1'", nCrn, nDes);
                    dbConn.ExecGetTable(strSql, ref dtEquCmd);

                    if (dtEquCmd.Rows.Count > 0)
                    {
                        sRunInfo = string.Format("站口:{0} 等待主机执行命令:{1}", sStn, dtEquCmd.Rows[0]["CmdSno"].ToString());
                        Log.WriteLog(cLog.RunLog, sRunInfo);
                        bStnCanUse = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
            }
            return bStnCanUse;
        }

        public static bool ChkCrnOutStnForOnlyOne(int nCrn, int nDes, string sStn, StnInfo stnInfo)
        {
            bool bStnCanUse = true;
            DataTable dtEquCmd = new DataTable();

            try
            {
                if (stnInfo.bAuto && stnInfo.bLoad == false && stnInfo.nCmdSno > 0 && stnInfo.nCmdMode > 0)
                {
                    string strSql = string.Format(@"select * from EquCmd where EquNo='{0}'  and CmdSts <= '1'", nCrn);
                    dbConn.ExecGetTable(strSql, ref dtEquCmd);

                    if (dtEquCmd.Rows.Count > 0)
                    {
                        sRunInfo = string.Format("站口:{0} 等待主机执行命令:{1}", sStn, dtEquCmd.Rows[0]["CmdSno"].ToString());
                        Log.WriteLog(cLog.RunLog, sRunInfo);
                        bStnCanUse = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
            }
            return bStnCanUse;
        }

        public static bool ChkCrnOutStnForOnlyOneForCycle(int nCrn, int nDes, string sStn, StnInfo stnInfo)
        {
            bool bStnCanUse = true;
            DataTable dtEquCmd = new DataTable();

            try
            {
                if (stnInfo.bAuto && stnInfo.bLoad == false)
                {
                    string strSql = string.Format(@"select * from EquCmd where EquNo='{0}'  and CmdSts <= '1'", nCrn);
                    dbConn.ExecGetTable(strSql, ref dtEquCmd);

                    if (dtEquCmd.Rows.Count > 0)
                    {
                        sRunInfo = string.Format("站口:{0} 等待主机执行命令:{1}", sStn, dtEquCmd.Rows[0]["CmdSno"].ToString());
                        Log.WriteLog(cLog.RunLog, sRunInfo);
                        bStnCanUse = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
            }
            return bStnCanUse;
        }

        public static string GetNewInLoc(int nCrn, object sLocSize, object sItmType, object sPltNo, out string sInfo)
        {
            string sLoc = string.Empty, sSql = string.Empty;
            sInfo = string.Empty;
            DataTable dt = new DataTable();

            if (sLocSize.ToString().Trim() == "L") sSql = string.Format("select x.Loc from Loc_Mst x where x.Loc_Sts='N' and x.Loc_Size in('L','M') and x.Row_X >=({0}-1)*4+1 and x.Row_X <= {1}*4 and exists (select y.Loc from LOC_MST y where y.Loc_Sts ='N' and (x.Row_X=y.Row_X + 2  or x.Row_X = ({1}-1)*4+1 or x.Row_X= ({1}-1)*4 +2 )and x.Bay_Y =y.Bay_Y and x.Lvl_Z=y.Lvl_Z) order by  Row_X desc, LOC_SIZE asc, Lvl_Z asc,Bay_Y asc  ", nCrn, nCrn);
            if (sLocSize.ToString().Trim() == "M") sSql = string.Format("select x.Loc from Loc_Mst x where x.Loc_Sts='N' and x.Loc_Size ='M' and x.Row_X >=({0}-1)*4+1 and x.Row_X <= {1}*4 and exists (select y.Loc from LOC_MST y where y.Loc_Sts ='N' and (x.Row_X=y.Row_X + 2  or x.Row_X = ({1}-1)*4+1 or x.Row_X= ({1}-1)*4 +2 )and x.Bay_Y =y.Bay_Y and x.Lvl_Z=y.Lvl_Z) order by  Row_X desc, LOC_SIZE desc, Lvl_Z asc,Bay_Y asc  ", nCrn, nCrn);
            if (sLocSize.ToString().Trim() == "H") sSql = string.Format("select x.Loc from Loc_Mst x where x.Loc_Sts='N' and x.Loc_Size ='H' and x.Row_X >=({0}-1)*4+1 and x.Row_X <= {1}*4 and exists (select y.Loc from LOC_MST y where y.Loc_Sts ='N' and (x.Row_X=y.Row_X + 2  or x.Row_X = ({1}-1)*4+1 or x.Row_X= ({1}-1)*4 +2 )and x.Bay_Y =y.Bay_Y and x.Lvl_Z=y.Lvl_Z) order by  Row_X desc, LOC_SIZE desc, Lvl_Z asc,Bay_Y asc  ", nCrn, nCrn);

            //sSql = string.Format("select x.Loc from Loc_Mst x where x.Loc_Sts='N' and x.Loc_Size='{0}' and x.Row_X >=({1}-1)*4+1 and x.Row_X <= {2}*4 and exists (select y.Loc from LOC_MST y where y.Loc_Sts ='N' and (x.Row_X=y.Row_X + 2  or x.Row_X = ({1}-1)*4+1 or x.Row_X= ({1}-1)*4 +2 )and x.Bay_Y =y.Bay_Y and x.Lvl_Z=y.Lvl_Z) order by Row_X desc,Bay_Y asc ,Lvl_Z asc  ", sLocSize, nCrn, nCrn);
            Globals.DB.ExecGetTable(sSql, ref dt);
            if (dt.Rows.Count <= 10)
            {
                dt.Clear();
                if (sLocSize.ToString().Trim() == "L")
                {
                    sSql = string.Format("select x.Loc from Loc_Mst x where x.Loc_Sts='N' and x.Loc_Size in ('H','L','M') and x.Row_X >=({0}-1)*4+1 and x.Row_X <= {1}*4 and exists (select y.Loc from LOC_MST y where y.Loc_Sts ='N' and (x.Row_X=y.Row_X + 2  or x.Row_X = ({1}-1)*4+1 or x.Row_X= ({1}-1)*4 +2 )and x.Bay_Y =y.Bay_Y and x.Lvl_Z=y.Lvl_Z) order by  Row_X desc, LOC_SIZE desc, Lvl_Z asc, Bay_Y asc  ", nCrn, nCrn);
                    Globals.DB.ExecGetTable(sSql, ref dt);
                }
                else if (sLocSize.ToString().Trim() == "M")
                {
                    sSql = string.Format("select x.Loc from Loc_Mst x where x.Loc_Sts='N' and x.Loc_Size in ('H','M') and x.Row_X >=({0}-1)*4+1 and x.Row_X <= {1}*4 and exists (select y.Loc from LOC_MST y where y.Loc_Sts ='N' and (x.Row_X=y.Row_X + 2  or x.Row_X = ({1}-1)*4+1 or x.Row_X= ({1}-1)*4 +2 )and x.Bay_Y =y.Bay_Y and x.Lvl_Z=y.Lvl_Z) order by  Row_X desc, LOC_SIZE desc, Lvl_Z asc, Bay_Y asc  ", nCrn, nCrn);
                    Globals.DB.ExecGetTable(sSql, ref dt);
                }
                else if (sLocSize.ToString().Trim() == "H")
                {
                    sSql = string.Format("select x.Loc from Loc_Mst x where x.Loc_Sts='N' and x.Loc_Size ='H' and x.Row_X >=({0}-1)*4+1 and x.Row_X <= {1}*4 and exists (select y.Loc from LOC_MST y where y.Loc_Sts ='N' and (x.Row_X=y.Row_X + 2  or x.Row_X = ({1}-1)*4+1 or x.Row_X= ({1}-1)*4 +2 )and x.Bay_Y =y.Bay_Y and x.Lvl_Z=y.Lvl_Z) order by  Row_X desc, Lvl_Z asc, Bay_Y asc  ", nCrn, nCrn);
                    Globals.DB.ExecGetTable(sSql, ref dt);
                }
            }
            foreach (DataRow dr in dt.Rows)
            {

                sLoc = dr["Loc"].ToString();
                if (IsInnerLoc(sLoc))
                {
                    string strOutLoc = GetOutLoc(sLoc);
                    DataTable dtLocN = new DataTable();
                    //sSql = string.Format("select * from loc_mst where loc_sts != '{0}' and loc = '{1}'", sItmType, strOutLoc);//判断sloc对应的外储位是否为空
                    sSql = string.Format("select * from loc_mst where loc_sts not in ('S','E') and loc = '{0}'", strOutLoc);//判断sloc对应的外储位是否为空
                    Globals.DB.ExecGetTable(sSql, ref dtLocN);
                    Log.WriteLog("查找入库库位判断(内库位)", sLoc + "------" + strOutLoc);
                    if (dtLocN.Rows.Count == 1)
                    {
                        sLoc = "";
                        continue;
                    }
                    else if (dtLocN.Rows.Count == 0) break;
                }
                else
                {
                    string strInLoc = GetInnerLoc(sLoc);
                    DataTable dtLocN1 = new DataTable();
                    sSql = string.Format("select * from loc_mst where loc_sts != 'N' and loc = '{0}'", strInLoc);
                    Globals.DB.ExecGetTable(sSql, ref dtLocN1);
                    Log.WriteLog("查找入库库位判断(外库位)", sLoc + "------" + strInLoc);
                    if (dtLocN1.Rows.Count == 1)
                    {
                        sLoc = "";
                        continue;
                    }

                    else if (dtLocN1.Rows.Count == 0) break;
                }
                break;
            }

            return sLoc;
        }


        public static string GetNewInL2L(int nCrn, object sLocSize, object sItmType, object sPltNo, string Old_Loc, out string sInfo)
        {
            string sLoc = string.Empty, sSql = string.Empty;
            sInfo = string.Empty;
            DataTable dt = new DataTable();
            int old_loc = 0;
            if (Old_Loc.Substring(0, 1) != "0")
            {
                old_loc = Convert.ToInt32(Old_Loc.Substring(0, 2));
            }
            else
            {
                old_loc = Convert.ToInt32(Old_Loc.Substring(1, 1));
            }
            if (old_loc % 2 == 0)//判断奇偶列，奇数列库对库到偶数列，偶数列库对库到奇数列
            {
                if (sLocSize.ToString().Trim() == "L") sSql = string.Format("select x.Loc from Loc_Mst x where x.Loc_Sts='N' and x.Loc_Size in('L','M') and x.Row_X in(({0}-1)*4+1,{1}*4-1)  and exists (select y.Loc from LOC_MST y where y.Loc_Sts ='N' and ( x.Row_X = ({0}-1)*4+1 or x.Row_X= {1}*4-1 )and x.Bay_Y =y.Bay_Y and x.Lvl_Z=y.Lvl_Z) order by  Row_X desc, LOC_SIZE asc, Lvl_Z asc,Bay_Y asc  ", nCrn, nCrn);
                if (sLocSize.ToString().Trim() == "M") sSql = string.Format("select x.Loc from Loc_Mst x where x.Loc_Sts='N' and x.Loc_Size ='M' and x.Row_X in(({0}-1)*4+1,{1}*4-1) and exists (select y.Loc from LOC_MST y where y.Loc_Sts ='N' and ( x.Row_X = ({0}-1)*4+1 or x.Row_X= {1}*4-1 )and x.Bay_Y =y.Bay_Y and x.Lvl_Z=y.Lvl_Z) order by  Row_X desc, LOC_SIZE desc, Lvl_Z asc,Bay_Y asc  ", nCrn, nCrn);
                if (sLocSize.ToString().Trim() == "H") sSql = string.Format("select x.Loc from Loc_Mst x where x.Loc_Sts='N' and x.Loc_Size ='H' and x.Row_X in(({0}-1)*4+1,{1}*4-1) and exists (select y.Loc from LOC_MST y where y.Loc_Sts ='N' and ( x.Row_X = ({0}-1)*4+1 or x.Row_X= {1}*4-1 )and x.Bay_Y =y.Bay_Y and x.Lvl_Z=y.Lvl_Z) order by  Row_X desc, LOC_SIZE desc, Lvl_Z asc,Bay_Y asc  ", nCrn, nCrn);

                //sSql = string.Format("select x.Loc from Loc_Mst x where x.Loc_Sts='N' and x.Loc_Size='{0}' and x.Row_X >=({1}-1)*4+1 and x.Row_X <= {2}*4 and exists (select y.Loc from LOC_MST y where y.Loc_Sts ='N' and (x.Row_X=y.Row_X + 2  or x.Row_X = ({1}-1)*4+1 or x.Row_X= ({1}-1)*4 +2 )and x.Bay_Y =y.Bay_Y and x.Lvl_Z=y.Lvl_Z) order by Row_X desc,Bay_Y asc ,Lvl_Z asc  ", sLocSize, nCrn, nCrn);
                Globals.DB.ExecGetTable(sSql, ref dt);
                if (dt.Rows.Count <= 10)
                {
                    dt.Clear();
                    if (sLocSize.ToString().Trim() == "L")
                    {
                        sSql = string.Format("select x.Loc from Loc_Mst x where x.Loc_Sts='N' and x.Loc_Size in ('H','L','M') and x.Row_X in(({0}-1)*4+1,{1}*4-1) and exists (select y.Loc from LOC_MST y where y.Loc_Sts ='N' and ( x.Row_X = ({0}-1)*4+1 or x.Row_X= {1}*4-1 ) and x.Bay_Y =y.Bay_Y and x.Lvl_Z=y.Lvl_Z) order by  Row_X desc, LOC_SIZE desc, Lvl_Z asc, Bay_Y asc  ", nCrn, nCrn);
                        Globals.DB.ExecGetTable(sSql, ref dt);
                    }
                    else if (sLocSize.ToString().Trim() == "M")
                    {
                        sSql = string.Format("select x.Loc from Loc_Mst x where x.Loc_Sts='N' and x.Loc_Size in ('H','M') and x.Row_X in(({0}-1)*4+1,{1}*4-1) and exists (select y.Loc from LOC_MST y where y.Loc_Sts ='N' and ( x.Row_X = ({0}-1)*4+1 or x.Row_X= {1}*4-1 ) and x.Bay_Y =y.Bay_Y and x.Lvl_Z=y.Lvl_Z) order by  Row_X desc, LOC_SIZE desc, Lvl_Z asc, Bay_Y asc  ", nCrn, nCrn);
                        Globals.DB.ExecGetTable(sSql, ref dt);
                    }
                    else if (sLocSize.ToString().Trim() == "H")
                    {
                        sSql = string.Format("select x.Loc from Loc_Mst x where x.Loc_Sts='N' and x.Loc_Size ='H' and x.Row_X in(({0}-1)*4+1,{1}*4-1) and exists (select y.Loc from LOC_MST y where y.Loc_Sts ='N' and ( x.Row_X = ({0}-1)*4+1 or x.Row_X= {1}*4-1 )and x.Bay_Y =y.Bay_Y and x.Lvl_Z=y.Lvl_Z) order by  Row_X desc, Lvl_Z asc, Bay_Y asc  ", nCrn, nCrn);
                        Globals.DB.ExecGetTable(sSql, ref dt);
                    }
                }

            }
            else
            {
                if (sLocSize.ToString().Trim() == "L") sSql = string.Format("select x.Loc from Loc_Mst x where x.Loc_Sts='N' and x.Loc_Size in('L','M') and x.Row_X in(({0}-1)*4+2,{1}*4) and exists (select y.Loc from LOC_MST y where y.Loc_Sts ='N' and ( x.Row_X = ({0}-1)*4+2 or x.Row_X= {1}*4 ) and x.Bay_Y =y.Bay_Y and x.Lvl_Z=y.Lvl_Z) order by  Row_X desc, LOC_SIZE asc, Lvl_Z asc,Bay_Y asc  ", nCrn, nCrn);
                if (sLocSize.ToString().Trim() == "M") sSql = string.Format("select x.Loc from Loc_Mst x where x.Loc_Sts='N' and x.Loc_Size ='M' and x.Row_X in(({0}-1)*4+2,{1}*4) and exists (select y.Loc from LOC_MST y where y.Loc_Sts ='N' and ( x.Row_X = ({0}-1)*4+2 or x.Row_X= {1}*4 ) and x.Bay_Y =y.Bay_Y and x.Lvl_Z=y.Lvl_Z) order by  Row_X desc, LOC_SIZE desc, Lvl_Z asc,Bay_Y asc  ", nCrn, nCrn);
                if (sLocSize.ToString().Trim() == "H") sSql = string.Format("select x.Loc from Loc_Mst x where x.Loc_Sts='N' and x.Loc_Size ='H' and x.Row_X in(({0}-1)*4+2,{1}*4) and exists (select y.Loc from LOC_MST y where y.Loc_Sts ='N' and ( x.Row_X = ({0}-1)*4+2 or x.Row_X= {1}*4 ) and x.Bay_Y =y.Bay_Y and x.Lvl_Z=y.Lvl_Z) order by  Row_X desc, LOC_SIZE desc, Lvl_Z asc,Bay_Y asc  ", nCrn, nCrn);

                //sSql = string.Format("select x.Loc from Loc_Mst x where x.Loc_Sts='N' and x.Loc_Size='{0}' and x.Row_X >=({1}-1)*4+1 and x.Row_X <= {2}*4 and exists (select y.Loc from LOC_MST y where y.Loc_Sts ='N' and (x.Row_X=y.Row_X + 2  or x.Row_X = ({1}-1)*4+1 or x.Row_X= ({1}-1)*4 +2 )and x.Bay_Y =y.Bay_Y and x.Lvl_Z=y.Lvl_Z) order by Row_X desc,Bay_Y asc ,Lvl_Z asc  ", sLocSize, nCrn, nCrn);
                Globals.DB.ExecGetTable(sSql, ref dt);
                if (dt.Rows.Count <= 10)
                {
                    dt.Clear();
                    if (sLocSize.ToString().Trim() == "L")
                    {
                        sSql = string.Format("select x.Loc from Loc_Mst x where x.Loc_Sts='N' and x.Loc_Size in ('H','L','M') and x.Row_X in(({0}-1)*4+2,{1}*4) and exists (select y.Loc from LOC_MST y where y.Loc_Sts ='N' and ( x.Row_X = ({0}-1)*4+2 or x.Row_X= {1}*4 ) and x.Bay_Y =y.Bay_Y and x.Lvl_Z=y.Lvl_Z) order by  Row_X desc, LOC_SIZE desc, Lvl_Z asc, Bay_Y asc  ", nCrn, nCrn);
                        Globals.DB.ExecGetTable(sSql, ref dt);
                    }
                    else if (sLocSize.ToString().Trim() == "M")
                    {
                        sSql = string.Format("select x.Loc from Loc_Mst x where x.Loc_Sts='N' and x.Loc_Size in ('H','M') and x.Row_X in(({0}-1)*4+2,{1}*4) and exists (select y.Loc from LOC_MST y where y.Loc_Sts ='N' and ( x.Row_X = ({0}-1)*4+2 or x.Row_X= {1}*4 ) and x.Bay_Y =y.Bay_Y and x.Lvl_Z=y.Lvl_Z) order by  Row_X desc, LOC_SIZE desc, Lvl_Z asc, Bay_Y asc  ", nCrn, nCrn);
                        Globals.DB.ExecGetTable(sSql, ref dt);
                    }
                    else if (sLocSize.ToString().Trim() == "H")
                    {
                        sSql = string.Format("select x.Loc from Loc_Mst x where x.Loc_Sts='N' and x.Loc_Size ='H' and x.Row_X in(({0}-1)*4+2,{1}*4) and exists (select y.Loc from LOC_MST y where y.Loc_Sts ='N' and ( x.Row_X = ({0}-1)*4+2 or x.Row_X= {1}*4 ) and x.Bay_Y =y.Bay_Y and x.Lvl_Z=y.Lvl_Z) order by  Row_X desc, Lvl_Z asc, Bay_Y asc  ", nCrn, nCrn);
                        Globals.DB.ExecGetTable(sSql, ref dt);
                    }
                }
            }
            foreach (DataRow dr in dt.Rows)
            {

                sLoc = dr["Loc"].ToString();
                if (IsInnerLoc(sLoc))
                {
                    string strOutLoc = GetOutLoc(sLoc);
                    DataTable dtLocN = new DataTable();
                    //sSql = string.Format("select * from loc_mst where loc_sts != '{0}' and loc = '{1}'", sItmType, strOutLoc);//判断sloc对应的外储位是否为空
                    sSql = string.Format("select * from loc_mst where loc_sts not in ('S','E') and loc = '{0}'", strOutLoc);//判断sloc对应的外储位是否为空
                    Globals.DB.ExecGetTable(sSql, ref dtLocN);
                    Log.WriteLog("查找入库库位判断(内库位)", sLoc + "------" + strOutLoc);
                    if (dtLocN.Rows.Count == 1)
                    {
                        sLoc = "";
                        continue;
                    }
                    else if (dtLocN.Rows.Count == 0) break;
                }
                else
                {
                    string strInLoc = GetInnerLoc(sLoc);
                    DataTable dtLocN1 = new DataTable();
                    sSql = string.Format("select * from loc_mst where loc_sts != 'N' and loc = '{0}'", strInLoc);
                    Globals.DB.ExecGetTable(sSql, ref dtLocN1);
                    Log.WriteLog("查找入库库位判断(外库位)", sLoc + "------" + strInLoc);
                    if (dtLocN1.Rows.Count == 1)
                    {
                        sLoc = "";
                        continue;
                    }

                    else if (dtLocN1.Rows.Count == 0) break;
                }
                break;
            }

            return sLoc;
        }


        public static bool GetReadyOut(string sLoc)
        {
            bool bReady = false;
            string sSql = string.Empty, sStnNo = string.Empty;
            string sPltNo = string.Empty, sInnerLoc = string.Empty;
            string sItmType = string.Empty, sNewLoc = string.Empty;
            string sIoType = string.Empty, sRunInfo = string.Empty;
            int nCrn = 0;

            DataRow drLoc = null;

            try
            {
                if (GCtrl.IsInnerLoc(sLoc)) return true;

                nCrn = GCtrl.GetEquFLoc(sLoc);
                sStnNo = string.Format("S{0}00", nCrn);
                sInnerLoc = GCtrl.GetInnerLoc(sLoc);
                sSql = string.Format(@"select * from Loc_Mst where Loc='{0}'", sInnerLoc);
                if (!dbConn.ExecGetRow(sSql, ref drLoc) || drLoc == null)
                    throw new Exception("获取内储位失败!");

                // 根据内储位状态判断如何处理
                eLocSts sLocSts = (eLocSts)Enum.Parse(typeof(eLocSts), drLoc["Loc_Sts"].ToString());

                switch (sLocSts)
                {
                    case eLocSts.S:
                    case eLocSts.E:
                        if (sLocSts == eLocSts.S) sItmType = cItmType.Stock;
                        else if (sLocSts == eLocSts.E) sItmType = cItmType.EptPlt;

                        sPltNo = drLoc["Plt_id"].ToString();
                        // 将内储位的货物放置到新的储位
                        sNewLoc = GCtrl.GetNewInL2L(nCrn, drLoc["Loc_Size"], sItmType, sPltNo, sInnerLoc, out sRunInfo);
                        if (sNewLoc.Length != 7)
                            throw new Exception(string.Format("储位:{0} 状态:{1},库对库{2}", sInnerLoc, sLocSts, sRunInfo));

                        Log.WriteLog(cLog.RunLog, string.Format("储位:{0} 状态:{1},库对库{2}:{3}", sInnerLoc, sLocSts, sRunInfo, sNewLoc));

                        dbConn.BeginTran(true);
                        // ======================================================================================================================
                        sSql = string.Format("update loc_mst set old_sts = loc_sts ,loc_sts = 'O',trn_time = '{0}' where loc = '{1}' and loc_sts in ('S','E')",
                            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), sInnerLoc);
                        if (!dbConn.ExecSql(sSql)) throw new Exception(dbConn.ErrorInfo);

                        sSql = string.Format("update loc_mst set old_sts = loc_sts ,loc_sts = 'I',trn_time = '{0}' where loc = '{1}' and loc_sts in ('N')",
                            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), sNewLoc);
                        if (!dbConn.ExecSql(sSql)) throw new Exception(dbConn.ErrorInfo);
                        Log.WriteLog("库对库更新库位状态为预约" + sSql);
                        //string sCmdSno = Globals.GetSno();
                        if (sItmType == cItmType.EptPlt) sIoType = cIoType.EptPltL2L;
                        else if (sItmType == cItmType.Stock) sIoType = cIoType.StockL2L;

                        // 产生库对库命令
                        if (!GCtrl.CrtCmdMst(sPltNo, "0", "1", "", cCmdMode.L2L, sIoType, sInnerLoc, sNewLoc, "", sPltNo, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "wcs", "wcs", "dfd", drLoc["Loc_Size"]))
                            throw new Exception(dbConn.ErrorInfo);

                        Log.WriteLog(cLog.RunLog, string.Format("储位:{0} 托盘:{1} 下库对库命令成功,命令号:{2}", sInnerLoc, sPltNo, sPltNo));
                        // ======================================================================================================================
                        dbConn.CommitTran();
                        break;
                    case eLocSts.N:
                        bReady = true;
                        break;
                    default:
                        sSql = string.Format(@"select Cmd_Sno from Cmd_Mst where Cmd_Mode='{0}' and Stn_No='{1}' and Loc='{2}' and Cmd_Sts <'8'",
                            cCmdMode.L2L, sStnNo, sInnerLoc);
                        if (!dbConn.ExecQuery(sSql))
                            Log.WriteLog(cLog.RunLog, string.Format("内储位:{0} 状态:{1},不能做移库!", sInnerLoc, sLocSts));
                        break;

                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
            }
            return bReady;
        }

        public static bool ChkInnerLoc(string sLoc)
        {
            int nRow = int.Parse(sLoc.Substring(0, 2));
            nRow = nRow % 4;
            if (nRow == 0) nRow = 4;
            if (nRow == 1 || nRow == 2)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 判断是否是内储位
        /// </summary>
        /// <param name="sLoc"></param>
        /// <returns>内储位true,外储位false</returns>
        public static bool IsInnerLoc(string sLoc)
        {
            int nRow = int.Parse(sLoc.Substring(0, 2));
            nRow = nRow % 4;
            if (nRow == 1 || nRow == 2)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 判断是否是内储位，如果是内储位，则返回原储位，如果是外储位则寻找内储位
        /// </summary>
        /// <param name="sLoc"></param>
        /// <returns></returns>
        public static string GetInnerLoc(string sLoc)
        {
            int nRow = int.Parse(sLoc.Substring(0, 2));
            nRow = nRow % 4;
            if (nRow == 0) nRow = 4;
            if (nRow == 1 || nRow == 2)
            {
                return sLoc;
            }
            else
            {
                nRow = int.Parse(sLoc.Substring(0, 2)) - 2;
                return nRow.ToString("00") + sLoc.Substring(2, 5);
            }
        }
        public static string GetOutLoc(string sLoc)
        {
            int nRow = int.Parse(sLoc.Substring(0, 2));
            nRow = int.Parse(sLoc.Substring(0, 2)) + 2;
            return nRow.ToString("00") + sLoc.Substring(2, 5);

        }
        public static void TestEquFin()
        {
            string sSql = string.Empty, sEquSts = string.Empty;
            string sCmdSts = string.Empty, sComCode = string.Empty;
            string sCmdMode = string.Empty, sEquMode = string.Empty;
            bool IsCycle = false;
            DataTable dtEqu = new DataTable();
            DataTable dtEquW = new DataTable();

            try
            {
                #region 主机完成码W1、W2 单独处理(删除原有equcmd命令，重新下达);
                sSql = string.Format("select * from equcmd where completecode in('W1','W2')");
                Globals.DB.ExecGetTable(sSql, ref dtEquW);
                foreach (DataRow dr in dtEquW.Rows)
                {
                    sSql = string.Format("update cmd_mst set cmd_sts = '0' where cmd_sts = '1' and cmd_sno = '{0}'", dr["cmdsno"]);
                    if (Globals.DB.ExecSql(sSql))
                    {
                        sSql = string.Format("delete from equcmd where cmdsno = '{0}'", dr["cmdsno"]);
                        Globals.DB.ExecSql(sSql);
                    }
                }
                #endregion
                sSql = string.Format(@"select * from EquCmd where  CmdSts in ('{0}','{1}','{2}','{3}') and" +
                    " (ReNewflag<>'F' or ReNewFlag is null)", cEquSts.Fin, cEquSts.FFin, cEquSts.FCanCel, cEquSts.CanCel);
                Globals.DB.ExecGetTable(sSql, ref dtEqu);

                foreach (DataRow drEqu in dtEqu.Rows)
                {
                    try
                    {
                        sEquSts = drEqu["CmdSts"].ToString();
                        sComCode = drEqu["CompleteCode"].ToString();
                        sEquMode = drEqu["CmdMode"].ToString();

                        switch (sEquSts)
                        {
                            case cEquSts.CanCel:
                            case cEquSts.FCanCel:
                                sCmdSts = "8";
                                break;
                            case cEquSts.FFin:
                            case cEquSts.Fin:
                                {
                                    IsCycle = Globals.DealWithCycle(drEqu["cmdsno"].ToString());
                                    if (IsCycle == false) sCmdSts = "7";
                                }
                                break;
                            default:
                                break;
                        }
                        if (IsCycle == false)
                        {
                            Globals.DB.BeginTran(true);
                            // =====================================================================================================================
                            sSql = string.Format(@"update Cmd_Mst set Cmd_Sts='{0}',End_Date='{1}',Trace='{2}' where Cmd_Sno='{3}'"
                                , sCmdSts, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), cTrace.I15, drEqu["CmdSno"]);
                            if (!Globals.DB.ExecSql(sSql)) throw new Exception();
                            sSql = string.Format(@"update EquCmd set ReNewFlag='F' where CmdSno='{0}'", drEqu["CmdSno"]);
                            if (!Globals.DB.ExecSql(sSql)) throw new Exception();
                            // =========================================================================================================================
                            Globals.DB.CommitTran();
                        }
                    }
                    catch (Exception ex)
                    {
                        Globals.DB.RollbackTran();
                        Log.WriteLog(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
            }
        }

        public static void MoveCmd()
        {
            try
            {
                string strSql = string.Empty;
                DataTable dtSlv = new DataTable();
                DataRow drEqu = null;

                #region Move EquCmd

                strSql = string.Format(@"select top(1) * from EquCmd where CmdSts>'1' and ReNewFlag='F' and EquNo='R1'");
                if (dbConn.ExecGetRow(strSql, ref drEqu) && drEqu != null)
                {
                    dbConn.BeginTran(true);
                    MakeSql mSql = new MakeSql("EquCmdHis", SqlType.Insert);
                    mSql.SetStrValue(drEqu, "CmdSno", "CmdSts", "CmdMode", "EquNo", "Source", "Destination", "LocSize",
                        "Priority", "RcvDT", "ActDT", "EndDT", "CompleteCode", "CompleteIndex", "CarNo", "ReNewFlag");
                    mSql.SetNumValue("HISDT", Globals.sEquDt);
                    strSql = mSql.GetSql();

                    if (!dbConn.ExecSql(strSql)) throw new Exception(dbConn.ErrorInfo);
                    strSql = string.Format(@"delete from EquCmd where CmdSno='{0}' and EquNo='R1'", drEqu["CmdSno"]);
                    if (!dbConn.ExecSql(strSql)) throw new Exception(dbConn.ErrorInfo);
                    dbConn.CommitTran();
                }

                # endregion
            }
            catch (Exception ex)
            {
                dbConn.RollbackTran();
                Log.WriteLog(ex);
            }
        }

        public static int MaxValue(string[] strVaules)
        {
            int nMaxVal = int.Parse(strVaules[0]);
            for (int i = 0; i < strVaules.Length; i++)
            {
                if (nMaxVal < int.Parse(strVaules[i]))
                    nMaxVal = int.Parse(strVaules[i]);
            }
            return nMaxVal;
        }


        public static int MinValue(string[] strVaules)
        {
            int nMinVal = int.Parse(strVaules[0]);
            for (int i = 0; i < strVaules.Length; i++)
            {
                if (nMinVal > int.Parse(strVaules[i]))
                    nMinVal = int.Parse(strVaules[i]);
            }
            return nMinVal;
        }


        public static int GetCycleStnInfo(string strEquNo, string strCmdMode)
        {
            int nResult = 0;
            string strEqu = strEquNo;
            string sSQL = string.Empty;
            string[] sData = new string[0];
            DataTable dtData = new DataTable();

            sSQL = string.Format("SELECT * FROM EQUPLCDATA WHERE EQUNO  ='{0}'", strEqu);
            if (Globals.DB.ExecGetTable(sSQL, ref dtData) == true)
            {
                foreach (DataRow dbRS in dtData.Rows)
                {
                    if (dbRS["SERIALNO"].ToString() == "2-3")
                    {
                        sData = dbRS["EQUPLCDATA"].ToString().Split(',');
                    }

                }
            }
            if (sData.Length >= 100)
            {
                //if()
                nResult = Int32.Parse(sData[87]) ^ 3;
            }
            return nResult;
        }



    }
}