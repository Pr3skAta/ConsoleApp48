using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public int Year { get; set; }
    public bool IsAvailable { get; set; }
}

class User
{
    public string Username { get; set; }
    public string FullName { get; set; }
    public List<Book> BorrowedBooks { get; set; } = new List<Book>();
}

class Library
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Director { get; set; }

    private List<Book> books = new List<Book>();
    private List<User> users = new List<User>();

    public void AddBook(Book book)
    {
        books.Add(book);
    }

    public void RemoveBook(string isbn)
    {
        Book bookToRemove = books.FirstOrDefault(x => x.ISBN == isbn);
        if (bookToRemove != null)
        {
            books.Remove(bookToRemove);
        }
    }

    public List<Book> SearchBookByTitle(string title)
    {
        return books.Where(x => x.Title.Contains(title)).ToList();
    }

    public void ListAllBooks()
    {
        foreach (var book in books)
        {
            Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, ISBN: {book.ISBN}, Year: {book.Year}, Available: {book.IsAvailable}");
        }
    }

    public void RegisterUser(User user)
    {
        users.Add(user);
    }

    public void BorrowBook(string isbn, string username)
    {
        Book bookToBorrow = books.FirstOrDefault(x => x.ISBN == isbn && x.IsAvailable);
        User user = users.FirstOrDefault(x => x.Username == username);

        if (bookToBorrow != null && user != null)
        {
            user.BorrowedBooks.Add(bookToBorrow);
            bookToBorrow.IsAvailable = false;
        }
    }

    public void ReturnBook(string isbn, string username)
    {
        Book bookToReturn = books.FirstOrDefault(x => x.ISBN == isbn);
        User user = users.FirstOrDefault(x => x.Username == username);

        if (bookToReturn != null && user != null)
        {
            user.BorrowedBooks.Remove(bookToReturn);
            bookToReturn.IsAvailable = true;
        }
    }
}

class Program
{
    static void Main()
    {
        Library library = new Library();
        library.Name = "My Library";
        library.Address = "123 Main Street";
        library.Director = "John Doe";

        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("Главно меню: ");
            Console.WriteLine("1. Добавяне на книга");
            Console.WriteLine("2. Премахване на книга");
            Console.WriteLine("3. Търсене на книга по заглавие");
            Console.WriteLine("4. Списък с всички книги");
            Console.WriteLine("5. Регистрация на потребител");
            Console.WriteLine("6. Заемане на книга");
            Console.WriteLine("7. Връщане на книга");
            Console.WriteLine("8. Изход");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:

                    Book newBook = new Book();
                    Console.Write("Въведете заглавие: ");
                    newBook.Title = Console.ReadLine();
                    Console.Write("Въведете автор: ");
                    newBook.Author = Console.ReadLine();
                    Console.Write("Въведете ISBN: ");
                    newBook.ISBN = Console.ReadLine();
                    Console.Write("Въведете година: ");
                    newBook.Year = int.Parse(Console.ReadLine());
                    newBook.IsAvailable = true;

                    library.AddBook(newBook);
                    Console.WriteLine("Книгата е добавена успешно!");
                    break;

                case 2:

                    Console.Write("Въведете ISBN на книгата за да премахнете: ");
                    string isbnToRemove = Console.ReadLine();
                    library.RemoveBook(isbnToRemove);
                    Console.WriteLine("Книгата е премахната успешно!");
                    break;

                case 3:

                    Console.Write("Въведете заглавие за да търсите: ");
                    string titleToSearch = Console.ReadLine();
                    List<Book> searchedBooks = library.SearchBookByTitle(titleToSearch);
                    foreach (var book in searchedBooks)
                    {
                        Console.WriteLine($"Заглавие: {book.Title}, Автор: {book.Author}, ISBN: {book.ISBN}, Година: {book.Year}, На разположение: {book.IsAvailable}");
                    }
                    break;

                case 4:

                    library.ListAllBooks();
                    break;

                case 5:

                    User newUser = new User();
                    Console.Write("Въведете Username: ");
                    newUser.Username = Console.ReadLine();
                    Console.Write("Въвдете цяло име: ");
                    newUser.FullName = Console.ReadLine();
                    library.RegisterUser(newUser);
                    Console.WriteLine("Потребителят е регистриран успешно!");
                    break;

                case 6:

                    Console.Write("Въведете ISBN на книгата за заемане: ");
                    string isbnToBorrow = Console.ReadLine();
                    Console.Write("Въведете потребителското име на потребителя: ");
                    string usernameToBorrow = Console.ReadLine();
                    library.BorrowBook(isbnToBorrow, usernameToBorrow);
                    Console.WriteLine("Книгата е заета успешно!");
                    break;

                case 7:

                    Console.Write("Въведете ISBN на книгата за връщане: ");
                    string isbnToReturn = Console.ReadLine();
                    Console.Write("Въведете потребителското име на потребителя: ");
                    string usernameToReturn = Console.ReadLine();
                    library.ReturnBook(isbnToReturn, usernameToReturn);
                    Console.WriteLine("Книгата е върната успешно!");
                    break;

                case 8:
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Невалиден избор. Моля, опитайте отново.");
                    break;
            }

            Console.WriteLine();
        }
    }
}
