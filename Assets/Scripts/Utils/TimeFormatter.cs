using System;

public static class TimeFormatter
{
    public static string Format(TimeSpan time) {
        double seconds = time.TotalSeconds;
        if (seconds > 60) {
            if (seconds > 3600) {
                if (seconds > 86400) {
                    return time.ToString("%d") + " días";
                }
                return time.ToString("%h") + " horas";
            }
            return time.ToString("%m") + " minutos";
        }
        return time.ToString("%s") + " segundos";
    }

    public static string Format(string from, string to, string format)
    {
        if (from.Equals("null")) return "Ninguno";
        DateTime dateTo = to.Equals("null") ? DateTime.Now : DateTime.ParseExact(to, format, null);
        TimeSpan time = dateTo - DateTime.ParseExact(from, format, null);
        double seconds = time.TotalSeconds;
        if (seconds > 60) {
            if (seconds > 3600) {
                if (seconds > 86400) {
                    return time.ToString("%d") + " días";
                }
                return time.ToString("%h") + " horas";
            }
            return time.ToString("%m") + " minutos";
        }
        return time.ToString("%s") + " segundos";
    }

    public static string Format(DateTime? from, DateTime? to)
    {
        if (from == null) return "Ninguno";
        TimeSpan time = (to == null ? DateTime.Now : to.Value) - from.Value;
        double seconds = time.TotalSeconds;
        if (seconds > 60)
        {
            if (seconds > 3600)
            {
                if (seconds > 86400)
                {
                    return time.ToString("%d") + " días";
                }
                return time.ToString("%h") + " horas";
            }
            return time.ToString("%m") + " minutos";
        }
        return time.ToString("%s") + " segundos";
    }
}
