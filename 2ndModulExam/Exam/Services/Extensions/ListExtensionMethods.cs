using Exam.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Services.Extensions;

public static class ListExtensionMethods
{
    public  static int NumberOfCopiesSold(this List<BookDto> booksDto)
    {
        return booksDto.Count;
    }
}
