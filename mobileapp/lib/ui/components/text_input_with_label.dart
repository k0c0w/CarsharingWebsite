import 'package:flutter/material.dart';
import 'package:mobileapp/ui/Components/styles.dart';

class InformationFieldWithLabel extends StatelessWidget {
  final String name;
  final String value;
  final String? error;
  final void Function() onTap;

  const InformationFieldWithLabel({
    super.key,
    required this.name,
    required this.onTap,
    required this.value,
    this.error
  });

  @override
  Widget build(BuildContext context) {

    final dataColumn = <Widget>[Text(value, style: DriveTextStyles.userInput)];
    if (error != null && error!.isNotEmpty) {
      dataColumn.add(Text(error!,
        style: const TextStyle(
            fontSize: 10,
            color: DriveColors.brightRedColor
        ),
      ));
    }

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
              Column(
                mainAxisAlignment: MainAxisAlignment.end,
                crossAxisAlignment: CrossAxisAlignment.end,
                children: dataColumn
              ),
              const Icon(Icons.arrow_right, color: Colors.black87, opticalSize: 0.2,),
            ],
          ),
        ],
      ),
    );
  }
}
