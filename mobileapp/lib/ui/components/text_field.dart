import 'package:flutter/material.dart';
import 'package:mobileapp/ui/Components/styles.dart';

class DriveTextFromField extends StatelessWidget {
  final bool enabled;
  final bool obscureText;
  final String label;
  final void Function(String) onChange;
  const DriveTextFromField({super.key,
    required this.enabled,
    required this.onChange,
    required this.label,
    this.obscureText = false,
  });

  @override
  Widget build(BuildContext context) {
    return TextFormField(
      obscureText: obscureText,
      decoration: InputDecoration(
        labelText: label,
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