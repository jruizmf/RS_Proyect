using Microsoft.EntityFrameworkCore;
using RS_DataAccess.models;

namespace RS_DataAccess
{
    public interface IAppDBContext
    {
        DbSet<User> Users { get; set; }
        DbSet<UserProfile> UserProfiles { get; set; }
    }
}
