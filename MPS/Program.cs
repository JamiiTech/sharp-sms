using System;
using System.ServiceModel;

namespace MPS {
  /// <summary>
  /// Инициализация WCF
  /// </summary>
  class Program {
    static void Main(string[] args) {
      var svcHost = new ServiceHost(typeof (MessageProcessing), new Uri[] {});
      svcHost.AddServiceEndpoint(typeof (IMessageProcessing), new NetTcpBinding(),
                                 "net.tcp://localhost:9000/MessageProcessing");
      svcHost.Open();
      Console.ReadKey();
      svcHost.Close();
    }
  }
}