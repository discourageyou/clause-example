namespace StarRezApi.Contracts.V1_0.Responses;

public record ValidateResponse(
    bool IsValid,
    string ExpectedResponse
);
