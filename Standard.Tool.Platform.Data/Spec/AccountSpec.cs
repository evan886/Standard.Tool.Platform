using Microsoft.EntityFrameworkCore;
using Standard.Tool.Platform.Data.Entities;
using Standard.Tool.Platform.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Tool.Platform.Data.Spec
{
    public sealed class AccountSpec : BaseSpecification<AccountEntity>
    {
        public AccountSpec(Guid id) : base(p => p.Id.Equals(id))
        {
            AddInclude(account => account
                .Include(p => p.AccountPermissions));
        }
    }

}
