using System;

namespace Devshock.Protocol.Smpp {
  [CLSCompliant(true)]
  public class SmppDateTime {
    readonly bool _DatePresent;
    readonly bool _SimpleMode;
    readonly string _SmppDate;
    DateTime _Date;

    public SmppDateTime(DateTime Date) {
      _SmppDate = string.Empty;
      _SmppDate = Date.Year.ToString().Substring(2, 2) + FillDatePart(Date.Month) + FillDatePart(Date.Day) +
                  FillDatePart(Date.Hour) + FillDatePart(Date.Minute) + FillDatePart(Date.Second) + "000R";
      _Date = Date;
      _DatePresent = true;
      _SimpleMode = false;
    }

    public SmppDateTime(string CurSmppDate) {
      _SmppDate = string.Empty;
      if (CurSmppDate != null)
        if (CurSmppDate.Length >= 0x10) {
          _SmppDate = CurSmppDate;
          _Date = new DateTime(TwoToFourDigYear(CurSmppDate.Substring(0, 2)),
                               Convert.ToInt32(CurSmppDate.Substring(2, 2)),
                               Convert.ToInt32(CurSmppDate.Substring(4, 2)),
                               Convert.ToInt32(CurSmppDate.Substring(6, 2)),
                               Convert.ToInt32(CurSmppDate.Substring(8, 2)),
                               Convert.ToInt32(CurSmppDate.Substring(10, 2)));
          _Date = _Date.AddMinutes(GetMinDiff(CurSmppDate.Substring(13, 3)));
          _DatePresent = true;
          _SimpleMode = false;
        } else if (CurSmppDate.Length >= 10) {
          _SmppDate = CurSmppDate;
          _Date = new DateTime(TwoToFourDigYear(CurSmppDate.Substring(0, 2)),
                               Convert.ToInt32(CurSmppDate.Substring(2, 2)),
                               Convert.ToInt32(CurSmppDate.Substring(4, 2)),
                               Convert.ToInt32(CurSmppDate.Substring(6, 2)),
                               Convert.ToInt32(CurSmppDate.Substring(8, 2)), 0);
          _DatePresent = true;
          _SimpleMode = true;
        } else
          _DatePresent = false;
    }

    public SmppDateTime(DateTime Date, bool SimpleMode) {
      _SmppDate = string.Empty;
      _SmppDate = Date.Year.ToString().Substring(2, 2) + FillDatePart(Date.Month) + FillDatePart(Date.Day) +
                  FillDatePart(Date.Hour) + FillDatePart(Date.Minute);
      _Date = Date;
      _DatePresent = true;
      _SimpleMode = true;
    }

    public SmppDateTime(DateTime Date, int TimeDifference) {
      string str;
      _SmppDate = string.Empty;
      if (TimeDifference < 0)
        str = "-";
      else
        str = "+";
      _SmppDate = Date.Year.ToString().Substring(2, 2) + FillDatePart(Date.Month) + FillDatePart(Date.Day) +
                  FillDatePart(Date.Hour) + FillDatePart(Date.Minute) + FillDatePart(Date.Second) + "0" +
                  FillDatePart(TimeDifference) + str;
      _Date = Date;
      _DatePresent = true;
    }

    public DateTime Date {
      get { return _Date; }
    }

    public bool DatePresent {
      get { return _DatePresent; }
    }

    public bool SimpleMode {
      get { return _SimpleMode; }
    }

    public string SmppDate {
      get { return _SmppDate; }
    }

    string FillDatePart(int n) {
      if (n < 10)
        return ("0" + n);
      return n.ToString();
    }

    int GetMinDiff(string s) {
      int num = Convert.ToInt32(s.Substring(0, 2))*15;
      switch (s.Substring(2, 1)) {
        case "R":
          return 0;

        case "-":
          return -num;

        case "+":
          return num;
      }
      return 0;
    }

    public override string ToString() {
      if (DatePresent)
        return _Date.ToString();
      return string.Empty;
    }

    int TwoToFourDigYear(string year) {
      string str = DateTime.Now.Year.ToString();
      int num = Convert.ToInt32(str.Substring(str.Length - 2));
      int num2 = Convert.ToInt32(str.Substring(0, 2));
      int num3 = Convert.ToInt32(year);
      if ((num - num3) < -50)
        return Convert.ToInt32((num2 - 1) + year);
      if ((num - num3) > 50)
        return Convert.ToInt32((num2 + 1) + year);
      return Convert.ToInt32(num2 + year);
    }
  }
}