
import 'dart:ui';

import 'package:flutter/material.dart';

class DriveColors {
  /// #10B5DC 100%
  static const lightBlueColor = Color.fromRGBO(16, 181, 220, 100);
  /// #757C7E 100%
  static const darkGreyColor = Color.fromRGBO(117, 124, 126, 100);
  /// #191818 100%
  static const blackColor = Color.fromRGBO(25, 25, 24, 100);
}

class DriveTextStyles {
  static const appBarTitle = TextStyle(
    fontSize: 20,
    ///todo: must be w500 with another font
    fontWeight: FontWeight.w400,
    //todo: import custom font to package
    // https://api.flutter.dev/flutter/painting/TextStyle-class.html
    fontFamily: 'Orbitron',
    letterSpacing: 2,
  );

  static const userInput = TextStyle(
    color: Colors.black87,
    overflow: TextOverflow.ellipsis,
    fontSize: 15,
    fontWeight: FontWeight.w500,
  );

  static const inputLabel = TextStyle(
    color: DriveColors.darkGreyColor,
    fontWeight: FontWeight.w600,
    fontSize: 15,
    letterSpacing: 2,
  );
}
