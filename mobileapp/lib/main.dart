import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:get_it/get_it.dart';
import 'package:mobileapp/drive_app.dart';
import 'package:mobileapp/utils/di.dart';

final getIt = GetIt.instance;

Future<void> main() async {
  WidgetsFlutterBinding.ensureInitialized();
  SystemChrome.setPreferredOrientations(
      [DeviceOrientation.portraitUp]);

  await registerServicesAtGetIt(getIt);

  runApp(const DriveApp());
}
