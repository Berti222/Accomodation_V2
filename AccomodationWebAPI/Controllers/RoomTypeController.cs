﻿using AccomodationWebAPI.DTOs.GetDTOs;
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
    public class RoomTypeController : CustomControllerBase<RoomTypeLogic, RoomTypeDTO, RoomTypePostDTO, RoomTypePutDTO>
    {
        public RoomTypeController(RoomTypeLogic logic, ILoggingHelper loggingHelper) 
            : base(logic, loggingHelper)
        {
        }
    }
}
