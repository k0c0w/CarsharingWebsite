import 'package:flutter/material.dart';
import 'package:mobileapp/domain/entities/booked_car/booked_car.dart';
import 'package:mobileapp/ui/components/car_card.dart';

class SubscriptionPageHistoryList extends StatelessWidget {
  const SubscriptionPageHistoryList({super.key});

  @override
  Widget build(BuildContext context) {
    final cars = [const BookedCar(
        id: 2,
        name: "Land Cruiser",
        licensePlate: "А 712 СУ 116 РУС",
        isOpened: false)
    ];

    return ListView.builder(
        scrollDirection: Axis.vertical,
        shrinkWrap: true,
        itemCount: cars.length,
        itemBuilder: (context, index) {
          final car = cars[index];
          return HistoryCard(
              name: car.name,
              description: car.licensePlate
          );
        }
    );
  }
}