import 'package:email_validator/email_validator.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:intl/intl.dart';
import 'package:intl_phone_number_input/intl_phone_number_input.dart';
import 'package:mobileapp/ui/components/appbar.dart';
import 'package:mobileapp/ui/components/bottom_button.dart';
import 'package:mobileapp/ui/components/styles.dart';

class DriveDateInputSubpage extends _InputSubpageBase {
  final void Function(DateTime) onSavePressed;
  final String? initialValue;
  final DateTime? firstDate;
  final DateTime? lastDate;

  const DriveDateInputSubpage({
    super.key,
    required super.hintText,
    required super.inputTitle,
    required this.onSavePressed,
    this.initialValue,
    this.firstDate,
    this.lastDate,
  });

  @override
  State<StatefulWidget> createState() {
    return _DriveDateInputSubpageState(
      inputName: inputTitle,
      hintText: hintText,
      onSavePressed: onSavePressed,
      initialDate: initialValue == null ? null : DateFormat("dd.MM.yyyy").tryParse(initialValue!),
      firstDate: firstDate,
      lastDate: lastDate,
    );
  }
}

class _DriveDateInputSubpageState extends _InputSubpageState<DriveDateInputSubpage> {
  static const int _centuryInDays = 100 * 365;
  final void Function(DateTime) onSavePressed;
  final DateTime? initialDate;
  var _firstDate = DateTime.fromMillisecondsSinceEpoch(0);
  var _lastDate = DateTime.fromMillisecondsSinceEpoch(0)
      .add(const Duration(days: _centuryInDays));
  
  DateTime? _pickedDate;

  _DriveDateInputSubpageState({
    required super.inputName,
    required super.hintText,
    required this.onSavePressed,
    this.initialDate,
    DateTime? firstDate,
    DateTime? lastDate,
  }):super(initialValue: initialDate?.toString().split(" ")[0]) {
    if (firstDate != null) {
      _firstDate = firstDate;
      _lastDate = firstDate.add(const Duration(days: _centuryInDays));
    }

    if (lastDate != null) {
      _lastDate = lastDate;
    }

    if (_firstDate.compareTo(_lastDate) > 0) {
      throw ArgumentError("Last date can not be less than first date.");
    }
  }

  @override
  void _changeState() {
    ///this state can only be modified in _selectDate
  }

  @override
  bool _inputValidator() {
    var pickedDate = _pickedDate;

    return pickedDate != null
        && (pickedDate.compareTo(_lastDate) <= 0)
        && (pickedDate.compareTo(_firstDate) >= 0);
  }

  @override
  void _onSavePressedInternal() {
    var pickedDate = _pickedDate;
    if (pickedDate != null) {
      onSavePressed(pickedDate);
    }
  }

  @override
  StatefulWidget get _textField => _constructPureTextField(
    filled: true,
    prefixIcon: const Icon(Icons.calendar_today),
    readOnly: true,
    onTap: () {_selectDate();},
  );

  Future<void> _selectDate() async {
    var pickedDate = await showDatePicker(
        context: context,
        initialDate: initialDate ?? DateTime.now(),
        firstDate: _firstDate,
        lastDate: _lastDate,
    );

    if (pickedDate == null) {
      return;
    }

    setState(() {
      _textEditingController.text = pickedDate.toString().split(" ")[0];
      _pickedDate = pickedDate;
    });
  }
}

class DrivePhoneNumberInputSubpage extends _TextBasedInputSubpage {
  const DrivePhoneNumberInputSubpage({
    super.key,
    required super.hintText,
    required super.inputTitle,
    required super.onSavePressed,
    super.initialValue,
  });

  @override
  State<StatefulWidget> createState()
    => _DrivePhoneNumberInputSubpageState(
      hintText: hintText,
      inputName: inputTitle,
      onSavePressed: onSavePressed,
      initialValue: initialValue,
    );
}

class _DrivePhoneNumberInputSubpageState extends _TextBasedInputSubpageState<DrivePhoneNumberInputSubpage> {
  static final RegExp _validNumberFormat = RegExp(r"^\+7\d{10}$");
  
  var _phoneNumber = PhoneNumber();

  _DrivePhoneNumberInputSubpageState({
    required super.inputName,
    required super.hintText,
    required super.onSavePressed,
    super.initialValue,
  });

  bool _isValidPhoneNumber(PhoneNumber phoneNumber) {
    var phoneNumberStr = phoneNumber.phoneNumber;
    return phoneNumberStr != null && _validNumberFormat.hasMatch(phoneNumberStr);;
  }

  void _onInputChanged(PhoneNumber phoneNumber) {
    if (_phoneNumber != phoneNumber) {
    setState(() {
      _phoneNumber = phoneNumber;
    });
    }
  }

  @override
  void _changeState() {
    /// the state is changed in _onInputChanged
  }

  @override
  bool _inputValidator() => _isValidPhoneNumber(_phoneNumber);

  @override
  void _onSavePressedInternal() {
    var phoneNumber = _phoneNumber;
    if (_isValidPhoneNumber(phoneNumber)) {
      onSavePressed(phoneNumber.phoneNumber!);
    }
  }

  @override
  void initState() {
    tryInitState();
  }

  void tryInitState() async {
    if (initialValue != null) {
      try{
        var pn = await PhoneNumber.getRegionInfoFromPhoneNumber(initialValue!);
        setState(() {
          _textEditingController.text = pn.parseNumber();
          _phoneNumber = pn;
        });
      }
      catch(e) {
      }
    }
  }

  @override
  StatefulWidget get _textField => InternationalPhoneNumberInput(
    initialValue: _phoneNumber,
    /// formatted russian number length
    maxLength: 13,
    onInputChanged: _onInputChanged,
    selectorTextStyle: DriveTextStyles.userInput,
    textStyle: DriveTextStyles.userInput,
    inputDecoration: InputDecoration(
      hintStyle: DriveTextStyles.inputLabel,
      hintText: hintText,
      focusedBorder: const UnderlineInputBorder(
        borderSide: BorderSide(color: DriveColors.lightBlueColor),
      ),
      enabledBorder: const UnderlineInputBorder(
        borderSide: BorderSide(color: DriveColors.darkGreyColor),
      ),
    ),
    ignoreBlank: false,
    countries: const [
      /// Russia
      "RU"
    ],
    selectorConfig: const SelectorConfig(
      setSelectorButtonAsPrefixIcon: true,
      showFlags: false,
      selectorType: PhoneInputSelectorType.DROPDOWN,
    ),
    cursorColor: DriveColors.lightBlueColor,
  );
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
    super.key,
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

  final String hintText;
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
    Icon? prefixIcon,
  }) {

    return TextField (
      controller: _textEditingController,
      decoration: InputDecoration(
        hintText: hintText,
        hintStyle: DriveTextStyles.inputLabel,
        filled: filled,
        prefixIcon: prefixIcon,
        focusedBorder: const UnderlineInputBorder(
            borderSide: BorderSide(color: DriveColors.lightBlueColor),
        ),
        enabledBorder: const UnderlineInputBorder(
          borderSide: BorderSide(color: DriveColors.darkGreyColor),
        ),
        fillColor: Colors.white10,
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
                onPressed: _inputValidator() ? () {
                  FocusScope.of(context).unfocus();
                  _onSavePressedInternal();
                }
                : null,
            ),
          ],
        ),
      ),
    );
  }
}
