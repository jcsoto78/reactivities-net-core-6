using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Activities;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;


namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        // private readonly IMediator _mediator;

        // // mediator thorugh DI, notice how an interface reference is passed in the constructor
        // public ActivitiesController(IMediator mediator)
        // {
        //     _mediator = mediator;
        // }

        [HttpGet] // any GET
        public async Task<ActionResult<List<Activity>>> GetActivities() {
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")] //endpoint: activities/id
        public async Task<ActionResult<Activity>> GetActivity(Guid id) {
            // return Ok();
            // List.Query is being initialized through an object initilizer
            return await Mediator.Send(new Details.Query{Id = id});
        }


        [HttpPost]
        // [FromBody] used explicitly but not actually needed, binder should take care
        public async Task<IActionResult> CreateActivity([FromBody]Activity activity) {
            return Ok(await Mediator.Send(new Create.Command{Activity = activity}));
        }

        [HttpPut("{id}")]
        // [FromBody] used explicitly but not actually needed, binder should take care
        public async Task<IActionResult> EditActivity([FromBody]Activity activity, Guid id) {
            activity.Id = id;
            return Ok(await Mediator.Send(new Edit.Command{Activity = activity}));
        }
    }
}