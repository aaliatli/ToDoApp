using System.ComponentModel.DataAnnotations;
public class Task
{
    [Key]
    public int Id { get; set; } // Birincil Anahtar
    public string Title { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public bool Done { get; set; }
}

