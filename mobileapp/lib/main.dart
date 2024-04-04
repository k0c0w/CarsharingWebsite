import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:mobileapp/Components/drawer.dart';
import 'package:mobileapp/Pages/pages_list.dart';

import 'Components/appbar.dart';
import 'Components/bottom_button.dart';

void main() {
  WidgetsFlutterBinding.ensureInitialized();
  SystemChrome.setPreferredOrientations(
      [DeviceOrientation.portraitUp]);
  runApp(const DriveApp());
}

class DriveApp extends StatelessWidget {
  const DriveApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Drive',
      theme: ThemeData(
        useMaterial3: true,
      ),
      //just for example
      home: Scaffold(
        drawer: DriveDrawer(),

      ),
      routes: {
        DriveRoutes.home : (context) => throw UnimplementedError("Implement page"),
        DriveRoutes.profile: (context) => throw UnimplementedError("Implement page"),
        DriveRoutes.userSubscriptions: (context) => throw UnimplementedError("Implement page"),
        DriveRoutes.payment: (context) => throw UnimplementedError("Implement page"),
        DriveRoutes.login: (context) => throw UnimplementedError("Implement page"),
        DriveRoutes.registration: (context) => throw UnimplementedError("Implement page"),
      }
    );
  }
}

class MainPage extends StatelessWidget {

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      // пример внедрения AppBar
      appBar: DriveAppBar(title: "Главная"),
      body: BottomButton(title: "Сохранить", onPressed: () {print("object");},),
    );
  }
}
