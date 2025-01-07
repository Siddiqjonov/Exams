using Exam.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Exam.Repositories;

public class BookReposiory : IBookReposiory
{
    private List<Book> _books;
    private string _path;
    public BookReposiory()
    {
        _books = new List<Book>();
        _path = "../../../DataAccess/Data/Books.json";
        if (!File.Exists(_path))
        {
            File.WriteAllText(_path, "[]");
        }
        _books = ReadAll();
    }
    public void Delete(Guid id)
    {
        var studentFromDb = GetById(id);
        _books.Remove(studentFromDb);
        SaveData();
    }

    public Book GetById(Guid id)
    {
        var books = ReadAll();
        foreach (var book in books)
        {
            if (book.Id == id) return book;
        }
        throw new Exception("InvalidIdEntered Id: " + id);
    }

    public List<Book> ReadAll()
    {
        var booksJson = File.ReadAllText(_path);
        var books = JsonSerializer.Deserialize<List<Book>>(booksJson);
        return books;
    }

    public void Update(Book book)
    {
        var bookFromDb = GetById(book.Id);
        var index = _books.IndexOf(book);
        _books[index] = book;
        SaveData();
    }

    public Guid Write(Book book)
    {
        _books.Add(book);
        SaveData();
        return book.Id;
    }
    private void SaveData()
    {
        var booksJson = JsonSerializer.Serialize(_books);
        File.WriteAllText(_path, booksJson);
    }
}
