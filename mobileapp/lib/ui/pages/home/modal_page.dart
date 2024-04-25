import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:mobileapp/ui/Components/styles.dart';
import 'package:mobileapp/ui/components/bottom_button.dart';
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

class _RentDateForm extends StatelessWidget {
  const _RentDateForm();

  Widget _createElevatedButton(void Function() function, Color background, Size size, String text) {
    return ElevatedButton(
      onPressed: function,
      style: ElevatedButton.styleFrom(
        fixedSize: size,
        alignment: Alignment.center,
        backgroundColor: background,
      ),
      child: Text(
        text,
        textAlign: TextAlign.center,
        style: GoogleFonts.openSans(
                  textStyle: const TextStyle(
                  overflow: TextOverflow.clip,
                  color: Colors.white,
                  letterSpacing: 5,
                  fontSize: 15,
                  fontWeight: FontWeight.w500,
                ),
            )
        )
    );
  }

  void onStartButtonPressed(context) {
    showDatePicker(context: context, firstDate: DateTime(2014), lastDate: DateTime(2014));
  }

  void onEndButtonPressed(context) {
    showDatePicker(context: context, firstDate: DateTime(2014), lastDate: DateTime(2014));
  }

  @override
  Widget build(BuildContext context) {
    final size = MediaQuery.of(context).size;
    final buttonSize = Size(size.width * 0.4027, size.height * 0.0675);
    return Row(
      mainAxisAlignment: MainAxisAlignment.spaceBetween,
      children: [
        _createElevatedButton(
            () => onStartButtonPressed(context),
            DriveColors.darkBlueColor,
            buttonSize,
            "Начало аренды"
        ),
        _createElevatedButton(
            () => onEndButtonPressed(context),
            DriveColors.deepBlueColor,
            buttonSize,
            "Конец аренды"
        ),
      ],
    );
  }
}

class HomePageRentModalWidget extends StatelessWidget {
  const HomePageRentModalWidget({super.key});

  @override
  Widget build(BuildContext context) {
    final size = MediaQuery.of(context).size;

    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 30),
      child: const Column(
        children: [
          _RentTitle(),
          _RentCarDescription(),
          _RentDateForm(),
          BottomButton(title: "АРЕНДОВАТЬ"),
        ],
      ),
    );
  }
}

class HomePage extends StatelessWidget {
  const HomePage({super.key});
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: ElevatedButton(
        child: const Center(child: Text("Dora"),),
        onPressed: () {
          showModalBottomSheet(
            context: context,
            builder: const HomePageRentModalWidget().build,
          );
        },
      ),
    );
  }
}
