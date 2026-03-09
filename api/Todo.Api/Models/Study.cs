namespace Todo.Api.Models;

public class Study
{

    public int Id { get; set; }
    public string PatientName { get; set; }
    public DateTime StudyDate { get; set; }
    public string StudyInstanceUID { get; set; }
    public string OrthancStudyId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}