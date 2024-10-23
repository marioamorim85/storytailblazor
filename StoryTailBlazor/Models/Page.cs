namespace StoryTailBlazor.Models;

public class Page
{
    public int Id { get; set; }
    public int BookId { get; set; } // Relacionamento com a tabela Book
    public Book Book { get; set; }

    public string PageImageUrl { get; set; }
    public string? AudioUrl { get; set; } // Permitir nulo

    public int PageIndex { get; set; }
}
