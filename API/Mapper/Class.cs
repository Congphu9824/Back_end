namespace API.Mapper
{
    public class Class
    {
        public string? Name { get; set; }
        public int Id { get; set; }
        public int Description { get; set; }
        public Class(string? name, int id, int description)
        {
            Name = name;
            Id = id;
            Description = description;
        }

    }
}
