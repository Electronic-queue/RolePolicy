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
    /// <summary>
    /// Получение списка всех связей роли и действий над ресурсами.
    /// </summary>
    /// <returns>Возвращает список всех связей роли и действий над ресурсами.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<RoleResourceActionListVm>>> GetAll()
    {
        var query = new GetRoleResourceActionListQuery();
        var result = await Mediator.Send(query);
        if (result.IsFailed)
        {
            return ProblemResponse(result.Error);
        }
        return Ok(result);
    }

    /// <summary>
    /// Получение связи роли и действий над ресурсами по id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Возвращает связь роли и действий над ресурсами по id.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RoleResourceActionByIdVm>> GetById(int id)
    {
        var query = new GetRoleResourceActionByIdQuery(id);
        var result = await Mediator.Send(query);
        if (result.IsFailed)
        {
            return ProblemResponse(result.Error);
        }
        return Ok(result);
    }

    /// <summary>
    /// Создание новой связи роли и действий над ресурсами.
    /// </summary>
    /// <param name="createRoleResourceActionDto"></param>
    /// <returns>Возвращает результат команды добавления новой связи роли и действий над ресурсами.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create([FromBody] CreateRoleResourceActionDto createRoleResourceActionDto)
    {
        var command = Mapper.Map<CreateRoleResourceActionCommand>(createRoleResourceActionDto);
        var result = await Mediator.Send(command);
        if (result.IsFailed)
        {
            return ProblemResponse(result.Error);
        }
        return Ok(result);
    }

    /// <summary>
    /// Обновление информации о связи роли и действий над ресурсами.
    /// </summary>
    /// <param name="updateRoleResourceActionDto"></param>
    /// <returns>Возвращает результат выполнения команды обновления связи роли и действий над ресурсами.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromBody] UpdateRoleResourceActionDto updateRoleResourceActionDto)
    {
        var command = Mapper.Map<UpdateRoleResourceActionCommand>(updateRoleResourceActionDto);
        var result = await Mediator.Send(command);
        if (result.IsFailed)
        {
            return ProblemResponse(result.Error);
        }
        return Ok(result);
    }

    /// <summary>
    /// Удаление связи роли и действий над ресурсами по id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Возвращает результат удаления связи роли и действий над ресурсами.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteRoleResourceActionCommand(id);
        var result = await Mediator.Send(command);
        if (result.IsFailed)
        {
            return ProblemResponse(result.Error);
        }
        return Ok(result);
    }
}
