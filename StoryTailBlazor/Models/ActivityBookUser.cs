namespace StoryTailBlazor.Models
{
    public class ActivityBookUser
    {
        public int ActivityBookId { get; set; }  // Chave estrangeira referenciando ActivityBook
        public ActivityBook ActivityBook { get; set; }  // Relacionamento com ActivityBook

        public int UserId { get; set; }  // Relacionamento com User
        public User User { get; set; }

        public int Progress { get; set; }  // Progresso do usuário
    }
}