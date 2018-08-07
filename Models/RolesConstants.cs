using System;
using System.Collections.Generic;
using System.Reflection;

namespace NewBlogAPI.Models
{
    /// <summary>
    /// Here should be declared only roles public constant fields,
    /// or <see cref="GetAllRoles()"/> will fail.
    /// </summary>
    public static class RolesConstants
    {
        public const string ROLE_SUPER_ADMIN    = "SuperAdmin";
        public const string ROLE_ADMIN          = "Admin";

        public static List<string> GetAllRoles()
        {
            Type selfType = MethodBase.GetCurrentMethod().DeclaringType;

            return new  List<FieldInfo>(selfType.
                                        GetFields()).
                            ConvertAll(new Converter<FieldInfo, string>(
                                input => 
                                    input.GetValue(selfType).ToString()));
        }
    }
}