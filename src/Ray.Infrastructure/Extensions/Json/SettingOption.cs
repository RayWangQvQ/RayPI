using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Ray.Infrastructure.Extensions.Json
{
    public class SettingOption
    {
        public JsonSerializerSettings SerializerSettings { get; set; }

        /// <summary>
        /// 忽略或只保留部分属性
        /// </summary>
        public FilterPropsOption FilterProps { get; set; }

        /// <summary>
        /// 枚举序列化为字符串
        /// </summary>
        public bool EnumToString { get; set; } = false;

        /// <summary>
        /// 构建
        /// </summary>
        /// <returns></returns>
        public JsonSerializerSettings BuildSettings()
        {
            if (SerializerSettings == null)
                SerializerSettings = new JsonSerializerSettings();

            //忽略/只保留部分属性
            if (FilterProps != null)
                SerializerSettings.ContractResolver = new FilterPropsContractResolver(FilterProps);

            //枚举处理
            if (EnumToString)
                SerializerSettings.Converters.Add(new StringEnumConverter());

            return SerializerSettings;
        }
    }
}
