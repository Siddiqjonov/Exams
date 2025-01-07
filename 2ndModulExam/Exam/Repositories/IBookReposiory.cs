using Exam.DataAccess.Entities;

namespace Exam.Repositories;

public interface IBookReposiory
{
    Guid Write(Book book);
    List<Book> ReadAll();
    void Delete(Guid id);
    void Update(Book book);
    Book GetById(Guid id);
}