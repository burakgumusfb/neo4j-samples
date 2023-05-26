using System;
using Neo4j.Samples.Domain.Common;

namespace Neo4j.Samples.Domain.Entities
{
    public class User : BaseEntity
    {	
        public int Id { get;  set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public User(int id, string email, string password)
        {
            this.Id = id;
            this.Email = email;
            this.Password = password;
        }
        public User(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }
    }
}

