using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ela_Kai_Pou.Entities.Models
{
    public class AppRole: IdentityRole
    {
        public AppRole() : base() { }

        public AppRole(string name) : base(name) { }

    }
}
