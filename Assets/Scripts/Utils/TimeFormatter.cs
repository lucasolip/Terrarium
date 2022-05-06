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
}
