namespace StoryTailBlazor.Models;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Relacionamento: Uma tag pode estar associada a muitos livros
    public ICollection<TaggingTagged> TaggingTagged { get; set; }
}
