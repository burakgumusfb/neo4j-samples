using System;
using Neo4j.Samples.Domain.Common;

namespace Neo4j.Samples.Domain.Entities
{
    public class User : BaseEntity
    {	
        public int Id { get;  set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public User(string productCode, string productName)
        {
            this.Email = productCode;
            this.Password = productName;
        }
    }
}

