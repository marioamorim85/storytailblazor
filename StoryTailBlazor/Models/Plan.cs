namespace StoryTailBlazor.Models;

public class Plan
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int AccessLevel { get; set; } // ex: 'Free', 'Premium'

    // Relacionamento: Um plano pode ter muitas subscrições
    public ICollection<Subscription> Subscriptions { get; set; }
}
