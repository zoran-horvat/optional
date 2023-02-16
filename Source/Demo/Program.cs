using CodingHelmet.Optional;
using Demo.Models;

Person mann = Person.Create("Thomas", "Mann");
Person aristotle = Person.Create("Aristotle");
Person austen = Person.Create("Jane", "Austen");
Person asimov = Person.Create("Isaac", "Asimov");
Person marukami = Person.Create("Haruki", "Murakami");

Book faustus = Book.Create("Doctor Faustus", mann);
Book rhetoric = Book.Create("Rhetoric", aristotle);
Book nights = Book.Create("One Thousand and One Nights");
Book foundation = Book.Create("Foundation", asimov);
Book robots = Book.Create("I, Robot", asimov);
Book pride = Book.Create("Pride and Prejudice", austen);
Book mahabharata = Book.Create("Mahabharata");
Book windup = Book.Create("Windup Bird Chronicle", marukami);

IEnumerable<Book> library = new[] { faustus, rhetoric, nights, foundation, robots, pride, mahabharata, windup };

var bookshelf = library
    .GroupBy(GetAuthorInitial)
    .OrderBy(group => group.Key.Reduce(string.Empty));

foreach (var group in bookshelf)
{
    string header = group.Key.Map(initial => $"[ {initial} ]").Reduce("[   ]");
    foreach (var book in group)
    {
        Console.WriteLine($"{header} -> {book}");
        header = "     ";
    }
}

Console.WriteLine(new string('-', 40));

var authorNameLengths = library
    .GroupBy(GetAuthorNameLength)
    .OrderBy(group => group.Key.Reduce(0));

foreach (var group in authorNameLengths)
{
    string header = group.Key.Map(length => $"[ {length,2} ]").Reduce("[    ]");
    foreach (var book in group)
    {
        Console.WriteLine($"{header} -> {book}");
        header = "      ";
    }
}

ValueOption<int> GetAuthorNameLength(Book book) =>
    book.Author.Map(GetName).MapValue(s => s.Length);

string GetName(Person person) =>
    person.LastName
        .Map(lastName => $"{person.FirstName} {lastName}")
        .Reduce(person.FirstName);

Option<string> GetAuthorInitial(Book book)
{
    return book.Author.MapOptional(GetPersonInitial);
}

Option<string> GetPersonInitial(Person person) =>
    person.LastName
        .MapValue(GetInitial)
        .Reduce(() => GetInitial(person.FirstName));

Option<string> GetInitial(string name) =>
    name.WhereNot(string.IsNullOrWhiteSpace)
        .Map(s => s.TrimStart().Substring(0, 1).ToUpper());