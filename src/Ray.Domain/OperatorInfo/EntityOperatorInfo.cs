using System;

namespace Ray.Domain.OperatorInfo
{
    public class EntityOperatorInfo: IEntityOperatorInfo
    {
        public string CreateName { get; set; }
        public long? CreateId { get; set; }
        public DateTime? CreateTime { get; set; }
        public string UpdateName { get; set; }
        public long? UpdateId { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
