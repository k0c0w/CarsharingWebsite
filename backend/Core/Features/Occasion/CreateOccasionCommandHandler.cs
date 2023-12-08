using Shared.CQRS;
using Shared.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features.Occasion;

internal class CreateOccasionCommandHandler : ICommandHandler<CreateOccasionCommand, Result<Guid>>
{
    public Task<Result<Result<Guid>>> Handle(CreateOccasionCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
