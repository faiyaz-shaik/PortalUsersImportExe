using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalUsersImportExe.Model
{
    public class ApplicationDto
    {
        public int Id { get; set; }

        public Guid OpenIdApplicationId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
