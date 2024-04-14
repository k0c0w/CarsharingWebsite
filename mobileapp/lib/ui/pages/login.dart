import 'package:flutter/material.dart';
import 'package:mobileapp/ui/components/appbar.dart';
import 'package:mobileapp/ui/pages/pages_list.dart';
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

  Future<void> tryAuthorize(BuildContext context) async {
    Navigator.restorablePushNamedAndRemoveUntil(context, DriveRoutes.home, (route) => false);
  }
}

class LoginPageWidget extends StatelessWidget {
  const LoginPageWidget({Key? key});

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
    final _viewModel = context.read<_ViewModel>();

    return Scaffold(
      appBar: DriveLoginAppBar(title: "ЛОГИН"),
      body: SafeArea(
        minimum: const EdgeInsets.symmetric(horizontal: 20),
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            const Spacer(),
            FormInputSubpage(
              label: 'Почта',
              onChanged: (value) {
                _viewModel.state.email = value;
              },
            ),
            const SizedBox(height: 20),
            FormInputSubpage(
              label: 'Пароль',
              obscureText: true,
              onChanged: (value) {
                _viewModel.state.password = value;
              },
            ),
            const SizedBox(height: 20),
            ElevatedButton(
              onPressed: () {
                _viewModel.onLoginPressed('Login');
              },
              child: const Text('Войти'),
            ),
            const Spacer(),
            CreateAccountButton(onPressed: () => _viewModel.tryAuthorize(context)),
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
      child: const Text('Нет аккаунта? Создайте!'),
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
        border: const OutlineInputBorder(),
      ),
      obscureText: obscureText,
      onChanged: onChanged,
    );
  }
}
