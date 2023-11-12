using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Tool.Platform.Data.Entities;

public class AccountPermissionEntity
{
    public Guid AccountId { get; set; }
    public Guid PermissionId { get; set; }

    public virtual AccountEntity Account { get; set; }
    public virtual PermissionEntity Permission { get; set; }
}
