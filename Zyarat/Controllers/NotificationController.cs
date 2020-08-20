using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Mvc;
using Zyarat.Data;
using Zyarat.Models.DTO;
using Zyarat.Models.Factories;
using Zyarat.Models.Services.NotificationService;

namespace Zyarat.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class NotificationController:Controller
    {
        private readonly INotificationService _service;
        private readonly IMapper _mapper;

        public NotificationController(INotificationService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        
        [HttpGet("{type}/{dataId}")]
        public async Task<IActionResult> GetEvent([FromRoute]string type,[FromRoute]int dataId)
        {
            int typeId;
            switch (type.ToLower())
            {
                case  "evaluation": typeId = 0;
                    break;
                default : typeId = -1;
                    break;
            }

            if (typeId==-1)
            {
                return BadRequest("the type is not valid !");
            }
            var r = await _service.GetEvent(dataId, typeId);
            if (!r.Success)
            {
                return BadRequest(r.Error);
            }
            var data=_mapper.Map<EventNotification,EventNotificationDto>(r.Source);
            data.Type = NotificationTypesEnum.Evaluation.ToString();
            return Ok(data);
        }

       
        
        [HttpGet("GetEvents")]
        public async  Task<IActionResult> GetEvents([FromQuery]int repId,[FromQuery]int pageNumber,[FromQuery]int pageSize)
        {
            var state = await _service.GetEventNotification(repId,pageNumber, pageSize);
            if (!state.Success)
            {
                return  BadRequest(state.Error);
            }

            var data = _mapper.Map<IEnumerable<EventNotification>, IEnumerable<EventNotificationDto>>(state.Source);
            data.ForAll(dto => dto.Type=NotificationTypesEnum.Evaluation.ToString());
            return Ok(data);
        }

        [HttpGet("CountUnRead/{repId}")]
        public async Task<IActionResult> CountUnRead([FromRoute] int repId)
        {
            var x = await _service.CountUnReadEvents(repId);
            return Ok(x);
        }
    }
}