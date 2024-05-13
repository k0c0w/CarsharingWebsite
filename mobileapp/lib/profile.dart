import 'dart:ffi';

import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

import 'Components/appbar.dart';
import 'Components/styles.dart';

class Profile extends StatelessWidget {
  const Profile({super.key});

  @override
  Widget build(BuildContext context) {

    const divider = Divider(
      height: 40,
      color: Colors.transparent,
    );

    return Scaffold(
      appBar:  DriveAppBar(title: "Profile"),
      body: const Column(
          mainAxisAlignment: MainAxisAlignment.center,
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            Balance(balanceNumber: 100000.0),
            divider,
            InformationTable(),
            divider,
            Text("Данные не подтверждены!", style: DriveTextStyles.errorLabel,),
            divider,
            TextButton(onPressed: null, child: Text("ВЫХОД", style: DriveTextStyles.userInput),),
          ])
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



