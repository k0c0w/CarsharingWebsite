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
      title:_Menu(openDrawer: openDrawer,),
  );
}

class _Menu extends StatelessWidget {
  final void Function() openDrawer;
  _Menu({required this.openDrawer});

  final List<DropdownMenuItem> dropdownMenu = [DropdownMenuItem(child:Text("TRAVEL"), value: 0,)];

  @override
  Widget build(BuildContext context) {
    return ButtonBar(
      alignment: MainAxisAlignment.spaceBetween,
      children: [
        // open drawer button
        SizedBox(
          width: 48,
          height: 48,
          child: ElevatedButton(onPressed: openDrawer, child: Icon(Icons.abc)),
        ),

        // select tariff
        Container(
          color: Colors.white,
          width: 150,
          height: 48,
          child: DropdownButtonHideUnderline(
            child: DropdownButton(
              iconSize: 0.0,
              items: dropdownMenu,
              value: 0,
              onChanged: (value) {
              },
              alignment: Alignment.centerRight,
            ),
          )
        ),

        // open chat
        SizedBox(
          width: 48,
          height: 48,
          child: ElevatedButton(onPressed: () {}, child: const Icon(Icons.chat_bubble_outline)),
        ),
      ],
    );
  }
}