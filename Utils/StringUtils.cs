using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNhaThuoc.Utils
{
    public static class StringUtils
    {
        /// <summary>
        ///     Chuyển một chuỗi số thành định dạng dễ đọc hơn
        ///     
        /// <list type="bullet">
        ///     <item> "1230" => "1,230" </item>
        ///     <item> "1230000" => "1,230,000" </item>
        ///     <item> "abc1234" => "abc1234" </item>
        /// </list>
        /// </summary>
        public static string FormatMoneyNumber(string number)
        {
            if (decimal.TryParse(number, out decimal res))
            {
                return FormatNumber(res);
            }
            return number;
        }

        /// <summary>
        ///     Chuyển một số thành chuỗi định dạng dễ đọc hơn
        ///     
        /// <list type="bullet">
        ///     <item> 1230 => "1,230" </item>
        ///     <item> 1230000 => "1,230,000" </item>
        /// </list>
        /// </summary>
        public static string FormatNumber(decimal number)
        {
            return number.ToString("#,##0");
        }
        
    }
}

