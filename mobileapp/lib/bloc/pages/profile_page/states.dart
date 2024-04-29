import 'package:freezed_annotation/freezed_annotation.dart';
import 'package:mobileapp/bloc/pages/profile_page/map_models.dart';

part 'states.freezed.dart';

@freezed
sealed class ProfilePageBlocState with _$ProfilePageBlocState {
  const factory ProfilePageBlocState.loading() = ProfilePageBlocStateLoading;
  const factory ProfilePageBlocState.loaded(ProfilePageBlocStateLoadedMapModel model) = ProfilePageBlocStateLoaded;
  const factory ProfilePageBlocState.loadError(String error) = ProfilePageBlocStateLoadError;
}
