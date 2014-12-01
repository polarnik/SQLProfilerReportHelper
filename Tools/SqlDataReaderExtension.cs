namespace Tools
{
    using System;
	using System.Data.SqlClient;

	public static class SqlDataReaderExtension
    {
        public static bool? GetBooleanOrNull(this SqlDataReader reader, int index)
        {
            bool? result;
            if (reader.IsDBNull(index))
            {
                result = null;
            }
            else
            {
                result = reader.GetBoolean(index);
            }
            return result;
        }

		public static long? GetLongOrNull(this SqlDataReader reader, int index)
		{
			long? result;
			if (reader.IsDBNull(index))
			{
				result = null;
			}
			else
			{
				result = reader.GetInt64(index);
			}
			return result;
		}

		public static float? GetFloatOrNull(this SqlDataReader reader, int index)
		{
			float? result;
			if (reader.IsDBNull(index))
			{
				result = null;
			}
			else
			{
				result = reader.GetFloat(index);
			}
			return result;
		}

		public static double? GetDoubleOrNull(this SqlDataReader reader, int index)
		{
			double? result;
			if (reader.IsDBNull(index))
			{
				result = null;
			}
			else
			{
				result = reader.GetDouble(index);
			}
			return result;
		}

		public static int? GetIntOrNull(this SqlDataReader reader, int index)
        {
            int? result;
            if (reader.IsDBNull(index))
            {
                result = null;
            }
            else
            {
                result = reader.GetInt32(index);
            }
            return result;
        }

        public static DateTime? GetDateTimeOrNull(this SqlDataReader reader, int index)
        {
            DateTime? result;
            if (reader.IsDBNull(index))
            {
                result = null;
            }
            else
            {
                result = reader.GetDateTime(index);
            }
            return result;
        }

        public static string GetStringOrNull(this SqlDataReader reader, int index)
        {
            string result;
            if (reader.IsDBNull(index))
            {
                result = null;
            }
            else
            {
                result = reader.GetString(index);
            }
            return result;
        }

        public static Guid? GetGuidOrNull(this SqlDataReader reader, int index)
        {
            Guid? result;
            if (reader.IsDBNull(index))
            {
                result = null;
            }
            else
            {
                result = reader.GetGuid(index);
            }
            return result;
        }
    }
}
