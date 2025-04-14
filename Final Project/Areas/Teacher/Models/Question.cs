namespace Final_Project.Areas.Teacher.Models
{
    public class Question
    {
        public int Id { get; set; }
        public required string Tilte { get; set; }
        public string Type { get; set; }

        public int QustionMark { get; set; } = 1;
        public ICollection<Option> Options { get; set; }

    }
}
