using System;

namespace LoggerEngine.Util
{
    public static class ValidationUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <param name="paramName"></param>
        public static void CheckArgumentNull<T>(T param, string paramName)
            where T : class
        {
            if (param == null)
                throw new ArgumentNullException(paramName);

        }
    }
}
