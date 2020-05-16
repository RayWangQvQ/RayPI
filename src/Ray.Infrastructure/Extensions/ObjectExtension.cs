using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System
{
    public static class ObjectExtension
    {
        #region BindingFlags常用组合
        /**
         * Default = 0x00,默认值
         * 用于查找：
         * IgnoreCase = 0x01,// 表示查找的时候，需要忽略大小写。
         * DeclaredOnly = 0x02,// 仅查找此特定类型中声明的成员，而不会包括这个类继承得到的成员。
         * Instance = 0x04,// 仅查找类型中的实例成员，与Static互补。
         * Static = 0x08,// 仅查找类型中的静态成员，与Instance互补。
         * Public = 0x10,// 仅查找类型中的公共成员。
         * NonPublic = 0x20,// 仅查找类型中的非公共成员（internal protected private）
         * FlattenHierarchy = 0x40,// 会查找此特定类型继承树上得到的静态成员。但仅继承公共（public）静态成员和受保护（protected）静态成员；不包含私有静态成员，也不包含嵌套类型。
         * 调用：
         * InvokeMethod = 0x0100,// 调用方法。
         * CreateInstance = 0x0200,// 创建实例。
         * GetField = 0x0400,// 获取字段的值。
         * SetField = 0x0800,// 设置字段的值。
         * GetProperty = 0x1000,// 获取属性的值。
         * SetProperty = 0x2000,// 设置属性的值。
         * 其他：
         * PutDispProperty = 0x4000,
         * PutRefDispProperty = 0x8000,
         * ExactBinding = 0x010000,
         * SuppressChangeType = 0x020000,
         * OptionalParamBinding = 0x040000,
         * IgnoreReturn = 0x01000000,// 忽略返回值（在 COM 组件的互操作中使用）
         * DoNotWrapExceptions = 0x02000000,// 此标记用于禁止把异常包装到 TargetInvocationException 中。
         */
        //所有成员
        public static BindingFlags FlagsOfAll => BindingFlags.NonPublic
                                                | BindingFlags.Public
                                                | BindingFlags.Static
                                                | BindingFlags.Instance;
        //所有当前类的成员，不包括继承自父类的
        public static BindingFlags FlagsOfAllCurrent => FlagsOfAll | BindingFlags.DeclaredOnly;
        //所有公有实例成员
        public static BindingFlags FlagsOfAllPulic => BindingFlags.Public | BindingFlags.Instance;
        //所有当前类公有成员
        public static BindingFlags FlagsOfAllPulicCurrent => FlagsOfAllPulic | BindingFlags.DeclaredOnly;
        #endregion

        /// <summary>
        /// 获取对象的字段和值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fieldName"></param>
        /// <param name="bindingFlags"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetFieldsWithValue(this object obj, string fieldName = null, BindingFlags? bindingFlags = null, bool includingBase = true)
        {

            Type type = obj.GetType();

            return GetFieldsWithValue(type, obj, fieldName, bindingFlags, includingBase);
        }

        private static Dictionary<string, object> GetFieldsWithValue(Type type, object obj, string fieldName = null, BindingFlags? bindingFlags = null, bool includingBase = false)
        {
            var dic = new Dictionary<string, object>();

            //筛选BindingFlags
            BindingFlags flags = bindingFlags ?? FlagsOfAll;

            //获取字段
            FieldInfo[] fieldInfos = type.GetFields(flags);

            //筛选字段名称（精确筛选）
            if (!string.IsNullOrWhiteSpace(fieldName)) fieldInfos = fieldInfos.Where(x => x.Name == fieldName).ToArray();

            //取值
            foreach (var fi in fieldInfos)
            {
                dic.Add(fi.Name, fi.GetValue(obj));
            }

            if (includingBase && (type = type.BaseType) != typeof(object))
            {
                var baseDic = GetFieldsWithValue(type, obj, fieldName, bindingFlags, true);
                dic.AddIfNotExist(baseDic);
            }

            return dic;
        }

        /// <summary>
        /// 利用反射获取实例的某个字段值
        /// （包括私有变量）
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fieldName"></param>
        /// <returns>返回装箱后的object对象</returns>
        public static object GetFieldValue(this object obj, string fieldName)
        {
            try
            {
                return obj.GetFieldsWithValue(fieldName)
                    .FirstOrDefault()
                    .Value;
            }
            catch
            {
                //todo:记录日志
                return default;
            }
        }

        /// <summary>
        /// 获取对象的属性和值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fieldName"></param>
        /// <param name="index"></param>
        /// <param name="bindingFlags"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetPropertiesWithValue(this object obj, string fieldName = null, object[] index = null, BindingFlags? bindingFlags = null)
        {
            var dic = new Dictionary<string, object>();

            Type type = obj.GetType();

            //筛选BindingFlags
            BindingFlags flags = bindingFlags ?? FlagsOfAll;

            PropertyInfo[] pis = type.GetProperties(flags);

            //筛选属性名称（精确筛选）
            if (!string.IsNullOrWhiteSpace(fieldName)) pis = pis.Where(x => x.Name == fieldName).ToArray();

            //取值
            foreach (var fi in pis)
            {
                dic.Add(fi.Name, fi.GetValue(obj));
            }

            return dic;
        }

        /// <summary>
        /// 利用反射获取实例的某个属性值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fieldName"></param>
        /// <param name="index"></param>
        /// <returns>返回装箱后的object对象</returns>
        public static object GetPropertyValue(this object obj, string fieldName, object[] index = null)
        {
            try
            {
                return obj.GetPropertiesWithValue(fieldName, index)
                    .FirstOrDefault()
                    .Value;
            }
            catch
            {
                //todo:记录日志
                return default;
            }
        }
    }
}
