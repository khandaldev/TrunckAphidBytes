using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts.Model
{
    public class SocialNetworkModel
    {
        public Guid ID { get; set; }
        public System.DateTime Expires { get; set; }
        public string Access_Token { get; set; }
        public Guid Aphid_id { get; set; }
        public string category { get; set; }
        public bool IsDeleted { get; set; }
        public string RefereshToken { get; set; }
        [Required(ErrorMessage = "*")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "*")]
        public string Password { get; set; }
        public string NewCount { get; set; }
        public string categorytype { get; set; }
        public string CurrentStatusSocial { get; set; }
    }
}
