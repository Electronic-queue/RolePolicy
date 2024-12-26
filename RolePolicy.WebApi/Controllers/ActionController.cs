using Microsoft.AspNetCore.Mvc;
using RolePolicy.Application.Actions.Commands.CreateAction;
using RolePolicy.Application.Actions.Commands.DeleteAction;
using RolePolicy.Application.Actions.Commands.UpdateAction;
using RolePolicy.Application.Actions.Queries.GetActionById;
using RolePolicy.Application.Actions.Queries.GetActionList;
using RolePolicy.WebApi.Contracts.ActionsContracts;

namespace RolePolicy.WebApi.Controllers;

[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/[controller]")]
public class ActionController : BaseController
{
    /// <summary>
    /// Получение списка всех действий.
    /// </summary>
    /// <returns>Возвращает список всех действий.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<ActionListVm>>> GetAll()
    {
        var query = new GetActionListQuery();
        var result = await Mediator.Send(query);
        if (result.IsFailed)
        {
            return ProblemResponse(result.Error);
        }
        return Ok(result);
    }

    /// <summary>
    /// Получение действия по id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Возвращает действие по id.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ActionByIdVm>> GetById(int id)
    {
        var query = new GetActionByIdQuery(id);
        var result = await Mediator.Send(query);
        if (result.IsFailed)
        {
            return ProblemResponse(result.Error);
        }
        return Ok(result);
    }

    /// <summary>
    /// Создание нового действия.
    /// </summary>
    /// <param name="createActionDto"></param>
    /// <returns>Возвращает результат команды добавления нового действия.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create([FromBody] CreateActionDto createActionDto)
    {
        var command = Mapper.Map<CreateActionCommand>(createActionDto);
        var result = await Mediator.Send(command);
        if (result.IsFailed)
        {
            return ProblemResponse(result.Error);
        }
        return Ok(result);
    }

    /// <summary>
    /// Обновление информации о действии.
    /// </summary>
    /// <param name="updateActionDto"></param>
    /// <returns>Возвращает результат выполнения команды обновления действия.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromBody] UpdateActionDto updateActionDto)
    {
        var command = Mapper.Map<UpdateActionCommand>(updateActionDto);
        var result = await Mediator.Send(command);
        if (result.IsFailed)
        {
            return ProblemResponse(result.Error);
        }
        return Ok(result);
    }

    /// <summary>
    /// Удаление действия по id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Возвращает результат удаления действия.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteActionCommand(id);
        var result = await Mediator.Send(command);
        if (result.IsFailed)
        {
            return ProblemResponse(result.Error);
        }
        return Ok(result);
    }
}
