using Microsoft.AspNetCore.Mvc;
using StarRezApi.Contracts.V1_0.Requests;
using StarRezApi.Contracts.V1_0.Responses;
using StarRezApi.Exceptions;
using StarRezApi.Mappers;
using StarRezApi.Repositories;
using StarRezApi.Services;
using StarRezApi.Validation;

namespace StarRezApi.Controllers;

[ApiController]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;
    private readonly IValidator<ValidateRequest> _validateRequestValidator;
    private readonly IValidator<CollectionRequest> _collectionRequestValidator;
    private readonly IValidator<HistoryRequest> _historyRequestValidator;
    private readonly IGameHistoryRepository _historyRepository;
    private readonly IGameHistoryMapper _historyMapper;

    public GameController(
        IGameService gameService,
        IValidator<ValidateRequest> validateRequestValidator,
        IValidator<CollectionRequest> collectionRequestValidator,
        IValidator<HistoryRequest> historyRequestValidator,
        IGameHistoryRepository historyRepository,
        IGameHistoryMapper historyMapper)
    {
        _gameService = gameService;
        _validateRequestValidator = validateRequestValidator;
        _collectionRequestValidator = collectionRequestValidator;
        _historyRequestValidator = historyRequestValidator;
        _historyRepository = historyRepository;
        _historyMapper = historyMapper;
    }

    [HttpPost("validate")]
    public async Task<ActionResult<ValidateResponse>> Validate(
        [FromBody] ValidateRequest request,
        CancellationToken cancellationToken)
    {
        _validateRequestValidator.Validate(request);

        var expectedResponse = _gameService.GetExpectedResponse(request.KidNumber);
        var isValid = _gameService.ValidateResponse(request.KidNumber, request.KidResponse);

        await _gameService.RecordValidationAsync(
            request.KidNumber,
            request.KidResponse,
            cancellationToken);

        return Ok(new ValidateResponse(isValid, expectedResponse));
    }

    [HttpGet("all")]
    public ActionResult<CollectionResponse> GetAll([FromQuery] CollectionRequest request)
    {
        _collectionRequestValidator.Validate(request);

        var data = _gameService.GetSequence(request.From, request.To).ToList();

        return Ok(new CollectionResponse(data));
    }

    [HttpGet("history")]
    public async Task<ActionResult<HistoryResponse>> GetHistory(
        [FromQuery] HistoryRequest request,
        CancellationToken cancellationToken)
    {
        _historyRequestValidator.Validate(request);

        var entries = await _historyRepository.GetAllAsync(
            request.KidNumber,
            request.Limit,
            request.Offset,
            cancellationToken);

        var totalCount = await _historyRepository.GetCountAsync(
            request.KidNumber,
            cancellationToken);

        var data = _historyMapper.ToResponseList(entries);

        return Ok(new HistoryResponse(data, totalCount));
    }

    [HttpGet("history/{id:guid}")]
    public async Task<ActionResult<HistoryEntryResponse>> GetHistoryEntry(
        Guid id,
        CancellationToken cancellationToken)
    {
        var entry = await _historyRepository.GetByIdAsync(id, cancellationToken);

        if (entry is null)
            throw new NotFoundException("HistoryEntry", id);

        return Ok(_historyMapper.ToResponse(entry));
    }
}
