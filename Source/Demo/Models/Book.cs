using CodingHelmet.Optional;

namespace Demo.Models;

public class Book
{
    public string Title { get; }
    public Option<Person> Author { get; }

    private Book(string title, Option<Person> author) =>
        (Title, Author) = (title, author);

    public static Book Create(string title) =>
        new(title, Option<Person>.None());

    public static Book Create(string title, Person author) =>
        new(title, Option<Person>.Some(author));

    public override string ToString() =>
        Author.Map(author => $"{Title} by {author}").Reduce(Title);
}