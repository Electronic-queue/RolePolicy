using KDS.Primitives.FluentResult;
using Microsoft.AspNetCore.Mvc;
using RolePolicy.Application.RoleResourceActions.Queries.GetRoleResourceActionList;
using RolePolicy.Application.Users.Commands.CreateUser;
using RolePolicy.Application.Users.Commands.DeleteUser;
using RolePolicy.Application.Users.Commands.UpdateUser;
using RolePolicy.Application.Users.Queries.GetUserById;
using RolePolicy.Application.Users.Queries.GetUserList;
using RolePolicy.WebApi.Contracts.RoleResourceActionsContracts;
using RolePolicy.WebApi.Contracts.UsersContracts;

namespace RolePolicy.WebApi.Controllers;

[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/[controller]")]
public class UserController : BaseController
{
    private readonly ILogger<UserController> _logger;
    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }
    /// <summary>
    /// Получение списка всех пользователей.
    /// </summary>
    /// <returns>Возвращает список всех пользователей.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserListVm))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> GetAll()
    {
        var scope = new Dictionary<string, object>();
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на чтение полного списка пользователей.");
            var result = await Mediator.Send(new GetUserListQuery());
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
    /// Получение пользователя по id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Возвращает пользователя по id.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserByIdVm))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> GetById(int id)
    {
        var scope = new Dictionary<string, object> { { "TargetUserId", id } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на чтение пользователя.");
            var result = await Mediator.Send(new GetUserByIdQuery(id));
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
    /// Создание нового пользователя.
    /// </summary>
    /// <param name="createUserDto"></param>
    /// <returns>Возвращает результат команды добавления нового пользователя.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Create([FromBody] CreateUserDto createUserDto)
    {
        var scope = new Dictionary<string, object>() {
            { "TargetFirstId", createUserDto.FirstName },
            { "TargetLastId", createUserDto.LastName }
        };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на создание пользователя.");
            var result = await Mediator.Send(Mapper.Map<CreateUserCommand>(createUserDto));
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
    /// Обновление информации о пользователе.
    /// </summary>
    /// <param name="updateUserDto"></param>
    /// <returns>Возвращает результат выполнения команды обновления пользователя.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Update([FromBody] UpdateUserDto updateUserDto)
    {
        var scope = new Dictionary<string, object>() { { "TargetUserId", updateUserDto.Id } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на обновление пользователя.");
            var result = await Mediator.Send(Mapper.Map<UpdateUserCommand>(updateUserDto));
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
    /// Удаление пользователя по id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Возвращает результат удаления пользователя.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Delete(int id)
    {
        var scope = new Dictionary<string, object> { { "TargetUserId", id } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на удаление пользователя.");
            var result = await Mediator.Send(new DeleteUserCommand(id));
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
