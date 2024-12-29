using KDS.Primitives.FluentResult;
using Microsoft.AspNetCore.Mvc;
using RolePolicy.Application.Resources.Commands.CreateResource;
using RolePolicy.Application.Resources.Commands.DeleteResource;
using RolePolicy.Application.Resources.Commands.UpdateResource;
using RolePolicy.Application.Resources.Queries.GetResourceById;
using RolePolicy.Application.Resources.Queries.GetResourceList;
using RolePolicy.WebApi.Contracts.ResourcesContracts;

namespace RolePolicy.WebApi.Controllers;

[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/[controller]")]
public class ResourceController : BaseController
{
    private readonly ILogger<ResourceController> _logger;
    public ResourceController(ILogger<ResourceController> logger)
    {
        _logger = logger;
    }
    /// <summary>
    /// Получение списка всех ресурсов.
    /// </summary>
    /// <returns>Возвращает список всех ресурсов.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResourceListVm))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> GetAll()
    {
        var scope = new Dictionary<string, object>();
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на чтение полного списка ресурсов.");
            var result = await Mediator.Send(new GetResourceListQuery());
            if (result.IsFailed)
            {
                _logger.LogError("Запрос вернул ошибку [{ErrorCode}] [{ErrorMessage}].", result.Error.Code, result.Error.Message);
                return ProblemResponse(result.Error);
            }
            _logger.LogInformation("Запрос прошел успешно.");
            return Ok(result);
        }
    }

    /// <summary>
    /// Получение ресурса по id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Возвращает ресурс по id.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResourceByIdVm))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> GetById(int id)
    {
        var scope = new Dictionary<string, object> { { "TargetResourceId", id } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на чтение ресурса.");
            var result = await Mediator.Send(new GetResourceByIdQuery(id));
            if (result.IsFailed)
            {
                _logger.LogError("Запрос вернул ошибку [{ErrorCode}] [{ErrorMessage}].", result.Error.Code, result.Error.Message);
                return ProblemResponse(result.Error);
            }
            _logger.LogInformation("Запрос прошел успешно.");
            return Ok(result);
        }
    }

    /// <summary>
    /// Создание нового ресурса.
    /// </summary>
    /// <param name="createResourceDto"></param>
    /// <returns>Возвращает результат команды добавления нового ресурса.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Create([FromBody] CreateResourceDto createResourceDto)
    {
        var scope = new Dictionary<string, object>() { { "TargetName", createResourceDto.Name } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на создание ресурса.");
            var result = await Mediator.Send(Mapper.Map<CreateResourceCommand>(createResourceDto));
            if (result.IsFailed)
            {
                _logger.LogError("Запрос вернул ошибку [{ErrorCode}] [{ErrorMessage}].", result.Error.Code, result.Error.Message);
                return ProblemResponse(result.Error);
            }
            _logger.LogInformation("Запрос прошел успешно.");
            return Ok(result);
        }
    }

    /// <summary>
    /// Обновление информации о ресурсе.
    /// </summary>
    /// <param name="updateResourceDto"></param>
    /// <returns>Возвращает результат выполнения команды обновления ресурса.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Update([FromBody] UpdateResourceDto updateResourceDto)
    {
        var scope = new Dictionary<string, object>() { { "TargetResourceId", updateResourceDto.Id } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на обновление ресурса.");
            var result = await Mediator.Send(Mapper.Map<UpdateResourceCommand>(updateResourceDto));
            if (result.IsFailed)
            {
                _logger.LogError("Запрос вернул ошибку [{ErrorCode}] [{ErrorMessage}].", result.Error.Code, result.Error.Message);
                return ProblemResponse(result.Error);
            }
            _logger.LogInformation("Запрос прошел успешно.");
            return Ok(result);
        }
    }

    /// <summary>
    /// Удаление ресурса по id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Возвращает результат удаления ресурса.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Delete(int id)
    {
        var scope = new Dictionary<string, object>() { { "TargetResourceId", id } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на удаление ресурса.");
            var result = await Mediator.Send(new DeleteResourceCommand(id));
            if (result.IsFailed)
            {
                _logger.LogError("Запрос вернул ошибку [{ErrorCode}] [{ErrorMessage}].", result.Error.Code, result.Error.Message);
                return ProblemResponse(result.Error);
            }
            _logger.LogInformation("Запрос прошел успешно.");
            return Ok(result);
        }
    }
}
