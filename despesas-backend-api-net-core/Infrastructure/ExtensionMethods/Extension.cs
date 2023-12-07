using System.Globalization;
using System.IdentityModel.Tokens.Jwt;

namespace despesas_backend_api_net_core.Infrastructure.ExtensionMethods
{
    public static class Extension
    {
        public static int ToInteger(this string strToConvert)
        {
            int strConvert;
            int.TryParse(strToConvert, out strConvert);
            return strConvert;
        }

        public static int ToInteger(this object objToConvert)
        {
            int objConvert;
            int.TryParse(objToConvert.ToString(), out objConvert);
            return objConvert;            
        }
        public static string ToDateBr(this DateTime objToConvert)
        {
            CultureInfo cultureInfo = new CultureInfo("pt-BR");
            string formattedDate = objToConvert.ToString("dd/MM/yyyy", cultureInfo);
            return formattedDate;
        }
        public static DateTime ToDateTime(this string objToConvert)
        {
            DateTime obj;
            CultureInfo cultureInfo = new CultureInfo("pt-BR");
            DateTime.TryParse(objToConvert, cultureInfo,  out obj);

            return obj;
        }
        public static decimal ToDecimal(this string objToConvert)
        {
            decimal obj;
            CultureInfo cultureInfo = new CultureInfo("pt-BR");
            decimal.TryParse(objToConvert, cultureInfo, out obj);

            return obj;
        }
    }
}