import 'dart:ffi';

import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

import 'Components/appbar.dart';
import 'Components/form_input_subpage.dart';
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

  static createInformationField (
      String name,
      String value,
      BuildContext context,
      { Widget? subpage, bool isError = false }
      )
  => Scaffold(body:Text("lol"));
  //TODO: Исправить
  /// Закомментировал, чтобы убедиться, что ничего не рендерится
  // => Row(mainAxisAlignment: MainAxisAlignment.spaceBetween,
  //       children: [
  //       Text(name, style: DriveTextStyles.inputLabel),
  //       TextButton(child: Text(value, style: isError ? DriveTextStyles.errorLabel : DriveTextStyles.userInput), onPressed: () => subpage != null ? Navigator.push(context, MaterialPageRoute(builder: (context) => subpage)) : null),
  //     ],
  //   );


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
          createInformationField("Номер телефона", "+ 7 (800) 555 35 35", context,
              subpage: DrivePhoneNumberInputSubpage(
                initialValue: "+ 7 (800) 555 35 35",
                inputTitle: "Номер телефона",
                hintText: "",
                onSavePressed: (String str) => {},
              )),
          divider,
          createInformationField("Почта", "example@mail.ru", context,
              subpage: DriveEmailInputSubpage(
                initialValue: "example@mail.ru",
                inputTitle: "Почта",
                hintText: "",
                onSavePressed: (String str) => {},
              )),
          divider,
          createInformationField("Имя", "Василий", context,
              subpage: DriveTextInputSubpage(
                initialValue: "Василий",
                inputTitle: "Отчество",
                hintText: "",
                onSavePressed: (String str) => {},
              )),
          divider,
          createInformationField("Фамилия", "Пупкин", context,
              subpage: DriveTextInputSubpage(
                initialValue: "Пупкин",
                inputTitle: "Отчество",
                hintText: "",
                onSavePressed: (String str) => {},
              )),
          divider,
          createInformationField("Отчество", "?", context, isError: true,
              subpage: DriveTextInputSubpage(
                initialValue: "?",
                inputTitle: "Отчество",
                hintText: "",
                onSavePressed: (String str) => {},
              )),
          divider,
          createInformationField("Дата рождения", "27.02.2024", context,
              subpage: DriveDateInputSubpage(
                inputTitle: "Дата рождения",
                hintText: "",
                onSavePressed: (DateTime date) => {},
              )),
          divider,
          createInformationField("Паспорт", "92 17 181511", context,
              subpage: DriveTextInputSubpage(
                initialValue: "92 17 181511",
                inputTitle: "Паспорт",
                hintText: "",
                onSavePressed: (String str) => {},
              )),
          divider,
          createInformationField("ВУ", "5643-47820732", context,
              subpage: DriveTextInputSubpage(
                initialValue: "5643-47820732",
                inputTitle: "ВУ",
                hintText: "",
                onSavePressed: (String str) => {},
              )),
        ]
    );
  }
}



