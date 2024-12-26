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
    /// <summary>
    /// Получение списка всех предоставлений ролей.
    /// </summary>
    /// <returns>Возвращает список всех предоставлений ролей.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<RoleAccessListVm>>> GetAll()
    {
        var query = new GetRoleAccessListQuery();
        var result = await Mediator.Send(query);
        if (result.IsFailed)
        {
            return ProblemResponse(result.Error);
        }
        return Ok(result);
    }

    /// <summary>
    /// Получение предоставления роли по id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Возвращает предоставление роли по id.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RoleAccessByIdVm>> GetById(int id)
    {
        var query = new GetRoleAccessByIdQuery(id);
        var result = await Mediator.Send(query);
        if (result.IsFailed)
        {
            return ProblemResponse(result.Error);
        }
        return Ok(result);
    }

    /// <summary>
    /// Создание нового предоставления роли.
    /// </summary>
    /// <param name="createRoleAccessDto"></param>
    /// <returns>Возвращает результат команды добавления нового предоставления роли.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create([FromBody] CreateRoleAccessDto createRoleAccessDto)
    {
        var command = Mapper.Map<CreateRoleAccessCommand>(createRoleAccessDto);
        var result = await Mediator.Send(command);
        if (result.IsFailed)
        {
            return ProblemResponse(result.Error);
        }
        return Ok(result);
    }

    /// <summary>
    /// Обновление информации о предоставлении роли.
    /// </summary>
    /// <param name="updateRoleAccessDto"></param>
    /// <returns>Возвращает результат выполнения команды обновления предоставления роли.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromBody] UpdateRoleAccessDto updateRoleAccessDto)
    {
        var command = Mapper.Map<UpdateRoleAccessCommand>(updateRoleAccessDto);
        var result = await Mediator.Send(command);
        if (result.IsFailed)
        {
            return ProblemResponse(result.Error);
        }
        return Ok(result);
    }

    /// <summary>
    /// Удаление предоставления роли по id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Возвращает результат удаления предоставления роли.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteRoleAccessCommand(id);
        var result = await Mediator.Send(command);
        if (result.IsFailed)
        {
            return ProblemResponse(result.Error);
        }
        return Ok(result);
    }
}
