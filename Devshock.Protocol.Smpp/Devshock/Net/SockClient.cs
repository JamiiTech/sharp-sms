using System;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using Devshock.Common;
using log4net;

namespace Devshock.Net {
  class SockClient {
    protected static ILog log =
      LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.ToString());

    readonly ManualResetEvent allDone = new ManualResetEvent(false);
    readonly ByteBuilder dataIn = new ByteBuilder(300);
    readonly ManualResetEvent disconnectedDone = new ManualResetEvent(false);
    Encoding _DataCoding = Encoding.GetEncoding(0x4e4);
    byte[] buffer;
    bool closing;
    int m_BufferSize = 0x800;
    string m_LocalHost = string.Empty;
    int m_LocalPort;
    string m_RemoteHost = string.Empty;
    Socket mSocket;
    int waitBytes;

    internal int BufferSize {
      get { return m_BufferSize; }
      set { m_BufferSize = value; }
    }

    internal bool Connected {
      get {
        try {
          return mSocket.Connected;
        } catch {
          return false;
        }
      }
    }

    internal Encoding DataCoding {
      get { return _DataCoding; }
      set { _DataCoding = value; }
    }

    internal string LocalHost {
      get { return m_LocalHost; }
      set { m_LocalHost = value; }
    }

    internal int LocalPort {
      get { return m_LocalPort; }
      set { m_LocalPort = value; }
    }

    internal string RemoteHost {
      get { return m_RemoteHost; }
      set { m_RemoteHost = value; }
    }

    internal int RemotePort { get; set; }

    internal object Tag { get; set; }

    internal event CloseConnectionEventHandler CloseConnection;

    internal event DataReceivedEventHandler DataReceived;

    internal event DataSentEventHandler DataSent;

    internal event LastErrorEventHandler LastError;

    internal event OpenConnectionEventHandler OpenConnection;

    internal void BeginConnect() {
      IPEndPoint endPoint = GetEndPoint();
      if ((m_LocalPort != 0) || (m_LocalHost != string.Empty))
        mSocket.Bind(GetLocalEndPoint());
      dataIn.Clear();
      mSocket.Blocking = false;
      AsyncCallback callback = ConnectCallBack;
      mSocket.BeginConnect(endPoint, callback, mSocket);
    }

    void BeginReceive() {
      lock (this) {
        if (closing)
          ReleaseSocket();
        else
          try {
            mSocket.BeginReceive(buffer, 0, m_BufferSize, SocketFlags.None,
                                 new AsyncCallback(ReceiveCallback), null);
          } catch (Exception ex) {
            log.Error("Devshosk: BeginReceive()", ex);
          }
      }
    }

    internal void BeginSend(byte[] byteBuffer) {
      try {
        mSocket.BeginSend(byteBuffer, 0, byteBuffer.Length, SocketFlags.None, new AsyncCallback(SendCallBack),
                          null);
      } catch {
        OnDataSent(new DataSentEventArgs(-1));
      }
    }

    internal void BeginSend(string StringBuffer) {
      BeginSend(Encoding.Default.GetBytes(StringBuffer));
    }

    internal void Connect() {
      IPEndPoint endPoint = GetEndPoint();
      if ((m_LocalPort != 0) || (m_LocalHost != string.Empty))
        mSocket.Bind(GetLocalEndPoint());
      dataIn.Clear();
      mSocket.Connect(endPoint);
      BeginReceive();
    }

    void ConnectCallBack(IAsyncResult ar) {
      Exception connError = null;
      try {
        if (mSocket.Connected)
          BeginReceive();
      } catch (Exception exception2) {
        connError = exception2;
      }
      OnOpenConnection(new OpenConnectionEventArgs(connError));
    }

    internal void Disconnect() {
      bool flag = false;
      lock (this) {
        closing = true;
        if ((mSocket != null) && mSocket.Connected) {
          try {
            mSocket.Shutdown(SocketShutdown.Both);
          } catch {}
          flag = true;
        }
      }
      if (flag && mSocket.Connected)
        disconnectedDone.WaitOne();
    }

    IPEndPoint GetEndPoint() {
      dataIn.RemoveRange(0, dataIn.Count);
      if (buffer == null)
        buffer = new byte[BufferSize];
      if ((mSocket != null) && mSocket.Connected)
        throw new Exception("You must disconnect before connect again", new SocketException(0x186a1));
      var point = new IPEndPoint(IPAddress.Parse(ParseRemoteHost()), RemotePort);
      mSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      closing = false;
      disconnectedDone.Reset();
      return point;
    }

    IPEndPoint GetLocalEndPoint() {
      IPAddress any;
      if ((mSocket != null) && mSocket.Connected)
        throw new Exception("You must disconnect before connect again", new SocketException(0x186a1));
      string ipString = ParseLocalHost();
      if (ipString != string.Empty)
        any = IPAddress.Parse(ipString);
      else
        any = IPAddress.Any;
      return new IPEndPoint(any, LocalPort);
    }

    bool isIpAddress(string inValue) {
      try {
        IPAddress.Parse(inValue);
        return true;
      } catch {
        return false;
      }
    }

    internal void OnCloseConnection(CloseConnectionEventArgs e) {
      if (CloseConnection != null)
        CloseConnection(this, e);
    }

    internal void OnDataReceived(DataReceivedEventArgs e) {
      if (DataReceived != null)
        DataReceived(this, e);
    }

    internal void OnDataSent(DataSentEventArgs e) {
      if (DataSent != null)
        DataSent(this, e);
    }

    internal void OnLastError(LastErrorEventArgs e) {
      if (LastError != null)
        LastError(this, e);
    }

    internal void OnOpenConnection(OpenConnectionEventArgs e) {
      if (OpenConnection != null)
        OpenConnection(this, e);
    }

    string ParseLocalHost() {
      if (!isIpAddress(m_LocalHost) && (m_LocalHost != string.Empty))
        return Dns.GetHostByAddress(m_LocalHost).AddressList[0].ToString();
      return m_LocalHost;
    }

    string ParseRemoteHost() {
      if (isIpAddress(m_RemoteHost))
        return m_RemoteHost;
      return Dns.GetHostByAddress(m_RemoteHost).AddressList[0].ToString();
    }

    internal string Peek() {
      return DataCoding.GetString(dataIn.ToArray());
    }

    internal string Peek(int Bytes) {
      return DataCoding.GetString(dataIn.ToArray(0, Bytes));
    }

    internal byte[] PeekBytes(int Bytes) {
      return dataIn.ToArray(0, Bytes);
    }

    internal string Read() {
      return Read(0, 0);
    }

    internal string Read(int TimeOut) {
      return Read(0, TimeOut);
    }

    string Read(int Bytes, int TimeOut) {
      int count;
      string str = string.Empty;
      lock (dataIn.SyncRoot) {
        count = dataIn.Count;
        if (((Bytes > 0) && (count < Bytes)) || ((Bytes == 0) && (TimeOut > 0))) {
          if (Bytes == 0)
            Bytes = -1;
          waitBytes = Bytes;
          allDone.Reset();
        } else
          allDone.Set();
      }
      allDone.WaitOne(TimeOut, true);
      count = dataIn.Count;
      if (count == 0)
        return string.Empty;
      if (Bytes <= 0) {
        str = DataCoding.GetString(dataIn.ToArray(0, count));
        dataIn.RemoveRange(0, count);
        return str;
      }
      if ((Bytes > 0) && (count < Bytes))
        return string.Empty;
      str = DataCoding.GetString(dataIn.ToArray(0, Bytes));
      dataIn.RemoveRange(0, Bytes);
      return str;
    }

    public byte[] ReadByteArray(int Bytes, int TimeOut) {
      byte[] buffer = null;
      int count;
      lock (dataIn.SyncRoot) {
        count = dataIn.Count;
        if (((Bytes > 0) && (count < Bytes)) || ((Bytes == 0) && (TimeOut > 0))) {
          if (Bytes == 0)
            Bytes = -1;
          waitBytes = Bytes;
          allDone.Reset();
        } else
          allDone.Set();
      }
      allDone.WaitOne(TimeOut, true);
      count = dataIn.Count;
      if (count == 0)
        return null;
      if (Bytes <= 0) {
        buffer = dataIn.ToArray(0, count);
        dataIn.RemoveRange(0, count);
        return buffer;
      }
      if ((Bytes > 0) && (count < Bytes))
        return null;
      buffer = dataIn.ToArray(0, Bytes);
      dataIn.RemoveRange(0, Bytes);
      return buffer;
    }

    internal string ReadBytes(int Bytes) {
      return Read(Bytes, 0);
    }

    internal string ReadBytes(int Bytes, int TimeOut) {
      return Read(Bytes, TimeOut);
    }

    internal int ReadSize() {
      return dataIn.Count;
    }

    void ReceiveCallback(IAsyncResult ar) {
      if (!mSocket.Connected)
        ReleaseSocket();
      else {
        int count = 0;
        try {
          count = mSocket.EndReceive(ar);
        } catch {}
        if (count > 0) {
          lock (dataIn.SyncRoot) {
            dataIn.AddRange(buffer, count);
            if (((waitBytes > 0) || (waitBytes == -1)) && (dataIn.Count >= waitBytes)) {
              waitBytes = 0;
              allDone.Set();
            }
          }
          OnDataReceived(new DataReceivedEventArgs(count));
          BeginReceive();
        } else
          ReleaseSocket();
      }
    }

    void ReleaseSocket() {
      try {
        mSocket.Close();
        OnCloseConnection(new CloseConnectionEventArgs());
      } catch {} finally {
        allDone.Set();
        disconnectedDone.Set();
      }
    }

    internal int Send(byte[] byteBuffer) {
      try {
        return mSocket.Send(byteBuffer);
      } catch {
        return -1;
      }
    }

    internal int Send(string StringBuffer) {
      return Send(Encoding.Default.GetBytes(StringBuffer));
    }

    void SendCallBack(IAsyncResult ar) {
      int bytesSent = 0;
      try {
        bytesSent = mSocket.EndSend(ar);
      } catch {
        bytesSent = -1;
      } finally {
        OnDataSent(new DataSentEventArgs(bytesSent));
      }
    }

    #region Nested type: CloseConnectionEventArgs

    internal class CloseConnectionEventArgs : EventArgs {}

    #endregion

    #region Nested type: CloseConnectionEventHandler

    internal delegate void CloseConnectionEventHandler(object sender, CloseConnectionEventArgs e);

    #endregion

    #region Nested type: DataReceivedEventArgs

    internal class DataReceivedEventArgs : EventArgs {
      internal readonly int BytesReceived;

      internal DataReceivedEventArgs(int BytesReceived) {
        this.BytesReceived = BytesReceived;
      }
    }

    #endregion

    #region Nested type: DataReceivedEventHandler

    internal delegate void DataReceivedEventHandler(object sender, DataReceivedEventArgs e);

    #endregion

    #region Nested type: DataSentEventArgs

    internal class DataSentEventArgs : EventArgs {
      internal readonly int BytesSent;

      internal DataSentEventArgs(int BytesSent) {
        this.BytesSent = BytesSent;
      }
    }

    #endregion

    #region Nested type: DataSentEventHandler

    internal delegate void DataSentEventHandler(object sender, DataSentEventArgs e);

    #endregion

    #region Nested type: LastErrorEventArgs

    internal class LastErrorEventArgs : EventArgs {
      internal readonly string Description;
      internal readonly int LocalCode;
      internal readonly int NativeCode;

      internal LastErrorEventArgs(int LocalCode, int NativeCode, string Description) {
        this.LocalCode = LocalCode;
        this.NativeCode = NativeCode;
        this.Description = Description;
      }
    }

    #endregion

    #region Nested type: LastErrorEventHandler

    internal delegate void LastErrorEventHandler(object sender, LastErrorEventArgs e);

    #endregion

    #region Nested type: OpenConnectionEventArgs

    internal class OpenConnectionEventArgs : EventArgs {
      internal readonly Exception ConnError;

      internal OpenConnectionEventArgs(Exception ConnError) {
        this.ConnError = ConnError;
      }
    }

    #endregion

    #region Nested type: OpenConnectionEventHandler

    internal delegate void OpenConnectionEventHandler(object sender, OpenConnectionEventArgs e);

    #endregion
  }
}