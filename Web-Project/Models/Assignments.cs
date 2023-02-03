namespace Web_Project.Models
{
    public class Assignments
    {
        public int Id { get; set; }

        public string AssignmentName { get; set; }

        public string Description { get;set; }

        public string SubmitingTime { get; set; }   

        public  int MaxScore { get; set; }

        public int SecionId { get; set; }

    }
}
