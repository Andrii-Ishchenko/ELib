namespace ELib.BL.DtoEntities
{
    public class CategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public int Level { get; set; }

        public int BookCount { get; set; }
    }
}
