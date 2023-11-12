using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Tool.Platform.Auth.AccountFeature.Validations
{
    public class CreateAccountValidation : AccountValidation<EditAccountRequest>
    {
        public CreateAccountValidation()
        {
            ValidateAccountUserName();
            ValidateAccountLoginName();
        }
    }
}
