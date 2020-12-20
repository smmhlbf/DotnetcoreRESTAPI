using System;
using System.Collections.Generic;
using System.Linq;
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
        [HttpGet]
        public IEnumerable<MobilePhoneDto> GetList() => _repository.GetMobilePhones().Select(i => i.AsDto());
        [HttpGet("{id}")]
        public MobilePhoneDto GetOne(Guid id) => _repository.GetMobilePhone(id).AsDto();
        [HttpPost]
        public ActionResult<MobilePhoneDto> CreateOne(MobilePhoneREST mobilePhone)
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
            _repository.CreateMobilePhone(mobile);
            return CreatedAtAction(nameof(GetOne),new{ id = mobile.Id}, mobile.AsDto());
        }
        [HttpPut("{id}")]
        public ActionResult UpdateOne(Guid id, MobilePhoneREST mobilePhone)
        {
            var existMobilePhone = _repository.GetMobilePhone(id);
            if (existMobilePhone == null) return NotFound($"您所更新的 ID 为 {id} 不存在，更新失败。");
            var update = existMobilePhone with
            {
                Type  = mobilePhone.Type,
                Name = mobilePhone.Name,
                OriginPrice = mobilePhone.OriginPrice,
                Discount = mobilePhone.Discount,
                ModifiedDate = DateTimeOffset.Now
            };
            _repository.UpdateMobilePhone(update);
            return Ok("更新成功！");
        }
        public ActionResult DeleteOne(Guid id)
        {
            var existMobilePhone = _repository.GetMobilePhone(id);
            if (existMobilePhone == null) return NotFound($"您所需删除的 ID 为 {id} 不存在，无需删除。");
            _repository.DeleteMobilePhone(id);
            return Ok("删除成功！");
        }
    }
}