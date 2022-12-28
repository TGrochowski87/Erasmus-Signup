namespace ErasmusRabbitContracts.OpinionContracts
{
    public record OpinionCreated(int Id, int SpecialityId, int UserId, int Rating);
    public record OpinionUpdated(int Id, int SpecialityId, int UserId, int Rating);
    public record OpinionDeleted(int Id);
}
