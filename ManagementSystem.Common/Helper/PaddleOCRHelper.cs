
using System.Text;
using System.Text.RegularExpressions;



namespace ManagementSystem.Common.Helper
{
    public static class PaddleOCRHelper
    {
        ///// <summary>
        ///// 解析姓名
        ///// </summary>
        ///// <param name="textBlocks"></param>
        ///// <returns></returns>
        //public static string ReadName(this List<TextBlock> textBlocks)
        //{
        //    string result = "";
        //    foreach (var item in textBlocks)
        //    {
        //        string txt = item.Text.Replace(" ", "").Trim();
        //        //排除其他项干扰
        //        if (txt.Contains("性别") || txt.Contains("民族") || txt.Contains("住址") || txt.Contains("公民身份证号码") || txt.Contains("身份") || txt.Contains("号码"))
        //        {
        //            continue;
        //        }
        //        if (Regex.IsMatch(txt, @"^姓名[\u4e00-\u9fa5]{2,4}$"))
        //        {
        //            result = txt.TrimStart('姓', '名');
        //            break;
        //        }
        //        else if (Regex.IsMatch(txt, @"^名[\u4e00-\u9fa5]{2,4}$"))
        //        {
        //            result = txt.TrimStart('名');
        //            break;

        //        }
        //        else if (Regex.IsMatch(txt, @"^[\u4e00-\u9fa5]{2,4}$"))
        //        {
        //            result = txt;
        //            break;
        //        }
        //    }
        //    return result;

        //}

        ///// <summary>
        ///// 解析身份证号码
        ///// </summary>
        ///// <param name="textBlocks"></param>
        ///// <returns></returns>
        // public static string ReadCardNo(this List<TextBlock> textBlocks)
        //{
        //    string result = "";
        //    foreach (var item in textBlocks)
        //    {
        //        string txt = item.Text.Replace(" ", "").Trim();
        //        if (Regex.IsMatch(txt, @"^\d{15}$|^\d{17}(\d|X|x)$"))
        //        {
        //            result = txt;
        //            break;
        //        }
        //    }
        //    return result;
        //}

        ///// <summary>
        ///// 解析民族
        ///// </summary>
        ///// <param name="textBlocks"></param>
        ///// <returns></returns>
        // public static string ReadNation(this List<TextBlock> textBlocks)
        //{
        //    string result = "";
        //    foreach (var item in textBlocks)
        //    {
        //        Regex regex = new Regex(@"民族(\w+)");
        //        Match match = regex.Match(item.Text);
        //        if (match.Success)
        //        {
        //            result = match.Groups[1].Value;
        //        }
        //    }
        //    return result;
        //}
        ///// <summary>
        ///// 解析地址
        ///// </summary>
        ///// <param name="textBlocks"></param>
        ///// <returns></returns>
        // public static string ReadAddr(this List<TextBlock> textBlocks)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    string[] temps = { "省", "市", "县", "区", "镇", "乡", "村", "组", "室", "栋", "街道", "号" };
        //    foreach (var item in textBlocks)
        //    {
        //        string txt = item.Text.Replace(" ", "").Trim();

        //        //排除干扰
        //        if (txt.Contains("姓名") || txt.Contains("号码"))
        //        {
        //            continue;
        //        }
        //        if (temps.Where(t => txt.Contains(t)).Count() > 0)
        //        {
        //            sb.Append(txt);
        //        }
        //    }
        //    sb = sb.Replace("住址", "");
        //    return sb.ToString();
        //}
    }
}
