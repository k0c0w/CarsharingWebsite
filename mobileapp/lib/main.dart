import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:mobileapp/ui/Components/styles.dart';
import 'package:mobileapp/ui/pages/home/home_page.dart';
import 'package:mobileapp/ui/pages/login.dart';
import 'package:mobileapp/ui/pages/home/modal_page.dart';
import 'package:mobileapp/ui/pages/pages_list.dart';
import 'package:mobileapp/ui/pages/payment.dart';
import 'package:mobileapp/ui/pages/register.dart';
import 'package:mobileapp/ui/pages/subscriptions.dart';
import 'package:mobileapp/ui/pages/unathorized_home_page.dart';
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
      home: HomePageWidget(),
      routes: {
        DriveRoutes.home : (_) => const HomePage(),
        DriveRoutes.unathorizedHome: (context) => const UnauthorizedHomePageWidget(),
        DriveRoutes.profile: (_) => const ProfilePageWidget(),
        DriveRoutes.userSubscriptions: (_) => const SubscriptionsPageWidget(),
        DriveRoutes.payment: (_) => const PaymentPageWidget(),
        DriveRoutes.login: (context) => const LoginPageWidget(),
        DriveRoutes.registration: (context) => const RegisterPageWidget(),
      },
    );
  }
}
