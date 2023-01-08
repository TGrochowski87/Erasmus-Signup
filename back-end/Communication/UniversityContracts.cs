namespace ErasmusRabbitContracts.UniversityContracts
{
    public record ProfileGet(int UserId);
    public record ProfileGetResult(short? StudyDomainId, double? AverageGrade);
}
