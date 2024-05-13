import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:mobileapp/ui/Components/styles.dart';

abstract class CarCardBase extends StatelessWidget {
  final String name;
  final String description;

  const CarCardBase({
    super.key,
    required this.name,
    required this.description,
  });

  Widget rightCardWidget();

  @override
  Widget build(BuildContext context) {
    return Center(
        child: SizedBox(width: 400, child: Card(
          margin: const EdgeInsets.all(7.0),
          shadowColor: const Color.fromRGBO(200, 200, 200, 1),
          color: const Color.fromRGBO(255, 255, 255, 1),
          child: Row(
            mainAxisSize: MainAxisSize.max,
            crossAxisAlignment: CrossAxisAlignment.end,
            children: <Widget>[
              SizedBox(
                  width: 180,
                  height: 50,
                  child: Column(mainAxisAlignment: MainAxisAlignment.center,
                      crossAxisAlignment: CrossAxisAlignment.center,
                      children: [
                        Text(name, style: GoogleFonts.orbitron(
                            color: Colors.black,
                            fontSize: 15,
                            fontWeight: FontWeight.bold)),
                        Text(description, style: GoogleFonts.orbitron(
                            color: Colors.grey,
                            fontSize: 10,
                            fontWeight: FontWeight.bold)),
                      ])),
              rightCardWidget(),
            ],
          ),
        ),
        )
    );
  }
}

class SubscriptionCard extends CarCardBase {
  final void Function() onOpenOrCloseCar;
  final void Function() onLightsUp;
  final void Function() onEngineStart;
  final bool isOpened;

  const SubscriptionCard({
    super.key,
    required this.onEngineStart,
    required this.onLightsUp,
    required this.onOpenOrCloseCar,
    required this.isOpened,
    required super.name,
    required super.description
  });

  static const buttonStyle = ButtonStyle(foregroundColor: MaterialStatePropertyAll(Colors.black));
  @override
  Widget rightCardWidget() => Row(
      children: [
        TextButton(
          style: buttonStyle,
          onPressed: onLightsUp,
          child: const ImageIcon(AssetImage("./assets/lights.png")),
        ),
        TextButton(
          style: buttonStyle,
          onPressed: onEngineStart,
          child:  const ImageIcon(AssetImage("./assets/engine.png")),
        ),
        TextButton(
          style: !isOpened ? buttonStyle : buttonStyle
              .copyWith(backgroundColor:const MaterialStatePropertyAll(DriveColors.deepBlueColor)),
          onPressed: onOpenOrCloseCar,
          child: const ImageIcon(AssetImage("./assets/key.png")),
        ),
      ]
  );
}


class HistoryCard extends CarCardBase {
  const HistoryCard({
    super.key,
    required super.name,
    required super.description
  });

  @override
  Widget rightCardWidget() {
    return Container();
  }
}


