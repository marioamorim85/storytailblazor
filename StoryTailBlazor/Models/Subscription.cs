namespace StoryTailBlazor.Models;

public class Subscription
{
    public int Id { get; set; }
    public int UserId { get; set; } // Relacionamento com a tabela User
    public User User { get; set; }

    public int PlanId { get; set; } // Relacionamento com a tabela Plan
    public Plan Plan { get; set; }

    public DateTime StartDate { get; set; }
}
