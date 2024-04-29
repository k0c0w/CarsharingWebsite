import 'package:flutter/cupertino.dart';

class DriveRoutes {
  static const String home = "/home";
  static const String profile = "/profile";
  static const String userSubscriptions = "/profile/subscriptions";
  static const String payment = "/payment";
  static const String registration = "/register";
  static const String login = "/login";
  static const String support = "/support";
}

enum Pages {
  home,
  profile,
  payment,
  support
}

var pages = <Map<Pages, Widget>>{
};
