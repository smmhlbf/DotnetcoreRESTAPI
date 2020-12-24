using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotnetcoreRESTAPI.Dtos;
using DotnetcoreRESTAPI.Models;

namespace DotnetcoreRESTAPI.Services
{
    public interface IRepository
    {
        #region Sync module
        // MobilePhone GetMobilePhone(Guid id);
        // IEnumerable<MobilePhone> GetMobilePhones();
        // void CreateMobilePhone(MobilePhone mobilePhone);
        // void UpdateMobilePhone(MobilePhone mobilePhone);
        // void DeleteMobilePhone(Guid id);
        #endregion
        #region Async module
        Task<MobilePhone> GetMobilePhoneAsync(Guid id);
        Task<IEnumerable<MobilePhone>> GetMobilePhonesAsync();
        Task CreateMobilePhoneAsync(MobilePhone mobilePhone);
        Task UpdateMobilePhoneAsync(MobilePhone mobilePhone);
        Task DeleteMobilePhoneAsync(Guid id);
        #endregion
    }
}