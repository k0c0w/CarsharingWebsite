import 'package:flutter/material.dart';

class LoadPageErrorMessageAtCenter extends StatelessWidget {

  final void Function()? onRetryPressed;
  final String? customErrorMessage;
  const LoadPageErrorMessageAtCenter({
    super.key,
    this.onRetryPressed,
    this.customErrorMessage
  });

  @override
  Widget build(BuildContext context) {

    final displayableWidgets = <Widget>[Text(customErrorMessage ?? "Что-то пошло не так..."),];
    if (onRetryPressed != null) {
      displayableWidgets.add(
          TextButton(
              onPressed: onRetryPressed!,
              child: const Text("Повторить"))
      );
    }

    return Center(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: displayableWidgets,
      )
    );
  }
}
