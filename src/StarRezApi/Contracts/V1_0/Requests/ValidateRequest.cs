namespace StarRezApi.Contracts.V1_0.Requests;

public record ValidateRequest
{
    public int KidNumber { get; init; }

    public string KidResponse { get; init; } = string.Empty;
}
