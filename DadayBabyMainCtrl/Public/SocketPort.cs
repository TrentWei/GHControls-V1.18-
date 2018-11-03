using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DadayBabyMainCtrl
{
    public class SocketPort
    {
        Socket hSocket = null;
        StringBuilder strRecice=null;
        IPAddress hIp = null;
        public bool bStart = false;
        public int nReadTimes = 0;
        int nPort = 0;

        public SocketPort()
        {
            hSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            hSocket.ReceiveBufferSize = 1024; 
            hSocket.SendBufferSize = 1024;
            hSocket.SendTimeout = 200;
            hSocket.ReceiveTimeout = 20;
        }

        public bool Init(string objPramas)
        {
            try
            {
                string[] pPras = objPramas.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                string[] pIps = pPras[0].Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                hIp = new IPAddress(new Byte[] {Byte.Parse(pIps[0]), Byte.Parse(pIps[1]), Byte.Parse(pIps[2]), Byte.Parse(pIps[3])});
                nPort = int.Parse(pPras[1]);
                strRecice = new StringBuilder();
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
                return false;
            }
        }

        public bool Open()
        {
            try
            {
                hSocket.Connect(hIp, nPort);
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
                return false;
            }
        }

        public bool ClearReadBuffer()
        {
            try
            {
                if (hSocket.Available > 0)
                {
                    hSocket.Receive(new Byte[1024]);
                }
                strRecice.Remove(0, strRecice.Length);
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
                return false;
            }
        }
        
        public bool Close()
        {
            try
            {
                hSocket.Close();
                hSocket.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
                return false;
            }
        }
        
        public bool Write(byte[] buffer, int offset, int nLen)
        {
            try
            {
                if (!hSocket.Connected) hSocket.Connect(hIp, nPort);
                hSocket.Send(buffer, offset, nLen, SocketFlags.None);
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
                return false;
            }
        }

        public string Read()
        {
            try
            {
                Decoder dc = Encoding.ASCII.GetDecoder();
                int nLen = hSocket.Available;
                if (nLen > 0)
                {
                    Char[] pChar = new Char[nLen];
                    byte[] buffer = new byte[nLen];
                    Array.Clear(buffer, 0, nLen);
                    Array.Clear(pChar, 0, nLen);
                    int nRead = hSocket.Receive(buffer, 0, nLen, SocketFlags.None);
                    dc.GetChars(buffer, 0, nRead, pChar, 0);
                    strRecice.Append(new string(pChar));
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
            }
            return strRecice.ToString();
        }

        public void DiscaredChars(int nChars)
        {
            strRecice.Remove(0, nChars);
        }

        public string GetReaded()
        {
            return strRecice.ToString();
        }
    }

}
