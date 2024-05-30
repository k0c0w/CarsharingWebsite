sealed class Result<T> {
}

class Ok<T> extends Result<T> {
  final T value;

  Ok(this.value);
}

class Error<T> extends Result<T> {
  String error;

  Error(this.error);
}