///////////////////////////////////////////////////////////////////////////////////////////////////////////
// 定义全局数据
//
// [1].如果在项目中有增加字段或其它字符标识.那么也要在中文字段映射表中增加相应的映射.
///////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Data;

/// <summary>
/// 字符表
/// </summary>
public class StrTables
{
    /////////////////
    //消息字符
    /////////////////

    //共用
    public const string MSG_NOT_FULL = "请将内容填写完整!";
    public const string MSG_INPUT_ERROR = "输入错误!请重新检查输入内容.";

    public const string MSG_EXIST_DATA = "已经存在数据!";
    public const string MSG_NONE_DATA = "没有发现数据!";
    public const string MSG_DISP_FINISHED = "处理成功!";
    public const string MSG_DISP_FAILED = "处理失败!";
    public const string MSG_NONE_WORK = "不允许操作!";
    public const string MSG_SURE = "是否确认操作?";
    public const string MSG_NONE_INFO = "数据不匹配超出指定范围!";
    public const string MSG_PARA_ERROR = "参数不正确!";

    public const string MSG_CREDATA_FAILED = "新增数据失败!";
    public const string MSG_UPDATA_FAILED = "修改数据失败!";
    public const string MSG_DELDATA_FAILED = "删除数据失败!";

    public const string MSG_IS_DEL = "是否确认删除?";
    public const string MSG_VALID_FAILED = "验证数据失败!";
    public const string MSG_UPDATE_FAILED = "更新数据失败!";
    //专用
    public const string MSG_INIT_FAILED = "初始信息及配置文件失败!";
    public const string MSG_LOGON_FAILED = "用户名不存在或密码错误.";
    public const string MSG_EXIT = "是否确认退出程序?";
    public const string MSG_NONE_LOC_EN = "没有可用储位!";
    public const string MSG_NONE_STOCK = "没有库存!";
    //public const string MSG_NONE_CYC		 = "没有物料可盘点!";
    public const string MSG_CONF_NOT_DB = "储位配置信息与数据库信息不匹配!请修改成一致状态再重试.";
    public const string MSG_EXPORT_FINISHED = "数据导出成功!\r\n文件保存至 {0}";
    public const string MSG_EXPORT_FAILED = "数据导出失败!";
    public const string MSG_NOT_MODIFY_CMD = "不能对该命令进行修改!";
    public const string MSG_RATE_MAX = "储位使用率已经最大!";
    public const string MSG_DB_CONN_FAILED = "数据库连接失败!\r\n请检查数据库是否开启或配置文件中的连接字符串是否设置正确.";

    public const string MSG_GET_CMD_FAILED = "获取命令号失败!\r\n请完成所有现执行命令.然后再\"系统维护\"模块中作一次命令清除操作.";
    public const string MSG_CRE_CMD_MST_FAILED = "生成命令主表失败!";
    public const string MSG_CRE_CMD_DTL_FAILED = "生成命令明细子表失败!";
    public const string MSG_UPD_LOC_STS_FAILED = "更新储位状态失败!";
    public const string MSG_UPD_ACT_FAILED = "更新预约数量失败!";
    public const string MSG_TRN_LOG_FAILED = "写异动档失败!";

    public const string UP_LOC_DTL_QTY_FAILED = "更新库存数量失败!";
    public const string UP_CYCLE_FAILED = "更新盘点单失败!";

    //过账使用
    public const string MSG_UPDATE_CMD_OPENED = "过账程序已经打开或刚刚被关闭.请检查其它计算机是否已经运行或稍后重试.";

    /////////////////
    //一般字符
    /////////////////
    public const string MEMBER_VAL = "VAL";	// 列表值标识
    public const string MEMBER_DSP = "DSP";	// 列表显示标识

    public const string DATE = "yyyyMMdd";
    public const string TIME = "HH:mm:ss";

    //储位状态
    public const string LOCSTS_S_CN = "库存储位S";
    public const string LOCSTS_E_CN = "空栈板E";
    public const string LOCSTS_N_CN = "空储位N";

    public const string LOCSTS_I_CN = "入库预约I";
    public const string LOCSTS_O_CN = "出库预约O";
    public const string LOCSTS_C_CN = "盘点预约C";
    public const string LOCSTS_P_CN = "调账预约P";
    public const string LOCSTS_X_CN = "禁用储位X";
    //入库模式
    public const string CMD_MODE_IN = "入库1";
    public const string CMD_MODE_OUT = "出库2";
    public const string CMD_MODE_CHK = "检料3";
    public const string CMD_MODE_L2L = "库对库4";
    public const string CMD_MODE_S2S = "站对站5";  
}

/// <summary>
/// 数据库表名[依赖项目]
/// </summary>
public class TableName
{
    /// <summary>
    /// 用户表
    /// </summary>
    public const string USR_INF = "USR_INF";
    /// <summary>
    /// 储位主表
    /// </summary>
    public const string LOC_MST = "LOC_MST";
    /// <summary>
    /// 命令主表
    /// </summary>
    public const string CMD_MST = "CMD_MST";
    /// <summary>
    /// 命令明细表
    /// </summary>
    public const string CMD_DTL = "CMD_DTL";
    /// <summary>
    /// 历史命令主表
    /// </summary>
    public const string CMD_MST_HIS = "CMD_MST_HIS";
    /// <summary>
    /// 流水号控制表
    /// </summary>
    public const string SNO_CTL = "SNO_CTL";
    /// <summary>
    /// 运行控制档
    /// </summary>
    public const string CtrlHs = "CtrlHs";
    /// <summary>
    /// PLC地址表
    /// </summary>
    public const string Stn_Add = "Stn_Add";
    /// <summary>
    /// 设备命令表
    /// </summary>
    public const string EquCmd = "EquCmd";
}

/// <summary>
/// 字段名[依赖项目]
/// </summary>
public class FieldName
{
    /// <summary>
    /// 用户号
    /// </summary>
    public const string USER_ID = "USER_ID";
    public const string USER_ID_CN = "用户号";
    public const string USER_ID_CN_WORK = "操作人员";
    /// <summary>
    /// 用户名称
    /// </summary>
    public const string USER_NAME = "USER_NAME";
    public const string USER_NAME_CN = "用户名";
    /// <summary>
    /// 用户密码
    /// </summary>
    public const string USER_PWD = "USER_PWD";
    public const string USER_PWD_CN = "用户密码";
    /// <summary>
    /// 命令号
    /// </summary>
    public const string CMD_SNO = "CMD_SNO";
    public const string CMD_SNO_CN = "命令号";
    /// <summary>
    /// 命令状态
    /// </summary>
    public const string CMD_STS = "CMD_STS";
    public const string CMD_STS_CN = "命令状态";
    /// <summary>
    /// 命令模式
    /// </summary>
    public const string CMD_MODE = "CMD_MODE";
    public const string CMD_MODE_CN = "命令模式";
    /// <summary>
    /// 作业形态
    /// </summary>
    public const string IO_TYPE = "IO_TYPE";
    public const string IO_TYPE_CN = "作业形态";
    /// <summary>
    /// 产生日期
    /// </summary>
    public const string CRT_DTE = "CRT_DTE";
    public const string CRT_DTE_CN = "创建日期";
    /// <summary>
    /// 产生时间
    /// </summary>
    public const string CRT_TIM = "CRT_TIM";
    public const string CRT_TIM_CN = "创建时间";
    /// <summary>
    /// 执行日期
    /// </summary>
    public const string EXP_DTE = "EXP_DTE";
    public const string EXP_DTE_CN = "执行日期";
    /// <summary>
    /// 执行时间
    /// </summary>
    public const string EXP_TIM = "EXP_TIM";
    public const string EXP_TIM_CN = "执行时间";
    /// <summary>
    /// 结束日期
    /// </summary>
    public const string END_DTE = "END_DTE";
    public const string END_DTE_CN = "结束日期";
    /// <summary>
    /// 结束时间
    /// </summary>
    public const string END_TIM = "END_TIM";
    public const string END_TIM_CN = "结束时间";
    /// <summary>
    /// 储位号
    /// </summary>
    public const string LOC = "LOC";
    public const string LOC_CN = "储位号";
    /// <summary>
    /// 储位状态
    /// </summary>
    public const string LOC_STS = "LOC_STS";
    public const string LOC_STS_CN = "储位状态";
    /// <summary>
    /// 储位原状态
    /// </summary>
    public const string LOC_OSTS = "LOC_OSTS";
    public const string LOC_OSTS_CN = "储位原状态";
    /// <summary>
    /// 列
    /// </summary>
    public const string ROW_X = "ROW_X";
    public const string ROW_X_CN = "列";
    /// <summary>
    /// 行
    /// </summary>
    public const string BAY_Y = "BAY_Y";
    public const string BAY_Y_CN = "行";
    /// <summary>
    /// 层
    /// </summary>
    public const string LVL_Z = "LVL_Z";
    public const string LVL_Z_CN = "层";
    /// <summary>
    /// 站号
    /// </summary>
    public const string STN_NO = "STN_NO";
    public const string STN_NO_CN = "站号";
    /// <summary>
    /// 新储位号
    /// </summary>
    public const string NEW_LOC = "NEW_LOC";
    public const string NEW_LOC_CN = "新储位号";
    /// <summary>
    /// 移动日期
    /// </summary>
    public const string Mov_Dte = "Mov_Dte";
    public const string Mov_Dte_CN = "移动日期";
    /// <summary>
    /// 移动时间
    /// </summary>
    public const string Mov_Tim = "Mov_Tim";
    public const string Mov_Tim_CN = "移动时间";
    /// <summary>
    /// 地址名称
    /// </summary>
    public const string Adr_Name = "Adr_Name";
    public const string Adr_Name_CN = "地址名称";
    /// <summary>
    /// 编号
    /// </summary>
    public const string Plc_ID = "Plc_Id";
    public const string Plc_ID_CN = "PLC编号";
    /// <summary>
    /// 站地址
    /// </summary>
    public const string Stn_Adr = "Stn_Adr";
    public const string Stn_Adr_CN = "站地址";
    /// <summary>
    /// 称重
    /// </summary>
    public const string Weight = "Weight";
    public const string Weight_CN = "称重";
    /// <summary>
    /// 预约主机
    /// </summary>
    public const string Alo_Crn = "Alo_Crn";
    public const string Alo_Crn_CN = "预约主机";
    /// <summary>
    ///储位使用率
    /// </summary>
    public const string Loc_Rate = "Loc_Rate";
    public const string Loc_Rate_CN = "储位使用率";
    /// <summary>
    ///部门
    /// </summary>
    public const string Department = "Department";
    public const string Department_CN = "部门";
    /// <summary>
    /// 流水号
    /// </summary>
    public const string Trace = "Trace";
    public const string Trace_CN = "流程码";
    /// <summary>
    /// 优先级
    /// </summary>
    public const string Prty = "Prty";
    public const string Prty_CN = "优先级";
    /// <summary>
    // 程序代码
    /// </summary>
    public const string Prog_ID = "Prog_ID";
    public const string Prog_ID_CN = "程序代码";
    /// <summary>
    // 盘点单号
    /// </summary>
    public const string Cyc_No = "Cyc_No";
    public const string Cyc_No_CN = "盘点单号";
    /// <summary>
    // 代码
    /// </summary>
    public const string Code = "Code";
    public const string Code_CN = "代码";
    /// <summary>
    // 定义
    /// </summary>
    public const string Def = "Def";
    public const string Def_CN = "定义";
    /// <summary>
    // 发生时间
    /// </summary>
    public const string Occ_Dte = "Occ_Dte";
    public const string Occ_Dte_CN = "发生时间";
}

/// <summary>
/// 命令状态
/// </summary>
public class cCmdSts
{
    /// <summary>
    /// 等待状态0
    /// </summary>
    public const string Wait = "0";
    public const string Wait_CN = "等待状态";
    /// <summary>
    /// 正在处理1
    /// </summary>
    public const string Proc = "1";
    public const string Proc_CN = "正在处理";
    /// <summary>
    /// 正常完成3
    /// </summary>
    public const string Fini = "3";
    public const string Fini_CN = "正常完成";
    /// <summary>
    /// 地上盘强制完成5
    /// </summary>
    public const string FFini = "5";
    public const string FFini_CN = "强制完成";
    /// <summary>
    /// 地方盘取消执行7
    /// </summary>
    public const string Cacl = "7";
    public const string Cacl_CN = "取消执行";
}

/// <summary>
/// 主机命令状态
/// </summary>
public class cEquSts
{
    /// <summary>
    /// 等待状态0
    /// </summary>
    public const string Init = "0";
    public const string Init_CN = "等待状态";
    /// <summary>
    /// 正常完成9
    /// </summary>
    public const string Fin = "9";
    public const string Fin_CN = "正常完成";
    /// <summary>
    /// 正常取消8
    /// </summary>
    public const string CanCel = "8";
    public const string CanCel_CN = "正常取消";
    /// <summary>
    /// 强制完成7
    /// </summary>
    public const string FFin = "7";
    public const string FFin_CN = "强制完成";
    /// <summary>
    /// 强制取消6
    /// </summary>
    public const string FCanCel = "6";
    public const string FCanCel_CN = "强制取消";
}

/// <summary>
/// 小车命令状态
/// </summary>
public class cSlvSts
{
    /// <summary>
    /// 等待状态
    /// </summary>
    public const string Init = "0";
    public const string Init_CN = "等待状态";
    /// <summary>
    /// 命令已下达起始站口
    /// </summary>
    public const string Write = "1";
    public const string Write_CN = "命令已下达起始站口";
    /// <summary>
    /// 车子在站口执行搬运动作
    /// </summary>
    public const string ArrStn = "2";
    public const string ArrStn_CN = "车子在站口执行搬运动作";
    /// <summary>
    /// 货物在车子上
    /// </summary>
    public const string Loading = "3";
    public const string Loading_CN = "货物在车子上";
    /// <summary>
    /// 已到达目的站口,命令结束
    /// </summary>
    public const string Fin = "9";
    public const string Fin_CN = "命令结束";
    /// <summary>
    /// PLC判读错误
    /// </summary>
    public const string ChkErr = "5";
    public const string ChkErr_CN = "PLC判读错误";
    /// <summary>
    /// 命令取消中
    /// </summary>
    public const string CanCelling = "6";
    public const string CanCelling_CN = "命令取消中";
    /// <summary>
    /// 命令取消
    /// </summary>
    public const string CanCel = "C";
    public const string CanCel_CN = "命令取消";
    /// <summary>
    /// 写入站口失败
    /// </summary>
    public const string WrtErr = "E";
    public const string WrtErr_CN = "写入站口失败";
}

/// <summary>
/// 主机信息
/// </summary>
public struct sCraneInfo
{
    public string sCmdSno;
    public string sCmdMode;
    public string sStatus;
    public bool bError;
}

//枚举数据///////////////////////////////////////////////////////////////////////////////////////////////////////////

public enum eCmdMode
{
    /// <summary>
    /// None0
    /// </summary>
    None = 0,
    /// <summary>
    /// 入库1
    /// </summary>
    In = 1,
    /// <summary>
    /// 出库2
    /// </summary>
    Out = 2,
    /// <summary>
    /// 检料3
    /// </summary>
    Chk = 3,
    /// <summary>
    /// 站对站4
    /// </summary>
    S2S = 4,
    /// <summary>
    /// 库对库5
    /// </summary>
    L2L = 5
}

/// <summary>
/// 优先码
/// </summary>
public enum ePRI
{
    /// <summary>
    /// 未定义0
    /// </summary>
    None = 0,
    /// <summary>
    /// 库对库2(最快)
    /// </summary>
    Loc = 2,
    /// <summary>
    /// 出库3
    /// </summary>
    Out = 3,
    /// <summary>
    /// 裁剪3
    /// </summary>
    Cut = 3,
    /// <summary>
    /// 入库4
    /// </summary>
    In = 4,
    /// <summary>
    /// Other5(一般)
    /// </summary>
    Other = 5,
    /// <summary>
    /// 盘点6
    /// </summary>
    Cycle = 6,
    /// <summary>
    /// 站对站7(最慢)
    /// </summary>
    S2S = 7
}

/// <summary>
/// 储位状态
/// </summary>
public enum eLocSts
{
    /// <summary>
    /// 空储位N
    /// </summary>
    N,
    /// <summary>
    /// 空栈板储位E
    /// </summary>
    E,
    /// <summary>
    /// 库存储位S
    /// </summary>
    S,
    /// <summary>
    /// 入库预约I
    /// </summary>
    I,
    /// <summary>
    /// 出库预约O
    /// </summary>
    O,
    /// <summary>
    /// 盘点预约C
    /// </summary>
    C,
    /// <summary>
    /// 调账预约P
    /// </summary>
    P,
    /// <summary>
    /// 禁用储位X
    /// </summary>
    X
}

/// <summary>
/// 仓库别
/// </summary>
public class cWarehouse
{    /// <summary>
    /// 成品仓
    /// </summary>
    public const string FWH = "A";
    /// <summary>
    /// 原料仓
    /// </summary>
    public const string WWH = "B";
}

/// <summary>
/// 储位规格
/// </summary>
public class cLocSize
{
    /// <summary>
    /// 在制品仓B
    /// </summary>
    public const string SbucketB = "B";
    public const string SbucketB_CN = "船型桶储位B";
    /// <summary>
    /// 成品仓L
    /// </summary>
    public const string PalletL = "L";
    public const string PalletL_CN = "轻储位L";
    /// <summary>
    /// 成品仓W
    /// </summary>
    public const string PalletW = "W";
    public const string PalletW_CN = "重储位W";
}

/// <summary>
/// 存取机结构
/// </summary>
struct S_CRN_INF
{
    /// <summary>
    /// 机号列表
    /// </summary>
    public DataTable dtCrn;	// 下标一般为1.二号机为2....
    /// <summary>
    /// 站号列表
    /// </summary>
    public DataTable dtStn;
    /// <summary>
    /// 列[重要](当根据存取机号获取储位时 根据此值来判定储位范围)
    /// </summary>
    public int nRows;
    /// <summary>
    /// 行
    /// </summary>
    public int nBays;
    /// <summary>
    /// 层
    /// </summary>
    public int nLvls;
}

/// <summary>
/// 作业模式
/// </summary>
public class cCmdMode
{
    public const string In = "1";
    public const string Out = "2";
    public const string Chk = "3";
    public const string S2S = "4";
    public const string L2L = "5";
    public const string Move = "6";
    public const string Pick = "8";
    public const string Put = "9";
}

/// <summary>
/// 作业形态
/// </summary>
public class cIoType
{
    /// <summary>
    /// 在线成品入库11
    /// </summary>
    public const string In11 = "11";
    /// <summary>
    /// 在线在制品入库13
    /// </summary>
    public const string In13 = "13";
    /// <summary>
    /// 空栈板入库15
    /// </summary>
    public const string EptPltIn = "15";
    /// <summary>
    /// 在线成品出库21
    /// </summary>
    public const string Out21 = "21";
    /// <summary>
    /// 在线在制品出库23
    /// </summary>
    public const string Out23 = "23";
    /// <summary>
    /// 空栈板出库25
    /// </summary>
    public const string EptPltOut = "25";
    /// <summary>
    /// 库对库52（栈板）
    /// </summary>
    public const string EptPltL2L = "52";
    /// <summary>
    /// 库对库51
    /// </summary>
    public const string StockL2L = "51";
    /// <summary>
    /// 库对库42
    /// </summary>
    public const string S2S = "42";
}

/// <summary>
/// 流程码
/// </summary>
public class cTrace
{
    /// <summary>
    /// 待执行
    /// </summary>
    public const string A00 = "00";
    /// <summary>
    /// 写资料到入库站口
    /// </summary>
    public const string I11 = "11";
    /// <summary>
    /// 下SLV入库命令
    /// </summary>
    public const string I12 = "12";
    /// <summary>
    /// SLV入库命令完成
    /// </summary>
    public const string I13 = "13";
    /// <summary>
    /// 下CRN入库命令
    /// </summary>
    public const string I14 = "14";
    /// <summary>
    /// CRN入库命令完成
    /// </summary>
    public const string I15 = "15";

    /// <summary>
    /// 写资料到出库站口
    /// </summary>
    public const string O21 = "21";
    /// <summary>
    /// 下CRN出库命令
    /// </summary>
    public const string O22 = "22";
    /// <summary>
    /// CRN出库命令完成
    /// </summary>
    public const string O23 = "23";
    /// <summary>
    /// 下SLV出库命令
    /// </summary>
    public const string O24 = "24";
    /// <summary>
    /// SLV出库命令完成
    /// </summary>
    public const string O25 = "25";
    /// <summary>
    /// 货物到达目的站口
    /// </summary>
    public const string O26 = "26";

    /// <summary>
    /// 下CRN库对库命令
    /// </summary>
    public const string L51 = "51";
    /// <summary>
    /// CRN库对库命令完成
    /// </summary>
    public const string L52 = "52";
}

/// <summary>
/// 优先级
/// </summary>
public class cPrty
{
    public const string L2L = "2";
    public const string OUT = "3";
    public const string IN = "4";
    public const string CYC = "5";
    public const string S2S = "6";
}

/// <summary>
/// 区域编号
/// </summary>
public class cAreaNo
{
    #region 入库口
    /// <summary>
    /// 成品仓HP侧C-T、SLV入库口
    /// </summary>
    public const string FHWR = "FHWR";
    /// <summary>
    /// 成品仓HP侧SLV、CRN入库口
    /// </summary>
    public const string FHNR = "FHNR";
    /// <summary>
    /// 成品车间C-T、SLV外入库口
    /// </summary>
    public const string FCWR = "FCWR";
    /// <summary>
    /// 成品仓OP侧SLV、CRN入库口
    /// </summary>
    public const string FONR = "FONR";
    /// <summary>
    /// 成品车间SLV中转入口
    /// </summary>
    public const string FCZR = "FCZR";
    /// <summary>
    /// 成品仓OP侧SLV中转入口
    /// </summary>
    public const string FOZR = "FOZR";
    #endregion

    #region 出库口
    /// <summary>
    /// 成品仓HP侧C-T、SLV出库口
    /// </summary>
    public const string FHWC = "FHWC";
    /// <summary>
    /// 成品仓HP侧SLV、CRN出库口
    /// </summary>
    public const string FHNC = "FHNC";
    /// <summary>
    /// 成品仓车间C-T、SLV出库口
    /// </summary>
    public const string FCWC = "FCWC";
    /// <summary>
    /// 成品仓OP侧SLV、CRN出库口
    /// </summary>
    public const string FONC = "FONC";
    /// <summary>
    /// 成品车间SLV中转出口
    /// </summary>
    public const string FCZC = "FCZC";
    /// <summary>
    /// 成品仓OP侧SLV中转出口
    /// </summary>
    public const string FOZC = "FOZC";
    #endregion
}

/// <summary>
/// 设备类型
/// </summary>
public class cEquType
{
    /// <summary>
    /// 周边输送机站口
    /// </summary>
    public const string Conveyer = "C-T";
    /// <summary>
    /// 主机
    /// </summary>
    public const string Crane = "CRN";
    /// <summary>
    /// RGV小车
    /// </summary>
    public const string RGV = "RGV";
    /// <summary>
    /// LoopCar小车
    /// </summary>
    public const string LoopCar = "SLV";
}

/// <summary>
/// 入库货物类型
/// </summary>
public class cItmType
{
    /// <summary>
    /// 空托盘
    /// </summary>
    public const string EptPlt = "E";
    /// <summary>
    /// 实物
    /// </summary>
    public const string Stock = "S";
}

