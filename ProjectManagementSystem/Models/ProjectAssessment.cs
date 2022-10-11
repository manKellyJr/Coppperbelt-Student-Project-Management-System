namespace ProjectManagementSystem.Models
{
    public class ProjectAssessment
    {
       
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }         
        public string MarkedBy { get; set; }
        public DateTime Date { get; set; }
        public Grade IntellectualQuality { get; set; }
        public Grade KnowledgeOfResearch { get; set; }
        public Grade QualityOfPresentation { get; set; }
        public Grade ResponseToQuestions { get; set; }
        public SecondGrade StructureOfPresentation { get; set; }
        public SecondGrade ReferencingAndAppendices { get; set; }
        public int Total { get => (int)IntellectualQuality+(int)KnowledgeOfResearch+(int)QualityOfPresentation+(int)ResponseToQuestions+(int)StructureOfPresentation+(int)ReferencingAndAppendices; }
    }
    public enum Grade:int
    {   None,
        Bad = 2,
        Fair = 4,
        Good = 6,
        VeryGood = 8,
        Excellent = 10,
    }   
    public enum SecondGrade:int
    {
        None,
        Bad = 1,
        Fair = 2,
        Good = 3,
        VeryGood = 4,
        Excellent = 5,

    }

}
