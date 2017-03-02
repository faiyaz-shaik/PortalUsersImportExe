using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalUsersImportExe.Model
{
    public class User
    {
        public int Id { get; set; }
        public Guid? OpenIdUserId { get; set; }

        public long? TurasUserId { get; set; }
        public string Title { get; set; }

        public string GivenName { get; set; }

        public string MiddleName { get; set; }

        public string Surname { get; set; }

        public string PrimaryEmailAddress { get; set; }

        public string Password { get; set; }

        //public List<SecurityRoleDto> SecurityRoles { get; set; }

        //public List<ApplicationDto> Applications { get; set; }


    }
}
