namespace TodoApplication.Model
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; } //personal
        public string Priority { get; set; } //High Low 
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
