import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:mobileapp/ui/Components/styles.dart';
import 'package:mobileapp/ui/pages/initial.dart';
import 'package:mobileapp/ui/pages/pages_list.dart';
import 'package:mobileapp/ui/pages/subscriptions.dart';
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
        colorScheme: ColorScheme.fromSeed(seedColor: DriveColors.lightBlueColor),
      ),
      home: InitialPageWidget.create(),
      routes: {
        DriveRoutes.home : (context) => throw UnimplementedError("Implement page"),
        DriveRoutes.profile: (_) => ProfilePageWidget.create(),
        DriveRoutes.userSubscriptions: (_) => SubscriptionsPageWidget.create(),
        DriveRoutes.payment: (context) => throw UnimplementedError("Implement page"),
        DriveRoutes.login: (context) => throw UnimplementedError("Implement page"),
        DriveRoutes.registration: (context) => throw UnimplementedError("Implement page"),
      },
    );
  }
}
