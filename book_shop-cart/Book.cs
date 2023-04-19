namespace Books
{
    public class Book
    {
        public string Author { get; }
        public string  Title { get; } 
        public decimal Price { get; }

        public Book(string author, string title, decimal price)
        {
            Author = author;
            Title  = title;
            Price  = price;
        }

        public override string ToString()
        {
            string formatedTitle = $"\"{Title}\"";
            string formatedPrice = $"[{Price}]₴";
            return $"|| {Author, -15} :: {formatedTitle, -30} || вартість {formatedPrice, -10} ||\n";
        }
    }
}