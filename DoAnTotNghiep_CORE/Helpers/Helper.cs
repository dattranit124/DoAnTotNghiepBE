using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DoAnTotNghiep_CORE.Helpers
{
   public class Helper
    {
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public static string GenId()
        {
            return ObjectId.GenerateNewId().ToString();
        }
        public static string GenSlug(string name)
        {
            return VNToEn(name).ToLower().Trim().Replace(" ", "-");
        }
        public static object HadleExceptionResult(Exception ex)
        {
            var err = new
            {
                devMsg = ex.Message,
                userMsg = "Có lỗi ! Vui lòng liên hệ Admin"
            };
            return err;
        }
        public static string VNToEn(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
    }
}
