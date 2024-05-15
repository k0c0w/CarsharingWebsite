import 'package:intl/intl.dart';

class DateTimeFormat {
  static DateFormat toStringFormatter = DateFormat("dd.MM.yyyy");

  static DateFormat toRussianDateFormatter = DateFormat.yMMMMd("ru");

  static DateFormat to24HoursFormatter = DateFormat.Hm();
}