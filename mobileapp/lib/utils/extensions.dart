import 'package:graphql_flutter/graphql_flutter.dart';

extension StatusCodeRetreiving<T> on QueryResult<T> {
  int? statusCode() {
    return context.entry<HttpLinkResponseContext>()?.statusCode;
  }
}

extension DateOnlyCompare on DateTime {
  bool isSameDate(DateTime other) {
    return year == other.year && month == other.month
        && day == other.day;
  }
}