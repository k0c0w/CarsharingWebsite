import 'dart:async';
import 'package:flutter/widgets.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobileapp/bloc/pages/register/state.dart';
import 'package:mobileapp/domain/results.dart';
import 'package:mobileapp/domain/use_cases/auth_cases.dart';
import 'package:mobileapp/ui/pages/pages_list.dart';
import 'package:mobileapp/utils/date_formatter.dart';

class ProfilePageCubit extends Cubit<ProfilePageCubitState> {
  final BuildContext buildContext;

  ProfilePageCubit(this.buildContext)
      : super(const ProfilePageCubitState(
      password: "",
      birthDate: null,
      email: "",
      name: "",
      secondName: "",
      error: "",
      confirmPassword: "",
      requestSent: false
  ));

  void _popOfPage() => Navigator.of(buildContext).pop();

  void changeEmail(String email) {
    emit(state.copyWith(email: email.trim()));
  }

  void changeName(String name) {
    emit(state.copyWith(name: name.trim()));
  }

  void changeSecondName(String secondName) {
    emit(state.copyWith(secondName: secondName.trim()));
  }

  void changeDate(String dateTime) {
    var error = "Введите дату в формате ${DateTimeFormat.toStringFormatter.pattern}";
    final maybeDate = DateTimeFormat.toStringFormatter.tryParse(dateTime);

    if (maybeDate != null && (state.error.isEmpty || state.error == error)) {
      error = "";
    }

    emit(state.copyWith(birthDate: maybeDate ?? state.birthDate, error: error));
  }

  void changePassword(String password) {
    emit(state.copyWith(password: password.trim()));
  }

  void changePasswordRepetition(String password) {
    emit(state.copyWith(confirmPassword: password.trim()));
  }

  Future<void> onRegisterPressed() async {
    if (state.birthDate == null) {
      emit(state.copyWith(error: "Дата рождения не указана"));
      return;
    }

    final age = DateTime.now().year - state.birthDate!.year;
    if (age < 18) {
      emit(state.copyWith(error: "Вам должно быть 18 или более лет."));
      return;
    }

    if (state.name.isEmpty|| state.secondName.isEmpty) {
      emit(state.copyWith(error: "Имя и фамилия обязательны для заполнения."));
      return;
    }

    if (state.password.isEmpty|| state.confirmPassword.isEmpty) {
      emit(state.copyWith(error: "Введите и подтвердите пароль."));
      return;
    }

    if (state.password != state.confirmPassword) {
      emit(state.copyWith(error: "Пароли не совпадают."));
      return;
    }

    emit(state.copyWith(requestSent: true));
    try{
      final registerResult = await SignUpUseCase()(
          state.email,
          state.name,
          state.secondName,
          state.birthDate!,
          state.password
      );

      if (registerResult is Error<bool>) {
        emit(state.copyWith(error: registerResult.error));
      } else if (registerResult is Ok<bool> && registerResult.value) {
        _popOfPage();
      }
    } catch(e) {
      emit(state.copyWith(error: "Ошибка при запросе."));
    }
    emit(state.copyWith(requestSent: false));
  }
}
