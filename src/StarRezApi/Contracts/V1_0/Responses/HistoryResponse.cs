namespace StarRezApi.Contracts.V1_0.Responses;

public record HistoryResponse(
    IReadOnlyList<HistoryEntryResponse> Data,
    int TotalCount,
    IReadOnlyList<string> Errors
);
