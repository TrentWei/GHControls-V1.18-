using System;
using System.Collections.Generic;
using System.Collections;
using ACTETHERLib;
using System.IO;
using System.Data;
using System.Linq;
using System.Threading;
using ACTMULTILib;
using Share;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;

namespace DadayBabyMainCtrl
{
    public class PLC
    {
        #region Member
        //private static ActEasyIF plcFHPPLC = null;
        //private static ActEasyIF plcFOPPLC = null;
        private static ActEasyIF plcFCJPLC = null;
        //private static ActEasyIF plcWHPPLC = null;
        //private static ActEasyIF plcWOPPLC = null;
        //private static ActEasyIF plcWCJPLC = null;
        
        private static Hashtable hPlcTable = null;
        #endregion

        #region Func

        private static ActEasyIF getPlc(object sName)
        {
            if (hPlcTable[sName] == null) return null;
            return (ActEasyIF)(hPlcTable[sName]);
        }
        
        private static bool InitPLC()
        {
            try
            {
                int.Parse(Globals.sFCJPLC);
            }
            catch (Exception)
            {
                Log.WriteLog(cLog.InitErr, "PLC Logical Station Number为数字");
                return false;
            }

            try
            {

                plcFCJPLC = new ActEasyIF();
                plcFCJPLC.ActLogicalStationNumber = int.Parse(Globals.sFCJPLC);

            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
            }

            hPlcTable = new Hashtable();
            //hPlcTable.Add(Plcs.FHPPLC, plcFHPPLC);
            //hPlcTable.Add(Plcs.FOPPLC, plcFOPPLC);
            hPlcTable.Add(Plcs.FCJPLC, plcFCJPLC);
            return true;
        }

        #endregion

        #region Member

        public static bool OpenPLCs(params string[] sPlcs)
        {
            try
            {
                InitPLC();
                foreach (string strPLC in sPlcs)
                {
                    getPlc(strPLC).Open();
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
                return false;
            }
        }

        public static int ReadPlc(string sPlc,string sDevice,int nLen,out int lpData)
        {
            int nRead = 0;
            ActEasyIF hPlc = getPlc(sPlc);
            try
            {
                Monitor.Enter(hPlc);
                nRead = hPlc.ReadDeviceBlock(sDevice, nLen, out lpData);
                Monitor.Exit(hPlc);
                return nRead;
            }
            catch (Exception)
            {
                Monitor.Exit(hPlc);
                lpData = 0;
                return 1;
            }
        }

        public static void ClosePlc()
        {
            hPlcTable.Clear();
           // plcFHPPLC.Close();
           // plcFOPPLC.Close();
            plcFCJPLC.Close();
        }

        public static bool WriteDevice(object sPlc, object sDevice, int nValue)
        {
            int nRec = 0;
            ActEasyIF hPlc = getPlc(sPlc);
            try
            {
                Monitor.Enter(hPlc);
                nRec = hPlc.SetDevice(sDevice.ToString(), nValue);
                Monitor.Exit(hPlc);
            }
            catch (Exception)
            {
                Monitor.Exit(hPlc);
            }
            if (nValue == nRec) return true; else return false;
        }

        public static bool WriteBlock(object sPlc, object sDevice, int nLen,int[] pValues)
        {
            int nRec = 0;
            ActEasyIF hPlc = getPlc(sPlc);
            try
            {
                Monitor.Enter(hPlc);
                nRec = hPlc.WriteDeviceBlock(sDevice.ToString(), nLen, ref pValues[0]);
                Monitor.Exit(hPlc);
            }
            catch (Exception)
            {
                Monitor.Exit(hPlc);
            }
            if (nRec==0) return true; else return false;
        }

        public static int ReadDevice(object sPlc, object sDevice)
        {
            int nValue = 0;
            ActEasyIF hPlc = getPlc(sPlc);
            try
            {
                Monitor.Enter(hPlc);
                hPlc.GetDevice(sDevice.ToString(), out nValue);
                Monitor.Exit(hPlc);
            }
            catch (Exception)
            {
                Monitor.Exit(hPlc);
            }
            return nValue;
        }

        public static void ClosePlc(object sPlc)
        {
            try
            {
                ActEasyIF hPlc = getPlc(sPlc);
                if (hPlc == null) return;
                hPlc.Close();
            }
            catch (Exception)
            {
            }            
        }

        public static bool OpenPlc(object sPlc)
        {
            try
            {
                ActEasyIF hPlc = getPlc(sPlc);
                if (hPlc == null) return false;
                if (hPlc.Open() == 0) return true;
                else return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool ConnectPlc(object sPlc)
        {
            try
            {
                ActEasyIF hPlc = getPlc(sPlc);
                if (hPlc == null) return false;
                if (hPlc.Connect() == 0) return true;
                else return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool ChkNetworkSts(IPAddress hIp)
        {
            Ping p = new Ping();
            PingOptions pOption = new PingOptions();
            pOption.DontFragment = true;
            bool bNetworkFlag = false;

            string data = "Test Data!";
            int timeout = 1000;
            byte[] buffer = Encoding.ASCII.GetBytes(data);

            PingReply reply = p.Send(hIp, timeout, buffer, pOption);
            if (reply.Status == IPStatus.Success)
            {
                bNetworkFlag = true;
            }
            return bNetworkFlag;
        }
        
        #endregion
    }

    public class Plcs
    { 
        public const string FCJPLC = "FCJPLC";
    }
}
