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
            var obj = DateTime.Parse(objToConvert.ToString()).ToString("dd/MM/yyyy");

            return obj;
        }
        public static DateTime ToDateTime(this string objToConvert)
        {
            DateTime obj;

            DateTime.TryParse(objToConvert, out obj);

            return obj;
        }
        public static decimal ToDecimal(this string objToConvert)
        {
            decimal obj;

            decimal.TryParse(objToConvert, out obj);

            return obj;
        }

        public static int? getIdUsuarioFromToken(this string token)
        {            
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token.Replace("Bearer ", "")) as JwtSecurityToken;
            var idUsuario = jwtToken.Claims.FirstOrDefault(c => c.Type == "IdUsuario").Value.ToInteger();
            return idUsuario.Equals(null) ? 0 : idUsuario;
        }


    }
}
