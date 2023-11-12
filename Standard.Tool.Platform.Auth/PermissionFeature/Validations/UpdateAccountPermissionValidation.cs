using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Standard.Tool.Platform.Auth.AccountFeature;

namespace Standard.Tool.Platform.Auth.PermissionFeature.Validations
{

    public class UpdateAccountPermissionValidation : AccountPermissionValidation<EditAccountPermissionRequest>
    {
        public UpdateAccountPermissionValidation()
        {
            ValidateCollectionCount();
        }
    }
}
