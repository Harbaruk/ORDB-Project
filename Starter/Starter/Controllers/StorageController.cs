using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Starter.Common.DomainTaskStatus;
using Starter.DAL.Entities;
using Starter.Services.ADOServices;

namespace Starter.API.Controllers
{
    [Produces("application/json")]
    [Route("api/storage")]
    public class StorageController : AbstractController
    {
        private readonly IBaseService<StorageEntity> _service;

        public StorageController(IBaseService<StorageEntity> service, DomainTaskStatus taskStatus) : base(taskStatus)
        {
            _service = service;
        }

        [HttpGet]
        [Route("get_all")]
        [ProducesResponseType(typeof(IEnumerable<StorageEntity>),200)]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(StorageEntity),200)]
        public IActionResult Get(int id)
        {
            return Ok(_service.Get(id));
        }

        [HttpPost]
        public void Insert([FromBody] StorageEntity storage)
        {
            _service.Insert(storage);
        }

        [HttpPut]
        public void Update([FromBody] StorageEntity storage)
        {
            _service.Update(storage);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}