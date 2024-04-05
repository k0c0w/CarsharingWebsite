import 'package:flutter/material.dart';
import 'package:mobileapp/Components/styles.dart';

const _ellipticalRadius = Radius.elliptical(50, 20);

class DriveAppBar extends AppBar {
  DriveAppBar({super.key, required String title})
      : super(
          title: Text(
            title.toUpperCase(),
            style: DriveTextStyles.appBarTitle,
          ),
          centerTitle: true,
          leading: const BackButton(),
          backgroundColor: DriveColors.lightBlueColor,
          foregroundColor: Colors.white,
          shape: const RoundedRectangleBorder(
            borderRadius: BorderRadius.only(
              bottomLeft: _ellipticalRadius,
              bottomRight: _ellipticalRadius
            )
          )
  );
}