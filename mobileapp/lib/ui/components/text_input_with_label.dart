import 'package:flutter/material.dart';
import 'package:mobileapp/ui/Components/styles.dart';

class InformationFieldWithLabel extends StatelessWidget {
  final String name;
  final String? value;
  final bool isError;
  final void Function() onTap;

  const InformationFieldWithLabel({
    super.key,
    required this.name,
    required this.onTap,
    required this.value,
    this.isError = false
  });

  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onTap,
      child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [
          Text(name, style: DriveTextStyles.inputLabel),
          Row(
            mainAxisAlignment: MainAxisAlignment.end,
            crossAxisAlignment: CrossAxisAlignment.center,
            children: [
              Text(value ?? "", style: isError ? DriveTextStyles.errorLabel : DriveTextStyles.userInput),
              const Icon(Icons.arrow_right, color: Colors.black87, opticalSize: 0.2,),
            ],
          )
        ],
      ),
    );
  }
}
