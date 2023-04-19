using System.Text;

using Books;
using BookShop;

internal static class Progam
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Catalog catalog = new();
        Cart cart = new();

        ConsoleKeyInfo keyInfo;
        bool run = true;

        Book book1 = new("Сергій Жадан", "Інтернат", 257.99m);
        Book book2 = new("Джордж Орвелл", "1984", 156m);
        Book book3 = new("Осаму Дадзай", "Сповідь Неповноцінної Людини", 610.66m);

        catalog.Add(book1);
        catalog.Add(book2);
        catalog.Add(book3);

        while (run)
        {
            do
            {
                Console.Clear();

                Console.WriteLine("ESC - вихід\n");
                catalog.OutputBooks();
                catalog.OutputStatistics();

                Console.Write("{1} - додати книгу до кошика" +
                            "\n{2} - перейти до кошика" +
                            "\n_");

                keyInfo = Console.ReadKey();
            }
            while (keyInfo.KeyChar != '1'
            && keyInfo.KeyChar != '2'
            && keyInfo.Key != ConsoleKey.Escape);

            switch(keyInfo.KeyChar)
            {
                case '1':

                    int index;

                    do
                    {
                        Console.Clear();

                        Console.WriteLine("ESC - назад\n");
                        catalog.OutputBooks();

                        Console.Write("введіть номер книги у каталозі" +
                                    "\n(номер йде по порядку зверху вниз)" +
                                    "\n_");

                        keyInfo = Console.ReadKey();
                        index = Convert.ToInt32(keyInfo.KeyChar) - 49;
                    }
                    while (keyInfo.Key != ConsoleKey.Escape
                    && catalog.GetBook(index) == null);

                    if (keyInfo.Key == ConsoleKey.Escape)
                        break;

                    if (!cart.Find(catalog.GetBook(index)))
                    {
                        cart.Add(catalog.GetBook(index));

                        Console.Clear();
                        Console.Write("книгу успішно додано до кошика!");

                    }
                    else
                    {
                        Console.Clear();
                        Console.Write("дана книга вже у кошику");
                    }

                    Console.Write("\n\nнатисність будь-яку клавішу..");
                    Console.ReadKey();

                    break;

                case '2':

                    Console.Clear();

                    BookPriceTotaller totaller = new();
                    cart.BooksProcessing(totaller.Add);

                    if (totaller.Count == 0)
                    {
                        Console.Write("кошик пустий" +
                                  "\n\nнатисність будь-яку клавішу..");
                        Console.ReadKey();
                        break;
                    }

                    do
                    {
                        Console.Clear();

                        Console.WriteLine("ESC - назад\n");
                        cart.OutputBooks();
                        cart.OutputStatistics();

                        Console.Write("{1} - видалити певну книгу з кошика" +
                                    "\n{2} - очистити кошик" +
                                    "\n{0} - зробити замовлення" +
                                    "\n_");

                        keyInfo = Console.ReadKey();
                    }
                    while (keyInfo.KeyChar != '1'
                    && keyInfo.KeyChar != '2' 
                    && keyInfo.KeyChar != '0'
                    && keyInfo.Key != ConsoleKey.Escape);

                    switch(keyInfo.KeyChar)
                    {
                        case '1':

                            do
                            {
                                Console.Clear();

                                Console.WriteLine("ESC - назад\n");
                                cart.OutputBooks();

                                Console.Write("введіть номер книги у кошику" +
                                    "\n(номер йде по порядку зверху вниз)" +
                                    "\n_");

                                keyInfo = Console.ReadKey();
                                index = Convert.ToInt32(keyInfo.KeyChar) - 49;
                            }
                            while (keyInfo.Key != ConsoleKey.Escape
                            && cart.GetBook(index) == null);

                            cart.Remove(index);

                            Console.Clear();
                            Console.WriteLine("книгу видалено" +
                                          "\n\nнатисність будь-яку клавішу..");

                            Console.ReadKey();

                            break;

                        case '2':

                            Console.Clear();
                            cart.Clear();
                            Console.WriteLine("кошик очищено" +
                                          "\n\nнатисність будь-яку клавішу..");

                            Console.ReadKey();

                            break;

                        case '0':

                            Console.Clear();
                            cart.Order();
                            Console.WriteLine("натисність будь-яку клавішу..");

                            Console.ReadKey();

                            break;

                        default:
                            break;
                    }

                    break;

                default:

                    run = false;

                    break;
            }
        }
    }
}