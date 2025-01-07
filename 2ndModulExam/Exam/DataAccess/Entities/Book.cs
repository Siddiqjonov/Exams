using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.DataAccess.Entities;

public class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int Pages { get; set; }
    public double Rating { get; set; }
    public int NumberOfCopiesSold { get; set; }
    public DateTime PublishedDate { get; set; }
}
