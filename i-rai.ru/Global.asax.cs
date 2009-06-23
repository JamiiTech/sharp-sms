using System;
using System.Web;

namespace i_rai.ru {
  public class Global : HttpApplication {
    protected void Application_Start(object sender, EventArgs e) {
    }

    protected void Session_Start(object sender, EventArgs e) {
    }

    protected void Application_BeginRequest(object sender, EventArgs e) {
      string host = Request.Url.OriginalString;
      string err = "?404;";
      int i = host.IndexOf(err);
      if (i > 0) // Если есть 404 ошибка
        host = host.Substring(i + err.Length);
      if (host.IndexOf("i-rai.ru") > 0)
        Response.Redirect(host.Replace("i-rai.ru:80", "i-rai.com"));
      else
        Response.Redirect("http://wap.i-rai.com");
      Response.End();
    }

    protected void Application_AuthenticateRequest(object sender, EventArgs e) {
    }

    protected void Application_Error(object sender, EventArgs e) {
    }

    protected void Session_End(object sender, EventArgs e) {
    }

    protected void Application_End(object sender, EventArgs e) {
    }
  }
}