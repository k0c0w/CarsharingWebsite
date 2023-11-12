﻿using Contracts;
using Shared.CQRS;

namespace Features.CarManagement.Admin;

public record CreateModelCommand : ICommand<int>
{
    public string? Brand { get; init; }

    public string? Model { get; init; }

    public string? Description { get; init; }

    public int TariffId { get; init; }

    public Contracts.File? ModelPhoto { get; init; }
}
