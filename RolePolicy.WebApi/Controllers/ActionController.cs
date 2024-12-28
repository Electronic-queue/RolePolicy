using KDS.Primitives.FluentResult;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RolePolicy.Application.Actions.Commands.CreateAction;
using RolePolicy.Application.Actions.Commands.DeleteAction;
using RolePolicy.Application.Actions.Commands.UpdateAction;
using RolePolicy.Application.Actions.Queries.GetActionById;
using RolePolicy.Application.Actions.Queries.GetActionList;
using RolePolicy.WebApi.Contracts.ActionsContracts;
using System.Diagnostics;

namespace RolePolicy.WebApi.Controllers;

[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/[controller]")]
public class ActionController : BaseController
{
    private readonly ILogger<ActionController> _logger;
    public ActionController(ILogger<ActionController> logger)
    {
        _logger = logger;
    }
    /// <summary>
    /// Получение списка всех действий.
    /// </summary>
    /// <returns>Возвращает список всех действий.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ActionListVm>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> GetAll()
    {
        var scope = new Dictionary<string, object>();
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на чтение полного списка действий.");
            var result = await Mediator.Send(new GetActionListQuery());
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
    /// Получение действия по id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Возвращает действие по id.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActionListVm))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> GetById(int id)
    {
        var scope = new Dictionary<string, object> { { "ActionId", id } };

        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на чтение действия с id");
            var result = await Mediator.Send(new GetActionByIdQuery(id));

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
    /// Создание нового действия.
    /// </summary>
    /// <param name="createActionDto"></param>
    /// <returns>Возвращает результат команды добавления нового действия.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Create([FromBody] CreateActionDto createActionDto)
    {
        var scope = new Dictionary<string, object>() { { "ActionName", createActionDto.Name } };

        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на создание действия.");
            var result = await Mediator.Send(Mapper.Map<CreateActionCommand>(createActionDto));
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
    /// Обновление информации о действии.
    /// </summary>
    /// <param name="updateActionDto"></param>
    /// <returns>Возвращает результат выполнения команды обновления действия.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Update([FromBody] UpdateActionDto updateActionDto)
    {
        var scope = new Dictionary<string, object>() { { "ActionId", updateActionDto.Id} };

        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на обновление действия с id {Id}", updateActionDto.Id);
            var result = await Mediator.Send(Mapper.Map<UpdateActionCommand>(updateActionDto));
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
    /// Удаление действия по id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Возвращает результат удаления действия.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Delete(int id)
    {
        var scope = new Dictionary<string, object>() { { "ActionId", id} };

        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на удаление действия с id {Id}", id);
            var result = await Mediator.Send(new DeleteActionCommand(id));
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
