using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Create
    {
        // Commands return no data and make changes to DB
        public class Command : IRequest {
            // Activity is the object to pass as parameter to the command
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
        private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                // tracking the adding of this new activity in memory without db changes
                _context.Add(request.Activity);

                await _context.SaveChangesAsync();

                // Command does not return data, Unit is MediatR object to return Task<Unit> on these methods
                return Unit.Value;
            }
        }
    }
}