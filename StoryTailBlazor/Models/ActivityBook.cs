namespace StoryTailBlazor.Models;

public class ActivityBook
{
    
    public int Id { get; set; }
    public int ActivityId { get; set; }
    public Activity Activity { get; set; }

    public int BookId { get; set; }
    public Book Book { get; set; }
    
    public ICollection<ActivityBookUser> ActivityBookUsers { get; set; }
}
