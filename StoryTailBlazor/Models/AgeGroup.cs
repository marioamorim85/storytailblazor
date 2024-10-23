namespace StoryTailBlazor.Models;

public class AgeGroup
{
    public int Id { get; set; }
    public string AgeGroupDescription { get; set; }

    // Relacionamento: Uma faixa etária pode ter muitos livros
    public ICollection<Book> Books { get; set; }
}
