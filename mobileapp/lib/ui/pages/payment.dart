import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:mobileapp/ui/Components/appbar.dart';
import 'package:mobileapp/ui/components/bottom_button.dart';

import '../Components/styles.dart';
import '../components/card_form.dart';


class PaymentPageWidget extends StatelessWidget {
  const PaymentPageWidget({super.key});

  @override
  Widget build(BuildContext context) {

    final requestSent = false;
    final children = <Widget>[const _MainViewWidget()];
    if (requestSent)
      children.add(const _PopScopedCircularProgressionWidget());

    return Scaffold(
      appBar: DriveAppBar(title: "ОПЛАТА"),
      body: SafeArea(
        minimum: const EdgeInsets.only(left: 20, right: 20, top: 80),
        child: Stack(
          children: children
        )
      ),
    );
  }
}

class _PopScopedCircularProgressionWidget extends StatelessWidget {
  const _PopScopedCircularProgressionWidget();

  @override
  Widget build(BuildContext context) {
    return PopScope(
      canPop: false,
      onPopInvoked: (canPop) {
        print("Pop");
      },
      child: const Center(child: CircularProgressIndicator(),),
    );
  }
}


class _MainViewErrorLabel extends StatelessWidget {
  const _MainViewErrorLabel();
  @override
  Widget build(BuildContext context) {
    return Container(
      margin: const EdgeInsets.only(top: 20),
      child: Text("Ошибка",
          style: GoogleFonts.openSans(
            textStyle: const TextStyle(
              color: DriveColors.brightRedColor,
              fontWeight: FontWeight.w500,
              fontSize: 15,
            ),
          )
      ),
    );
  }
}

class _MainViewWidget extends StatelessWidget {
  const _MainViewWidget();

  @override
  Widget build(BuildContext context) {
    final screenSize = MediaQuery.of(context).size;
    final isEnabled = false;
    return Column(
    children: [
      Container(
        color: Colors.black87,
        padding: const EdgeInsets.symmetric(vertical: 20, horizontal: 30),
        width: screenSize.width * 0.875,
        height: screenSize.height * 0.2125,
        margin: const EdgeInsets.only(bottom: 25),
        child: const CardFromInput(isEnabled: false),
      ),
      TextFormField(inputFormatters: [
        FilteringTextInputFormatter.digitsOnly,
      ],
        keyboardType: TextInputType.number,
        decoration: const InputDecoration(
          hintText: "Сумма",
          focusedBorder: UnderlineInputBorder(
              borderSide: BorderSide(color:DriveColors.lightBlueColor)
          ),
        ),
        textAlign: TextAlign.center,
        enabled: isEnabled,
      ),
      const _MainViewErrorLabel(),
      BottomButton(title: "ОПЛАТИТЬ"),
    ],
    );
  }
}