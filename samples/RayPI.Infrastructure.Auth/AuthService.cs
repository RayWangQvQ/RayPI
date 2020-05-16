using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using RayPI.Infrastructure.Auth.Enums;
using RayPI.Infrastructure.Auth.Jwt;

namespace RayPI.Infrastructure.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IJwtService _jwtService;

        public AuthService(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        public string GetToken(string userName, List<string> roleCodeList)
        {
            Claim[] userClaims = _jwtService.BuildClaims(userName, roleCodeList);
            string tokenStr = _jwtService.BuildToken(userClaims);
            return tokenStr;
        }

        public List<Permission> GetPermissions(List<string> roleCodeList)
        {
            List<Permission> re = new List<Permission>();
            if (roleCodeList == null) return re;
            foreach (var roleCode in roleCodeList)
            {
                List<Permission> l = GetPermissions(roleCode);
                re.AddRange(l);
            }
            return re;
        }

        private List<Permission> GetPermissions(string roleCode)
        {
            switch (roleCode)
            {
                case "StudentAdmin":
                    return new List<Permission>
                    {
                        new Permission(OperateEnum.Retrieve.ToString(), ResourceEnum.Student.ToString()),
                        new Permission(OperateEnum.Create.ToString(),ResourceEnum.Student.ToString()),
                        new Permission(OperateEnum.Delete.ToString(),ResourceEnum.Student.ToString()),
                        new Permission(OperateEnum.Update.ToString(),ResourceEnum.Student.ToString()),
                    };
                case "TeacherAdmin":
                    return new List<Permission>
                    {
                        new Permission(OperateEnum.Retrieve.ToString(), ResourceEnum.Teacher.ToString()),
                        new Permission(OperateEnum.Create.ToString(), ResourceEnum.Teacher.ToString()),
                        new Permission(OperateEnum.Delete.ToString(), ResourceEnum.Teacher.ToString()),
                        new Permission(OperateEnum.Update.ToString(), ResourceEnum.Teacher.ToString()),
                    };
                default:
                    return new List<Permission>();
            }
        }
    }
}
