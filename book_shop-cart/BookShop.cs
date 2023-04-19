namespace BookShop
{
    using Books;

    public delegate void BookDelegate(Book book);

    class BookPriceTotaller
    {
        int count;
        decimal totalPrice;

        public int Count => count;
        public decimal TotalPrice => totalPrice;
        public decimal AveragePrice => totalPrice / count;

        public void Add(Book book)
        {
            count += 1;
            totalPrice += book.Price;
        }
    }

    class Catalog
    {
        private List<Book> books = new();

        public void Add(Book book)
        { books.Add(book); }

        public void Add(string author, string name, decimal price)
        { books.Add(new Book(author, name, price)); }

        public void Remove(Book book)
        { books.Remove(book); }

        public void Remove(int index)
        {
            try
            {
                books.Remove(books[index]);
            }
            catch(ArgumentOutOfRangeException)
            {
                Console.WriteLine("(!) помилковий індекс");
            }
        }

        public void Remove(string title)
        { books = books.Where(book => book.Title != title).ToList(); }

        public Book? GetBook(int index)
        {
            try
            {
                return books[index];
            }
            catch(ArgumentOutOfRangeException)
            {
                Console.WriteLine("(!) помилковий індекс");
                return null;
            }
        }

        public Book? GetBook(string title)
        {
            Book? SearchedBook = books.Find(book => book.Title == title);

            if (SearchedBook == null)
                Console.WriteLine("(!) книги за шуканим ім'ям не знайдено");

            return SearchedBook;
        }

        public bool Find(Book book)
        { return books.Contains(book); }

        public void BooksProcessing(BookDelegate bookDelegate)
        {
            foreach(Book book in books)
            {
                bookDelegate(book);
            }
        }

        public void OutputBooks()
        {
            Console.WriteLine("(/) КАТАЛОГ:\n");
            BooksProcessing(Console.WriteLine);
            Console.Write("\n");
        }

        public void OutputStatistics()
        {
            BookPriceTotaller totaller = new();
            BooksProcessing(totaller.Add);

            Console.WriteLine($"всього книг => [{totaller.Count}]" +
                             "\nсередня вартість => [{0:#.##}]₴\n", totaller.AveragePrice);
        }
    }

    class Cart
    {
        private List<Book> books = new();

        public void Add(Book book)
        { books.Add(book); }

        public Book? GetBook(int index)
        {
            try
            {
                return books[index];
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("(!) помилковий індекс");
                return null;
            }
        }

        public void Remove(int index)
        {
            try
            {
                books.Remove(books[index]);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("(!) помилковий індекс");
            }
        }

        public void Clear()
        { books.Clear(); }

        public bool Find(Book book)
        { return books.Contains(book); }

        public void BooksProcessing(BookDelegate bookDelegate)
        {
            foreach (Book book in books)
            {
                bookDelegate(book);
            }
        }

        public void OutputBooks()
        {
            Console.WriteLine("(#) КОШИК:\n");
            BooksProcessing(Console.WriteLine);
            Console.Write("\n");
        }

        public void OutputStatistics()
        {
            BookPriceTotaller totaller = new();
            BooksProcessing(totaller.Add);

            Console.WriteLine($"всього книг => [{totaller.Count}]" +
                            "\nзагальна вартість => [{0:#.##}]₴\n", totaller.TotalPrice);
        }

        public void Order()
        {
            BookPriceTotaller totaller = new();
            BooksProcessing(totaller.Add);

            Console.WriteLine($"замовлення прийнято!" +
                            $"\nкниг замовлено [{totaller.Count}]" +
                             "\nзагальна вартість покупки [{0:#.##}]₴\n", totaller.TotalPrice);

            Clear();
        }
    }
}