using KDS.Primitives.FluentResult;
using Microsoft.AspNetCore.Mvc;
using RolePolicy.Application.RoleAccesses.Commands.CreateRoleAccess;
using RolePolicy.Application.RoleAccesses.Commands.DeleteRoleAccess;
using RolePolicy.Application.RoleAccesses.Commands.UpdateRoleAccess;
using RolePolicy.Application.RoleAccesses.Queries.GetRoleAccessById;
using RolePolicy.Application.RoleAccesses.Queries.GetRoleAccessList;
using RolePolicy.WebApi.Contracts.RoleAccessesContracts;

namespace RolePolicy.WebApi.Controllers;

[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/[controller]")]
public class RoleAccessController : BaseController
{
    private readonly ILogger<RoleAccessController> _logger;
    public RoleAccessController(ILogger<RoleAccessController> logger)
    {
        _logger = logger;
    }
    /// <summary>
    /// Получение списка всех предоставлений ролей.
    /// </summary>
    /// <returns>Возвращает список всех предоставлений ролей.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleAccessListVm))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> GetAll()
    {
        var scope = new Dictionary<string, object>();
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на чтение полного списка предоставлений ролей.");
            var result = await Mediator.Send(new GetRoleAccessListQuery());
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
    /// Получение предоставления роли по id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Возвращает предоставление роли по id.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleAccessByIdVm))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> GetById(int id)
    {
        var scope = new Dictionary<string, object> { { "TargetRoleAccessId", id } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на чтение предоставления роли.");
            var result = await Mediator.Send(new GetRoleAccessByIdQuery(id));
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
    /// Создание нового предоставления роли.
    /// </summary>
    /// <param name="createRoleAccessDto"></param>
    /// <returns>Возвращает результат команды добавления нового предоставления роли.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Create([FromBody] CreateRoleAccessDto createRoleAccessDto)
    {
        var scope = new Dictionary<string, object>() { 
            { "TargetUserId", createRoleAccessDto.UserId },
            { "TargetRoleId", createRoleAccessDto.RoleId },
        };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на создание ресурса предоставления роли.");
            var result = await Mediator.Send(Mapper.Map<CreateRoleAccessCommand>(createRoleAccessDto));
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
    /// Обновление информации о предоставлении роли.
    /// </summary>
    /// <param name="updateRoleAccessDto"></param>
    /// <returns>Возвращает результат выполнения команды обновления предоставления роли.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Update([FromBody] UpdateRoleAccessDto updateRoleAccessDto)
    {
        var scope = new Dictionary<string, object>() { { "TargetRoleAccessId", updateRoleAccessDto.Id } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на обновление ресурса предоставления роли.");
            var result = await Mediator.Send(Mapper.Map<UpdateRoleAccessCommand>(updateRoleAccessDto));
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
    /// Удаление предоставления роли по id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Возвращает результат удаления предоставления роли.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Delete(int id)
    {
        var scope = new Dictionary<string, object>() { { "TargetRoleAccessId", id } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на удаление предоставления роли.");
            var result = await Mediator.Send(new DeleteRoleAccessCommand(id));
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
