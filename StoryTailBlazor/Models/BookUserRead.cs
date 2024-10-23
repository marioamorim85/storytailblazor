namespace StoryTailBlazor.Models;

public class BookUserRead
{
    public int BookId { get; set; }
    public Book Book { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public int Progress { get; set; }
    public int? Rating { get; set; }
    public DateTime? ReadDate { get; set; }
}
