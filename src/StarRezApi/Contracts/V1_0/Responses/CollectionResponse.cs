namespace StarRezApi.Contracts.V1_0.Responses;

public record CollectionResponse(
    IReadOnlyList<KidResultDto> Data
);
