using System;
using System.Collections.Generic;
using DotnetcoreRESTAPI.Dtos;
using DotnetcoreRESTAPI.Models;

namespace DotnetcoreRESTAPI.Services
{
    public interface IRepository
    {
        MobilePhone GetMobilePhone(Guid id);
        List<MobilePhone> GetMobilePhones();
        void CreateMobilePhone(MobilePhone mobilePhone);
        void UpdateMobilePhone(MobilePhone mobilePhone);
        void DeleteMobilePhone(Guid id);
    }
}