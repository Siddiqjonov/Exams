using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Bll.Dtos;

namespace TaskManager.Bll.FluentValidations;

public class ToDoItemCreateDtoValidator : AbstractValidator<ToDoItemCreateDto>
{
    public ToDoItemCreateDtoValidator()
    {
        RuleFor(t => t.Title).
            MaximumLength(50).
            NotEmpty().
            WithMessage("Title is required");

        RuleFor(t => t.Description).
            MaximumLength(255).
            NotEmpty().
            WithMessage("Description is required");

        RuleFor(t => t.DueDate).
            NotEmpty().
            NotNull().
            WithMessage("DueDate is required");
    }
}
