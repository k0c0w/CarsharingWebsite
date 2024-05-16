import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobileapp/bloc/pages/home_page/bloc.dart';
import 'package:mobileapp/bloc/pages/home_page/events.dart';
import 'package:mobileapp/bloc/pages/home_page/state.dart';
import 'package:mobileapp/domain/entities/tariff/tariff.dart';
import 'package:mobileapp/ui/components/styles.dart';
import 'package:mobileapp/ui/pages/pages_list.dart';

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

  const _Menu({required this.openDrawer});

  @override
  Widget build(BuildContext context) {
    return ButtonBar(
      alignment: MainAxisAlignment.spaceBetween,
      children: [
        _ButtonMenu(onClick:openDrawer, iconData: Icons.menu,),
        _TariffList(),
        _ButtonMenu(
          onClick:() => Navigator.of(context).pushNamed(DriveRoutes.support),
          iconData: Icons.chat_bubble_outline_outlined,),
      ],
    );
  }
}

class _TariffList extends StatelessWidget {

  List<DropdownMenuItem> _mapToItems(List<Tariff> tariffs)
  => tariffs
      .asMap()
      .map((int i, Tariff tariff) => MapEntry(
  i,
  DropdownMenuItem(
  value: i,
  child: Text(
  tariff.name.toUpperCase(),
  textAlign: TextAlign.end,
  )
  )
  ))
      .values
      .toList();

  @override
  Widget build(BuildContext context) {
    return BlocBuilder<HomePageBloc, HomePageBlocState>(
        builder: (ctx, st) {
          final bloc = context.read<HomePageBloc>();
          late final int? selectedTariffIndex;
          late final List<DropdownMenuItem> tariffsMenuItems;
          if (st is HomePageBlocLoadedState) {
            selectedTariffIndex = st.selectedTariffIndex;
            tariffsMenuItems = _mapToItems(st.tariffs);
          } else {
            selectedTariffIndex = null;
            tariffsMenuItems = [];
          }

          return Container(
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
                      items: tariffsMenuItems,
                      value: selectedTariffIndex,
                      borderRadius: BorderRadius.circular(10),
                      onChanged: (value) => bloc.add(HomePageBlocEvent.selectAnotherTariff(value!)),
                      alignment: AlignmentDirectional.center,
                    ),
                  )
              )
          );
        }
    );
  }
}

class _ButtonMenu extends StatelessWidget {
  final void Function() onClick;
  final IconData iconData;
  const _ButtonMenu({required this.onClick, required this.iconData});

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      width: 48,
      height: 48,
      child: RawMaterialButton(
        onPressed: onClick,
        elevation: 2.0,
        fillColor: Colors.white,
        padding: const EdgeInsets.all(10.0),
        shape: const CircleBorder(),
        child: Icon(iconData, size: 24.0, color: DriveColors.darkBlueColor,),
      ),
    );
  }
}
