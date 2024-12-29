using KDS.Primitives.FluentResult;
using Microsoft.AspNetCore.Mvc;
using RolePolicy.Application.RoleAccesses.Queries.GetRoleAccessList;
using RolePolicy.Application.Roles.Commands.CreateRole;
using RolePolicy.Application.Roles.Commands.DeleteRole;
using RolePolicy.Application.Roles.Commands.UpdateRole;
using RolePolicy.Application.Roles.Queries.GetRoleById;
using RolePolicy.Application.Roles.Queries.GetRoleList;
using RolePolicy.WebApi.Contracts.RolesContracts;

namespace RolePolicy.WebApi.Controllers;

[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/[controller]")]
public class RoleController : BaseController
{
    private readonly ILogger<RoleController> _logger;
    public RoleController(ILogger<RoleController> logger)
    {
        _logger = logger;
    }
    /// <summary>
    /// Получение списка всех ролей.
    /// </summary>
    /// <returns>Возвращает список всех ролей.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleAccessListVm))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> GetAll()
    {
        var scope = new Dictionary<string, object>();
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на чтение полного списка ролей.");
            var result = await Mediator.Send(new GetRoleListQuery());
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
    /// Получение роли по id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Возвращает роль по id.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleByIdVm))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> GetById(int id)
    {
        var scope = new Dictionary<string, object> { { "TargetRoleId", id } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на чтение роли.");
            var result = await Mediator.Send(new GetRoleByIdQuery(id));
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
    /// Создание новой роли.
    /// </summary>
    /// <param name="createRoleDto"></param>
    /// <returns>Возвращает результат команды добавления новой роли.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Create([FromBody] CreateRoleDto createRoleDto)
    {
        var scope = new Dictionary<string, object>() { 
            { "TargetNameRu", createRoleDto.NameRu },
            { "TargetNameKk", createRoleDto.NameKk },
            { "TargetNameEn", createRoleDto.NameEn }
        };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на создание роли.");
            var result = await Mediator.Send(Mapper.Map<CreateRoleCommand>(createRoleDto));
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
    /// Обновление информации о роли.
    /// </summary>
    /// <param name="updateRoleDto"></param>
    /// <returns>Возвращает результат выполнения команды обновления роли.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Update([FromBody] UpdateRoleDto updateRoleDto)
    {
        var scope = new Dictionary<string, object>() { { "TargetRoleId", updateRoleDto.Id } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на обновление роли.");
            var result = await Mediator.Send(Mapper.Map<UpdateRoleCommand>(updateRoleDto));
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
    /// Удаление роли по id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Возвращает результат удаления роли.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Delete(int id)
    {
        var scope = new Dictionary<string, object>() { { "TargetRoleId", id } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на удаление роли.");
            var result = await Mediator.Send(new DeleteRoleCommand(id));
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
