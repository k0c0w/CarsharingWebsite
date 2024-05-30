namespace Features.Users.Queries.Verify;

//TODO: хз что делать с этим. Вроде бы query, но по сути ничего не возвращает, но и не меняет
public record VerifyQuery (string UserId) : IQuery<bool>;