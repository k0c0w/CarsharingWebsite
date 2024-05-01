import 'package:graphql_flutter/graphql_flutter.dart';

extension StatusCodeRetreiving<T> on QueryResult<T> {
  int? statusCode() {
    return context.entry<HttpLinkResponseContext>()?.statusCode;
  }
}