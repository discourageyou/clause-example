using Microsoft.AspNetCore.Mvc;

namespace StarRezApi.Contracts.V1_0.Requests;

public record CollectionRequest
{
    [FromQuery]
    public int From { get; init; } = 1;

    [FromQuery]
    public int To { get; init; } = 100;
}
