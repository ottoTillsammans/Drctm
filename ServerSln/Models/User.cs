using System;

namespace ServerSln
{
    internal class User
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        
        public string? Password { get; set; }

        public State? State { get; set; }
    }
}
