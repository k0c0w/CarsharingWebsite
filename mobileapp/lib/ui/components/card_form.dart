import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class _CardFormInputState extends State<CardFromInput> {
  bool isEnabled;

  _CardFormInputState(this.isEnabled);

  TextEditingController carNumberController = TextEditingController();
  _CardType cardType = _CardType.invalid;
  void getCardTypeFromNumber() {
      final cardNum = _CardUtils.getCleanedNumber(carNumberController.text);
      final cardType = _CardUtils.getCardTypeFromNumber(cardNum);

      if (this.cardType != cardType) {
        setState(() {
          this.cardType = cardType;
        });
    }
  }

  @override
  void initState() {
    carNumberController.addListener(getCardTypeFromNumber);

    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Form(
        child: Column(
          children: [
            TextFormField(
              controller: carNumberController,
              keyboardType: TextInputType.number,
              inputFormatters: [
                FilteringTextInputFormatter.digitsOnly,
                LengthLimitingTextInputFormatter(19),
                _CardNumberInputFormatter(),
              ],
              enabled: isEnabled,
              cursorColor: Colors.white,
              style: const TextStyle(color: Colors.white),
              decoration: InputDecoration(
                  hintText: "карта",
                  hintStyle: const TextStyle(color: Colors.white, fontWeight: FontWeight.w400),
                  prefixIcon: const Padding(
                    padding: EdgeInsets.symmetric(vertical: 10),
                    child: Icon(Icons.credit_card, color: Colors.white,),
                  ),
                  suffixIcon: Padding(
                    padding: const EdgeInsets.all(8.0),
                    child: _CardUtils.getCardIcon(cardType),
                  ),
                focusedBorder: const UnderlineInputBorder(borderSide: BorderSide(color: Colors.white)),
                enabledBorder: const UnderlineInputBorder(borderSide: BorderSide(color: Colors.white)),
                disabledBorder: UnderlineInputBorder(borderSide: BorderSide(color: Colors.white)),
              ),
            ),
            Row(
              children: [
                Expanded(child: TextFormField(
                  keyboardType: TextInputType.number,
                  inputFormatters: [
                    FilteringTextInputFormatter.digitsOnly,
                    LengthLimitingTextInputFormatter(4),
                  ],
                  enabled: isEnabled,
                  cursorColor: Colors.white,
                  style: const TextStyle(color: Colors.white),
                  textAlign: TextAlign.center,
                  decoration: const InputDecoration(
                      hintText: "CVV/CVC",
                      focusColor: Colors.white,
                      hintStyle: TextStyle(color: Colors.white, fontWeight: FontWeight.w400,),
                    focusedBorder: UnderlineInputBorder(borderSide: BorderSide(color: Colors.white)),
                    enabledBorder: UnderlineInputBorder(borderSide: BorderSide(color: Colors.white)),
                    disabledBorder: UnderlineInputBorder(borderSide: BorderSide(color: Colors.white)),
                  ),

                )),
                const SizedBox(width: 16.0,),
                Expanded(child: TextFormField(
                  keyboardType: TextInputType.number,
                  inputFormatters: [
                    FilteringTextInputFormatter.digitsOnly,
                    LengthLimitingTextInputFormatter(5),
                    _CardMonthFormatter(),
                  ],
                  enabled: isEnabled,
                  cursorColor: Colors.white,
                  style: const TextStyle(color: Colors.white),
                  decoration: const InputDecoration(
                    hintText: "мм/гг",
                    hintStyle: TextStyle(color: Colors.white, fontWeight: FontWeight.w400,),
                    prefixIcon: Padding(
                      padding: EdgeInsets.symmetric(vertical: 10.0),
                      child: Icon(Icons.date_range, color: Colors.white,),
                    ),
                    focusedBorder: UnderlineInputBorder(borderSide: BorderSide(color: Colors.white)),
                    enabledBorder: UnderlineInputBorder(borderSide: BorderSide(color: Colors.white)),
                    disabledBorder: UnderlineInputBorder(borderSide: BorderSide(color: Colors.white)),
                  ),
                )),
              ],
            ),
          ],
        )
    );
  }
}

class CardFromInput extends StatefulWidget {
  final bool isEnabled;
  const CardFromInput({super.key, this.isEnabled = true});

  @override
  State<StatefulWidget> createState() => _CardFormInputState(isEnabled);
}

enum _CardType {
  invalid,
  undefined,
  masterCard,
  visa,
  mir,
}

class _CardUtils {
  static String getCleanedNumber(String dirtyCarNumber) {
    final regExp = RegExp(r"[^0-9]");
    return dirtyCarNumber.replaceAll(regExp, '');
  }

  static Widget? getCardIcon(_CardType? cardType) {
    var img = "";
    Icon? icon;
    switch (cardType) {
      case _CardType.masterCard:
        img = "mastercard.png";
        break;
      case _CardType.visa:
        img = "visa.png";
        break;
      case _CardType.mir:
        img = "mir.png";
        break;
      default:
        icon = const Icon(
            Icons.warning,
            size: 24,
            color: Colors.white
        );
        break;
    }

    return img.isEmpty ? icon : Image.asset('assets/cards/logos/$img', width: 40.0);
  }

  static String? validateCardNumber(String? input) {
    if (input == null || input.isEmpty) {
      return "Поле обязательно для заполнения.";
    }

    final cleanInput = getCleanedNumber(input);

    if(cleanInput.length < 8) {
      return "Поле заполнено неверно.";
    }

    int sum = 0;
    int length = cleanInput.length;
    for (var i = 0; i < length; i++) {
      int digit = int.parse(input[length - i - 1]);
      if (i % 2 == 1) {
        digit *= 2;
      }

      sum += digit > 9 ? (digit - 9) : digit;
    }

    if (sum % 10 == 0) {
      return null;
    }

    return "Не верный номер карты.";
  }

  static _CardType getCardTypeFromNumber(String input) {
    if (input.startsWith(RegExp(
        r'((5[1-5])|(222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720))'
    ))) {
      return _CardType.masterCard;
    }
    if(input.startsWith(RegExp(r'[4]'))) {
      return _CardType.visa;
    }
    if (input.length >=4 && input.startsWith('2')) {
      return _CardType.mir;
    }

    if(input.length <= 8){
      return _CardType.undefined;
    }

    return _CardType.invalid;
  }
}

class _CardMonthFormatter extends TextInputFormatter {

  @override
  TextEditingValue formatEditUpdate(TextEditingValue oldValue, TextEditingValue newValue) {
    var newText = newValue.text;

    if (newValue.selection.baseOffset == 0) {
      return newValue;
    }

    var buffer = StringBuffer();
    for (var i = 0; i < newText.length; i++) {
      buffer.write(newText[i]);
      var nonZeroIndex = i + 1;
      if (nonZeroIndex % 2 ==0 && nonZeroIndex != newText.length){
        buffer.write('/');
      }
    }

    final string = buffer.toString();
    return newValue.copyWith(
      text: string,
      selection: TextSelection.collapsed(offset: string.length),
    );
  }

}
class _CardNumberInputFormatter extends TextInputFormatter {
  @override
  TextEditingValue formatEditUpdate(TextEditingValue oldValue, TextEditingValue newValue) {
    if(newValue.selection.baseOffset == 0){
      return newValue;
    }

    final inputData = newValue.text;
    final buffer = StringBuffer();

    for(var i = 0; i < inputData.length; i++) {
      buffer.write(inputData[i]);
      final index = i + 1;

      if (index % 4 == 0 && inputData.length != index) {
        buffer.write("  ");
      }
    }

    final bufferString = buffer.toString();
    return TextEditingValue(
      text: bufferString,
      selection: TextSelection.collapsed(offset: bufferString.length),
    );
  }
}