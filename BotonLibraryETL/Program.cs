using System.Security.Principal;

var lines = File.ReadAllLines(Path.Combine(AppContext.BaseDirectory, "legacy_books.csv"));
var headers = lines[0].Split(',');
var rawRecords = lines.Skip(1).Select(l => l.Split(',')).ToList();
Console.WriteLine($"Extracted {rawRecords.Count} records.");

var transformed = rawRecords.Select(r => new TransformedBook
{
    Id = int.Parse(r[0]),
    Title = ToTitleCase(r[1]),
    Author = ToTitleCase(r[2]),
    Genre = string.IsNullOrWhiteSpace(r[3]) ? "General" : ToTitleCase(r[3]),
    Available = r[4].Trim().ToUpper() == "YES",
    PublishedYear = int.Parse(r[5].Trim())
}).ToList();
static string ToTitleCase(string s) => System.Globalization.CultureInfo.CurrentCulture.TextInfo
    .ToTitleCase(s.Trim().ToLower());

foreach (var b in transformed)
    Console.WriteLine($"{b.Id} | {b.Title} | {b.Author} | {b.Genre}  | {b.Available} | {b.PublishedYear} |");

using var client = new HttpClient();
client.DefaultRequestVersion = new Version(1, 1);
client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
client.BaseAddress = new Uri("https://cuariolibrarynowapi-c1n9.onrender.com");

foreach (var book in transformed)
{
    var json = System.Text.Json.JsonSerializer.Serialize(book);
    var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
    var response = await client.PostAsync("api/v1/books", content);
    Console.WriteLine($"Loaded: {book.Title} ? {response.StatusCode}");
}


class TransformedBook
{
    public int Id { get; set; }

    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? Genre { get; set; }
    public bool Available { get; set; }
    public int PublishedYear { get; set; }
}
