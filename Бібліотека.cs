using System;
using System.Collections.Generic;

class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
    public string ISBN { get; set; }
}

class Library
{
    public List<Book> Books { get; } = new List<Book>();

    public void GetBooks()
    {
        int index = 0;
        foreach (var book in Books)
        {
            Console.WriteLine($"{index}. {book.Title} {book.Author} {book.Year}");
            ++index;
        }
        Console.WriteLine();
    }

    public void AddBook()
    {
        Book book = new Book();
        Console.Write("Введіть назву: ");
        book.Title = Console.ReadLine();
        Console.Write("Введіть автора: ");
        book.Author = Console.ReadLine();
        Console.Write("Введіть рік випуску: ");
        if (int.TryParse(Console.ReadLine(), out int year))
        {
            book.Year = year;
            Books.Add(book);
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Спробуйте ще раз");
            AddBook();
        }
    }

    public void RemoveBook()
    {
        Console.Write("Введіть номер книги у списку:");
        if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < Books.Count)
        {
            Books.RemoveAt(index);
        }
        else
        {
            Console.Clear();
            GetBooks();
            RemoveBook();
        }
    }

    public void SearchBook()
    {
        Console.Write("Введіть ключовий пункт (назва, автор чи рік видання):> ");
        string filter = Console.ReadLine();
        Console.Clear();
        foreach (var book in Books)
        {
            if (book.Author.Contains(filter) || book.Title.Contains(filter))
            {
                Console.WriteLine($"{book.Title} {book.Author} {book.Year}");
            }
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        Library library = new Library();
        Menu menu = new Menu(library);
        menu.StartMenu();
    }
}

class Menu
{
    private Library library;
    private int stateMenu;

    public Menu(Library library)
    {
        this.library = library;
    }

    public void GetMenu()
    {
       
        Console.WriteLine("[1] Список книжок");
        Console.WriteLine("[2] Пошук книжки");
        Console.WriteLine("[3] Додати книжку");
        Console.WriteLine("[4] Видалити книжку");
        Console.WriteLine("[5] Очистити консоль");
        Console.WriteLine("[6] Вихід");
        Console.Write("Введіть пункт: ");
        if (int.TryParse(Console.ReadLine(), out int state))
        {
            stateMenu = state;
        }
        else
        {
            Console.Clear();
            GetMenu();
        }
    }

    public void ClearConsole()
    {
        Console.Clear();
        GetMenu();
    }

    public void StartMenu()
    {
        ClearConsole();

        while (stateMenu != 0)
        {
            switch (stateMenu)
            {
                case 1:
                    Console.Clear();
                    library.GetBooks();
                    GetMenu();
                    break;
                case 2:
                    Console.Clear();
                    library.SearchBook();
                    GetMenu();
                    break;
                case 3:
                    Console.Clear();
                    library.AddBook();
                    ClearConsole();
                    break;
                case 4:
                    if (library.Books.Count > 0)
                    {
                        Console.Clear();
                        library.GetBooks();
                        library.RemoveBook();
                        Console.Clear();
                        library.GetBooks();
                        GetMenu();
                    }
                    else
                    {
                        ClearConsole();
                    }
                    break;
                case 5:
                    ClearConsole();
                    break;
                default:
                    Console.Clear();
                    ClearConsole();
                    break;
            }
        }
    }
}
