///////////////////////////////////////////////////////////////////////////////////////////////////////////
// 查找注释有"[依赖项目]"标识的代码.则在新项目初始时必需检查其匹配性.
//
// [1].如果在项目中有增加字段或其它字符标识.那么也要在中文字段映射表中增加相应的映射.
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.Xml;
using ACTETHERLib;
using System.Configuration;
using System.Drawing;
using DadayBabyMainCtrl;
using Share;
using DB;
using System.Net;


/// <summary>
/// 全局调用处理函数/变量/结构
/// </summary>
public class Globals
{
    //变量///////////////////////////////////////////////////////////////////////////////////////////////////////////
    #region 基础数据
    /// <summary>
    /// 项目中文名[依赖项目]
    /// </summary>
    public static string PROJ_NAME = "";
    /// <summary>
    /// 当前登录用户号
    /// </summary>
    public static string USER_ID = string.Empty;
    /// <summary>
    /// 当前登录用户号
    /// </summary>
    public static string Exe_Path = string.Empty;
    /// <summary>
    /// 当前登录用户名称
    /// </summary>
    public static string USER_NAME = string.Empty;
    public static bool bLogin = true;
    public static string strEquNo = string.Empty;
    /// <summary>
    /// 默认超级用户及用户名
    /// </summary>
    public const string SUPER_ID = "mirle";
    public const string SUPER_USER = "mirle";
    /// <summary>
    /// 是否默认超级用户
    /// </summary>
    public static bool IS_SUPER_USER = false;
    public static string AppFlag = string.Empty;
    /// <summary>
    /// 数据库连接对象
    /// </summary>
    public static DbAccess DB;
    public static DbAccess DBF1;
    public static DbAccess DBW1;
    /// <summary>
    /// 当前数据库连接名称
    /// </summary>
    private static string DB_NAME = string.Empty;
    /// <summary>
    /// 日志文件名/路径
    /// </summary>
    public static string LOG_FILE = string.Empty;
    /// <summary>
    /// 导出资料保存路径
    /// </summary>
    public static string EXP_PATH = string.Empty;
    /// <summary>
    /// PLC 逻辑号
    /// </summary>
    public static string sFHPPLC = string.Empty;
    public static string sFOPPLC = string.Empty;
    public static string sFCJPLC = string.Empty;
    public static string sWHPPLC = string.Empty;
    public static string sWOPPLC = string.Empty;
    public static string sWCJPLC = string.Empty;
    /// <summary>
    /// PLC IP地址
    /// </summary>
    public static IPAddress ipFHPPLC = null;
    public static IPAddress ipFOPPLC = null;
    public static IPAddress ipFCJPLC = null;
    public static IPAddress ipWHPPLC = null;
    public static IPAddress ipWOPPLC = null;
    public static IPAddress ipWCJPLC = null;
    /// <summary>
    /// BCR 参数设置
    /// </summary>
    public static string sComBcrC101 = string.Empty;
    public static string sComBcrC102 = string.Empty;
    /// <summary>
    /// 程序动态错误信息
    /// </summary>
    private static string LOG_INFO = string.Empty;
    /// <summary>
    /// 储位排序规则
    /// </summary>
    public static string LOC_ORDER = " order by bay_y,row_x,loc ";
    public static string sSqlDte = "rtrim(convert(char, datepart(yy,getdate()))) + right('00' + rtrim(convert(char, datepart(mm,getdate()))), 2) + right('00' + rtrim(convert(char, datepart(dd,getdate()))), 2)";
    public static string sSqlDteL = "rtrim(convert(char, datepart(yy,getdate())))+ '-' + right('00' + rtrim(convert(char, datepart(mm,getdate()))), 2)+'-' + right('00' + rtrim(convert(char, datepart(dd,getdate()))), 2)";
    public static string sSqlTim = "right('00' + rtrim(convert(char, datepart(hh,getdate()))), 2) + ':' + right('00' + rtrim(convert(char, datepart(mi,getdate()))), 2) + ':' + right('00' + rtrim(convert(char, datepart(ss,getdate()))), 2)";
    public static string sEquDt = "rtrim(convert(char, datepart(yy,getdate()))) + '-' + right('00' + rtrim(convert(char, datepart(mm,getdate()))), 2) + '-'  + right('00' + rtrim(convert(char, datepart(dd,getdate()))), 2) + ' ' + " +
        "right('00' + rtrim(convert(char, datepart(hh,getdate()))), 2) + ':' + right('00' + rtrim(convert(char, datepart(mi,getdate()))), 2) + ':' + right('00' + rtrim(convert(char, datepart(ss,getdate()))), 2)";
    /// <summary>
    /// 数据库字段中英文映射哈希表y
    /// </summary>
    private static Hashtable hTable = null;

    #endregion
    //方法///////////////////////////////////////////////////////////////////////////////////////////////////////////

    #region 与项目自身无关功能函数.一般不做修改.

    /// <summary>
    /// 获取服务器当前日期(8位无分隔符)yyyyMMdd
    /// </summary>
    /// <returns></returns>
    public static string GetCurDate(DbAccess Db)
    {
        string strTime = string.Empty;
        string sSql = string.Empty;
        if (Db.DBType == eDBType.MSSQL
            || Db.DBType == eDBType.SYBASE)
        {
            sSql = "select rtrim(convert(char, datepart(yy,getdate()))) + '-' + right('00' + rtrim(convert(char, datepart(mm,getdate()))), 2) " +
                " + '-' + right('00' + rtrim(convert(char, datepart(dd,getdate()))), 2)";
        }
        else if (Db.DBType == eDBType.ORACLE)
        {
            sSql = "select to_char(sysdate,'yyyy-mm-dd') from dual";
        }
        else if (Db.DBType == eDBType.DB2)
        {
            sSql = "SELECT current date FROM sysibm.sysdummy1 ";
        }
        else
        {
            strTime = DateTime.Now.ToString("yyyyMMdd");   //不能识别则返回本地时间
            return strTime;
        }

        if (!Db.ExecGetValue(sSql, out strTime))
        {
            return string.Empty;
        }
        DateTime dt = DateTime.Parse(strTime);
        return GetDteFrmCtl(dt);
    }

    /// <summary>
    /// 获取服务器当前时间(8位有分隔符)HH:mm:ss
    /// </summary>
    /// <returns></returns>
    public static string GetCurTime(DbAccess Db)
    {
        string strTime = string.Empty;

        string sSql = string.Empty;
        if (Db.DBType == eDBType.MSSQL
            || Db.DBType == eDBType.SYBASE)
        {
            //sSql = "select convert(varchar(100), getdate(), 108)";
            sSql = "select right('00' + rtrim(convert(char, datepart(hh,getdate()))), 2) + ':' + right('00' + rtrim(convert(char, datepart(mi,getdate()))), 2) + ':' + right('00' + rtrim(convert(char, datepart(ss,getdate()))), 2)";
        }
        else if (Db.DBType == eDBType.ORACLE)
        {
            sSql = "select to_char(sysdate,'hh24:mm:ss') from dual";
        }
        else if (Db.DBType == eDBType.DB2)
        {
            sSql = "SELECT current time FROM sysibm.sysdummy1 ";
        }
        else
        {
            strTime = DateTime.Now.ToString("HH:mm:ss");
        }

        if (!Db.ExecGetValue(sSql, out strTime))
        {
            return string.Empty;
        }

        DateTime dt = DateTime.Parse(strTime);
        return GetTimFrmCtl(dt);
    }

    /// <summary>
    /// 获取服务器当前日期(10位有分隔符)yyyy-MM-dd
    /// </summary>
    /// <returns></returns>
    public static string GetCurDateL(DbAccess Db)
    {
        string strTime = string.Empty;

        string sSql = string.Empty;
        if (Db.DBType == eDBType.MSSQL
            || Db.DBType == eDBType.SYBASE)
        {
            sSql = "select rtrim(convert(char, datepart(yy,getdate()))) + '-' + right('00' + rtrim(convert(char, datepart(mm,getdate()))), 2) " +
                " + '-' + right('00' + rtrim(convert(char, datepart(dd,getdate()))), 2)";
        }
        else if (Db.DBType == eDBType.ORACLE)
        {
            sSql = "select to_char(sysdate,'yyyy-mm-dd') from dual";
        }
        else
        {
            strTime = DateTime.Now.ToString("yyyy-MM-dd");   //不能识别则返回本地时间
        }

        if (!Db.ExecGetValue(sSql, out strTime))
        {
            return string.Empty;
        }

        return strTime;
    }

    /// <summary>
    /// 获取服务器当前日期+时间
    /// </summary>
    /// <returns></returns>
    public static string GetCurAllTime(DbAccess Db)
    {
        string strTime = string.Empty;

        string sSql = string.Empty;
        if (Db.DBType == eDBType.MSSQL
            || Db.DBType == eDBType.SYBASE)
        {
            sSql = "select rtrim(convert(char, datepart(yy,getdate()))) + '-' + right('00' + rtrim(convert(char, datepart(mm,getdate()))), 2) + '-' + right('00' + rtrim(convert(char, datepart(dd,getdate()))), 2)"
                + " + ' ' + right('00' + rtrim(convert(char, datepart(hh,getdate()))), 2) + ':' + right('00' + rtrim(convert(char, datepart(mi,getdate()))), 2) + ':' + right('00' + rtrim(convert(char, datepart(ss,getdate()))), 2)";
        }
        else if (Db.DBType == eDBType.ORACLE)
        {
            sSql = "select to_char(sysdate,'yyyy-mm-dd hh24:mm:ss') from dual";
        }
        else
        {
            strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");   //不能识别则返回本地时间
        }

        if (!Db.ExecGetValue(sSql, out strTime))
        {
            return string.Empty;
        }

        return strTime;
    }

    public static string GetTimFrmCtl(DateTime dt)
    {
        return dt.Hour.ToString("00") +":" + dt.Minute.ToString("00") +":"+ dt.Second.ToString("00");
    }

    public static string GetDteFrmCtl(DateTime dt)
    {
        return dt.Year.ToString("0000") + dt.Month.ToString("00") + dt.Day.ToString("00");
    }

    public static string GetDteTimFrmCtl(DateTime dt)
    {
        return dt.Year.ToString("0000") + dt.Month.ToString("00") + dt.Day.ToString("00") + " " 
            + dt.Hour.ToString("00") + ":" + dt.Minute.ToString("00") + ":" + dt.Second.ToString("00");
    }

    public static string GetDteFrmCtlL(DateTime dt)
    {
        return dt.Year.ToString("0000") + "-" + dt.Month.ToString("00") + "-" + dt.Day.ToString("00");
    }

    public static string GetDteTimFrmCtlL(DateTime dt)
    {
        return dt.Year.ToString("0000") + "-" + dt.Month.ToString("00") + "-" + dt.Day.ToString("00") + " "
            + dt.Hour.ToString("00") + ":" + dt.Minute.ToString("00") + ":" + dt.Second.ToString("00");
    }

    /// <summary>
    /// 获取本地当前日期(8位无分隔符)yyyyMMdd
    /// </summary>
    /// <returns></returns>
    public static string GetLocalCurDate()
    {
        return DateTime.Now.ToString("yyyyMMdd");
    }

    /// <summary>
    /// 设置错误信息(程序中有需要记录错误的地方都要调用此函数)
    /// </summary>
    /// <param name="st">错误来源</param>
    /// <param name="strErrInfo">错误信息</param>
    public static void SetErrInfo(StackTrace st, string strErrInfo)
    {
        StackFrame sf = st.GetFrame(0);

        LOG_INFO = string.Format("{0}\r\nStack trace for current level: {1}\r\nFile: {2}\r\nMethod: {3}\r\nLine Number: {4}"
            , strErrInfo, st.ToString(), sf.GetFileName(), sf.GetMethod().Name, sf.GetFileLineNumber());
        ShareData.LogEvent(Globals.LOG_FILE, Globals.LOG_INFO);	// 写入日志文件
    }

    /// <summary>
    /// 验证控件的Text属性是否为空
    /// </summary>
    /// <param name="controls">控件集</param>
    /// <returns>是否有空值</returns>
    public static bool CtlValidate(params Control[] controls)
    {
        foreach (Control con in controls)
        {
            if (con.Text == null || con.Text.Trim() == string.Empty)
                return false;
        }
        return true;
    }

    /// <summary>
    /// 清空控件数据
    /// </summary>
    /// <param name="controls">控件集</param>
    /// <returns>是否成功</returns>
    public static bool CtlClear(params Control[] controls)
    {
        foreach (Control con in controls)
        {
            if (con is CheckBox)
                ((CheckBox)con).Checked = false;
            else
                con.Text = null;    //不能使用string.empty
        }
        return true;
    }

    /// <summary>
    /// 改变控件有效状态
    /// </summary>
    /// <param name="isEn">是否启true还是禁用false</param>
    /// <param name="controls">控件集</param>
    /// <returns>是否全部成功</returns>
    public static bool CtlState(bool isEn, params Control[] controls)
    {
        foreach (Control con in controls)
        {
            con.Enabled = isEn;
        }
        return true;
    }
    // 添加自定义功能函数......
    #endregion

    #region 与项目自身耦合较低的功能函数.如果使用模板构建.则一般无需改动.
    /// <summary>
    /// 初始化应用程序
    /// </summary>
    public static bool InitApp()
    {
        try
        {
            // 读取XML配置文件的数据
            XmlDocument XmlConfig = new XmlDocument();
            XmlConfig.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            XmlNodeList XmlList;
            XmlList = XmlConfig.GetElementsByTagName("appSettings");
            XmlNode node = XmlList[0];

            string DbProvider;
            string DbCnStr;    

            // 初始数据库
            DbProvider = "System.Data.SqlClient";
            DbCnStr = string.Format("Data Source={0};Initial Catalog={1};User ID=WCS;Password=WCS,123; Connection Timeout=5", 
                node["ServerName"].InnerText, node["DatabaseName"].InnerText);
            DB = new DbAccess(DbProvider, DbCnStr);
            DBF1 = new DbAccess(DbProvider, DbCnStr);
            DBW1 = new DbAccess(DbProvider, DbCnStr);
            
            // 其它数据
            LOG_FILE = node["LogEvent"].InnerText;
            Log.strLogPath = node["LogPath"].InnerText;
            AppFlag = node["AppFlag"].InnerText;

            // PLC参数
            //GetPlcPramas(node["FHPPLC"].InnerText, out ipFHPPLC, out sFHPPLC);
            //GetPlcPramas(node["FOPPLC"].InnerText, out ipFOPPLC, out sFOPPLC);
            GetPlcPramas(node["FCJPLC"].InnerText, out ipFCJPLC, out sFCJPLC);
            //GetPlcPramas(node["WHPPLC"].InnerText, out ipWHPPLC, out sWHPPLC);
            //GetPlcPramas(node["WOPPLC"].InnerText, out ipWOPPLC, out sWOPPLC);
            //GetPlcPramas(node["WCJPLC"].InnerText, out ipWCJPLC, out sWCJPLC);

            // BCR参数
            sComBcrC101 = node["BcrC101"].InnerText;
            sComBcrC102 = node["BcrC102"].InnerText;

            Exe_Path = Application.StartupPath;
        }
        catch (Exception appEx)
        {
            Log.WriteLog(appEx);
            Log.CloseLog();
            return false;
        }

        return true;
    }

    /// <summary>
    /// 获取相应类别单据的流水号.单据类型+服务器时间+5位序号(当已使用当前流水号后.要记得再以参数为true调用一次更新流水号)
    /// </summary>
    /// <param name="sNoType">单据类型</param>
    /// <param name="bIsUpdate">是否更新(流水号+1)</param>
    /// <returns>当设置不更新流水号时返回当前流水号.当设置更新流水号时 返回新的流水号.失败返回空值</returns>
    public static string GetSno(bool bIsUpdate = true)
    {
        string strNo = string.Empty;

        // 先取出当前单据中的流水号
        string sSql = string.Format("select sno from {0} where sno_type = 'C'", TableName.SNO_CTL);
        if (!Globals.DB.ExecGetValue(sSql, out strNo))
        {
            throw new ApplicationException("获取流水号失败!");
        }

        int nSno = Convert.ToInt32(strNo);
        strNo = nSno.ToString("00000");	// 格式化命令号

        // 判断命令号是否可用
        bool IsReset = false;	// 是否重新获取命令号 而不以单据中的序号为准

        //如果命令序号max 则reset value 1
        if (nSno >= 29999)
        {
            IsReset = true;
        }
        else
        {
            // 验证此命令号是否已存在
            sSql = string.Format("select 1 from {0} where cmd_sno = '{1}'", TableName.CMD_MST, strNo);
            if (Globals.DB.ExecQuery(sSql))
            {
                IsReset = true;
            }
        }

        // 如果需要重新获取
        if (IsReset)
        {
            try
            {
                // 循环所有命令序号 获取可用命令号
                nSno = Convert.ToInt32(strNo);	// 当前使用的序号
                int nIdx = 1;	// 循环次数
                while (nIdx < 1000)
                {
                    // 超过上限则设置为1开始
                    if (nSno >= 29999) nSno = 1;

                    strNo = nSno.ToString("00000");
                    // 查询出一个有效命令号(查询下一个命令号是否正没有执行或没有记录则可以使用)
                    sSql = string.Format("select cmd_sno from {0} where cmd_sts < '8' and cmd_sno = '{1}'", TableName.CMD_MST, strNo);
                    if (!Globals.DB.ExecQuery(sSql))
                    {
                        // 判断是否是有此命令记录但已完成还是无此命令记录可以使用
                        sSql = string.Format("if exists(select cmd_sno from {0} where cmd_sno = '{1}') delete from {0} where cmd_sno = '{1}'", TableName.CMD_DTL, strNo);
                        Globals.DB.ExecQuery(sSql);	// 防止有原记录删除
                        sSql = string.Format("if exists(select cmd_sno from {0} where cmd_sno = '{1}') delete from {0} where cmd_sno = '{1}'", TableName.CMD_MST, strNo);
                        Globals.DB.ExecQuery(sSql);	// 防止有原记录删除
                        // 更新单据流水号
                        sSql = string.Format("update {0} set sno = {1}, trn_dte = {2} where sno_type = 'C'", TableName.SNO_CTL, nSno, sSqlDte);
                        if (!Globals.DB.ExecSql(sSql))
                        {
                            return string.Empty;
                        }

                        break;
                    }
                    nSno++;	// 判断序号递增
                    nIdx++;	// 循环次数递增
                }

                if (strNo == string.Empty)
                {
                    // 获取命令号失败
                    //MsgBox.Error(StrTables.MSG_GET_CMD_FAILED);
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                //MsgBox.Error(StrTables.MSG_GET_CMD_FAILED);
                Globals.SetErrInfo(new StackTrace(new StackFrame(true)), ex.Message + StrTables.MSG_GET_CMD_FAILED);
                return string.Empty;
            }
        }
        // 更新流水号
        if (bIsUpdate)
        {
            sSql = string.Format("update {0} set sno = sno + 1, trn_dte = {1} where sno_type = 'C'", TableName.SNO_CTL, sSqlDte);
            if (!Globals.DB.ExecSql(sSql))
            {
                throw new ApplicationException(StrTables.MSG_GET_CMD_FAILED);
                //return string.Empty;
            }
        }

        return strNo;
    }

    /// <summary>
    /// 获取PLC参数信息
    /// </summary>
    /// <param name="objPramas"></param>
    /// <param name="hIp"></param>
    /// <param name="strLogicalStnNo"></param>
    public static void GetPlcPramas(string objPramas, out IPAddress hIp, out string strLogicalStnNo)
    {
        hIp = null;
        strLogicalStnNo = string.Empty;

        try
        {
            string[] pPras = objPramas.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            string[] pIps = pPras[0].Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            hIp = new IPAddress(new Byte[] { Byte.Parse(pIps[0]), Byte.Parse(pIps[1]), Byte.Parse(pIps[2]), Byte.Parse(pIps[3]) });
            strLogicalStnNo = pPras[1];
        }
        catch (Exception ex)
        {
            Log.WriteLog(ex);
        }
    }

    /// <summary>
    /// 映射英文名字段到中文名(主要用于显示数据)
    /// </summary>
    /// <param name="dgv">源数据表控件 将其列表名译成中文</param>
    public static void MappedFieldCN(DataGridView dgv)
    {
        if (null == dgv)
            return;

        try
        {
            foreach (DataGridViewColumn dCol in dgv.Columns)
            {
                if (hTable.ContainsKey(dCol.HeaderText))
                    dCol.HeaderText = hTable[dCol.HeaderText].ToString();
            }
        }
        catch (Exception ex)
        {
            Globals.SetErrInfo(new StackTrace(new StackFrame(true)), ex.Message);
        }
    }
   
    /// <summary>
    /// 映射英文名数据到中文名(主要用于显示数据为中文名)
    /// </summary>
    /// <param name="dgv">源数据表控件 将其列表数据译成中文</param>
    /// <param name="column">要操作的列名以及要使用的哈希表(因为数据列会有重覆 所以不同的数据列使用了不同的哈希表 这个参数不仅指定了要操作的列 也设置了操作包含此名称的哈希表.)</param>
    /// <param name="ht">手动指定源哈希表(因为系统中所有定义的哈希表中可能有重复信息 所以这里允许手动指定搜索哪个哈希表)</param>
    public static void MappedValueCN(DataGridView dgv, string column, Hashtable ht)
    {
        if (null == dgv)
            return;

        try
        {
            foreach (DataGridViewRow dRow in dgv.Rows)
            {
                if (ht.ContainsKey(dRow.Cells[column].Value))
                    dRow.Cells[column].Value = ht[dRow.Cells[column].Value].ToString();
            }
        }
        catch (Exception ex)
        {
            Log.WriteLog(ex);
        }
    }
    public static  string GetDestinateStn(string strStnNo)
    {
        string strRes = string.Empty;
        switch (strStnNo)
        {
            case "B123":
                strRes = "1";
                break;
            case "B134":
                strRes = "2";
                break;
            case "B144":
                strRes = "3";
                break;
            case "B154":
                strRes = "4";
                break;
            case "B164":
                strRes = "5";
                break;
            case "A51":
                strRes = "1";
                break;
            case "A41":
                strRes = "2";
                break;
            case "A31":
                strRes = "3";
                break;
            case "A21":
                strRes = "4";
                break;
            case "A11":
                strRes = "5";
                break;
            case "C51":
                strRes = "1";
                break;
            case "C41":
                strRes = "2";
                break;
            case "C31":
                strRes = "3";
                break;
            case "C21":
                strRes = "4";
                break;
            case "C11":
                strRes = "5";
                break;
        }
        return strRes;
    }

    public static bool DealWithCycle(string strCmdSno)
    {
        string strSql = string.Empty;
        DataTable dtCmdMst = new DataTable();
        DataTable dtCmdDtl = new DataTable();
        bool bResult = false;
        try
        {
            strSql = string.Format("select * from cmd_mst where cmd_sno = '{0}' and io_type = '32' and cmd_mode = '3'", strCmdSno);
            Globals.DB.ExecGetTable(strSql, ref dtCmdMst);
            if (dtCmdMst.Rows.Count <= 0) return bResult;
            strSql = string.Format("select * from cmd_dtl where cmd_sno = '{0}'", strCmdSno);
            Globals.DB.ExecGetTable(strSql, ref dtCmdDtl);

            Globals.DB.BeginTran(true);
            foreach (DataRow drCmdMst in dtCmdMst.Rows)
            {
                //更新库位状态信息
                strSql = string.Format("update  loc_mst set old_sts = loc_sts, loc_sts = 'N',plt_id = '' where loc = '{0}'", drCmdMst["Loc"]);
                if (!Globals.DB.ExecSql(strSql)) throw new Exception();
                foreach (DataRow drCmdDtl in dtCmdDtl.Rows)
                {
                    strSql = string.Format("delete from loc_dtl where loc = '{0}' and lot_no = '{1}'", drCmdMst["Loc"], drCmdDtl["lot_no"]);
                    if (!Globals.DB.ExecSql(strSql)) throw new Exception();
                }
                //更新出库命令信息
                strSql = string.Format("update cmd_mst set cmd_sts = '0',io_type = '31',loc = '' where cmd_sno = '{0}'", drCmdMst["cmd_sno"]);
                if (!Globals.DB.ExecSql(strSql)) throw new Exception();
                //更新equcmd
                strSql = string.Format(@"update EquCmd set ReNewFlag='F' where CmdSno='{0}'", drCmdMst["Cmd_Sno"]);
                if (!Globals.DB.ExecSql(strSql)) throw new Exception();
                bResult = true;
            }
            Globals.DB.CommitTran();
            return bResult;
        }
        catch (Exception ex)
        {
            Globals.DB.RollbackTran();
            Log.WriteLog(ex.Message);
            return false;
        }
    }

    #endregion

    #region 与项目自身耦合较高的功能函数.如果使用模板构建.一般需要少量改动且很可能会增加数据.
    private static void SetHashTable()
    {
        //数据库字段映射
        hTable = new Hashtable(System.StringComparer.Create(System.Globalization.CultureInfo.CurrentCulture, true));  //构造中加入忽略大小写

        hTable.Add(FieldName.USER_ID, FieldName.USER_ID_CN);
        hTable.Add(FieldName.USER_NAME, FieldName.USER_NAME_CN);
        hTable.Add(FieldName.USER_PWD, FieldName.USER_PWD_CN);

        hTable.Add(FieldName.CMD_SNO, FieldName.CMD_SNO_CN);
        hTable.Add(FieldName.CMD_STS, FieldName.CMD_STS_CN);
        hTable.Add(FieldName.CMD_MODE, FieldName.CMD_MODE_CN);
        hTable.Add(FieldName.IO_TYPE, FieldName.IO_TYPE_CN);
        hTable.Add(FieldName.CRT_DTE, FieldName.CRT_DTE_CN);
        hTable.Add(FieldName.CRT_TIM, FieldName.CRT_TIM_CN);
        hTable.Add(FieldName.EXP_DTE, FieldName.EXP_DTE_CN);
        hTable.Add(FieldName.EXP_TIM, FieldName.EXP_TIM_CN);
        hTable.Add(FieldName.END_DTE, FieldName.END_DTE_CN);
        hTable.Add(FieldName.END_TIM, FieldName.END_TIM_CN);

        hTable.Add(FieldName.LOC, FieldName.LOC_CN);
        hTable.Add(FieldName.LOC_STS, FieldName.LOC_STS_CN);
        hTable.Add(FieldName.LOC_OSTS, FieldName.LOC_OSTS_CN);
        hTable.Add(FieldName.ROW_X, FieldName.ROW_X_CN);
        hTable.Add(FieldName.BAY_Y, FieldName.BAY_Y_CN);
        hTable.Add(FieldName.LVL_Z, FieldName.LVL_Z_CN);

        hTable.Add(FieldName.STN_NO, FieldName.STN_NO_CN);
        hTable.Add(FieldName.NEW_LOC, FieldName.NEW_LOC_CN);

        hTable.Add(FieldName.Loc_Rate, FieldName.Loc_Rate_CN);
        hTable.Add(FieldName.Prty, FieldName.Prty_CN);
        hTable.Add(FieldName.Trace, FieldName.Trace_CN);
        hTable.Add(FieldName.Department, FieldName.Department_CN);
        hTable.Add(FieldName.Weight, FieldName.Weight_CN);
        hTable.Add(FieldName.Prog_ID, FieldName.Prog_ID_CN);
        hTable.Add(FieldName.Cyc_No, FieldName.Cyc_No_CN);
        hTable.Add(FieldName.Code, FieldName.Code_CN);
        hTable.Add(FieldName.Def, FieldName.Def_CN);
        hTable.Add(FieldName.Occ_Dte, FieldName.Occ_Dte_CN);
        hTable.Add("EquNo", "设备");
        hTable.Add("AlarmDesc", "异常描述");
        hTable.Add("STRDT", "发生时间");
    }

    #endregion
}
