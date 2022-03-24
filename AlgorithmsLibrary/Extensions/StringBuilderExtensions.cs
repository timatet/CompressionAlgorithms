using System.Text;

namespace AlgorithmsLibrary.StringBuilderExtensions
{
    /*
     * Если необходима оптимизация
     * stackoverflow.com/questions/12261344/fastest-search-method-in-stringbuilder
     * */
    public static class StringBuilderExtensions
    {
        public static bool Contains(this StringBuilder @this, string match)
        {
            return @this.IndexOf(match) != -1;
        }
        public static string Substring(this StringBuilder @this, int startpos, int length)
        {
            return @this.ToString().Substring(startpos, length);
        }

        public static int IndexOf(this StringBuilder @this, string match)
        {
            return @this.ToString().IndexOf(match);
        }
    }
}
