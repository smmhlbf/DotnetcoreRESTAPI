using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetcoreRESTAPI.Dtos;
using DotnetcoreRESTAPI.Extsions;
using DotnetcoreRESTAPI.Models;
using DotnetcoreRESTAPI.Repositories;
using DotnetcoreRESTAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotnetcoreRESTAPI.Controllers
{
    [ApiController]
    [Route("MobilePhone")]
    public class MobilePhoneController : ControllerBase
    {
        private readonly IRepository _repository;
        public MobilePhoneController(IRepository repository) => _repository = repository;
        #region Sync module
        // [HttpGet]
        // //Get MobilePhone
        // public IEnumerable<MobilePhoneDto> GetList() => _repository.GetMobilePhonesAsync().Select(i => i.AsDto());
        // [HttpGet("{id}")]
        // //Get MobilePhone{id}
        // public MobilePhoneDto GetOne(Guid id) => _repository.GetMobilePhoneAsync(id).AsDto();
        // [HttpPost]
        // //Post MobilePhone
        // public ActionResult<MobilePhoneDto> CreateOne(MobilePhoneREST mobilePhone)
        // {
        //     MobilePhone mobile = new MobilePhone()
        //     {
        //         Id = Guid.NewGuid(),
        //         Type = mobilePhone.Type,
        //         Name = mobilePhone.Name,
        //         OriginPrice = mobilePhone.OriginPrice,
        //         Discount = mobilePhone.Discount,
        //         CreateDate = DateTimeOffset.Now
        //     };
        //     _repository.CreateMobilePhoneAsync(mobile);
        //     return CreatedAtAction(nameof(GetOne),new{ id = mobile.Id}, mobile.AsDto());
        // }
        // [HttpPut("{id}")]
        // //Put MobilePhone{id}
        // public ActionResult UpdateOne(Guid id, MobilePhoneREST mobilePhone)
        // {
        //     var existMobilePhone = _repository.GetMobilePhoneAsync(id);
        //     if (existMobilePhone == null) return NotFound($"您所更新的 ID 为 {id} 不存在，更新失败。");
        //     var update = existMobilePhone with
        //     {
        //         Type  = mobilePhone.Type,
        //         Name = mobilePhone.Name,
        //         OriginPrice = mobilePhone.OriginPrice,
        //         Discount = mobilePhone.Discount,
        //         ModifiedDate = DateTimeOffset.Now
        //     };
        //     _repository.UpdateMobilePhone(update);
        //     return Ok("更新成功！");
        // }
        // [HttpDelete("{id}")]
        // //Delete MobilePhone{id}
        // public ActionResult DeleteOne(Guid id)
        // {
        //     var existMobilePhone = _repository.GetMobilePhoneAsync(id);
        //     if (existMobilePhone == null) return NotFound($"您所需删除的 ID 为 {id} 不存在，无需删除。");
        //     _repository.DeleteMobilePhoneAsync(id);
        //     return Ok("删除成功！");
        // }
        #endregion
        #region Async module
        [HttpGet]
        //Get MobilePhone
        public async Task<IEnumerable<MobilePhoneDto>> GetListAsync() => (await _repository.GetMobilePhonesAsync()).Select(i => i.AsDto());
        [HttpGet("{id}")]
        //Get MobilePhone{id}
        public async Task<MobilePhoneDto> GetOneAsync(Guid id) => (await _repository.GetMobilePhoneAsync(id)).AsDto();
        [HttpPost]
        //Post MobilePhone
        public async Task<ActionResult<MobilePhoneDto>> CreateOneAsync(MobilePhoneREST mobilePhone)
        {
            MobilePhone mobile = new MobilePhone()
            {
                Id = Guid.NewGuid(),
                Type = mobilePhone.Type,
                Name = mobilePhone.Name,
                OriginPrice = mobilePhone.OriginPrice,
                Discount = mobilePhone.Discount,
                CreateDate = DateTimeOffset.Now
            };
            await _repository.CreateMobilePhoneAsync(mobile);
            return CreatedAtAction(nameof(GetOneAsync),new{ id = mobile.Id}, mobile.AsDto());
        }
        [HttpPut("{id}")]
        //Put MobilePhone{id}
        public async Task<ActionResult> UpdateOneAsync(Guid id, MobilePhoneREST mobilePhone)
        {
            var existMobilePhone = await _repository.GetMobilePhoneAsync(id);
            if (existMobilePhone == null) return NotFound($"您所更新的 ID 为 {id} 不存在，更新失败。");
            var update = existMobilePhone with
            {
                Type  = mobilePhone.Type,
                Name = mobilePhone.Name,
                OriginPrice = mobilePhone.OriginPrice,
                Discount = mobilePhone.Discount,
                ModifiedDate = DateTimeOffset.Now
            };
            await _repository.UpdateMobilePhoneAsync(update);
            return Ok("更新成功！");
        }
        [HttpDelete("{id}")]
        //Delete MobilePhone{id}
        public async Task<ActionResult> DeleteOneAsync(Guid id)
        {
            var existMobilePhone = await _repository.GetMobilePhoneAsync(id);
            if (existMobilePhone == null) return NotFound($"您所需删除的 ID 为 {id} 不存在，无需删除。");
            await _repository.DeleteMobilePhoneAsync(id);
            return Ok("删除成功！");
        }
        #endregion
    }
}