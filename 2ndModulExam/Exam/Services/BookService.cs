using Exam.DataAccess.Entities;
using Exam.Repositories;
using Exam.Services.DTOs;
using Exam.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Services;

public class BookService : IBookService
{
    private IBookReposiory _bookReposiory;
    public BookService()
    {
        _bookReposiory = new BookReposiory();
    }
    public Guid AddBook(BookCreateDto bookCreateDto)
    {
        //var toBook = ConvertToEntity(bookCreateDto);
        //var id = _bookReposiory.Write(toBook);
        //return id;

        return _bookReposiory.Write(ConvertToEntity(bookCreateDto));
    }

    public void DeleteBook(Guid id)
    {
        _bookReposiory.Delete(id);
    }

    public List<BookDto> GetAllBooks()
    {
        List<BookDto> booksDto = new List<BookDto>();
        var books = _bookReposiory.ReadAll();
        foreach (var book in books)
        {
            booksDto.Add(ConvertToDto(book));
        }
        return booksDto;
    }

    public void UpdateBook(BookDto bookDto)
    {
        _bookReposiory.Update(ConvertToEntity(bookDto));
    }

    public List<BookDto> GetAllBooksByAuthor(string author)
    {
        var booksDto = GetAllBooks();
        var booksByAuthorDto = new List<BookDto>();
        foreach (var book in booksDto)
        {
            if (book.Author == author)
            {
                booksByAuthorDto.Add(book);
            }
        }
        return booksByAuthorDto;
    }

    public BookDto GetBookById(Guid id)
    {
        return ConvertToDto(_bookReposiory.GetById(id));
    }

    public List<BookDto> GetBooksPublishedAfterYear(int year)
    {
        var books = GetAllBooks();
        var booksAfterYear = new List<BookDto>();
        foreach (var book in books)
        {
            if (book.PublishedDate.Year >= year)
            {
                booksAfterYear.Add(book);
            }
        }
        return booksAfterYear;
    }

    public List<BookDto> GetBooksSortedByRating()
    {
        var books = GetAllBooks();
        int[] array= new int[books.Count];
        var booksSorted = new List<BookDto>();
        foreach (var book in books)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = Convert.ToInt32(book.Rating);
            }
        }
        Array.Sort(array);
        Array.Reverse(array);
        foreach (var num in array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if(num == books[i].Rating)
                {
                    booksSorted.Add(books[i]);
                }
            }
        }
        return booksSorted;
    }

    public List<BookDto> GetBooksWithinPageRange(int minPage, int maxPage)
    {
        var books = GetAllBooks();
        var booksToReturn = new List<BookDto>();
        foreach (var book in books)
        {
            if (book.GetBookPages() >= minPage && book.GetBookPages() <= maxPage)
            {
                booksToReturn.Add(book);
            }
        }
        return booksToReturn;
    }

    public BookDto GetMostPopularBook()
    {
        var books = GetAllBooks();
        int[] array = new int[books.Count];
        foreach (var book in books)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = book.NumberOfCopiesSold;
            }
        }
        Array.Sort(array);
        Array.Reverse(array);
        foreach (var book in books)
        {
            if (book.NumberOfCopiesSold == array[0])
            {
                return book;
            }
        }
        throw new Exception("SystemFailed");
    }

    public List<BookDto> GetRecentBooks(int year)
    {
        var books = GetAllBooks();
        var recentBooks = new List<BookDto>();
        foreach (var book in books)
        {
            if(book.PublishedDate.Year == DateTime.Now.Year)
            {
                recentBooks.Add(book);
            }
        }
        return recentBooks;
    }

    public BookDto GetTopRatedBook()
    {
        var books = GetBooksSortedByRating();
        BookDto book = books[0];
        return book;
    }

    public int GetTotalCopiesSoldByAuthor(string author)
    {
        var booksByAuthor = GetAllBooksByAuthor(author);
        return booksByAuthor.Count;
    }

    public List<BookDto> SearchBooksByTitle(string keyword)
    {
        var books = GetAllBooks();
        var booksByTitle = new List<BookDto>();
        foreach (var book in books)
        {
            if (book.Title.Contains(keyword))
            {
                booksByTitle.Add(book);
            }
        }
        return booksByTitle;
    }

    private Book ConvertToEntity(BookCreateDto bookCreateDto)
    {
        return new Book()
        {
            Id = Guid.NewGuid(),
            Title = bookCreateDto.Title,
            Author = bookCreateDto.Author,
            Pages = bookCreateDto.Pages,
            Rating = bookCreateDto.Rating,
            NumberOfCopiesSold = bookCreateDto.NumberOfCopiesSold,
            PublishedDate = bookCreateDto.PublishedDate,
        };
    }

    private Book ConvertToEntity(BookDto bookDto)
    {
        return new Book()
        {
            Id = Guid.NewGuid(),
            Title = bookDto.Title,
            Author = bookDto.Author,
            Pages = bookDto.Pages,
            Rating = bookDto.Rating,
            NumberOfCopiesSold = bookDto.NumberOfCopiesSold,
            PublishedDate = bookDto.PublishedDate,
        };
    }

    private BookDto ConvertToDto(Book book)
    {
        return new BookDto()
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Pages = book.Pages,
            Rating = book.Rating,
            NumberOfCopiesSold = book.NumberOfCopiesSold,
            PublishedDate = book.PublishedDate,
        };
    }
}
