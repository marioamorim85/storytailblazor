namespace StoryTailBlazor.Models;

public class ActivityImage
{
    public int Id { get; set; }
    public int ActivityId { get; set; }
    public string Title { get; set; } // Título da imagem
    public string ImageUrl { get; set; } // URL da imagem associada à atividade

    // Relacionamento: Uma imagem pertence a uma atividade
    public Activity Activity { get; set; }
}
