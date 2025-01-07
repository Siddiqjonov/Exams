using Exam.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Services.Extensions;

public static class BookDtoExtensionMethods
{
    public static int GetBookPages(this BookDto bookDto)
    {
        return bookDto.Pages;
    }
}
