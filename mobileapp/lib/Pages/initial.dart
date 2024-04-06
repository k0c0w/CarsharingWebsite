import 'dart:ui';

import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobileapp/Components/styles.dart';

class InitialPage extends StatefulWidget {
  const InitialPage({super.key});

  @override
  State<StatefulWidget> createState() => _InitialPage();
}

class _InitialPage extends State<InitialPage> {

  @override
  Widget build(BuildContext context) {
    return const Scaffold(
      body: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        crossAxisAlignment: CrossAxisAlignment.center,
        children: [
          Text(
            "D",
            style: TextStyle(
              color: DriveColors.deepBlueColor,
              fontWeight: FontWeight.w500,
              fontSize: 26,
            ),)
        ],
      ),
    );
  }
}
