namespace StoryTailBlazor.Models;

public class Activity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    // Relacionamento: Uma atividade pode estar associada a muitos livros
    public ICollection<ActivityBook> ActivityBooks { get; set; }
}
