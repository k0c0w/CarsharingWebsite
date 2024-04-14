import 'package:flutter/material.dart';
import 'package:mobileapp/ui/components/appbar.dart';
import 'package:provider/provider.dart';
import 'login.dart';

class _ViewModelState {
  String? email;
  String? password;
  String? confirmPassword;

  _ViewModelState({
    required this.email,
    required this.password,
    required this.confirmPassword
  });
}

class _ViewModel extends ChangeNotifier {
  final _state = _ViewModelState(
      email: "",
      password: "",
      confirmPassword: ""
  );

  _ViewModelState get state => _state;

  Future<void> onLoginPressed(String name) async {
    notifyListeners();
  }
}

class RegisterPageWidget extends StatelessWidget {
  const RegisterPageWidget({Key? key});

  static Widget create() {
    return ChangeNotifierProvider(
        create: (_) => _ViewModel(),
        child: const RegisterPageWidget()
    );
  }

  @override
  Widget build(BuildContext context) {
    final _viewModel = Provider.of<_ViewModel>(context);

    return Scaffold(
      appBar: DriveLoginAppBar(title: "Регистрация"),
      body: SafeArea(
        minimum: EdgeInsets.symmetric(horizontal: 20),
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            Spacer(),
            FormInputSubpage(
              label: 'Почта',
              onChanged: (value) {
                _viewModel.state.email = value;
              },
            ),
            SizedBox(height: 20),
            FormInputSubpage(
              label: 'Пароль',
              obscureText: true,
              onChanged: (value) {
                _viewModel.state.password = value;
              },
            ),
            SizedBox(height: 20),
            FormInputSubpage(
              label: 'Подтвердите пароль',
              obscureText: true,
              onChanged: (value) {
                _viewModel.state.password = value;
              },
            ),
            SizedBox(height: 20),
            ElevatedButton(
              onPressed: () {
                _viewModel.onLoginPressed('Register');
              },
              child: Text('Создать'),
            ),
            Spacer(),
          ],
        ),
      ),
    );
  }
}