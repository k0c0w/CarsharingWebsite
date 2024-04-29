import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobileapp/ui/Components/styles.dart';

class DriveTextFromField extends StatelessWidget {
  final bool enabled;
  final void Function(String) onChange;
  const DriveTextFromField({super.key,
    required this.enabled,
    required this.onChange,

  });

  @override
  Widget build(BuildContext context) {
    return TextFormField(
      obscureText: true,
      decoration: InputDecoration(
        labelText: "Пароль",
        enabledBorder: const UnderlineInputBorder(
            borderSide: BorderSide(
              color: DriveColors.lightBlueColor,
            )
        ),
        disabledBorder: const UnderlineInputBorder(),
        enabled: enabled,
      ),
      onChanged: onChange,
    );
  }
}