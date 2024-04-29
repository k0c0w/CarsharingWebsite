import 'package:mobileapp/domain/results.dart';
import 'package:mobileapp/domain/use_cases/base.dart';

class PaymentUseCase extends UseCase<double> {

  Future<Result<bool>> call(double money) async {

    await Future.delayed(Duration(seconds: 5));
    return  Ok(true);
  }
}