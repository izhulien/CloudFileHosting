namespace CFH.Models
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Security.Claims;
    using Microsoft.AspNet.Identity;

    public class ApplicationUser : IdentityUser
    {
        private ICollection<File> files;

        public ApplicationUser()
        {
            this.files = new HashSet<File>();
        }

        public virtual ICollection<File> Files
        {
            get
            {
                return this.files;
            }

            set
            {
                this.files = value;
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            return userIdentity;
        }
    }
}
