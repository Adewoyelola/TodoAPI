namespace TodoAPIClass.Models
{
    public class Todo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsDeleted { get; set; } = false;


       
    }
}
