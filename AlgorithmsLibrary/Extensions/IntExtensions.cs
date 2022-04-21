namespace AlgorithmsLibrary.IntExtensions
{
    public static class IntExtensions
    {
        public static bool InRange(this int @this, int left, int right)
        {
            if (@this >= left && @this <= right)
                return true;
            return false;
        }
        public static bool InRange(this decimal @this, decimal left, decimal right)
        {
            if (@this >= left && @this <= right)
                return true;
            return false;
        }
        public static bool InRange(this double @this, double left, double right)
        {
            if (@this >= left && @this <= right)
                return true;
            return false;
        }
        public static bool InRange(this long @this, long left, long right)
        {
            if (@this >= left && @this <= right)
                return true;
            return false;
        }
    }
}
