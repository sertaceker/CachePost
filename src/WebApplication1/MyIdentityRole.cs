using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CachePostRest
{
    public class MyIdentityRole : IdentityRole
    {
        public string Description { get; set; }
    }
}
