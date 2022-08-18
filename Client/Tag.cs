namespace Client
{
    public class Tag
    {
        private string Name { get; set; }

        public Tag(string name)
        {
            Name = name;
        }

        public string Open() => string.Format("<{0}>", Name);

        public string Close() => string.Format("</{0}>", Name);
    }
}
