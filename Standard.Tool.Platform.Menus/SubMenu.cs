using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Tool.Platform.Menus
{
    public record SubMenu
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ScreenName { get; set; }

        public int DisplayOrder { get; set; }
    }
}
