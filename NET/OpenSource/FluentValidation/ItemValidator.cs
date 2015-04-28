using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using OpenSource.Model;

namespace OpenSource.FluentValidation
{
    public class ItemValidator : AbstractValidator<Item>
    {
        public ItemValidator()
        {
            RuleFor(item => item.ItemNumber)
                .NotEmpty()
                .Length(5, 10)
                .WithName("9SI");
            RuleFor(item => item.Title).NotEmpty().Length(2 /* 含包min */, 10 /* 含包max */);
            RuleFor(item => item.Price).ExclusiveBetween(0 /* 不含包from */, 100 /* 不含包to */);
        }
    }
}
