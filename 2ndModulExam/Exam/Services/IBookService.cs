using Exam.Services.DTOs;

namespace Exam.Services;

public interface IBookService
{
    Guid AddBook(BookCreateDto bookCreateDto);
    List<BookDto> GetAllBooks();
    void DeleteBook(Guid id);
    void UpdateBook(BookDto bookDto);
    BookDto GetBookById(Guid id);
    List<BookDto> GetAllBooksByAuthor(string author);
    BookDto GetTopRatedBook();
    List<BookDto> GetBooksPublishedAfterYear(int year);
    BookDto GetMostPopularBook();
    List<BookDto> SearchBooksByTitle(string keyword);
    List<BookDto> GetBooksWithinPageRange(int minPage, int maxPage);
    int GetTotalCopiesSoldByAuthor(string author);
    List<BookDto> GetBooksSortedByRating();
    List<BookDto> GetRecentBooks(int year);
}