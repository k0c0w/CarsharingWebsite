import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import '../Components/appbar.dart';
import '../Components/styles.dart';

class Subscriptions extends StatelessWidget {
  const Subscriptions({super.key});

  @override
  Widget build(BuildContext context) {
    const divider = Divider(
      height: 20,
      color: Colors.transparent,
    );

    return Scaffold(
      appBar: DriveAppBar(title: "Subscriptions"),
      body: const Column(
        mainAxisAlignment: MainAxisAlignment.start,
        children: [
          divider,
          Center(child:SubscriptionToggleButton()),
          SubscriptionList()
        ],
      ),
    );
  }
}

class Balance extends StatelessWidget {
  const Balance({super.key, required this.balanceNumber});
  final double balanceNumber;

  @override
  Widget build(BuildContext context) {
    return Column(children: [
      Text(balanceNumber.toString(),
          style: GoogleFonts.orbitron(color: Colors.black54, fontSize: 35, fontWeight: FontWeight.bold)),
      CommentaryStyles.createMediumText("баланс")
    ]);
  }
}

class SubscriptionToggleButton extends StatefulWidget {
  const SubscriptionToggleButton({super.key});

  @override
  State<SubscriptionToggleButton> createState() => _SubscriptionToggleButton();
}

class _SubscriptionToggleButton extends State<SubscriptionToggleButton> {
  List<bool> isSelected = [
    true, false
  ];

  changeButtonState(int index) {
    isSelected = [false, false];
    setState(() => isSelected[index] = !isSelected[index]);
  }

  @override
  Widget build(BuildContext context) {
    return ToggleButtons(
      isSelected: isSelected,
      onPressed: changeButtonState,
      borderRadius: const BorderRadius.all(Radius.circular(10)),
      selectedColor: Colors.white,
      fillColor: const Color.fromRGBO(5, 59, 74, 1),
      color: const Color.fromRGBO(117, 124, 126, 1),
      constraints: const BoxConstraints(
        minHeight: 33.0,
        minWidth: 160.0,
      ),
      children: const [Text("АКТИВНЫЕ"), Text("ИСТОРИЯ")],
    );
  }
}


class SubscriptionList extends StatelessWidget {
  const SubscriptionList({super.key});

  @override
  Widget build(BuildContext context) {
    return const Column(
        children: [
          SubscriptionCard(name: "Toyota Camry", description: "A 720 СУ 160 рус",),
        ]
    );
  }
}

class SubscriptionCard extends StatelessWidget{
  const SubscriptionCard({super.key, required this.name, required this.description});
  final String name;
  final String description;

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
              SubscriptionCardButtons(),
            ],
          ),
        ),
        ));
  }
}

class SubscriptionCardButtons extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    // TODO: implement build
    return Row(
        children: [
          TextButton(
            style: const ButtonStyle(foregroundColor: MaterialStatePropertyAll(Colors.black)),
            child: const ImageIcon(AssetImage("./assets/raketa.png")),
            onPressed: () {
              /* ... */
            },
          ),
          TextButton(
            style: const ButtonStyle(foregroundColor: MaterialStatePropertyAll(Colors.black)),
            child: ImageIcon(AssetImage("./assets/trooba.png")),
            onPressed: () {
              /* ... */
            },
          ),
          TextButton(
            style: const ButtonStyle(foregroundColor: MaterialStatePropertyAll(Colors.black)),
            child: const ImageIcon(AssetImage("./assets/key.png")),
            onPressed: () {
              /* ... */
            },
          ),
    ]);
  }
}