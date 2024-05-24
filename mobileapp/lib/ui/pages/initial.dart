import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobileapp/bloc/auth/auth_bloc.dart';
import 'package:mobileapp/bloc/auth/auth_bloc_states.dart';
import 'package:mobileapp/bloc/pages/initial/cubit.dart';
import 'package:mobileapp/bloc/pages/initial/state.dart';
import 'package:mobileapp/ui/components/center_circular_progress_indicator.dart';
import 'package:mobileapp/ui/pages/pages_list.dart';

class InitialPageWidget extends StatelessWidget {
  final AuthBloc _authBloc = AuthBloc(AuthCheckStatusInProgressState());

  InitialPageWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return BlocProvider<InitialPageCubit>(
      create: (context) => InitialPageCubit(_authBloc),
      lazy: false,
      child: const _View(),
    );
  }
}

class _View extends StatelessWidget {
  const _View();

  void _onCubitStateChange(BuildContext context, InitialPageCubitState state) {
    final route = state == InitialPageCubitState.authorized ? DriveRoutes.home : DriveRoutes.unauthorizedHome;
    Navigator.of(context).pushReplacementNamed(route);
  }

  @override
  Widget build(BuildContext context) {
    return BlocListener<InitialPageCubit, InitialPageCubitState>(
      listenWhen: (_, current) => current != InitialPageCubitState.undefined,
      listener: _onCubitStateChange,
      child: const Scaffold(
        body: CenterCircularProgressIndicator()
      ),
    );
  }
}