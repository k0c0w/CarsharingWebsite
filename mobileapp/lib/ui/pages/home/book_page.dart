import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:mobileapp/bloc/pages/home_page/bloc.dart';
import 'package:mobileapp/bloc/pages/home_page/events.dart';
import 'package:mobileapp/bloc/pages/home_page/state.dart';
import 'package:mobileapp/domain/entities/car/car.dart';
import 'package:mobileapp/domain/entities/car_model/car_model.dart';
import 'package:mobileapp/domain/entities/tariff/tariff.dart';
import 'package:mobileapp/ui/Components/styles.dart';
import 'package:mobileapp/ui/components/bottom_button.dart';
import 'package:mobileapp/ui/components/date_input.dart';
import 'package:provider/provider.dart';
import 'package:transparent_image/transparent_image.dart';

class _RentTitle extends StatelessWidget {
  final Tariff tariff;
  const _RentTitle({required this.tariff});

  @override
  Widget build(BuildContext context) {
    return Container(
      margin: const EdgeInsets.symmetric(vertical: 20),
      child: Text(
        tariff.name.toUpperCase(),
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
  final String brandTitle;
  final String modelTitle;
  _RentCarDescriptionCarName({required this.brandTitle, required this.modelTitle});

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
        Text(brandTitle, style: _style,),
        Text(modelTitle, style: _style,)
      ],
    );
  }
}

class _RentCarDescriptionImage extends StatelessWidget {
  final String url;
  const _RentCarDescriptionImage({required this.url});

  @override
  Widget build(BuildContext context) {
    final size = MediaQuery.of(context).size;
    return SizedBox(
      height: size.height * 0.08875,
      width: size.width * 0.572,
      child: FadeInImage.memoryNetwork(
          placeholder: kTransparentImage,
          image: url),
    );
  }
}

class _RentCarDescription extends StatelessWidget {
  final CarModel carModel;
  const _RentCarDescription({required this.carModel});

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        Row(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            _RentCarDescriptionCarName(
              brandTitle: carModel.brand,
              modelTitle: carModel.model,
            ),
            _RentCarDescriptionImage(url: carModel.url),
          ],
        ),
        Container(
          margin: const EdgeInsets.only(top: 20, bottom: 20),
          child: Text(
            carModel.description,
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
  final DateTime minDate;
  final DateTime maxDate;
  final _formKey = GlobalKey<FormState>();
  DateTime? _firstDate;
  DateTime? _secondDate;

  _RentDateFormState({required this.minDate, required this.maxDate});

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
      Navigator.of(context).pop();
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
                        firstDate: minDate,
                        lastDate: maxDate,
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
                        firstDate: minDate,
                        lastDate: maxDate,
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
  final DateTime minDate;
  final DateTime maxDate;
  const _RentDateForm({required this.maxDate, required this.minDate});

  @override
  State<StatefulWidget> createState()
  => _RentDateFormState(maxDate: maxDate, minDate: minDate);
}

class _View extends StatelessWidget {
  final Car car;
  final Tariff tariff;
  const _View({required this.car, required this.tariff});

  @override
  Widget build(BuildContext context) {
    final minDate = DateTime.now();
    final maxDate = minDate.add(Duration(minutes: tariff.maxBookMinutes.toInt()));
    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 30),
      child: Column(
        children: [
          _RentTitle(tariff: tariff,),
          _RentCarDescription(carModel: car.model,),
          _RentDateForm(minDate: minDate, maxDate: maxDate,),
        ],
      ),
    );
  }
}

class HomePageCarBookingWidget extends StatelessWidget {
  final Car car;
  final Tariff tariff;
  final HomePageBloc injectableBloc;
  const HomePageCarBookingWidget({
    super.key,
    required this.injectableBloc,
    required this.car,
    required this.tariff,
  });

  @override
  Widget build(BuildContext context) {
    return Provider(
      create: (_) => injectableBloc,
      lazy: false,
      child: _View(car: car, tariff: tariff),
    );
  }
}
