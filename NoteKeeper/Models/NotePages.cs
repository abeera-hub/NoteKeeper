
namespace NoteKeeper.Models;

public class NotePage
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Content { get; set; }

    public int NotebookId { get; set; }

    public string UserId { get; set; } = string.Empty;
}
