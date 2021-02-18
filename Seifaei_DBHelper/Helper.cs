using System;
using System.Data.SqlTypes;
using System.Globalization;
using System.Text;
using Microsoft.SqlServer.Server;

public class UserDefinedFunctions
{
    private static readonly PersianCalendar PersianCalendar = new PersianCalendar();   

    [SqlFunction(DataAccess = DataAccessKind.None)]
    public static SqlString AS_Date_MiladiToShamsi(DateTime? date)
    {
        if (date == null)
            return null;
        return new SqlString(
            PersianCalendar.GetYear(date.Value).ToString("0000") + "/" +
            PersianCalendar.GetMonth(date.Value).ToString("00") + "/" +
            PersianCalendar.GetDayOfMonth(date.Value).ToString("00")
        );
    }

    [SqlFunction(DataAccess = DataAccessKind.None)]
    public static SqlString AS_Date_MiladiToShamsi_Separator(DateTime? date, char separator)
    {
        if (date == null)
            return null;
        return new SqlString(
            PersianCalendar.GetYear(date.Value).ToString("0000") + separator +
            PersianCalendar.GetMonth(date.Value).ToString("00") + separator +
            PersianCalendar.GetDayOfMonth(date.Value).ToString("00")
        );
    }

    [SqlFunction(DataAccess = DataAccessKind.None)]
    public static SqlString AS_Date_MiladiToShamsi_Time(DateTime? date, char separatorDate, char separatorTime)
    {
        if (date == null)
            return null;
        var dt = DateTime.Now;
        return new SqlString(
            PersianCalendar.GetYear(date.Value).ToString("0000") + separatorDate +
            PersianCalendar.GetMonth(date.Value).ToString("00") + separatorDate +
            PersianCalendar.GetDayOfMonth(date.Value).ToString("00") + " " +
            dt.Hour.ToString("00") + separatorTime +
            dt.Minute.ToString("00") + separatorTime +
            dt.Second.ToString("00")
        );
    }

    [SqlFunction(DataAccess = DataAccessKind.None)]
    public static DateTime? AS_Date_ShamsiToMiladi(SqlString str)
    {
        if (str.IsNull)
        {
            return null;
        }

        var ary = str.Value.Split('/');

         return  PersianCalendar.ToDateTime(int.Parse(ary[0]), int.Parse(ary[1]), int.Parse(ary[2]), 0, 0, 0, 0);
    }

    [SqlFunction(DataAccess = DataAccessKind.None)]
    public static DateTime? AS_Date_TryShamsiToMiladi(SqlString str)
    {
        if (str.IsNull)
        {
            return null;
        }

        var ary = str.Value.Split('/');

        DateTime? re = null;
        try
        {
            re = PersianCalendar.ToDateTime(int.Parse(ary[0]), int.Parse(ary[1]), int.Parse(ary[2]), 0, 0, 0, 0);
        }
        catch (Exception ex)
        {
            re = null;
        }
        return re;
    }



    [SqlFunction(DataAccess = DataAccessKind.None)]
    public static DateTime? AS_Date_ShamsiToMiladi_Separator(SqlString str, char separator)
    {
        if (str.IsNull)
        {
            return null;
        }
        var ary = str.Value.Split(separator);
        return PersianCalendar.ToDateTime(int.Parse(ary[0]), int.Parse(ary[1]), int.Parse(ary[2]), 0, 0, 0, 0);
    }

    [SqlFunction(DataAccess = DataAccessKind.None)]
    public static DateTime? AS_Date_ShamsiToMiladi_Time(SqlString str, char separatorDate, char separatorTime)
    {
        if (str.IsNull)
        {
            return null;
        }
        var ary = str.Value.Split(' ');
        var aryDate = ary[0].Split(separatorDate);
        var aryTime = ary[1].Split(separatorTime);

        return PersianCalendar.ToDateTime(int.Parse(aryDate[0]), int.Parse(aryDate[1]), int.Parse(aryDate[2]),
            int.Parse(aryTime[0]), int.Parse(aryTime[1]), int.Parse(aryTime[2]), 0);
    }


    [SqlFunction(DataAccess = DataAccessKind.None)]
    public static int? AS_Date_Shamsi_GetDayOfMonth(DateTime? time)
    {
        if (time == null)
            return null;
        return PersianCalendar.GetDayOfMonth(time.Value);
    }

    [SqlFunction(DataAccess = DataAccessKind.None)]
    public static int? AS_Date_Shamsi_GetDayOfYear(DateTime? time)
    {
        if (time == null)
            return null;
        return PersianCalendar.GetDayOfYear(time.Value);
    }

    [SqlFunction(DataAccess = DataAccessKind.None)]
    public static int? AS_Date_Shamsi_GetDaysInMonth(int? year, int? month)
    {
        if (year == null || month == null)
            return null;
        return PersianCalendar.GetDaysInMonth(year.Value, month.Value);
    }

    [SqlFunction(DataAccess = DataAccessKind.None)]
    public static int? AS_Date_Shamsi_GetDaysInYear(int? year)
    {
        if (year == null)
            return null;
        return PersianCalendar.GetDaysInYear(year.Value);
    }

    [SqlFunction(DataAccess = DataAccessKind.None)]
    public static int? AS_Date_Shamsi_GetLeapMonth(int? year)
    {
        if (year == null)
            return null;
        return PersianCalendar.GetLeapMonth(year.Value);
    }

    [SqlFunction(DataAccess = DataAccessKind.None)]
    public static int? AS_Date_Shamsi_GetMonth(DateTime? time)
    {
        if (time == null)
            return null;
        return PersianCalendar.GetMonth(time.Value);
    }

    [SqlFunction(DataAccess = DataAccessKind.None)]
    public static int? AS_Date_Shamsi_GetMonthsInYear(int? year)
    {
        if (year == null)
            return null;
        return PersianCalendar.GetMonthsInYear(year.Value);
    }

    [SqlFunction(DataAccess = DataAccessKind.None)]
    public static int? AS_Date_Shamsi_GetWeekOfYear(DateTime? time, int firstDayOfWeek_SunIsZero)
    {
        if (time == null)
            return null;
        return PersianCalendar.GetWeekOfYear(time.Value, CalendarWeekRule.FirstDay, (DayOfWeek)firstDayOfWeek_SunIsZero);
    }

    [SqlFunction(DataAccess = DataAccessKind.None)]
    public static int? AS_Date_Shamsi_GetYear(DateTime? time)
    {
        if (time == null)
            return null;
        return PersianCalendar.GetYear(time.Value);
    }

    [SqlFunction(DataAccess = DataAccessKind.None)]
    public static bool? AS_Date_Shamsi_IsLeapDay(int? year, int? month, int? day)
    {
        if (year == null || month == null || day == null)
            return null;
        return PersianCalendar.IsLeapDay(year.Value, month.Value, day.Value);
    }

    [SqlFunction(DataAccess = DataAccessKind.None)]
    public static bool? AS_Date_Shamsi_IsLeapMonth(int? year, int? month)
    {
        if (year == null || month == null)
            return null;
        return PersianCalendar.IsLeapMonth(year.Value, month.Value);
    }

    [SqlFunction(DataAccess = DataAccessKind.None)]
    public static bool? AS_Date_Shamsi_IsLeapYear(int? year)
    {
        if (year == null)
            return null;
        return PersianCalendar.IsLeapYear(year.Value);
    }

    [SqlFunction(DataAccess = DataAccessKind.None)]
    public static byte[] AS_HexVarCharToVarBinary(string hexVarChar)
    {
        if (string.IsNullOrEmpty(hexVarChar))
        {
            return null;
        }

        

        if ((hexVarChar.Length & 1) != 0)
        {
            throw new ArgumentOutOfRangeException("hexString", hexVarChar, "hexString must contain an even number of characters.");
        }

        byte[] result = new byte[hexVarChar.Length / 2];

        for (int i = 0; i < hexVarChar.Length; i += 2)
        {
            result[i / 2] = byte.Parse(hexVarChar.Substring(i, 2), NumberStyles.HexNumber);
        }

        return result;
    }


    [SqlFunction(DataAccess = DataAccessKind.None)]
    public static string AS_VarBinaryToHexVarChar(byte[] data)
    {
        if (data == null ||  data.Length == 0)
            return null;
        return BitConverter.ToString(data).Replace("-", "");
        
    }

    [SqlFunction(DataAccess = DataAccessKind.None)]
    public static byte[] AS_NewUUID()
    {
        return AS_HexVarCharToVarBinary(Guid.NewGuid().ToString().Replace("-", ""));

    }

    [SqlFunction(DataAccess = DataAccessKind.None)]
    public static string AS_Date_TryToStandardFormat10Char(string date, char separator)
    {
        if (date == null || separator == null )
            return null;


        var aryStr = date.Split(separator);
        int year, month, day;
        if (aryStr.Length == 3 
            && int.TryParse(aryStr[0], out year)
            && int.TryParse(aryStr[1], out month)
            && int.TryParse(aryStr[2], out day))
        {
            return year.ToString("0000") + separator + month.ToString("00") + separator + day.ToString("00");
        }
        else
        {
            return null;
        }
    }
}