namespace StoryTailBlazor.Models
{
    public class User
    {
        public int Id { get; set; }
        public int UserTypeId { get; set; } // Relacionamento com a tabela UserType
        public UserType UserType { get; set; } // Relacionamento com UserType

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? UserPhotoUrl { get; set; } // Permitir nulo

        // Relacionamento: Um utilizador pode ter muitas subscrições
        public ICollection<Subscription> Subscriptions { get; set; }

        // Relacionamento: Um utilizador pode ler muitos livros
        public ICollection<BookUserRead> BooksRead { get; set; }

        // Relacionamento: Um utilizador pode ter muitos livros favoritos
        public ICollection<BookUserFavourite> FavoriteBooks { get; set; }

        // Relacionamento: Um utilizador pode fazer muitos comentários
        public ICollection<Comment> Comments { get; set; }

        // Relacionamento: Um utilizador pode estar associado a muitas ActivityBookUser
        public ICollection<ActivityBookUser> ActivityBookUsers { get; set; }
    }
}