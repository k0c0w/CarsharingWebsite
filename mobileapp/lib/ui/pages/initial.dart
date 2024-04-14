import 'dart:async';
import 'dart:ui';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:mobileapp/ui/pages/pages_list.dart';
import 'package:provider/provider.dart';
import '../../domain/services/auth_service.dart';

class _ViewModel {
  final _authService = AuthService();
  BuildContext context;

  _ViewModel(this.context){
    _resolveActions();
  }

  Future<void> _resolveActions() async {
    final isAuthorizedUser = await _authService.checkAuth();
    final route = isAuthorizedUser ? DriveRoutes.home : DriveRoutes.registration;

    Navigator.pushNamedAndRemoveUntil(context, route, (route) => false);
  }
}

class InitialPageWidget extends StatelessWidget {
  const InitialPageWidget({super.key});

  static Widget create() {
    return Provider(
        create: (context) => _ViewModel(context),
        lazy: false,
        child: const InitialPageWidget(),
    );
  }

  @override
  Widget build(BuildContext context) {
    return const Scaffold(
      body: Center(
        child: CircularProgressIndicator(strokeCap: StrokeCap.square,),
      ),
    );
  }
}