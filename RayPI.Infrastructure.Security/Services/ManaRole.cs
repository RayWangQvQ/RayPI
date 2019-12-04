using System.Collections.Generic;
using System.Linq;
using RayPI.Infrastructure.Security.Models;

namespace RayPI.Infrastructure.Security.Services
{
    public class ManaRole
    {
        private static Dictionary<string, List<OneApiModel>> roleModel = new Dictionary<string, List<OneApiModel>>()
        {
            {"学生管理员",new List<OneApiModel>{new OneApiModel { ApiName="Student接口", ApiUrl= "/api/Student/Student" } } },
        };
        private static Dictionary<string, List<string>> userModel = new Dictionary<string, List<string>>()
        {
            {"stuAdmin",new List<string>{ "学生管理员" } }
        };

        public List<RoleModel> Roles
        {
            get
            {
                List<RoleModel> roles = new List<RoleModel>();
                foreach (var item in roleModel)
                {
                    roles.Add(new RoleModel() { RoleName = item.Key.ToLower(), Apis = item.Value });
                }
                return roles;
            }
        }

        public List<UserModel> User
        {
            get
            {
                List<UserModel> users = new List<UserModel>();
                foreach (var item in userModel)
                {
                    users.Add(new UserModel() { UserName = item.Key.ToLower(), BeRoles = item.Value });
                }
                return users;
            }
        }
        // 锁
        private static object objLock = new object();

        /// <summary>
        /// 检查是用户是否属于此角色
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="roleName">角色名</param>
        /// <returns></returns>
        public bool IsUserToRole(string userName, string roleName)
        {
            if (!userModel.ContainsKey(userName)) return false;
            List<string> rolesList = userModel[userName];
            return roleModel.Any(x => x.Key == roleName);
        }

        // 检查是否存在此角色
        public bool IsHasRole(string roleName)
        {
            return roleModel.Any(x => x.Key.ToLower() == roleName.ToLower());
        }

        // 检查是否存在此用户
        public bool IsHasUser(string userName)
        {
            return userModel.Any(x => x.Key.ToLower() == userName.ToLower());
        }

        // 增加一个角色
        protected bool AddRole(RoleModel role)
        {
            if (role == null)
                return false;
            if (roleModel.ContainsKey(role.RoleName.ToLower()))
                return false;
            roleModel.Add(role.RoleName.ToLower(), role.Apis);
            return true;
        }

        // 移除一个角色
        protected bool RemoveRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return false;
            bool result = roleModel.Remove(roleName);
            return result;
        }

        // 添加一个用户
        protected bool AddUser(UserModel user)
        {
            if (user == null)
                return false;
            if (roleModel.ContainsKey(user.UserName.ToLower()))
                return false;
            userModel.Add(user.UserName.ToLower(), user.BeRoles);
            return true;
        }

        // 移除一个用户
        protected bool RemoveUser(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return false;
            bool result = userModel.Remove(userName);
            return result;
        }

        // 获取一个用户所属的角色
        public UserModel GetUserBeRoles(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName) || !IsHasUser(userName))
            {
                UserModel user = default;
                return user;
            }
            var result = userModel[userName.ToLower()];
            return new UserModel
            {
                UserName = userName,
                BeRoles = result
            };
        }

        // 获取一个角色拥有的授权API
        public RoleModel GetRoleBeApis(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName) || !IsHasRole(roleName))
            {
                RoleModel role = default;
                return role;
            }
            var result = roleModel[roleName.ToLower()];
            return new RoleModel
            {
                RoleName = roleName,
                Apis = result
            };
        }
    }
}
