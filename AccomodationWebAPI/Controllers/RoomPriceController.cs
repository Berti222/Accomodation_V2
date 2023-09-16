using AccomodationWebAPI.DTOs.GetDTOs;
using AccomodationWebAPI.DTOs.PostDTOs;
using AccomodationWebAPI.DTOs.PutDTOs;
using AccomodationWebAPI.Logic.ControllerLogic;
using AccomodationWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccomodationWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomPriceController : CustomControllerBase<RoomPriceLogic, RoomPriceDTO, RoomPricePostDTO, RoomPricePutDTO>
    {
        public RoomPriceController(RoomPriceLogic logic, ILoggingHelper loggingHelper) 
            : base(logic, loggingHelper)
        {
        }
    }
}
