using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zyarat.Models.DTO;
using Zyarat.Models.Factories;
using Zyarat.Models.Services.NotificationService;
using Zyarat.Resources;
using Zyarat.Responses;

namespace Zyarat.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class MessageController:Controller
    {
        
        private readonly INotificationService _service;
        private readonly IMapper _mapper;

        public MessageController(INotificationService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        
        [HttpGet("GetMessage/{messageId}")]
        public async Task<IActionResult> GetMessage([FromRoute]int messageId)
        {
            var r = await _service.GetMessage(messageId);
            if (!r.Success)
            {
                return BadRequest(r.Error);
            }
            return Ok(new MessageDto
            {
                Id = r.Source.Id,
                Content = r.Source.Content.Content,
                Read = r.Source.Read,
                Type = NotificationTypesEnum.Message.ToString(),
                DateTime = r.Source.DateTime,
            });
        }
        [HttpGet("GetGlobal/{messageId}/{repId}")]
        public async Task<IActionResult> GetGlobal([FromRoute]int messageId,[FromRoute]int repId)
        {
            var r = await _service.GetGlobalMessage(messageId,repId);
            if (!r.Success)
            {
                return BadRequest(r.Error);
            }

            return Ok(new GlobalMessageDto
            {
                Id = r.Source.Id,
                Content = r.Source.Content,
                Read = r.Source.Read,
                Type = NotificationTypesEnum.GlobalMessage.ToString(),
                DateTime = r.Source.DateTime
            });
        }
        [HttpPost("AddMessageToGroup")]
        public async Task<IActionResult> AddMessageToGroup([FromBody] SendNotificationToGroup resource)
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
                Id = state.Source.Select(message => message.Content.Id).FirstOrDefault(),
                Content = resource.Content,
                Receivers = resource.Receivers,
            });
        }

        [HttpPost("AddGlobalMessage/{content}")]
        public async Task<IActionResult> AddGlobalMessage([FromRoute] string content)
        {
            var state = await _service.AddGlobalMessageAsync(content);
            if (!state.Success)
            {
                return BadRequest(state.Error);
            }

            return Ok(new GlobalMessageDto
            {
                Id = state.Source.Id,
                Content = state.Source.MessageContent.Content,
                Read = false,
                Type = NotificationTypesEnum.GlobalMessage.ToString(),
                DateTime = state.Source.DateTime
            });
        }

        [HttpPost("AddMessage/{repId}/{content}")]
        public async Task<IActionResult> AddMessage([FromRoute]int repId,[FromRoute]string content)
        {
            var state = await _service.AddAMessageAsync(content, repId);
            if (!state.Success)
            {
                return BadRequest(state.Error);
            }

            return Ok(new MessageDto
            {
                Id=state.Source.Id,
                DateTime =state.Source.DateTime,
                Content= state.Source.Content.Content,
                Read = state.Source.Read,
                Type = NotificationTypesEnum.Message.ToString(),
                Receivers = new List<int>{repId}
            });
        }
        [HttpGet("GetMessages")]
        public async Task<IActionResult> GetMessages([FromQuery]int repId,[FromQuery]int pageNumber,[FromQuery]int pageSize)
        {
            var state = await  _service.GetAllMessages(repId, pageNumber, pageSize);
            if (!state.Success)
            {
                return  BadRequest(state.Error);
            }
            return Ok( state.Source);
        }
        
        [HttpGet("CountUnRead/{repId}")]
        public IActionResult CountUnRead([FromRoute]int repId)
        {
            return Ok( _service.CountUnReadMessages(repId));
        }
    }
}