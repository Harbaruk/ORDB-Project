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
    [Route("api/abstractObject")]
    public class AbstractObjectController : AbstractController
    {
        private readonly IBaseService<AbstractObjectEntity> _service;

        public AbstractObjectController(IBaseService<AbstractObjectEntity> service, DomainTaskStatus taskStatus) : base(taskStatus)
        {
            _service = service;
        }

        [HttpGet]
        [Route("get_all")]
        [ProducesResponseType(typeof(IEnumerable<AbstractObjectEntity>), 200)]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }
    }
}