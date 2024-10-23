namespace StoryTailBlazor.Models;

public class BookClick
{
    public int Id { get; set; }
    public int BookId { get; set; } // Relacionamento com a tabela Book
    public Book Book { get; set; }

    public DateTime ClickedAt { get; set; }
}
