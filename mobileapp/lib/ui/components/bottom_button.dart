import 'dart:ui';

import 'package:flutter/material.dart';
import 'package:mobileapp/ui/components/styles.dart';

class BottomButton extends StatelessWidget {
  final String title;
  final void Function()? onPressed;
  const BottomButton({super.key, required this.title, this.onPressed});

  @override
  Widget build(BuildContext context) {
    return Expanded(
        child: Align(
          alignment: Alignment.bottomCenter,
          child: Container(
            height: 50,
            margin: const EdgeInsets.only(bottom: 30, right: 20, left: 20),
            alignment: Alignment.center,
            child: ElevatedButton(
              style:  ElevatedButton.styleFrom(
                minimumSize: const Size(322, 45),
                backgroundColor: DriveColors.lightBlueColor,
                alignment: Alignment.center,
                shape: const RoundedRectangleBorder(borderRadius: BorderRadius.all(Radius.circular(7))),
                foregroundColor: Colors.white,
              ),
              onPressed: onPressed,
              child: Text(
                title,
                style: const TextStyle(
                  fontSize: 15,
                  fontWeight: FontWeight.normal,
                  letterSpacing: 4.5,
                ),
              ),
            ),
          ),
        ),
    );
  }
}
