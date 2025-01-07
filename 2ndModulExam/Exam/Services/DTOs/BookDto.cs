using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Services.DTOs;

public class BookDto : BookCreateDto
{
    public Guid Id { get; set; }
}
