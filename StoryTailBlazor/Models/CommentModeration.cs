namespace StoryTailBlazor.Models;

public class CommentModeration
{
    public int Id { get; set; }
    public int CommentId { get; set; } // Relacionamento com a tabela Comment
    public Comment Comment { get; set; }

    public int UserId { get; set; } // Relacionamento com a tabela User (Administrador)
    public User User { get; set; }

    public string Status { get; set; }
    public DateTime ModerationDate { get; set; }
}
