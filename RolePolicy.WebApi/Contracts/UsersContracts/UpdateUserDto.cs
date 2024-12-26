namespace RolePolicy.WebApi.Contracts.UsersContracts;

public record UpdateUserDto(
    int Id,
    string? FirstName,
    string? LastName,
    string? Surname,
    string? Login,
    string? PasswordHash,
    bool? IsDeleted);