namespace StoryTailBlazor.Models;

public class Comment
{
    public int Id { get; set; }
    public int BookId { get; set; } // Relacionamento com a tabela Book
    public Book Book { get; set; }

    public int UserId { get; set; } // Relacionamento com a tabela User
    public User User { get; set; }

    public string CommentText { get; set; }
    public string Status { get; set; }
}
