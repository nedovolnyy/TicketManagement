using System.Globalization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using UserApiClientGenerated;

namespace TicketManagement.WebUI.Helpers;

public static class HtmlHelperExtensions
{
    public static string TimeZoneCookieName { get; } = "TimeZone";

    public static HtmlString ModifyDateTime<TModel>(this IHtmlHelper<TModel> helper, DateTimeOffset dateTimeOffset)
    {
        var timeZone = helper.ViewContext.HttpContext.Request.Cookies[TimeZoneCookieName];
        if (timeZone is not null)
        {
            return new HtmlString(dateTimeOffset.ToOffset(TimeSpan.Parse(timeZone)).DateTime.ToString(
                DateTimeFormatInfo.CurrentInfo.ShortTimePattern + " " + DateTimeFormatInfo.CurrentInfo.ShortDatePattern));
        }

        return new HtmlString(string.Empty);
    }

    public static void SaveUserCookies(HttpResponse response, User user)
    {
        response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(user.Language)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), });

        response.Cookies.Append(
            TimeZoneCookieName, user.TimeZone,
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), });
    }
}
