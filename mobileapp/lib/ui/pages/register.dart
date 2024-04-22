import 'package:flutter/material.dart';
import 'package:mobileapp/ui/components/appbar.dart';
import 'package:provider/provider.dart';
import 'login.dart';

class _ViewModelState {
  String? email;
  String? password;
  String? confirmPassword;
  String? firstName;
  String? lastName;
  DateTime? dateOfBirth;
  String? errorText;

  _ViewModelState({
    required this.email,
    required this.password,
    required this.confirmPassword,
    required this.firstName,
    required this.lastName,
    required this.dateOfBirth,
    required this.errorText,
  });
}

class _ViewModel extends ChangeNotifier {
  final _state = _ViewModelState(
    email: "",
    password: "",
    confirmPassword: "",
    firstName: null,
    lastName: null,
    dateOfBirth: null,
    errorText: null
  );

  _ViewModelState get state => _state;

  Future<void> onRegisterPressed() async {
    if (_state.dateOfBirth == null) {
      _state.errorText = "Дата рождения не указана";
      notifyListeners();
      return;
    }

    final age = DateTime.now().year - _state.dateOfBirth!.year;
    if (age < 18) {
      _state.errorText = "Вам должно быть 18 лет или старше";
      notifyListeners();
      return;
    }

    if (_state.firstName == null || _state.lastName == null) {
      _state.errorText = "Имя и фамилия обязательны для заполнения";
      notifyListeners();
      return;
    }

    if (_state.password != _state.confirmPassword) {
      _state.errorText = "Пароль и подтверждение пароля не совпадают";
      notifyListeners();
      return;
    }
  }
}

class RegisterPageWidget extends StatelessWidget {
  const RegisterPageWidget({Key? key});

  @override
  Widget build(BuildContext context) {
    return ChangeNotifierProvider(
      create: (_) => _ViewModel(),
      child: const _View(),
    );
  }
}

class _View extends StatelessWidget {
  const _View();

  @override
  Widget build(BuildContext context) {
    final viewModel = Provider.of<_ViewModel>(context);

    return Scaffold(
      appBar: DriveAppBar(title: "Регистрация"),
      body: SafeArea(
        minimum: const EdgeInsets.symmetric(horizontal: 20),
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            const Spacer(),
            FormInputSubpage(
              label: 'Почта',
              onChanged: (value) {
                viewModel.state.email = value;
              },
            ),
            const SizedBox(height: 20),
            FormInputSubpage(
              label: 'Пароль',
              obscureText: true,
              onChanged: (value) {
                viewModel.state.password = value;
              },
            ),
            const SizedBox(height: 20),
            FormInputSubpage(
              label: 'Подтвердите пароль',
              obscureText: true,
              onChanged: (value) {
                viewModel.state.password = value;
              },
            ),
            const SizedBox(height: 20),
            FormInputSubpage(
              label: 'Имя',
              onChanged: (value) {
                viewModel.state.firstName = value;
              },
            ),
            const SizedBox(height: 20),
            FormInputSubpage(
              label: 'Фамилия',
              onChanged: (value) {
                viewModel.state.lastName = value;
              },
            ),
            const SizedBox(height: 20),
            FormInputSubpage(
              label: 'Дата рождения',
              onChanged: (value) {
                viewModel.state.dateOfBirth = DateTime.tryParse(value);
              },
            ),
            const SizedBox(height: 20),
            if (viewModel.state.errorText != null)
              Text(
                viewModel.state.errorText!,
                style: TextStyle(color: Colors.red),
              ),
            const SizedBox(height: 20),
            ElevatedButton(
              onPressed: () {
                viewModel.onRegisterPressed();
              },
              child: Text('Создать'),
            ),
            const Spacer(),
          ],
        ),
      ),
    );
  }
}
