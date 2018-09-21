using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UTH.Infrastructure.Utility;

namespace UTH.Framework
{
    public class AuthorizationService : IAuthorizeService
    {
        public async Task AuthorizeAsync(string permissions)
        {
            await CheckFeatures(permissions);
        }
        public async Task AuthorizeAsync(MethodInfo methodInfo, Type type)
        {
            await CheckFeatures(methodInfo, type);
        }

        private async Task CheckFeatures(string permissions)
        {
            //测试
            if (!permissions.IsEmpty() && permissions.Contains("testpermissions"))
            {
                throw new AuthorizeException(EnumCode.暂无权限);
            }

            await Task.FromResult(false);
        }
        private async Task CheckFeatures(MethodInfo methodInfo, Type type)
        {
            await Task.FromResult(false);
        }
    }
}
