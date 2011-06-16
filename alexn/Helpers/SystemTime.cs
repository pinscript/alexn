using System;

namespace alexn.Helpers {
    public static class SystemTime {
        public static Func<DateTime> Now = () => DateTime.UtcNow;
    }
}