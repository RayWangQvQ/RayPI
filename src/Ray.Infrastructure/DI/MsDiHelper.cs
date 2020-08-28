using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ray.Infrastructure.DI.Dtos;
using Ray.Infrastructure.Extensions.Json;

namespace Ray.Infrastructure.DI
{
    public class MsDiHelper
    {
        public static string SerializeServiceDescriptors(IServiceProvider serviceProvider, Action<SerializeServiceDescriptorOptions> options)
        {
            var serializeOptions = new SerializeServiceDescriptorOptions();
            options(serializeOptions);

            var descs = serviceProvider.GetServiceDescriptorsFromScope()
                .Select(x => new ServiceDescriptorDto(x));

            string jsonStr = descs.AsJsonStr(option =>
            {
                option.SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                    NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                };
                option.EnumToString = true;
                option.FilterProps = new Ray.Infrastructure.Extensions.Json.FilterPropsOption
                {
                    FilterEnum = Ray.Infrastructure.Extensions.Json.FilterEnum.Ignore,
                    Props = serializeOptions.IgnorePropsAll.ToArray()
                };
            }).AsFormatJsonStr();

            return jsonStr;
        }
    }
}
