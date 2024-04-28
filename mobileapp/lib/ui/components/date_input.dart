import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:mobileapp/utils/date_formatter.dart';

class _DateFromInputState extends State<DateFromInput> {
  final TextEditingController _textEditingController = TextEditingController();
  final DateTime firstDate;
  final DateTime lastDate;
  final DateTime? initialDate;
  final String labelText;
  final void Function(DateTime)? afterDateSet;
  final String? Function(String?) validator;

  _DateFromInputState({
    required this.firstDate,
    required this.lastDate,
    required this.labelText,
    required this.validator,
    this.afterDateSet,
    this.initialDate,
  });

  Future<DateTime?> _selectDate(BuildContext context)  {
    return showDatePicker(
        context: context,
        firstDate: firstDate,
        lastDate: lastDate);
  }

  Future<void> _onTap(BuildContext context) async {
    FocusScope.of(context).requestFocus(FocusNode());
    final date = await _selectDate(context);
    if (date == null){
      return;
    }

    final dateString = DateTimeFormat.toStringFormatter.format(date);
    _textEditingController.text = dateString;

    if (afterDateSet != null) {
      afterDateSet!(date);
    }
  }

  final textStyle = GoogleFonts.openSans(
      textStyle: const TextStyle(
        color: Colors.white,
        fontWeight: FontWeight.w500,
      )
  );
  @override
  Widget build(BuildContext context) {
    return TextFormField(
        textAlign: TextAlign.center,
        showCursor: false,

        decoration: InputDecoration(
          hintText: labelText,
          hintStyle: textStyle,
          border: const UnderlineInputBorder(borderSide: BorderSide.none),
          errorMaxLines: 1,
        ),

        controller: _textEditingController,
        onTap: () => _onTap(context),
        validator: validator,
        initialValue: initialDate == null ? null : DateTimeFormat.toStringFormatter.format(initialDate!),
        style: textStyle,
    );
  }
}

class DateFromInput extends StatefulWidget {
  final String labelText;
  final DateTime firstDate;
  final DateTime lastDate;
  final DateTime? initialDate;
  final String? Function(String?) validator;
  final void Function(DateTime)? afterDateTimeSet;

  const DateFromInput({
    required this.labelText,
    required this.firstDate,
    required this.lastDate,
    required this.validator,
    this.afterDateTimeSet,
    this.initialDate,
  });

  @override
  State<StatefulWidget> createState() =>
      _DateFromInputState(
          firstDate: firstDate,
          lastDate: lastDate,
          labelText: labelText,
          initialDate: initialDate,
          validator: validator,
          afterDateSet: afterDateTimeSet,
      );
}
