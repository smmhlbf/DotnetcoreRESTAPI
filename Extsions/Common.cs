using DotnetcoreRESTAPI.Dtos;
using DotnetcoreRESTAPI.Models;

namespace DotnetcoreRESTAPI.Extsions
{
    public static class Common
    {
        public static MobilePhoneDto AsDto(this MobilePhone mobilePhone) => new MobilePhoneDto()
        {
            Id = mobilePhone.Id,
            Type = mobilePhone.Type,
            Name = mobilePhone.Name,
            SellPrice = mobilePhone.OriginPrice * (decimal)mobilePhone.Discount,
            CreateDate = mobilePhone.CreateDate,
            ModifiedDate = mobilePhone.ModifiedDate
        };
    }
}