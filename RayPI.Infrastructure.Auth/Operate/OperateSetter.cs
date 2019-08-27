using RayPI.Infrastructure.Auth.Models;
using RayPI.Treasury.Interfaces;
using RayPI.Treasury.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.Infrastructure.Auth.Operate
{
    public class OperateSetter : IEntityBaseAutoSetter
    {
        /// <summary>The _operate content</summary>
        private readonly TokenModel _tokenModel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operateInfo">Content of the operate.</param>
        public OperateSetter(IOperateInfo operateInfo)
        {
            _tokenModel = operateInfo.TokenModel;
        }

        /// <summary>创建人姓名</summary>
        /// <value>The name of the create.</value>
        public string CreateName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_tokenModel?.Uname))
                    return string.Empty;
                return _tokenModel?.Uname;

            }
        }

        /// <summary>创建人Id</summary>
        /// <value>The create identifier.</value>
        public long CreateId => _tokenModel?.Uid ?? -1L;

        /// <summary>创建时间</summary>
        /// <value>The create time.</value>
        public DateTime CreateTime => DateTime.Now;

        /// <summary>更新人姓名</summary>
        /// <value>The name of the update.</value>
        public string UpdateName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_tokenModel?.Uname))
                    return string.Empty;
                return _tokenModel?.Uname;
            }
        }

        /// <summary>更新人Id</summary>
        /// <value>The update identifier.</value>
        public long UpdateId => this._tokenModel?.Uid ?? -1L;

        /// <summary>更新时间</summary>
        /// <value>The update time.</value>
        public DateTime UpdateTime => DateTime.Now;
    }
}
