
import 'dart:ui';

import 'package:flutter/material.dart';

class DriveColors {
  /// #10B5DC 100%
  static const lightBlueColor = Color.fromRGBO(16, 181, 220, 1);
  /// #757C7E 100%
  static const darkGreyColor = Color.fromRGBO(117, 124, 126, 1);
  /// #191818 100%
  static const blackColor = Color.fromRGBO(25, 25, 24, 1);

  static const deepBlueColor = Color.fromRGBO(0, 122, 174, 1);
  static const brightRedColor = Color.fromRGBO(248, 91, 91, 1);
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
    color: DriveColors.blackColor,
    overflow: TextOverflow.ellipsis,
    fontSize: 15,
    fontWeight: FontWeight.w500,
  );

  static const drawerListItem = TextStyle(
    color: DriveColors.blackColor,
    letterSpacing: 2,
    fontWeight: FontWeight.w600,
    fontFamily: 'Open Sans',
    fontSize: 13,
    overflow: TextOverflow.clip,
  );

  static const drawerHeaderMain = TextStyle(
    color: DriveColors.blackColor,
    letterSpacing: 2,
    fontWeight: FontWeight.w600,
    fontFamily: 'Open Sans',
    fontSize: 15,
    overflow: TextOverflow.clip,
  );

  static const drawerHeaderSubtitle = TextStyle(
    color: DriveColors.darkGreyColor,
    fontWeight: FontWeight.w400,
    fontFamily: 'Open Sans',
    fontSize: 13,
    overflow: TextOverflow.clip,
  );

  static const drawerBottomText = TextStyle(
      color: DriveColors.deepBlueColor,
      fontSize: 27,
      fontWeight: FontWeight.w800,
      fontFamily: "Orbitron",
      letterSpacing: 5
  );

  static const inputLabel = TextStyle(
    color: DriveColors.darkGreyColor,
    fontWeight: FontWeight.w600,
    fontSize: 15,
    letterSpacing: 2,
  );

  static const errorLabel = TextStyle(
    color: DriveColors.brightRedColor,
    fontWeight: FontWeight.w600,
    fontSize: 15,
    letterSpacing: 2,
  );
}

class CommentaryStyles {
  static const greyBigComment = TextStyle(
    color: Colors.black26,
    fontSize: 30,
    fontWeight: FontWeight.normal,
  );

  static const greyMediumComment = TextStyle(
    color: Colors.black26,
    fontSize: 15,
    fontWeight: FontWeight.normal,
  );

  static createMediumText(String text) {
    return Text(
      text,
      style: greyMediumComment,
    );
  }

  static createBigText(String text) {
    return Text(
      text,
      style: greyBigComment,
    );
  }
}