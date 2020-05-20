using System;

namespace Ray.Domain.OperatorInfo
{
    public class DefaultEntityOperatorInfoBuilder : IEntityOperatorInfoBuilder
    {
        public IEntityOperatorInfo Build()
        {
            return new EntityOperatorInfo
            {
                CreateId = null,
                CreateName = null,
                CreateTime = DateTime.Now,
                UpdateId = null,
                UpdateName = null,
                UpdateTime = DateTime.Now
            };
        }
    }
}
