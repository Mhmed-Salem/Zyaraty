using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zyarat.Data;
using Zyarat.Models.DTO;
using Zyarat.Models.Factories;
using Zyarat.Models.Services.NotificationService;
using Zyarat.Resources;
using Zyarat.Responses;

namespace Zyarat.Controllers.Hubs
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

        [HttpPost("SendEvent/{type}/{eventId}/{senderId}")]
        public async Task<IActionResult> SendEvent([FromRoute] string type,[FromRoute]int eventId,[FromRoute]int senderId)
        {
            NotificationTypesEnum typesEnum;
            switch (type.ToLower())
            {
                case "evaluation": typesEnum = NotificationTypesEnum.Evaluation; 
                    break;
                default: return BadRequest("Invalid Type");
            }

            var state =await  _service.AddEventNotificationAsync(typesEnum, senderId, dataId: eventId);
            if (!state.Success)
            {
                return BadRequest(state.Error);
            }

            return Ok(_mapper.Map<EventNotification,EventNotificationDto>(state.Source));
        }

        [HttpPost("SendMessageToGroup")]
        public async Task<IActionResult> SendMessageToGroup([FromBody] SendNotificationToGroup resource)
        {
           
            if (!resource.Receivers.Any())
            {
                return BadRequest("No Receivers had been set !");
            }

            var state = await _service.AddMessagesAsync(resource.Content, resource.Receivers);
            if (!state.Success)
            {
                return BadRequest(state.Error);
            }

            return Ok(new SendAMessageToGroupResponse
            {
                Content = resource.Content,
                Receivers = resource.Receivers,
                ContentId = state.Source.Select(message => message.Content.Id).FirstOrDefault()
            });
        }
        
        [HttpPost("SendGlobalMessage/{content}")]
        public async Task<IActionResult> SendGlobalMessage([FromRoute]string content)
        {
            var state = await _service.AddGlobalMessageAsync(content);
            if (!state.Success)
            {
                return BadRequest(state.Error);
            }

            return Ok(new AddGlobalMessageResponse
            {
                Content = content,
                Id = state.Source.Id,
                DateTime = state.Source.DateTime,
                MessageContentId = state.Source.MessageContentId
            });
        }

        [HttpPost("sendMessage/{repId}/{content}")]
        public async Task<IActionResult> AddMessage([FromRoute]int repId,[FromRoute]string content)
        {
            var state = await _service.AddAMessageAsync(content, repId);
            if (!state.Success)
            {
                return BadRequest(state.Error);
            }

            return Ok(new
            {
                contentId=state.Source.Content.Id,
                content=state.Source.Content.Content,
                ReceiverId=repId
            });
        }

        [HttpGet("GetEvents")]
        public IActionResult GetEvents([FromQuery]int pageNumber,[FromQuery]int pageSize)
        {
            var state =  _service.GetEventNotification(pageNumber, pageSize);
            if (!state.Success)
            {
                return  BadRequest(state.Error);
            }

            return Ok(
                _mapper.Map<IEnumerable<EventNotification>,IEnumerable<EventNotificationDto>>(state.Source));
        }
        
        [HttpGet("GetMessages")]
        public IActionResult GetMessages([FromQuery]int repId,[FromQuery]int pageNumber,[FromQuery]int pageSize)
        {
            var state =   _service.GetAllMessages(repId, pageNumber, pageSize);
            if (!state.Success)
            {
                return  BadRequest(state.Error);
            }
            return Ok( state.Source);
        }
        
        
        
        
    }
}