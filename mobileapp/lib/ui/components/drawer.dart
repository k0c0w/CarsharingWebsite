import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobileapp/ui/components/styles.dart';
import 'package:mobileapp/ui/pages/pages_list.dart';

const _drawerLeftGapProportion = 0.065;

class DriveDrawer extends StatelessWidget {
  const DriveDrawer({super.key});

  @override
  Widget build(BuildContext context) {
    return Drawer(
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(0)),
      child: Container(
        padding: const EdgeInsets.symmetric(horizontal: 3),
        child: Column(
          mainAxisSize: MainAxisSize.max,
          children:
          [
            /// to profile
            _DrawerLeadItem(fio: "Якупов Артур Булатович",),
            /// list of other pages
            _DrawerPageList(),
            const Spacer(),
            /// Drive label
            Container(
              alignment: Alignment.bottomLeft,
              padding: EdgeInsets.only(left: 5, bottom: 5),
              child: const Text(
                "Drive",
                style: TextStyle(
                    color: DriveColors.deepBlueColor,
                    fontSize: 27,
                    fontWeight: FontWeight.w800,
                    fontFamily: "Orbitron",
                    letterSpacing: 5
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}

class _DrawerPageList extends StatelessWidget {

  void Function() createNavigateToRoute(BuildContext context, String route) {
    return () {
      Navigator.pushNamed(context, route);
    };
  }

  @override
  Widget build(BuildContext context) {
    var screenSize = MediaQuery.of(context).size;

    return Container(
      margin: EdgeInsets.only(top: screenSize.height * 0.09125),
      child: ListBody(
        mainAxis: Axis.vertical,
        children: [
          /// Subs
          _DrawerListItem(
            title: "ПОДПИСКИ",
            onTap: createNavigateToRoute(context, DriveRoutes.userSubscriptions),
          ),
          /// Support
          _DrawerListItem(
            title: "ПОДДЕРЖКА",
            onTap: createNavigateToRoute(context, DriveRoutes.support),
          ),
          /// Payment
          _DrawerListItem(
            title: "ОПЛАТА",
            onTap: createNavigateToRoute(context, DriveRoutes.payment),
          ),
        ],
      ),
    );
  }
}

class _DrawerListItem extends StatelessWidget {
  final String title;
  final void Function() onTap;

  const _DrawerListItem({
    required this.title,
    required this.onTap,
  });

  @override
  Widget build(BuildContext context) {
    var screenSize = MediaQuery.of(context).size;

    return
      Container(
      margin: const EdgeInsets.only(bottom: 5),
      child: InkWell(
        customBorder: RoundedRectangleBorder(borderRadius: BorderRadius.circular(3)),
        onTap: onTap,
        child: Container(
          height:  screenSize.height * 0.05075,
          padding: EdgeInsets.only(left: screenSize.width * _drawerLeftGapProportion),
          alignment: Alignment.centerLeft,
          child: Text(
            title,
            style: const  TextStyle(
              color: DriveColors.blackColor,
              letterSpacing: 2,
              fontWeight: FontWeight.w600,
              fontFamily: 'Open Sans',
              fontSize: 13,
              overflow: TextOverflow.clip,
            ),),
        ),
      ),
    );

  }
}

class _DrawerLeadItem extends StatelessWidget {
  final String fio;

  const _DrawerLeadItem({required this.fio});

  @override
  Widget build(BuildContext context) {
    var screenSize = MediaQuery.of(context).size;
    return InkWell(
        customBorder: RoundedRectangleBorder(borderRadius: BorderRadius.circular(5)),
        onTap: () {
          Navigator.pushNamed(context, DriveRoutes.profile);
        },
        child: Container(
          height: screenSize.height * 0.235,
          padding: EdgeInsets.only(
              left: screenSize.width * _drawerLeftGapProportion,
              bottom: screenSize.height * 0.08),
          width: 1000,
          alignment: Alignment.bottomLeft,
          child: Column(
            mainAxisAlignment: MainAxisAlignment.end,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
              fio.toUpperCase(),
              style: const TextStyle(
                  color: DriveColors.blackColor,
                  letterSpacing: 2,
                  fontWeight: FontWeight.w600,
                  fontFamily: 'Open Sans',
                  fontSize: 15,
                  overflow: TextOverflow.clip,
                ),
              ),
              const Padding(
                padding: EdgeInsets.only(top: 7),
                child: Text(
                  "Аккаунт не подтвержден",
                  style:  TextStyle(
                    color: DriveColors.darkGreyColor,
                    fontWeight: FontWeight.w400,
                    fontFamily: 'Open Sans',
                    fontSize: 13,
                    overflow: TextOverflow.clip,
                  ),
                ),
              )
            ],
          ),
        ),
      );
  }
}
