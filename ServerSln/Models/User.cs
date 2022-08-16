namespace ServerSln
{
    public class User
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        
        public string? Password { get; set; }

        public State? State { get; set; }

        public User(string? name, string? password, State? state)
        {
            Name = name;
            Password = password;
            State = state;
        }
    }
}
