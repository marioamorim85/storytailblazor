using System.Text.Json.Serialization;

namespace StoryTailBlazor.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ReadTime { get; set; }
        public bool IsActive { get; set; }
        public int AccessLevel { get; set; }

        public int AgeGroupId { get; set; } // Relacionamento com a tabela AgeGroup
        public AgeGroup AgeGroup { get; set; }

        // Relacionamento: Um livro pode ter muitos autores
        [JsonIgnore]  // Evita o ciclo de referência com AuthorBooks
        public ICollection<AuthorBook> AuthorBooks { get; set; } = new List<AuthorBook>();

        // Relacionamento: Um livro pode ter muitas atividades
        [JsonIgnore]  // Evita o ciclo de referência com ActivityBooks
        public ICollection<ActivityBook> ActivityBooks { get; set; } = new List<ActivityBook>();

        // Relacionamento: Um livro pode ter muitos comentários
        [JsonIgnore]  // Evita o ciclo de referência com Comments
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        // Relacionamento: Um livro pode ter muitas tags
        [JsonIgnore]  // Evita o ciclo de referência com TaggingTagged
        public ICollection<TaggingTagged> TaggingTagged { get; set; } = new List<TaggingTagged>();

        // Relacionamento: Um livro pode ter muitas páginas
        [JsonIgnore]  // Evita o ciclo de referência com Pages
        public ICollection<Page> Pages { get; set; } = new List<Page>();

        // Relacionamento: Um livro pode ter muitos vídeos
        [JsonIgnore]  // Evita o ciclo de referência com Videos
        public ICollection<Video> Videos { get; set; } = new List<Video>();

        // Relacionamento: Um livro pode ser lido por muitos utilizadores
        [JsonIgnore]  // Evita o ciclo de referência com BooksRead
        public ICollection<BookUserRead> BooksRead { get; set; } = new List<BookUserRead>();

        // Relacionamento: Um livro pode ser favorito de muitos utilizadores
        [JsonIgnore]  // Evita o ciclo de referência com FavoriteBooks
        public ICollection<BookUserFavourite> FavoriteBooks { get; set; } = new List<BookUserFavourite>();
    }
}
