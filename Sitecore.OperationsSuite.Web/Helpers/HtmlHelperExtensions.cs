namespace System.Web.Mvc
{
    public static class HtmlHelperExtensions
    {
        public static String ScheduledForUtc(this HtmlHelper htmlHelper, DateTime expectedStart, DateTime expectedEnd, String prefix = "Scheduled for")
        {
            return $"{prefix} {expectedStart.ToString("MMM dd")}, {expectedStart.ToString("HH:mm")} - {expectedEnd.ToString("MMM dd")}, {expectedEnd.ToString("HH:mm")} UTC";
        }

        public static String PrintDateUtc(this HtmlHelper htmlHelper, DateTime postedDate, String prefix)
        {
            return $"{prefix} {postedDate.ToString("MMM dd, HH:mm")} UTC";
        }

        public static String Truncate(this HtmlHelper htmlHelper, String content, Int32 length)
        {
            if (String.IsNullOrEmpty(content))
                return content;

            if (content.Length < length)
                return content;

            return content.Substring(0, length) + " ...";
        }
    }
}