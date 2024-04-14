import 'package:flutter/material.dart';
import 'package:mobileapp/ui/components/appbar.dart';
import 'package:provider/provider.dart';

class _ViewModelState {
  String? email;
  String? password;


  _ViewModelState({
    required this.email,
    required this.password,
  });
}

class _ViewModel extends ChangeNotifier {
  final _state = _ViewModelState(
      email: "",
      password: ""
  );

  _ViewModelState get state => _state;

  Future<void> onLoginPressed(String name) async {
    notifyListeners();
  }
}

class LoginPageWidget extends StatelessWidget {
  const LoginPageWidget({Key? key});

  static Widget create() {
    return ChangeNotifierProvider(
        create: (_) => _ViewModel(),
        child: const LoginPageWidget()
    );
  }

  @override
  Widget build(BuildContext context) {
    final _viewModel = Provider.of<_ViewModel>(context);
    void _goToRegisterWidget() {
        //переход на страничку создания аккаунта
    }

    return Scaffold(
      appBar: DriveLoginAppBar(title: "ЛОГИН"),
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
            ElevatedButton(
              onPressed: () {
                _viewModel.onLoginPressed('Login');
              },
              child: Text('Войти'),
            ),
            Spacer(),
            CreateAccountButton(onPressed: _goToRegisterWidget),
          ],
        ),
      ),
    );
  }
}

class CreateAccountButton extends StatelessWidget {
  final VoidCallback onPressed;

  const CreateAccountButton({Key? key, required this.onPressed}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return TextButton(
      onPressed: onPressed,
      child: Text('Нет аккаунта? Создайте!'),
    );
  }
}


class FormInputSubpage extends StatelessWidget {
  final String label;
  final bool obscureText;
  final ValueChanged<String> onChanged;

  const FormInputSubpage({
    Key? key,
    required this.label,
    this.obscureText = false,
    required this.onChanged,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return TextFormField(
      decoration: InputDecoration(
        labelText: label,
        border: OutlineInputBorder(),
      ),
      obscureText: obscureText,
      onChanged: onChanged,
    );
  }
}
