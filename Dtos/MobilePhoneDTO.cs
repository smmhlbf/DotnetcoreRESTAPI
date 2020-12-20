using System;

namespace DotnetcoreRESTAPI.Dtos
{
    public record MobilePhoneDto
    {
        public Guid Id { get; init; }
        public string Type { get; init; }
        public string Name { get; init; }
        public decimal SellPrice { get; init; }
        public DateTimeOffset CreateDate { get; init; }
        public DateTimeOffset? ModifiedDate { get; init; }
    }
}