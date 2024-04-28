
import 'package:mobileapp/domain/entities/booked_car/booked_car.dart';
import 'package:mobileapp/domain/use_cases/base.dart';

class RetrieveSubscriptionsUseCase extends UseCase<List<BookedCar>>{

  Future<List<BookedCar>> call() async {


    throw Error();
  }
}

class ChangeCarOpenStateUseCase extends UseCase<BookedCar> {
  Future<BookedCar> call() async {
    throw Error();
  }
}

