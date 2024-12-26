namespace RolePolicy.WebApi.Contracts.UsersContracts;

public record CreateUserDto(
    string FirstName,
    string LastName,
    string? Surname,
    string Login,
    string PasswordHash,
    int CreatedBy);