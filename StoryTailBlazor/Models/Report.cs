namespace StoryTailBlazor.Models;

public class Report
{
    public int Id { get; set; }
    public string ReportType { get; set; } // Tipo de relatório (ex: 'book_clicks', 'book_ratings')
    public string ReportData { get; set; } // Armazena dados do relatório em formato JSON
}
