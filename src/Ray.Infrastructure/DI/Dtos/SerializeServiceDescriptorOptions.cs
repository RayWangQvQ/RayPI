using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.Infrastructure.DI.Dtos
{
    public class SerializeServiceDescriptorOptions
    {
        /// <summary>
        /// 添加忽略的属性
        /// </summary>
        public List<String> IgnorePropsAdditional { get; set; } = new List<string>();

        /// <summary>
        /// 默认忽略的属性
        /// </summary>
        public List<String> IgnorePropsDefault { get; set; } = new List<string>
        {
            "UsePollingFileWatcher",
            "Action",
            "Method",
            "Assembly"
        };

        /// <summary>
        /// 是否实例化实例对象
        /// </summary>
        public bool IsSerializeImplementationInstance { get; set; } = false;

        public List<string> IgnorePropsAll
        {
            get
            {
                var result = new List<string>();
                result.AddRange(IgnorePropsDefault ?? new List<string>());
                result.AddRange(IgnorePropsAdditional ?? new List<string>());
                if (!IsSerializeImplementationInstance) result.Add("ImplementationInstanceObj");
                return result;
            }
        }
    }
}
