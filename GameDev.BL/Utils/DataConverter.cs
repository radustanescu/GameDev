using System;
using System.Security.Cryptography;
using System.Text;

namespace GameDev.BL.Utils
{
    public class DataConverter
    {
        public Guid ToGuid(object data, Guid nullValue)
        {
            if (data == null || data == DBNull.Value)
                return nullValue;

            if (data is Guid)
                return (Guid)data;

            Guid g;

            try
            {
                g = new Guid(data.ToString());
            }
            catch
            {
                g = nullValue;
            }

            return g;
        }

        public Guid StringToGuid(string value)
        {
            var md5Hasher = MD5.Create();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            return new Guid(data);
        }
    }
}