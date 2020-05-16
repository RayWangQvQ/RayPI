using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Ray.Infrastructure.Extensions.Json
{
    public class FilterPropsContractResolver : DefaultContractResolver
    {
        private readonly string[] _props = null;
        private readonly FilterEnum _retain;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="props">传入的属性数组</param>
        /// <param name="retain">true:表示props是需要保留的字段  false：表示props是要排除的字段</param>
        public FilterPropsContractResolver(string[] props, FilterEnum retain = FilterEnum.Ignore)
        {
            this._props = props;
            this._retain = retain;
        }

        public FilterPropsContractResolver(FilterPropsOption ignoreOption)
        {
            this._props = ignoreOption.Props;
            this._retain = ignoreOption.FilterEnum;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> list = base.CreateProperties(type, memberSerialization);
            return list.Where(p => _retain == FilterEnum.Ignore
                ? !_props.Contains(p.PropertyName)
                : _props.Contains(p.PropertyName))
                .ToList();
        }
    }
}
