import 'dart:async';
import 'dart:convert';
import 'package:mobileapp/map_models/drawer_user.dart';
import 'package:shared_preferences/shared_preferences.dart';

class _Keys {
  static String user = "drawer_user";
}

class DrawerUserInfoDataProvider {
  final SharedPreferences _sharedPreferences;

  DrawerUserInfoDataProvider(this._sharedPreferences);

  DrawerUserInfo? getSavedUserInfo() {
    final jsonString = _sharedPreferences.getString(_Keys.user);
    if (jsonString == null) {
      return null;
    } else {
      return DrawerUserInfo.fromJson(json.decode(jsonString));
    }
  }

  Future<void> saveUserInfo(DrawerUserInfo user) {
    return _sharedPreferences.setString(_Keys.user, json.encode(user.toJson()));
  }

  Future<void> deleteUserInfo() {
    return _sharedPreferences.remove(_Keys.user);
  }
}