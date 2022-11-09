namespace Communication.NoteContracts
{
    public record NoteCreated(int Id, string Content);
    public record NoteUpdated(int Id, string Content);
    public record NoteDeleted(int Id);
}
