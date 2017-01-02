using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace CachePostRest
{
    public class MyIdentityUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
