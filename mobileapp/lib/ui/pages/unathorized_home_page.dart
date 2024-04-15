
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:mobileapp/ui/Components/styles.dart';
import 'package:mobileapp/ui/pages/pages_list.dart';
import '../components/bottom_button.dart';

class UnauthorizedHomePageWidget extends StatelessWidget {
  const UnauthorizedHomePageWidget({super.key});
  @override
  Widget build(BuildContext context) {

    return Scaffold(
      body: SafeArea(
        right: true,
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            const Spacer(),
            const Spacer(),
            const _DriveLabelWidget(),
            const _RegistrationInServiceButton(),
            const Spacer(),
            BottomButton(title: "ВОЙТИ", onPressed: () {
                Navigator.of(context).pushNamed(DriveRoutes.login);
              },
            ),
          ],
        ),
      )
    );
  }
}

class _DriveLabelWidget extends StatelessWidget {
  const _DriveLabelWidget();
  @override
  Widget build(BuildContext context) {
    var screenSize = MediaQuery.of(context).size;
    return Row(
      mainAxisAlignment: MainAxisAlignment.spaceEvenly,
      crossAxisAlignment: CrossAxisAlignment.end,
      children: [
        Image(
          image: const AssetImage('assets/entry_page/entry_car.png'),
          alignment: Alignment.centerLeft,
          fit: BoxFit.fitHeight,
          height: 0.275 * screenSize.height,
        ),
        Flexible(
            child:
                RotatedBox(
                  quarterTurns: 3,
                  child: Text(
                    "Drive",
                    style: GoogleFonts.orbitron(
                      textStyle: const TextStyle(
                          fontSize: 120,
                          fontWeight: FontWeight.w800,
                          letterSpacing: 10
                      ),
                      color: DriveColors.deepBlueColor,
                  ),
                ),
            )
        )
      ],
    );
  }
}

class _RegistrationInServiceButton extends StatelessWidget {
  const _RegistrationInServiceButton();

  @override
  Widget build(BuildContext context) {
    return Row(
      mainAxisAlignment: MainAxisAlignment.start,
      crossAxisAlignment: CrossAxisAlignment.end,
      children: [
        TextButton(
            onPressed: () {
              Navigator.of(context).pushNamed(DriveRoutes.registration);
            },
            style: const ButtonStyle(
              animationDuration: Duration.zero,
            ),
            child: Text(
              "Регистрация в сервисе",
              style: GoogleFonts.openSans(
                textStyle: const TextStyle(
                  color: DriveColors.blackColor,
                  decoration: TextDecoration.underline,
                  fontSize: 13,
                  fontWeight: FontWeight.w500,
                  decorationColor: DriveColors.lightBlueColor,
                ),
              )
            ))
      ],
    );
  }
}
