sealed class Result<T> {
}

class Ok<T> implements Result<T> {
  final T value;

  Ok(this.value);
}

class Error<T> implements Result<T> {
  String error;

  Error(this.error);
}