using System.Security.Claims;
using GraphQL_Test2.Models;

namespace GraphQL_Test2;

public class Query
{
    public string GetMe(ClaimsPrincipal claimsPrincipal)
    {
        var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        return userId;
    }
    
    public Book GetBook() =>
        new Book
        {
            Title = "C# in depth.",
            Author = new Author
            {
                Name = "Jon Skeet"
            }
        };

    [UsePaging(MaxPageSize = 1, IncludeTotalCount = true)]
    public List<Book> GetBooks() =>
        new List<Book>
        {
            new Book
            {
                Title = "Pride & Prejudice",
                Author = new Author
                {
                    Name = "Jane Austin"
                }
            },
            new Book
            {
              Title = "Paper Towns",
              Author = new Author
              {
                  Name = "John Green"
              }
            },
            new Book
            {
                Title = "C# in depth.",
                Author = new Author
                {
                    Name = "Jon Skeet"
                }
            }
        };
}