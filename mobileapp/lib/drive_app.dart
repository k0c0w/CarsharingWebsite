import 'package:flutter/material.dart';
import 'package:mobileapp/ui/Components/styles.dart';
import 'package:mobileapp/ui/pages/home/home_page.dart';
import 'package:mobileapp/ui/pages/initial.dart';
import 'package:mobileapp/ui/pages/login.dart';
import 'package:mobileapp/ui/pages/pages_list.dart';
import 'package:mobileapp/ui/pages/payment.dart';
import 'package:mobileapp/ui/pages/profile.dart';
import 'package:mobileapp/ui/pages/register.dart';
import 'package:mobileapp/ui/pages/subscriptions/subscription_page.dart';
import 'package:mobileapp/ui/pages/support_chat_page.dart';
import 'package:mobileapp/ui/pages/unathorized_home_page.dart';

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
      initialRoute: DriveRoutes.appLoader,
      routes: {
        DriveRoutes.home : (_) => const HomePageWidget(),
        DriveRoutes.unathorizedHome: (context) => const UnauthorizedHomePageWidget(),
        DriveRoutes.profile: (_) => const ProfilePageWidget(),
        DriveRoutes.userSubscriptions: (_) => const SubscriptionsPageWidget(),
        DriveRoutes.payment: (_) => const PaymentPageWidget(),
        DriveRoutes.login: (_) => const LoginPageWidget(),
        DriveRoutes.registration: (_) => const RegisterPageWidget(),
        DriveRoutes.appLoader: (_) => InitialPageWidget(),
        DriveRoutes.support: (_) => const SupportChatPageWidget(),
      },
    );
  }
}
