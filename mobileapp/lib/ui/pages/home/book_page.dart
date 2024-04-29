import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:mobileapp/bloc/pages/home_page/bloc.dart';
import 'package:mobileapp/bloc/pages/home_page/events.dart';
import 'package:mobileapp/bloc/pages/home_page/state.dart';
import 'package:mobileapp/ui/Components/styles.dart';
import 'package:mobileapp/ui/components/bottom_button.dart';
import 'package:mobileapp/ui/components/confirmation_container.dart';
import 'package:mobileapp/ui/components/date_input.dart';
import 'package:provider/provider.dart';
import 'package:transparent_image/transparent_image.dart';

class _RentTitle extends StatelessWidget {
  const _RentTitle();

  @override
  Widget build(BuildContext context) {
    return Container(
      margin: const EdgeInsets.symmetric(vertical: 20),
      child: Text(
        "TRAVEL",
        style: GoogleFonts.openSans(
          textStyle: const TextStyle(
            fontSize: 25,
            color: DriveColors.darkBlueColor,
            fontWeight: FontWeight.w500,
            letterSpacing: 5,
          ),
        ),
      ),
    );
  }
}

class _RentCarDescriptionCarName extends StatelessWidget {
  _RentCarDescriptionCarName();

  final TextStyle _style = GoogleFonts.openSans(
    textStyle: const TextStyle(
      fontSize: 15,
      fontWeight: FontWeight.w500,
      color: Colors.black87,
      letterSpacing: 5,
    )
  );

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        Text("Toyota", style: _style,),
        Text("Crown", style: _style,)
      ],
    );
  }
}

class _RentCarDescriptionImage extends StatelessWidget {
  const _RentCarDescriptionImage();

  @override
  Widget build(BuildContext context) {
    final size = MediaQuery.of(context).size;
    return SizedBox(
      height: size.height * 0.08875,
      width: size.width * 0.572,
      child: FadeInImage.memoryNetwork(
          placeholder: kTransparentImage,
          image: "https://www.agscenter.ru/upload/resize_cache/iblock/68e/352_300_1/TOYOTA%20Crown.png"),
    );
  }
}

class _RentCarDescription extends StatelessWidget {
  const _RentCarDescription();

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        Row(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            _RentCarDescriptionCarName(),
            const _RentCarDescriptionImage(),
          ],
        ),
        Container(
          margin: const EdgeInsets.only(top: 20, bottom: 20),
          child: Text(
            "АКПП, люкс автомобиль на 4 человека. Новый саолн и мультимедиа, детские кресла.",
            style: GoogleFonts.openSans(
                textStyle: const TextStyle(
                  letterSpacing: 5,
                  fontSize: 15,
                  fontWeight: FontWeight.w500,
                  color: DriveColors.lightGreyColor,
                  overflow: TextOverflow.fade,
                )
            ),
          ),
        )
      ],
    );
  }
}

class _RentDateFormState extends State<_RentDateForm> {
  final _formKey = GlobalKey<FormState>();
  DateTime? _firstDate;
  DateTime? _secondDate;

  _RentDateFormState();

  String? _dateInputValidator(String? value) {
    if (value == null || value.isEmpty) {
      return  "Поле обязательно";
    }

    return null;
  }


  Future<void> _onPressed(BuildContext context) async {
    final firstDateIsLessThanSecond = _firstDate != null && _secondDate != null
        && _firstDate!.compareTo(_secondDate!) <= 0;

    if (_formKey.currentState!.validate() && firstDateIsLessThanSecond) {
      final bloc = context.read<HomePageBloc>();
      bloc.add(HomePageBlocEvent.tryBook(_firstDate!, _secondDate!));
    }
  }

  @override
  Widget build(BuildContext context) {
    final size = MediaQuery.of(context).size;
    final buttonSize = Size(size.width * 0.4027, size.height * 0.0675);
    final isTryingToRentCar = context.read<HomePageBloc>().state is HomePageBlocRentingState;
    final isButtonEnabled = !isTryingToRentCar && _firstDate != null && _secondDate != null;

    return Form(
        key: _formKey,
        child: Expanded(
          child: Column(
            children: [
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Container(
                      decoration: const BoxDecoration(
                        color: DriveColors.darkBlueColor,
                        border: Border.fromBorderSide(BorderSide.none),
                        borderRadius: BorderRadius.all(Radius.circular(25)),
                      ),
                      height: buttonSize.height,
                      width: buttonSize.width,
                      child: DateFromInput(
                        labelText: "Начало аренды",
                        firstDate: DateTime(2014),
                        lastDate: DateTime(2034),
                        initialDate: _firstDate,
                        validator: _dateInputValidator,
                        afterDateTimeSet: (date) => setState(() {
                          _firstDate = date;
                        }),
                      )
                  ),
                  Container(
                      decoration: const BoxDecoration(
                        color: DriveColors.deepBlueColor,
                        border: Border.fromBorderSide(BorderSide.none),
                        borderRadius: BorderRadius.all(Radius.circular(25)),
                      ),
                      height: buttonSize.height,
                      width: buttonSize.width,
                      child: DateFromInput(
                        labelText: "Конец аренды",
                        firstDate: DateTime(2014),
                        lastDate: DateTime(2034),
                        initialDate: _secondDate,
                        validator: _dateInputValidator,
                        afterDateTimeSet: (date) => setState(() {
                          _secondDate = date;
                        }),
                      ),
                  ),
                ],
              ),
              BottomButton(
                title: "АРЕНДОВАТЬ",
                onPressed: isButtonEnabled ? () => _onPressed(context) : null,
              ),
            ],
          ),
        )
    );
  }
}

class _RentDateForm extends StatefulWidget {
  const _RentDateForm();

  @override
  State<StatefulWidget> createState() => _RentDateFormState();
}

class _View extends StatelessWidget {

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 30),
      child: const Column(
        children: [
          _RentTitle(),
          _RentCarDescription(),
          _RentDateForm(),
        ],
      ),
    );
  }
}


class HomePageCarBookingWidget extends StatelessWidget {
  final HomePageBloc injectableBloc;
  const HomePageCarBookingWidget({super.key, required this.injectableBloc});

  @override
  Widget build(BuildContext context) {
    return Provider(
      create: (_) => injectableBloc,
      lazy: false,
      child: _View(),
    );
  }
}
