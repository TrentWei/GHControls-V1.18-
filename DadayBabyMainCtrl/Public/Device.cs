using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.IO.Ports;
using DB;
using System.Collections;
using System.Text;

namespace DadayBabyMainCtrl
{
    public class Device
    {
        #region Memeber

        public static string sBcrHead = string.Empty;
        public static string sBcrEnd = string.Empty;

        private static Object obj = new object();

        private static Hashtable hBcrPort;
        private static SocketPort bcrPort1 = null;
        private static SocketPort bcrPort2 = null;
        
        #endregion

        public static bool InitDevice(params string[] sDevices)
        {
            hBcrPort = new Hashtable();
            bcrPort1 = new SocketPort();
            bcrPort2 = new SocketPort();

            hBcrPort.Add(Ports.C101, bcrPort1);
            hBcrPort.Add(Ports.C102, bcrPort2);

            try
            {
                Decoder dc = Encoding.ASCII.GetDecoder();
                Char[] pChar = new Char[5];
                byte[] buffer = new byte[5];
                Array.Clear(buffer, 0, 5);
                Array.Clear(pChar, 0, 5);
                buffer[0] = 2;
                buffer[1] = 50;
                buffer[2] = 49;
                buffer[3] = 3;
                buffer[4] = 2;
                dc.GetChars(buffer, 0, 5, pChar, 0);
                sBcrHead = new string(pChar);
                sBcrEnd = sBcrHead.Substring(3, 1);

                SocketPort dPort = null;
                dPort = (SocketPort)hBcrPort[Ports.C101];
                bcrPort1.Init(Globals.sComBcrC101);

                dPort = (SocketPort)hBcrPort[Ports.C102];
                bcrPort2.Init(Globals.sComBcrC102);

                foreach (string obj in sDevices)
                {
                    if (hBcrPort.Contains(obj))
                    {
                        dPort = (SocketPort)hBcrPort[obj];
                        dPort.Open();
                    }
                }
            }
            catch(Exception ex)
            {
                Log.WriteLog(cLog.Exception, ex.Message);
                return false;
            }
            return true;
        }

        public static bool EndReadBcr(object sStn)
        {
            SocketPort dPort = (SocketPort)(hBcrPort[sStn]);
            bool bRec = true;
            if (dPort.bStart == false) return false;
            Byte[] pBuf = new Byte[50];
            Array.Clear(pBuf, 0, 50);
            try
            {
                pBuf[0] = 0x02;
                pBuf[1] = 0x32;
                pBuf[2] = 0x32;
                pBuf[3] = 0x03;
                dPort.Write(pBuf, 0, 4);
                dPort.bStart = false;
                Thread.Sleep(2);
            }
            catch (Exception ex)
            {
                Log.WriteLog(cLog.Exception, ex.Message + "\r\n" + ex.StackTrace);
                bRec = false;
            }
            return bRec;
        }

        public static string ReadBcr(object sStn)
        {
            SocketPort dPort = (SocketPort)(hBcrPort[sStn]);
            string strValue = string.Empty;
            Byte[] pBuf = new Byte[4];
            Array.Clear(pBuf, 0, 4);

            try
            {
                if (dPort.bStart == false)
                {
                    dPort.ClearReadBuffer();
                    pBuf[0] = 0x02;
                    pBuf[1] = 0x32;
                    pBuf[2] = 0x31;
                    pBuf[3] = 0x03;
                    dPort.Write(pBuf, 0, 4);
                    dPort.bStart = true;
                    Thread.Sleep(2);
                }
                strValue = dPort.Read();
                Log.WriteLog(cLog.RunLog, string.Format("Stn:{0} 读条码原始:{1}", sStn, strValue));                
                while (strValue.Length > 10)
                {
                    if (strValue.StartsWith(sBcrHead))
                    {            
                        if( strValue.Length >= 16)
                        {
                            if (strValue.Substring(15, 1) == sBcrEnd)
                            {
                                dPort.bStart = false;
                                strValue = strValue.Substring(9, 6);
                            }
                            else
                            {
                                dPort.DiscaredChars(4);
                                //continue;
                            }
                        }
                        else
                            strValue = string.Empty;
                        break;
                    }
                    else
                    {
                        dPort.DiscaredChars(1);
                    }
                    strValue = dPort.GetReaded();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(cLog.Exception, ex.Message + "\r\n" + ex.StackTrace);
                strValue = string.Empty;
            }
            if (strValue.Length != 6) strValue = string.Empty;
            return strValue;
        }

        public static void ClosePorts()
        {
            SocketPort dPort = null;
            foreach (object sObj in hBcrPort.Values)
            {
                dPort = (SocketPort)(sObj);
                dPort.Close();
            }
        }
    }

    public class Ports
    {
        /// <summary>
        /// 一楼地上盘侧在制品仓BCR C101
        /// </summary>
        public const string C101 = "C101";
        /// <summary>
        /// 一楼地上盘侧在制品仓BCR C102
        /// </summary>
        public const string C102 = "C102";
    }
}