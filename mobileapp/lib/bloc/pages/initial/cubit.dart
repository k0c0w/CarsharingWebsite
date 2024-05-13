import 'dart:async';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobileapp/bloc/auth/auth_bloc.dart';
import 'package:mobileapp/bloc/auth/auth_bloc_events.dart';
import 'package:mobileapp/bloc/auth/auth_bloc_states.dart';
import 'package:mobileapp/bloc/pages/initial/state.dart';

class InitialPageCubit extends Cubit<InitialPageCubitState> {
  final AuthBloc authBloc;
  late final StreamSubscription<AuthState> authBlocSubscription;

  InitialPageCubit(this.authBloc) : super(InitialPageCubitState.undefined) {
    _onState(authBloc.state);
    authBlocSubscription = authBloc.stream.listen(_onState);

    authBloc.add(AuthCheckStatusEvent());
  }

  void _onState(AuthState state) {
    if (state is AuthAuthorizedState) {
      emit(InitialPageCubitState.authorized);
    } else if (state is AuthUnauthorizedState) {
      emit(InitialPageCubitState.notAuthorized);
    }
  }

  @override
  Future<void> close() {
    authBlocSubscription.cancel();
    return super.close();
  }
}
