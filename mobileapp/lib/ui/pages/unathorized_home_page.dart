
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobileapp/ui/Components/styles.dart';
import 'package:mobileapp/ui/pages/pages_list.dart';

import '../components/bottom_button.dart';

class UnauthorizedHomePageWidget extends StatelessWidget {
  const UnauthorizedHomePageWidget({super.key});
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        crossAxisAlignment: CrossAxisAlignment.center,
        children: [
          const Row(),
          Row(
            children: [
              TextButton(
                  onPressed: () {
                    Navigator.of(context).pushNamed(DriveRoutes.registration);
                  },
                  child: Text(
                      "Регистрация в сервисе",
                       style: TextStyle(color: DriveColors.deepBlueColor),
                  ))
            ],
            mainAxisAlignment: MainAxisAlignment.start,
            crossAxisAlignment: CrossAxisAlignment.end,
          ),
          const Spacer(),
          BottomButton(title: "ВОЙТИ", onPressed: () {
              Navigator.of(context).pushNamed(DriveRoutes.login);
            },
          ),
        ],
      ),
    );
  }
}
