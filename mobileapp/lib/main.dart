import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:get_it/get_it.dart';
import 'package:mobileapp/bloc/auth/auth_bloc.dart';
import 'package:mobileapp/bloc/auth/auth_bloc_states.dart';
import 'package:mobileapp/ui/Components/styles.dart';
import 'package:mobileapp/ui/pages/home/home_page.dart';
import 'package:mobileapp/ui/pages/initial.dart';
import 'package:mobileapp/ui/pages/login.dart';
import 'package:mobileapp/ui/pages/pages_list.dart';
import 'package:mobileapp/ui/pages/payment.dart';
import 'package:mobileapp/ui/pages/register.dart';
import 'package:mobileapp/ui/pages/subscriptions.dart';
import 'package:mobileapp/ui/pages/unathorized_home_page.dart';
import 'ui/pages/profile.dart';

final getIt = GetIt.instance;

void main() {
  getIt.registerSingleton(AuthBloc(AuthCheckStatusInProgressState()));

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
      routes: {
        DriveRoutes.home : (_) => const HomePageWidget(),
        DriveRoutes.unathorizedHome: (context) => const UnauthorizedHomePageWidget(),
        DriveRoutes.profile: (_) => const ProfilePageWidget(),
        DriveRoutes.userSubscriptions: (_) => const SubscriptionsPageWidget(),
        DriveRoutes.payment: (_) => const PaymentPageWidget(),
        DriveRoutes.login: (_) => const LoginPageWidget(),
        DriveRoutes.registration: (_) => const RegisterPageWidget(),
        DriveRoutes.appLoader: (_) => InitialPageWidget(),
      },
      initialRoute: DriveRoutes.appLoader,
    );
  }
}
