namespace StoryTailBlazor.Models;

public class UserType
{
    public int Id { get; set; }
    public string UserTypeName { get; set; } // ex: 'admin', 'premium', 'free'

    // Relacionamento: Um tipo de utilizador pode ter muitos utilizadores
    public ICollection<User> Users { get; set; }
}
