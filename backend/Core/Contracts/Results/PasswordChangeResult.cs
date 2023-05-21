namespace Contracts.Results;

public record PasswordChangeResult(bool Success, IEnumerable<string> Errors = default);