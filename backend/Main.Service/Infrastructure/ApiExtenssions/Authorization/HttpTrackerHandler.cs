﻿using Microsoft.AspNetCore.Http;

namespace ApiExtensions.Authorization;

public class HttpTrackerHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _context;

    public HttpTrackerHandler(IHttpContextAccessor context)
    {
        _context = context;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (_context.HttpContext!.Request.Headers.TryGetValue("Authorization", out var jwt))
        {
            Console.WriteLine($"Added {jwt} to request");
            request.Headers.Add("Authorization", jwt.FirstOrDefault());
        }

        if (_context.HttpContext.Request.Headers.TryGetValue("TraceId", out var traceId))
            request.Headers.Add("TraceId", traceId.FirstOrDefault());

        return base.SendAsync(request, cancellationToken);
    }
}