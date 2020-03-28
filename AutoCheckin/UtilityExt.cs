using System.Net;
using System.Text;
using System.IO;

namespace AutoCheckin
{
    public static class UtilityExt
    {
        public static string Decode(this string str)
        {
            Encoding win1251 = Encoding.GetEncoding("Windows-1251");
            byte[] utf8Bytes = win1251.GetBytes(str);
            return Encoding.UTF8.GetString(utf8Bytes);
        }

        public static string FindSubstring(this string text, string start, string end)
        {
            int startIndex = text.IndexOf(start);
            int endIndex = text.IndexOf(end, startIndex);
            if (startIndex >= 0 && endIndex >= 0) return text.Substring(startIndex + start.Length, endIndex - startIndex - start.Length);
            else return "-1";
        }

        /*public static string UploadString(this HttpWebRequest request, string data)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(data);
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }*/
    }
}
