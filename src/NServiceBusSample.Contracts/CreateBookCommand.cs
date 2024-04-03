namespace NServiceBusSample.Contracts;

public class CreateBookCommand
{
    
    public Guid BookId { get; set; }

    public string Title { get; set; }

    public string Author { get; set; }
    
}