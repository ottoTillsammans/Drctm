namespace ServerSln
{
    internal class User
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        
        public string? Password { get; set; }

        public State? State { get; set; }

        public User(int? id, string? name, string? password, State? state)
        {
            Id = id;
            Name = name;
            Password = password;
            State = state;
        }
    }
}
