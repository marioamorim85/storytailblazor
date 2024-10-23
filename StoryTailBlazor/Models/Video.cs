namespace StoryTailBlazor.Models;

public class Video
{
    public int Id { get; set; }
    public int BookId { get; set; } // Relacionamento com a tabela Book
    public Book Book { get; set; }

    public string Title { get; set; }
    public string VideoUrl { get; set; }
}
