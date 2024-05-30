import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobileapp/ui/Components/styles.dart';

class CenterCircularProgressIndicator extends StatelessWidget {
  const CenterCircularProgressIndicator({super.key});

  @override
  Widget build(BuildContext context) {
    return const Center(
      child: CircularProgressIndicator(
        strokeCap: StrokeCap.square,
        backgroundColor: Colors.transparent,
        color: DriveColors.lightBlueColor,
        strokeWidth: 2,
      ),
    );
  }
}