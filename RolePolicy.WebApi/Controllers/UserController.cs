using Microsoft.AspNetCore.Mvc;
using RolePolicy.Application.Users.Commands.CreateUser;
using RolePolicy.Application.Users.Commands.DeleteUser;
using RolePolicy.Application.Users.Commands.UpdateUser;
using RolePolicy.Application.Users.Queries.GetUserById;
using RolePolicy.Application.Users.Queries.GetUserList;
using RolePolicy.WebApi.Contracts.UsersContracts;

namespace RolePolicy.WebApi.Controllers;

[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/[controller]")]
public class UserController : BaseController
{
    /// <summary>
    /// Получение списка всех пользователей.
    /// </summary>
    /// <returns>Возвращает список всех пользователей.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<UserListVm>>> GetAll()
    {
        var query = new GetUserListQuery();
        var result = await Mediator.Send(query);
        if (result.IsFailed)
        {
            return ProblemResponse(result.Error);
        }
        return Ok(result);
    }

    /// <summary>
    /// Получение пользователя по id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Возвращает пользователя по id.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserByIdVm>> GetById(int id)
    {
        var query = new GetUserByIdQuery(id);
        var result = await Mediator.Send(query);
        if (result.IsFailed)
        {
            return ProblemResponse(result.Error);
        }
        return Ok(result);
    }

    /// <summary>
    /// Создание нового пользователя.
    /// </summary>
    /// <param name="createUserDto"></param>
    /// <returns>Возвращает результат команды добавления нового пользователя.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create([FromBody] CreateUserDto createUserDto)
    {
        var command = Mapper.Map<CreateUserCommand>(createUserDto);
        var result = await Mediator.Send(command);
        if (result.IsFailed)
        {
            return ProblemResponse(result.Error);
        }
        return Ok(result);
    }

    /// <summary>
    /// Обновление информации о пользователе.
    /// </summary>
    /// <param name="updateUserDto"></param>
    /// <returns>Возвращает результат выполнения команды обновления пользователя.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromBody] UpdateUserDto updateUserDto)
    {
        var command = Mapper.Map<UpdateUserCommand>(updateUserDto);
        var result = await Mediator.Send(command);
        if (result.IsFailed)
        {
            return ProblemResponse(result.Error);
        }
        return Ok(result);
    }

    /// <summary>
    /// Удаление пользователя по id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Возвращает результат удаления пользователя.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteUserCommand(id);
        var result = await Mediator.Send(command);
        if (result.IsFailed)
        {
            return ProblemResponse(result.Error);
        }
        return Ok(result);
    }
}
