import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'Components/appbar.dart';
import 'Components/styles.dart';

const List<Widget> fruits = <Widget>[
  Text('Apple'),
  Text('Banana'),
  Text('Orange')
];

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
      body: Column(
        mainAxisAlignment: MainAxisAlignment.start,
        children: [
          divider,
          Center(child:SubscriptionToggleButton()),
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

class InformationTable extends StatelessWidget {
  const InformationTable({super.key});

  static createInformationField (String name, String value, { bool isError = false }) {
    return Container(margin:  const EdgeInsets.only(left: 20.0, right: 20.0), child: Row(mainAxisAlignment: MainAxisAlignment.spaceBetween,
      children: [
        Text(name, style: DriveTextStyles.inputLabel),
        Text(value, style: isError ? DriveTextStyles.errorLabel : DriveTextStyles.userInput),
      ],
    ));
  }

  @override
  Widget build(BuildContext context) {
    const divider = Divider(
      height: 30,
      thickness: 0.4,
      indent: 20,
      endIndent: 20,
    );

    return Column(
        children: [
          createInformationField("Номер телефона", "+ 7 (800) 555 35 35"),
          divider,
          createInformationField("Почта", "example@mail.ru"),
          divider,
          createInformationField("Имя", "Василий"),
          divider,
          createInformationField("Фамилия", "Пупкин"),
          divider,
          createInformationField("Отчество", "?", isError: true),
          divider,
          createInformationField("Дата рождения", "27.02.2024"),
          divider,
          createInformationField("Паспорт", "92 17 181511"),
          divider,
          createInformationField("ВУ", "5643-47820732"),
        ]
    );
  }
}

class SubscriptionToggleButton extends StatefulWidget {
  SubscriptionToggleButton({super.key});

  @override
  _SubscriptionToggleButton createState() => _SubscriptionToggleButton();
}

class _SubscriptionToggleButton extends State<SubscriptionToggleButton> {
  List<bool> isSelected = [
    false, false
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
