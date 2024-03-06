namespace EBTCO.Core.ExtensionMethods
{
    public static class StringExtensionMethods
    {
        public static bool EqualIgnoreCase(this String input, String str2)
        {
            return input.ToLower().Equals(str2.ToLower());
        }
    }
}
