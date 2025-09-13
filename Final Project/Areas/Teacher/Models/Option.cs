namespace Final_Project.Areas.Teacher.Models
{
    public class Option
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public bool IsCorrect { get; set; } = false;
        public required int QustionId { get; set; }
        public Question Question { get; set; }

    }
}
