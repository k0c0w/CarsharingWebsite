import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:mobileapp/ui/components/drawer.dart';
import 'package:mobileapp/ui/pages/pages_list.dart';
import 'package:mobileapp/ui/pages/subscriptions.dart';
import 'ui/components/bottom_button.dart';
import 'ui/pages/profile.dart';

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
        DriveRoutes.profile: (_) => ProfilePageWidget.create(),
        DriveRoutes.userSubscriptions: (_) => SubscriptionsPageWidget.create(),
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
      body: BottomButton(title: "Сохранить", onPressed: () {print("object");},),
    );
  }
}
