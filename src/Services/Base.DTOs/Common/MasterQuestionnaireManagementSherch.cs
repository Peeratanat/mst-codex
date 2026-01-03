namespace Base.DTOs.Common
{
     
    public partial class MasterQuestionnaireManagementSherch
    {
        public string? Questionnal { get; set; }
        public string? CRMInterface { get; set; }
        public string? S3Interface { get; set; }
        public string? Status { get; set; }
        public string? QuestionCategoryKey { get; set; }
        
        public int Take { get; set; }
        public int Skip { get; set; }
        public string? SortOrder { get; set; }
    } 
}
