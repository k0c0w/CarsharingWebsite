import 'package:email_validator/email_validator.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:intl_phone_number_input/intl_phone_number_input.dart';
import 'package:mobileapp/Components/appbar.dart';
import 'package:mobileapp/Components/bottom_button.dart';
import 'package:mobileapp/Components/styles.dart';

class DrivePhoneNumberInputSubpage extends StatefulWidget {
  final void Function(String) onSavePressed;

  const DrivePhoneNumberInputSubpage({
    super.key,

    required this.onSavePressed,
  });

  @override
  State<StatefulWidget> createState()
    => _DrivePhoneNumberInputSubpageState(

    );
}

class _DrivePhoneNumberInputSubpageState extends State<DrivePhoneNumberInputSubpage> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Column(children: [
        InternationalPhoneNumberInput(onInputChanged: (p) {}),
      ],),
    );
  }

}

class DriveNumberInputSubpage extends _InputSubpageBase {
  final void Function(num) onSavePressed;
  final num? initialValue;

  DriveNumberInputSubpage({
    required super.hintText,
    required super.inputTitle,
    required this.onSavePressed,
    this.initialValue,
  });

  @override
  State<StatefulWidget> createState()
  => _DriveNumberInputSubpageState(
      inputName: inputTitle,
      hintText: hintText,
      onSavePressed: onSavePressed,
      initialValue: initialValue,
  );
}

class _DriveNumberInputSubpageState extends _InputSubpageState<DriveNumberInputSubpage> {
  final void Function(num) onSavePressed;
  num _number = 0;

  _DriveNumberInputSubpageState({
    required super.inputName,
    required super.hintText,
    required this.onSavePressed,
    num? initialValue,
  }) :super(initialValue: initialValue?.toString()) {
    _number = initialValue ?? 0;
  }

  @override
  void _changeState() {
    setState(() {
      var parsedNum = num.tryParse(_textEditingController.text.trim());
      if (parsedNum != null && parsedNum.isFinite) {
        _number = parsedNum;
      }
    });
  }

  @override
  bool _inputValidator() {
    var parsedNum = num.tryParse(_textEditingController.text.trim());

    return parsedNum != null && parsedNum.isFinite;
  }

  @override
  void _onSavePressedInternal() {
    onSavePressed(_number);
  }

  @override
  TextField get _textField => _constructPureTextField(
    initialText: initialValue?.toString(),
    inputFormatters: [
      FilteringTextInputFormatter.allow(RegExp(r"[0-9]+([.])?(([0-9])+)?")),
      FilteringTextInputFormatter.singleLineFormatter,
    ],
    textInputType: TextInputType.number,
  );
}

class DriveEmailInputSubpage extends _TextBasedInputSubpage {
  const DriveEmailInputSubpage({
    required super.hintText,
    required super.inputTitle,
    required super.onSavePressed,
    super.initialValue,
  });

  @override
  State<StatefulWidget> createState()
    => _DriveEmailInputSubpageState(
        inputName: inputTitle,
        hintText: hintText,
        onSavePressed: onSavePressed,
        initialValue: initialValue,
    );
}

class _DriveEmailInputSubpageState
    extends _TextBasedInputSubpageState<DriveEmailInputSubpage> {
  static const _emailAllowedSymbols = r"[a-zA-Z0-9.!#$%&'*+\-/=?^_`{|}~\\(),:;<>@\[\]\w]";

  _DriveEmailInputSubpageState({
    required super.inputName,
    required super.hintText,
    required super.onSavePressed,
    super.initialValue
  });

  @override
  void _changeState() {
    setState(() {
      _value = _textEditingController.text.trim();
    });
  }

  @override
  bool _inputValidator() {
    return EmailValidator.validate(_value);
  }

  @override
  TextField get _textField => _constructPureTextField (
    textInputType: TextInputType.emailAddress,
    inputFormatters: [
      FilteringTextInputFormatter.singleLineFormatter,
      FilteringTextInputFormatter.allow(RegExp(_emailAllowedSymbols)),
    ],
  );
}

class DriveTextInputSubpage extends _TextBasedInputSubpage {
  const DriveTextInputSubpage({
    super.key,
    required super.hintText,
    required super.inputTitle,
    required super.onSavePressed,
    super.initialValue,
    });

  @override
  State<StatefulWidget> createState()
    =>  _TextInputSubpageState(
      onSavePressed: onSavePressed,
      hintText: hintText,
      inputName: inputTitle,
      initialValue: initialValue,
    );
}

class _TextInputSubpageState extends _TextBasedInputSubpageState<DriveTextInputSubpage> {

  _TextInputSubpageState({
    required super.inputName,
    required super.hintText,
    required super.onSavePressed,
    super.initialValue,
  });
}

abstract class _TextBasedInputSubpage extends _InputSubpageBase {
  final void Function(String) onSavePressed;
  final String? initialValue;

  const _TextBasedInputSubpage({
    super.key,
    required super.hintText,
    required super.inputTitle,
    required this.onSavePressed,
    this.initialValue,
  });
}


abstract class _TextBasedInputSubpageState<T extends StatefulWidget> extends _InputSubpageState<T> {
  final void Function(String) onSavePressed;
  @protected
  String _value = "";

  _TextBasedInputSubpageState({
    required super.inputName,
    required super.hintText,
    required this.onSavePressed,
    super.initialValue,
  });

  @override
  StatefulWidget get _textField
  => _constructPureTextField();

  @override
  bool _inputValidator()
  => _value.isNotEmpty;

  @override
  void _changeState() {
    setState(() {
      _value = _textEditingController.text;
    });
  }

  @override
  void _onSavePressedInternal() => onSavePressed(_value);
}

abstract class _InputSubpageBase extends StatefulWidget {
  final String hintText;
  final String inputTitle;

  const _InputSubpageBase({
    super.key,
    required this.hintText,
    required this.inputTitle,
  });
}

abstract class _InputSubpageState<T extends StatefulWidget> extends State<T> {
  @protected
  final TextEditingController _textEditingController = TextEditingController();

  @protected
  StatefulWidget get _textField;

  final String? hintText;
  final String inputName;
  final String? initialValue;

  _InputSubpageState({
    required this.inputName,
    required this.hintText,
    this.initialValue,
  }){
    _textEditingController.addListener(_changeState);
  }

  @protected
  TextField _constructPureTextField({
    String? initialText,
    bool readOnly = false,
    bool filled = false,
    TextStyle? style,
    TextInputType? textInputType,
    void Function()? onTap,
    List<TextInputFormatter>? inputFormatters,
  }) {

    return TextField (
      controller: _textEditingController,
      decoration: InputDecoration(
        hintText: hintText,
        hintStyle: DriveTextStyles.inputLabel,
        filled: filled,
        focusedBorder: const UnderlineInputBorder(
            borderSide: BorderSide(color: DriveColors.lightBlueColor),
        ),
        enabledBorder: const UnderlineInputBorder(
          borderSide: BorderSide(color: DriveColors.darkGreyColor),
        ),
      ),
      keyboardType: textInputType ?? TextInputType.text,
      style: style ?? DriveTextStyles.userInput,
      onTap: onTap,
      readOnly: readOnly,
      textAlign: TextAlign.center,
      inputFormatters: inputFormatters,
      cursorColor: DriveColors.lightBlueColor,
    );
  }

  @protected
  bool _inputValidator();

  @protected
  void _changeState();

  @protected
  void _onSavePressedInternal();

  @override
  void dispose() {
    _textEditingController.dispose();
    super.dispose();
  }

  @override
  void initState() {
    if (initialValue != null){
      _textEditingController.text = initialValue!;
    }

    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: DriveAppBar(title: inputName),
      body: Padding(
        padding: const EdgeInsets.all(20),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Padding(
              padding: const EdgeInsets.only(top: 60),
              child: _textField,
            ),
            BottomButton(
                title: "CОХРАНИТЬ",
                onPressed: _inputValidator() ? _onSavePressedInternal : null,
            ),
          ],
        ),
      ),
    );
  }
}
