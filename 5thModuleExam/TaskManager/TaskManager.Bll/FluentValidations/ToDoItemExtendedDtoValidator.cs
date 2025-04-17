using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Bll.Dtos;

namespace TaskManager.Bll.FluentValidations;

public class ToDoItemExtendedDtoValidator : AbstractValidator<ToDoItemExtendedDto>
{
    public ToDoItemExtendedDtoValidator()
    {
        RuleFor(t => t.Title).
            MaximumLength(50).
            NotEmpty().
            WithMessage("Title is required");

        RuleFor(t => t.Description).
            MaximumLength(255).
            NotEmpty().
            WithMessage("Description is required");

        RuleFor(t => t.CreatedAt).
            NotEmpty().
            NotNull().
            WithMessage("CreatedAt is required");

        RuleFor(t => t.DueDate).
            NotEmpty().
            NotNull().
            WithMessage("DueDate is required");

        //RuleFor(i => i.IsCompleted).
        //    NotEmpty().
        //    WithMessage("IsCompleted is required");
    }
}
