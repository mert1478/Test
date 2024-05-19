using D1TechTestCase.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D1TechTestCase.Repository.Seeds
{
    public class UserSeed : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User { 
                    Id = Guid.Parse("212ec710-146e-4ea2-b3ce-894041f0d35f"),
                    UserName = "Username1", 
                    NormalizedUserName = "username1", 
                    PasswordHash = "e43d1148859359692d0029a4eab11f38b4abe08651a6494fe918b5d242997ba0", 
                    SecurityStamp= "4SQt0BzbEKvLy6xqQcQcpRWYGGNcqcVK", 
                    UpdatedDate = DateTime.UtcNow,
                    CreatedDate = DateTime.UtcNow,
                    Name = "User1",
                    Surname = "Surname1",
                    // password = Username1.
                },
                new User
                {
                    Id = Guid.Parse("dd5530f4-5666-4cb7-985f-2fb4af8bf1f3"),
                    UserName = "Username2",
                    NormalizedUserName = "username2",
                    PasswordHash = "0c626bc1bb36f539438b2dfeb9dad4ba91e0ea1a137b6ea4f4e77d3ad85ccdbd",
                    SecurityStamp = "39f0VgWxGH8X9cqn9Mwn4zf0a4bY0fmS",
                    UpdatedDate = DateTime.UtcNow,
                    CreatedDate = DateTime.UtcNow,
                    Name = "User2",
                    Surname = "Surname2",
                    // password = Username2.
                },
                new User
                {
                    Id = Guid.Parse("64e12255-c39c-4129-805a-d1d62043f356"),
                    UserName = "Admin1",
                    NormalizedUserName = "admin1",
                    PasswordHash = "42065179a605734438267e630d871655eee6bf86d2df5e0c95ca253ea83807da",
                    SecurityStamp = "fM1jtK7Y5xqAbEFa2uc0DQdheCaBYMq0",
                    UpdatedDate = DateTime.UtcNow,
                    CreatedDate = DateTime.UtcNow,
                    Name = "Admin1",
                    Surname = "Admin1",
                    // password = Admin1.
                });
        }
    }
}
