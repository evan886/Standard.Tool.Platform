using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Tool.Platform.Materials.Validations
{
    public class CreateMaterialValidation : MaterialValidation<EditMaterialRequest>
    {
        public CreateMaterialValidation()
        {
            ValidateCollectionCount();
        }
    }
}
