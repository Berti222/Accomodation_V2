using AccomodationWebAPI.DTOs.GetDTOs;
using AccomodationWebAPI.DTOs.PostDTOs;
using AccomodationWebAPI.DTOs.PutDTOs;
using AccomodationWebAPI.Logic.ControllerLogic;
using AccomodationWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccomodationWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : CustomControllerBase<RoomLogic, RoomDTO, RoomPostDTO, RoomPutDTO>
    {
        public RoomController(RoomLogic logic, ILoggingHelper loggingHelper) 
            : base(logic, loggingHelper)
        {
        }
    }
}
