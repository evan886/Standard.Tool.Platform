using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Tool.Platform.Locations.Validations
{
    public class CreateLocationValidation : LocationValidation<EditLocationRequest>
    {
        public CreateLocationValidation()
        {
            ValidateCollectionCount();
        }
    }
}
