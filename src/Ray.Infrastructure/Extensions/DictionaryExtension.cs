using System.Text;

namespace System.Collections.Generic
{
    public static class DictionaryExtension
    {
        public static void AddIfNotExist<TKey, TValue>(this Dictionary<TKey, TValue> dic, Dictionary<TKey, TValue> addDic)
        {
            foreach (var item in addDic)
            {
                dic.AddIfNotExist(item.Key, item.Value);
            }
        }

        public static void AddIfNotExist<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, TValue value)
        {
            if (dic.ContainsKey(key)) return;
            dic.Add(key, value);
        }

        public static string GetSelectionToStr(this Dictionary<string, string> dic)
        {
            var sb = new StringBuilder();
            foreach (var item in dic)
            {
                sb.AppendLine($"{item.Key}：{item.Value}");
            }

            return sb.ToString();
        }
    }
}
