import 'package:flutter/material.dart';
import 'package:mobileapp/ui/components/styles.dart';
import 'package:transparent_image/transparent_image.dart';

const _ellipticalRadius = Radius.elliptical(50, 20);

class DriveAppBar extends AppBar {
  DriveAppBar({super.key, required String title})
      : super(
          title: Text(
            title.toUpperCase(),
            style: DriveTextStyles.appBarTitle,
          ),
          centerTitle: true,
          leading: const BackButton(),
          backgroundColor: DriveColors.lightBlueColor,
          foregroundColor: Colors.white,
          shape: const RoundedRectangleBorder(
            borderRadius: BorderRadius.only(
              bottomLeft: _ellipticalRadius,
              bottomRight: _ellipticalRadius
            )
          )
  );
}

class DriveHomePageAppBar extends AppBar {
  final void Function() openDrawer;
  DriveHomePageAppBar({required this.openDrawer, super.key})
      : super(
      centerTitle: true,
      automaticallyImplyLeading: false,
      backgroundColor: Colors.transparent,
      foregroundColor: Colors.white,
      title:Menu(openDrawer: openDrawer,),
  );
}

class Menu extends StatefulWidget {
  final void Function() openDrawer;
  Menu({required this.openDrawer});

  @override
  _Menu createState() => _Menu();

}

class _Menu extends State<Menu> {
  var selectedDropDownItemValue = 0;

  final List<DropdownMenuItem> dropdownMenu = [
    DropdownMenuItem(child:Text("TRAVEL", textAlign: TextAlign.center,), value: 0,),
    DropdownMenuItem(child:Text("ITEM 1", textAlign: TextAlign.center,), value: 1,)
  ];

  onChange (value) =>
    setState((){
      selectedDropDownItemValue = value;
    });

  @override
  Widget build(BuildContext context) {
    return ButtonBar(
      alignment: MainAxisAlignment.spaceBetween,
      children: [
        SizedBox(
          width: 48,
          height: 48,
          child: RawMaterialButton(
            onPressed: widget.openDrawer,
            elevation: 2.0,
            fillColor: Colors.white,
            padding: const EdgeInsets.all(10.0),
            shape: const CircleBorder(),
            child: const Icon(Icons.menu, size: 24.0, color: DriveColors.darkBlueColor,),
          ),
        ),

        // select tariff
        Container(
          decoration:  BoxDecoration(
            borderRadius: BorderRadius.circular(10),
            color: Colors.white,),
          width: 150,
          padding: const EdgeInsets.all(10.0),
          height: 48,
          child: DropdownButtonHideUnderline(
            child: Center(
              child: DropdownButton(
                iconSize: 0.0,
                isExpanded: true, // не знаю как сделать посередине
                items: dropdownMenu,
                value: selectedDropDownItemValue,
                borderRadius: BorderRadius.circular(10),
                onChanged: onChange,
                alignment: AlignmentDirectional.center,
            ),)
          )
        ),

        SizedBox(
          width: 48,
          height: 48,
          child: RawMaterialButton(
            onPressed: () {},
            elevation: 2.0,
            fillColor: Colors.white,
            padding: const EdgeInsets.all(10.0),
            shape: const CircleBorder(),
            child: const Icon(Icons.chat_bubble_outline_outlined, size: 24.0, color: DriveColors.darkBlueColor,),
          ),
        )
      ],
    );
  }
}