using System;
using System.Runtime.Serialization;

namespace SYN.Core.Exceptions
{
    /// <summary>
    /// 配置异常
    /// </summary>
    public class ConfigurationException : Exception
    {
        public ConfigurationException() : base("缺少相关配置")
        {
        }

        public ConfigurationException(string configKey) : base($"缺少相关配置： {configKey}")
        {
        }

        public ConfigurationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
