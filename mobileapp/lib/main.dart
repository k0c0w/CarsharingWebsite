import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:mobileapp/ui/Components/styles.dart';
import 'package:mobileapp/ui/pages/initial.dart';
import 'package:mobileapp/ui/pages/login.dart';
import 'package:mobileapp/ui/pages/pages_list.dart';
import 'package:mobileapp/ui/pages/register.dart';
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
      home: const InitialPageWidget(),
      routes: {
        DriveRoutes.home : (context) => throw UnimplementedError("Implement page"),
        DriveRoutes.profile: (_) => const ProfilePageWidget(),
        DriveRoutes.userSubscriptions: (_) => const SubscriptionsPageWidget(),
        DriveRoutes.payment: (context) => throw UnimplementedError("Implement page"),
        DriveRoutes.login: (context) => const LoginPageWidget(),
        DriveRoutes.registration: (context) => const RegisterPageWidget(),
      },
    );
  }
}
