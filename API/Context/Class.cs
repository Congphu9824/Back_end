namespace API.Context
{
    public class Class
    {
        public string? Name { get; set; }
        public int Id { get; set; }
        public int description { get; set; }

        public Class(string? name, int id, int description)
        {
            Name = name;
            Id = id;
            this.description = description;
        }
    }
}
