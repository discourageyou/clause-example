using Microsoft.AspNetCore.Mvc;

namespace StarRezApi.Contracts.V1_0.Requests;

public record HistoryRequest
{
    [FromQuery]
    public int? KidNumber { get; init; }

    [FromQuery]
    public int Limit { get; init; } = 100;

    [FromQuery]
    public int Offset { get; init; } = 0;
}
