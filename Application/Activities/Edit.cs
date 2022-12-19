using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        // Commands return no data and make changes to DB
        public class Command : IRequest {
            // Activity is the object to pass as parameter to the command
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {

                Activity? activity = await _context.Activities.FindAsync(request.Activity.Id);
                
                // use Automapper for property changes
                // activity.Title = request.Activity.Title ?? activity.Title;
                // any other attribute change

                _mapper.Map(request.Activity, activity);

                await _context.SaveChangesAsync();

                // Command does not return data, Unit is MediatR object to return Task<Unit> on these methods
                return Unit.Value;
            }
        }
    }
}