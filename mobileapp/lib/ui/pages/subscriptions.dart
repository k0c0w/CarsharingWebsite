import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:mobileapp/domain/entities/car.dart';
import 'package:mobileapp/ui/Components/appbar.dart';
import 'package:provider/provider.dart';


enum _SelectedPage {
  activeSubscriptions,
  subscriptionHistory,
}

class _ViewModel extends ChangeNotifier {
  _SelectedPage _selectedPage = _SelectedPage.activeSubscriptions;

  _SelectedPage get selectedSubscriptionPage => _selectedPage;
  set selectedSubscriptionPage(_SelectedPage value) {
    _selectedPage = value;
    notifyListeners();
  }

  List<Car> activeCars = [Car(model: "Camry 3.5", brand: "Toyota", licensePlate: "A 720 СУ 160 рус")];
}

class SubscriptionsPageWidget extends StatelessWidget {
  const SubscriptionsPageWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return ChangeNotifierProvider(
        create: (_) => _ViewModel(),
        child: const _View()
    );
  }
}

class _View extends StatelessWidget {
  const _View();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: DriveAppBar(title: "ПОДПИСКИ"),
      body: const Column(
        mainAxisAlignment: MainAxisAlignment.start,
        children: [
          Divider(
            height: 20,
            color: Colors.transparent,
          ),
          Center(child:_SubscriptionToggleButtons()),
          _CarList(),
        ],
      ),
    );
  }
}

class _SubscriptionToggleButtons extends StatelessWidget {
  const _SubscriptionToggleButtons();

  @override
  Widget build(BuildContext context) {
    final model = context.read<_ViewModel>();
    final selectedPage = context.select((_ViewModel value) => value.selectedSubscriptionPage);

    return ToggleButtons(
      isSelected: [selectedPage == _SelectedPage.activeSubscriptions, selectedPage == _SelectedPage.subscriptionHistory],
      onPressed: (int index) => model.selectedSubscriptionPage = _SelectedPage.values[index],
      borderRadius: const BorderRadius.all(Radius.circular(10)),
      selectedColor: Colors.white,
      fillColor: const Color.fromRGBO(5, 59, 74, 1),
      color: const Color.fromRGBO(117, 124, 126, 1),
      constraints: const BoxConstraints(
        minHeight: 33.0,
        minWidth: 160.0,
      ),
      children: const [Text("АКТИВНЫЕ"), Text("ИСТОРИЯ")],
    );
  }
}

class _CarList extends StatelessWidget {
  const _CarList();

  @override
  Widget build(BuildContext context) {
     final selectedPage = context.select((_ViewModel model) => model.selectedSubscriptionPage);
     return selectedPage == _SelectedPage.activeSubscriptions
         ? const _SubscriptionList() : const _HistoryList();
  }
}

class _HistoryList extends StatelessWidget {
  const _HistoryList();

  //todo: rebuild
  @override
  Widget build(BuildContext context) {
    final cars = context.select((_ViewModel value) => value.activeCars);

    return ListView.builder(
        scrollDirection: Axis.vertical,
        shrinkWrap: true,
        itemCount: cars.length,
        itemBuilder: (context, index) {
          final car = cars[index];
          return _HistoryCard(name: "${car.brand} ${car.model}", description: car.licensePlate);
        }
    );
  }
}

class _SubscriptionList extends StatelessWidget {
  const _SubscriptionList();

  @override
  Widget build(BuildContext context) {
    final cars = context.select((_ViewModel value) => value.activeCars);

    return ListView.builder(
        scrollDirection: Axis.vertical,
        shrinkWrap: true,
        itemCount: cars.length,
        itemBuilder: (context, index) {
          final car = cars[index];
          return _SubscriptionCard(name: "${car.brand} ${car.model}", description: car.licensePlate);
        }
    );
  }
}

abstract class _CarCardBase extends StatelessWidget {
  final String name;
  final String description;

  const _CarCardBase({super.key, required this.name, required this.description});

  Widget rightCardWidget();

  @override
  Widget build(BuildContext context) {
    return Center(
        child: SizedBox(width: 400, child: Card(
          margin: const EdgeInsets.all(7.0),
          shadowColor: const Color.fromRGBO(200, 200, 200, 1),
          color: const Color.fromRGBO(255, 255, 255, 1),
          child: Row(
            mainAxisSize: MainAxisSize.max,
            crossAxisAlignment: CrossAxisAlignment.end,
            children: <Widget>[
              SizedBox(
                  width: 180,
                  height: 50,
                  child: Column(mainAxisAlignment: MainAxisAlignment.center,
                      crossAxisAlignment: CrossAxisAlignment.center,
                      children: [
                        Text(name, style: GoogleFonts.orbitron(
                            color: Colors.black,
                            fontSize: 15,
                            fontWeight: FontWeight.bold)),
                        Text(description, style: GoogleFonts.orbitron(
                            color: Colors.grey,
                            fontSize: 10,
                            fontWeight: FontWeight.bold)),
                      ])),
              rightCardWidget(),
            ],
          ),
        ),
        ));
  }
}

class _HistoryCard extends _CarCardBase {
  const _HistoryCard({required super.name, required super.description});

  @override
  Widget rightCardWidget() {
    return Container();
  }
}

class _SubscriptionCard extends _CarCardBase {
  const _SubscriptionCard({required super.name, required super.description});

  @override
  Widget rightCardWidget() => _SubscriptionCardButtons();
}

class _SubscriptionCardButtons extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Row(
        children: [
          TextButton(
            style: const ButtonStyle(foregroundColor: MaterialStatePropertyAll(Colors.black)),
            child: const ImageIcon(AssetImage("./assets/raketa.png")),
            onPressed: () {
              /* ... */
            },
          ),
          TextButton(
            style: const ButtonStyle(foregroundColor: MaterialStatePropertyAll(Colors.black)),
            child:  const ImageIcon(AssetImage("./assets/trooba.png")),
            onPressed: () {
              /* ... */
            },
          ),
          TextButton(
            style: const ButtonStyle(foregroundColor: MaterialStatePropertyAll(Colors.black)),
            child: const ImageIcon(AssetImage("./assets/key.png")),
            onPressed: () {
              /* ... */
            },
          ),
    ]);
  }
}