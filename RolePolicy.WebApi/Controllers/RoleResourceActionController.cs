using KDS.Primitives.FluentResult;
using Microsoft.AspNetCore.Mvc;
using RolePolicy.Application.RoleResourceActions.Commands.CreateRoleResourceAction;
using RolePolicy.Application.RoleResourceActions.Commands.DeleteRoleResourceAction;
using RolePolicy.Application.RoleResourceActions.Commands.UpdateRoleResourceAction;
using RolePolicy.Application.RoleResourceActions.Queries.GetRoleResourceActionById;
using RolePolicy.Application.RoleResourceActions.Queries.GetRoleResourceActionList;
using RolePolicy.WebApi.Contracts.RoleResourceActionsContracts;

namespace RolePolicy.WebApi.Controllers;

[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/[controller]")]
public class RoleResourceActionController : BaseController
{
    private readonly ILogger<RoleResourceActionController> _logger;
    public RoleResourceActionController(Logger<RoleResourceActionController> logger)
    {
        _logger = logger;
    }
    /// <summary>
    /// Получение списка всех связей роли и действия над ресурсом.
    /// </summary>
    /// <returns>Возвращает список всех связей роли и действия над ресурсом.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleResourceActionListVm))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> GetAll()
    {
        var scope = new Dictionary<string, object>();
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на чтение полного списка связей роли и действия над ресурсом.");
            var result = await Mediator.Send(new GetRoleResourceActionListQuery());
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
    /// Получение связи роли и действия над ресурсом по id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Возвращает связь роли и действия над ресурсом по id.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleResourceActionByIdVm))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> GetById(int id)
    {
        var scope = new Dictionary<string, object> { { "TargetRoleResourceActionId", id } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на чтение связи роли и действия над ресурсом.");
            var result = await Mediator.Send(new GetRoleResourceActionByIdQuery(id));
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
    /// Создание новой связи роли и действия над ресурсом.
    /// </summary>
    /// <param name="createRoleResourceActionDto"></param>
    /// <returns>Возвращает результат команды добавления новой связи роли и действия над ресурсом.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Create([FromBody] CreateRoleResourceActionDto createRoleResourceActionDto)
    {
        var scope = new Dictionary<string, object>() {
            { "TargetRoleId", createRoleResourceActionDto.RoleId },
            { "TargetActionId", createRoleResourceActionDto.ActionId },
            { "TargetResourceId", createRoleResourceActionDto.ResourceId }
        };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на создание связи роли и действия над ресурсом.");
            var result = await Mediator.Send(Mapper.Map<CreateRoleResourceActionCommand>(createRoleResourceActionDto));
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
    /// Обновление информации о связи роли и действия над ресурсом.
    /// </summary>
    /// <param name="updateRoleResourceActionDto"></param>
    /// <returns>Возвращает результат выполнения команды обновления связи роли и действия над ресурсом.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Update([FromBody] UpdateRoleResourceActionDto updateRoleResourceActionDto)
    {
        var scope = new Dictionary<string, object>() { { "TargetRoleResourceActionId", updateRoleResourceActionDto.Id } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на обновление связи роли и действия над ресурсом.");
            var result = await Mediator.Send(Mapper.Map<UpdateRoleResourceActionCommand>(updateRoleResourceActionDto));
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
    /// Удаление связи роли и действия над ресурсом по id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Возвращает результат удаления связи роли и действия над ресурсом.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Delete(int id)
    {
        var scope = new Dictionary<string, object>() { { "TargetRoleResourceActionId", id } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на удаление связи роли и действия над ресурсом.");
            var result = await Mediator.Send(new DeleteRoleResourceActionCommand(id));
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
