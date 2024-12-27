using Microsoft.AspNetCore.Mvc;
using RolePolicy.Application.Resources.Commands.CreateResource;
using RolePolicy.Application.Resources.Commands.DeleteResource;
using RolePolicy.Application.Resources.Commands.UpdateResource;
using RolePolicy.Application.Resources.Queries.GetResourceById;
using RolePolicy.Application.Resources.Queries.GetResourceList;
using RolePolicy.WebApi.Contracts.ResourcesContracts;

namespace RolePolicy.WebApi.Controllers;

[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/[controller]")]
public class ResourceController : BaseController
{
    private readonly ILogger<ResourceController> _logger;
    public ResourceController(ILogger<ResourceController> logger)
    {
        _logger = logger;
    }
    /// <summary>
    /// Получение списка всех ресурсов.
    /// </summary>
    /// <returns>Возвращает список всех ресурсов.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<ResourceListVm>>> GetAll()
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
    /// Получение ресурса по id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Возвращает ресурс по id.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResourceByIdVm>> GetById(int id)
    {
        var query = new GetResourceByIdQuery(id);
        var result = await Mediator.Send(query);
        if (result.IsFailed)
        {
            return ProblemResponse(result.Error);
        }
        return Ok(result);
    }

    /// <summary>
    /// Создание нового ресурса.
    /// </summary>
    /// <param name="createResourceDto"></param>
    /// <returns>Возвращает результат команды добавления нового ресурса.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create([FromBody] CreateResourceDto createResourceDto)
    {
        var command = Mapper.Map<CreateResourceCommand>(createResourceDto);
        var result = await Mediator.Send(command);
        if (result.IsFailed)
        {
            return ProblemResponse(result.Error);
        }
        return Ok(result);
    }

    /// <summary>
    /// Обновление информации о ресурсе.
    /// </summary>
    /// <param name="updateResourceDto"></param>
    /// <returns>Возвращает результат выполнения команды обновления ресурса.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromBody] UpdateResourceDto updateResourceDto)
    {
        var command = Mapper.Map<UpdateResourceCommand>(updateResourceDto);
        var result = await Mediator.Send(command);
        if (result.IsFailed)
        {
            return ProblemResponse(result.Error);
        }
        return Ok(result);
    }

    /// <summary>
    /// Удаление ресурса по id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Возвращает результат удаления ресурса.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteResourceCommand(id);
        var result = await Mediator.Send(command);
        if (result.IsFailed)
        {
            return ProblemResponse(result.Error);
        }
        return Ok(result);
    }
}
