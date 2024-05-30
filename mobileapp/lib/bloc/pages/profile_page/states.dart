import 'package:freezed_annotation/freezed_annotation.dart';
import 'package:mobileapp/map_models/profile_map_model.dart';

part 'states.freezed.dart';

@freezed
sealed class ProfilePageBlocState with _$ProfilePageBlocState {
  const factory ProfilePageBlocState.loading() = ProfilePageBlocStateLoading;
  const factory ProfilePageBlocState.loaded(ProfilePageBlocStateLoadedMapModel model) = ProfilePageBlocStateLoaded;
  const factory ProfilePageBlocState.loadError(String error) = ProfilePageBlocStateLoadError;
}
