import 'package:flutter/material.dart';
import 'package:mobileapp/ui/Components/styles.dart';

class ConfirmationContainerWidget extends StatelessWidget {

  final void Function() onAccept;
  final void Function() onDecline;
  final String questionText;
  final String acceptanceText;
  final String declineText;

  const ConfirmationContainerWidget({
    super.key,
    required this.acceptanceText,
    required this.declineText,
    required this.onAccept,
    required this.onDecline,
    required this.questionText,
  });

  @override
  Widget build(BuildContext context) {
    const buttonsSize = Size(322, 45);

    return Container(
      padding: const EdgeInsets.symmetric(vertical: 15, horizontal: 5),
      child: Column(
        mainAxisAlignment: MainAxisAlignment.spaceEvenly,
        children: [
          Text(questionText),
          Column(
            mainAxisAlignment: MainAxisAlignment.end,
            children: [
              TextButton(
                  style: TextButton.styleFrom(
                    fixedSize: buttonsSize,
                    backgroundColor: DriveColors.lightBlueColor,
                    foregroundColor: Colors.white,
                  ),
                  onPressed: onAccept,
                  child: Text(acceptanceText)
              ),
              TextButton(
                style: TextButton.styleFrom(
                    foregroundColor: DriveColors.deepBlueColor,
                    fixedSize: buttonsSize
                ),
                onPressed: onDecline,
                child: Text(declineText),
              ),
            ],
          )
        ],
      ),
    );
  }
}
