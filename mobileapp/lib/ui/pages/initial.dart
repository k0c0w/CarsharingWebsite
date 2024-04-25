import 'dart:async';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobileapp/domain/bloc/auth/auth_bloc.dart';
import 'package:mobileapp/domain/bloc/auth/auth_bloc_events.dart';
import 'package:mobileapp/domain/bloc/auth/auth_bloc_states.dart';
import 'package:mobileapp/ui/components/center_circular_progress_indicator.dart';
import 'package:mobileapp/ui/pages/pages_list.dart';

enum _ViewCubitState {
  authorized,
  notAuthorized,
  undefined,
}

class _ViewCubit extends Cubit<_ViewCubitState> {
  final AuthBloc authBloc;
  late final StreamSubscription<AuthState> authBlocSubscription;

  _ViewCubit(this.authBloc) : super(_ViewCubitState.undefined) {
    _onState(authBloc.state);
    authBlocSubscription = authBloc.stream.listen(_onState);

    authBloc.add(AuthCheckStatusEvent());
  }

  void _onState(AuthState state) {
    if (state is AuthAuthorizedState) {
      emit(_ViewCubitState.authorized);
    } else if (state is AuthUnauthorizedState) {
      emit(_ViewCubitState.notAuthorized);
    }
  }

  @override
  Future<void> close() {
    authBlocSubscription.cancel();
    return super.close();
  }
}

class InitialPageWidget extends StatelessWidget {
  final AuthBloc _authBloc = AuthBloc(AuthCheckStatusInProgressState());

  InitialPageWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return BlocProvider<_ViewCubit>(
      create: (context) => _ViewCubit(_authBloc),
      lazy: false,
      child: const _View(),
    );
  }
}

class _View extends StatelessWidget {
  const _View();

  void _onCubitStateChange(BuildContext context, _ViewCubitState state) {
    final route = state == _ViewCubitState.authorized ? DriveRoutes.home : DriveRoutes.unathorizedHome;
    Navigator.of(context).pushReplacementNamed(route);
  }

  @override
  Widget build(BuildContext context) {
    return BlocListener<_ViewCubit, _ViewCubitState>(
      listenWhen: (_, current) => current != _ViewCubitState.undefined,
      listener: _onCubitStateChange,
      child: const Scaffold(
        body: CenterCircularProgressIndicator()
      ),
    );
  }
}