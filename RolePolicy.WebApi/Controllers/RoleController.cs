using Microsoft.AspNetCore.Mvc;
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
    /// <summary>
    /// Получение списка всех ролей.
    /// </summary>
    /// <returns>Возвращает список всех ролей.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<RoleListVm>>> GetAll()
    {
        var query = new GetRoleListQuery();
        var result = await Mediator.Send(query);
        if (result.IsFailed)
        {
            return ProblemResponse(result.Error);
        }
        return Ok(result);
    }

    /// <summary>
    /// Получение роли по id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Возвращает роль по id.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RoleByIdVm>> GetById(int id)
    {
        var query = new GetRoleByIdQuery(id);
        var result = await Mediator.Send(query);
        if (result.IsFailed)
        {
            return ProblemResponse(result.Error);
        }
        return Ok(result);
    }

    /// <summary>
    /// Создание новой роли.
    /// </summary>
    /// <param name="createRoleDto"></param>
    /// <returns>Возвращает результат команды добавления новой роли.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create([FromBody] CreateRoleDto createRoleDto)
    {
        var command = Mapper.Map<CreateRoleCommand>(createRoleDto);
        var result = await Mediator.Send(command);
        if (result.IsFailed)
        {
            return ProblemResponse(result.Error);
        }
        return Ok(result);
    }

    /// <summary>
    /// Обновление информации о роли.
    /// </summary>
    /// <param name="updateRoleDto"></param>
    /// <returns>Возвращает результат выполнения команды обновления роли.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromBody] UpdateRoleDto updateRoleDto)
    {
        var command = Mapper.Map<UpdateRoleCommand>(updateRoleDto);
        var result = await Mediator.Send(command);
        if (result.IsFailed)
        {
            return ProblemResponse(result.Error);
        }
        return Ok(result);
    }

    /// <summary>
    /// Удаление роли по id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Возвращает результат удаления роли.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteRoleCommand(id);
        var result = await Mediator.Send(command);
        if (result.IsFailed)
        {
            return ProblemResponse(result.Error);
        }
        return Ok(result);
    }
}
