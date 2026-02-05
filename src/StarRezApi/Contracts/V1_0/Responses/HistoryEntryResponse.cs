namespace StarRezApi.Contracts.V1_0.Responses;

public record HistoryEntryResponse(
    Guid Id,
    int KidNumber,
    string KidResponse,
    string ExpectedResponse,
    bool WasValid,
    DateTime ValidatedAt
);
