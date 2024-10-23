namespace StoryTailBlazor.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string? AuthorPhotoUrl { get; set; } // Permitir nulo
        public string Nationality { get; set; }

        // Relacionamento: Um autor pode ter muitos livros
        public ICollection<AuthorBook> AuthorBooks { get; set; }
    }
}

