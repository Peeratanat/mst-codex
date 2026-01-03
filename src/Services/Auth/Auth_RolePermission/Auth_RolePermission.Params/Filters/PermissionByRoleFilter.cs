using System;
using System.Collections.Generic;

namespace Auth_RolePermission.Params.Filters
{
    public class PermissionByRoleFilter
    {
        /// <summary>
        /// ModuleIDs
        /// </summary>
        public List<Guid> ModuleIDs { get; set; }

        /// <summary>
        /// MenuIDs
        /// </summary>
        public List<Guid> MenuIDs { get; set; }

        /// <summary>
        /// MenuActionIDs
        /// </summary>
        public List<Guid> MenuActionIDs { get; set; }

        /// <summary>
        /// RoldIDs
        /// </summary>
        public List<Guid> RoldIDs { get; set; }
    }
}
