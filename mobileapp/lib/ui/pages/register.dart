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
  Widget build(BuildContext context){
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
            ElevatedButton(
              onPressed: () {
                viewModel.onLoginPressed('Register');
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