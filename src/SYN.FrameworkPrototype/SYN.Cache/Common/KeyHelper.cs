using SYN.Common.Config;
using SYN.Core.Exceptions;

namespace SYN.Cache.Common
{
    /// <summary>
    /// 缓存Key帮助类
    /// </summary>
    public static class KeyHelper
    {
        #region 属性

        private static string _region;

        /// <summary>
        /// 代码区分区域
        /// </summary>
        private static string Region
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_region))
                {
                    var key = "Cache:Redis:Region";
                    _region = ConfigHelper.GetValue(key);
                    if (string.IsNullOrWhiteSpace(_region))
                    {
                        throw new ConfigurationException(key);
                    }
                }

                return _region;
            }
        }

        #endregion

        /// <summary>
        /// 获取标准Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetKey(string key)
        {
            return $"{Region}:{key}";
        }

    }
}
