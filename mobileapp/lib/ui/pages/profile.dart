import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:mobileapp/bloc/auth/auth_bloc.dart';
import 'package:mobileapp/bloc/pages/profile_page/bloc.dart';
import 'package:mobileapp/bloc/pages/profile_page/events.dart';
import 'package:mobileapp/bloc/pages/profile_page/states.dart';
import 'package:mobileapp/main.dart';
import 'package:mobileapp/ui/Components/appbar.dart';
import 'package:mobileapp/ui/Components/styles.dart';
import 'package:mobileapp/ui/components/center_circular_progress_indicator.dart';
import 'package:mobileapp/ui/components/confirmation_container.dart';
import 'package:mobileapp/ui/components/error_page.dart';
import 'package:mobileapp/ui/components/form_input_subpage.dart';
import 'package:mobileapp/ui/components/text_input_with_label.dart';

class ProfilePageWidget extends StatelessWidget {
  const ProfilePageWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return BlocProvider<ProfilePageBloc>(
        create: (ctx) {
          final bloc = ProfilePageBloc(
              const ProfilePageBlocState.loading(),
              authBloc: getIt<AuthBloc>(),
              buildContext: ctx,
          );
          bloc.add(const ProfilePageBlocEvent.load());

          return bloc;
        },
        child: const _View()
    );
  }
}

class _View extends StatelessWidget {
  const _View();

  Widget _build(BuildContext context)
  => GestureDetector(
    onHorizontalDragEnd: (_) => context
        .read<ProfilePageBloc>()
        .add(const ProfilePageBlocEvent.load(allowCache: false)),
      child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            const _BalanceWidget(),
            const _PersonalInformationTableWidget(),
            const _PersonalDataConfirmationLabelWidget(),
            _AppLogoutButton(),
          ]
      )
  );

  @override
  Widget build(BuildContext context) {
    return BlocBuilder<ProfilePageBloc, ProfilePageBlocState>(
        builder: (ctx, state) {
          final widget = switch(state) {
            ProfilePageBlocStateLoading() => const CenterCircularProgressIndicator(),
            ProfilePageBlocStateLoadError(:final error) => LoadPageErrorMessageAtCenter(
              customErrorMessage: error,
              onRetryPressed: () => ctx.read<ProfilePageBloc>().add(const ProfilePageBlocEvent.load()),
            ),
            ProfilePageBlocStateLoaded() => _build(ctx),
          };

          return Scaffold(
            appBar: DriveAppBar(title: "ПРОФИЛЬ"),
            body: SafeArea(
              minimum: const EdgeInsets.only(left: 20, right: 20, bottom: 10),
              child: widget,
            ),
          );
        }
    );
  }
}

class _BalanceWidget extends StatelessWidget {
  const _BalanceWidget();

  @override
  Widget build(BuildContext context) {
    final state = context.select((ProfilePageBloc bloc) => bloc.state) as ProfilePageBlocStateLoaded;
    final prevValue = state.model.balance.text;

    return BlocBuilder<ProfilePageBloc, ProfilePageBlocState> (
        buildWhen: (_, curr) => curr is ProfilePageBlocStateLoaded
            && prevValue != curr.model.balance.text,
        builder: (ctx, state) {
          final model = (state as ProfilePageBlocStateLoaded).model.balance;
          return Container(
            margin: const EdgeInsets.only(top: 40, bottom: 40),
            child: Column(
                children: [
                  Text(
                      model.text,
                      style: GoogleFonts.orbitron(
                          color: Colors.black54,
                          fontSize: 35,
                          fontWeight: FontWeight.bold)
                  ),
                  const Text(
                    "баланс",
                    style: TextStyle(
                      color: Colors.black26,
                      fontSize: 15,
                      fontWeight: FontWeight.normal,
                    ),
                  ),
                ]
            ),
          );
        }
    );
  }
}

class _PersonalInformationTableWidget extends StatelessWidget {
  const _PersonalInformationTableWidget();

  @override
  Widget build(BuildContext context) {
    return Flexible(
            child: Column(
                children: [
                  _PhoneInput(),
                  const Divider(),
                  _EmailInput(),
                  const Divider(),
                  _NameInput(),
                  const Divider(),
                  _SecondNameInput(),
                  const Divider(),
                  _BirthDateInput(),
                  const Divider(),
                  _PassportInput(),
                  const Divider(),
                  _LicenseInput(),
                ]
            )
    );
  }
}

class _PhoneInput extends StatelessWidget {
  static const phoneNumberLabel = "Номер телефона";

  @override
  Widget build(BuildContext context) {
    return InformationFieldWithLabel(
      name: phoneNumberLabel,
      value: "+78005553535",
      onTap: () =>
          Navigator.of(context).push(MaterialPageRoute(builder:
              (_) => DrivePhoneNumberInputSubpage(
            hintText: phoneNumberLabel,
            inputTitle: "ИЗМЕНИТЬ НОМЕР ТЕЛЕФОНА",
            initialValue: "+7800553535",
            onSavePressed: (_) {},
          )
          )),
    );
  }
}

class _EmailInput extends StatelessWidget {
  static const eMailLabel = "e-mail";

  void _onSavePressed(String value, BuildContext context, ProfilePageBloc bloc) {
    bloc.add(ProfilePageBlocEvent.emailChanged(value));
    Navigator.of(context).pop();
  }

  @override
  Widget build(BuildContext context) {
    return BlocBuilder<ProfilePageBloc, ProfilePageBlocState>(
        buildWhen: (prev, current) => prev is ProfilePageBlocStateLoaded
            && current is ProfilePageBlocStateLoaded
            && current.model.email != prev.model.email,
        builder: (ctx, state) {
          final model = (state as ProfilePageBlocStateLoaded).model.email;

          return InformationFieldWithLabel(
              name: eMailLabel,
              value: model.text,
              error: model.error,
              onTap: () =>
                  Navigator.of(ctx).push(MaterialPageRoute(builder:
                      (routeContext) => DriveEmailInputSubpage(
                    hintText: eMailLabel,
                    inputTitle: "ИЗМЕНИТЬ E-MAIL",
                    onSavePressed: (value) => _onSavePressed(
                        value,
                        routeContext,
                        ctx.read<ProfilePageBloc>()
                    ),
                    initialValue: model.text,
                  )))
          );
        }
    );
  }
}

class _NameInput extends StatelessWidget {
  static const nameLabel = "Имя";

  void _onSaveNamePressed(String name, BuildContext context, ProfilePageBloc bloc) {
    bloc.add(ProfilePageBlocEvent.nameChanged(name));
    Navigator.of(context).pop();
  }

  @override
  Widget build(BuildContext context) {
    return BlocBuilder<ProfilePageBloc, ProfilePageBlocState>(
        buildWhen: (prev, current) => prev is ProfilePageBlocStateLoaded
          && current is ProfilePageBlocStateLoaded
          && prev.model.name != current.model.name,
        builder: (ctx, currentState) {
          final model = (currentState as ProfilePageBlocStateLoaded).model.name;
          final bloc = ctx.read<ProfilePageBloc>();

          return InformationFieldWithLabel(
            name: nameLabel,
            value: model.text,
            error: model.error,
            onTap: () {
              Navigator.of(context).push(MaterialPageRoute(builder:
                  (ctx) => DriveTextInputSubpage(
                hintText: nameLabel,
                inputTitle: "ИЗМЕНИТЬ ИМЯ",
                onSavePressed: (value) => _onSaveNamePressed(value, ctx, bloc),
                initialValue: model.text,
              )
              )
              );
            },
          );
        }
    );
  }
}

class _SecondNameInput extends StatelessWidget {
  static const secondNameLabel = "Фамилия";

  void _onSaveSecondNamePressed(String secondName, BuildContext context, ProfilePageBloc bloc) {
    bloc.add(ProfilePageBlocEvent.secondNameChanged(secondName));
    Navigator.of(context).pop();
  }

  @override
  Widget build(BuildContext context) {
    final bloc = context.read<ProfilePageBloc>();
    return BlocBuilder<ProfilePageBloc, ProfilePageBlocState>(
      buildWhen: (prev, current) => prev is ProfilePageBlocStateLoaded
                && current is ProfilePageBlocStateLoaded
                && prev.model.secondName != current.model.secondName,
      builder: (ctx, state) {
        final model = (state as ProfilePageBlocStateLoaded).model.secondName;
        return InformationFieldWithLabel(
            name: secondNameLabel,
            value: model.text,
            error: model.error,
            onTap: () =>
                Navigator.of(context).push(MaterialPageRoute(builder:
                    (ctx) => DriveTextInputSubpage(
                  hintText: secondNameLabel,
                  inputTitle: "ИЗМЕНИТЬ ФАМИЛИЮ",
                  onSavePressed: (value) => _onSaveSecondNamePressed(value, ctx, bloc),
                  initialValue: model.text,
                )))
        );
      },
    );
  }
}

class _BirthDateInput extends StatelessWidget {
  static const birthDateLabel = "Дата рождения";

  void _onBirthDateSave(DateTime dateTime, BuildContext context, ProfilePageBloc bloc) {
    bloc.add(ProfilePageBlocEvent.ageChanged(dateTime));
    Navigator.of(context).pop();
  }

  @override
  Widget build(BuildContext context) {
    final bloc = context.read<ProfilePageBloc>();
    return BlocBuilder<ProfilePageBloc, ProfilePageBlocState>(
        buildWhen: (previous, current) =>
        previous is ProfilePageBlocStateLoaded
            && current is ProfilePageBlocStateLoaded
            && previous.model.age != current.model.age,
        builder: (ctx, state) {
          final model = (state as ProfilePageBlocStateLoaded).model.age;
          return InformationFieldWithLabel(
            name: birthDateLabel,
            value: model.text,
            error: model.error,
            onTap: () =>
                Navigator.of(context).push(MaterialPageRoute(builder:
                    (ctx) =>
                    DriveDateInputSubpage(
                      hintText: birthDateLabel,
                      inputTitle: "ИЗМЕНИТЬ ДАТУ РОЖДЕНИЯ",
                      onSavePressed: (value) =>
                          _onBirthDateSave(value, ctx, bloc),
                      initialValue: model.text,
                      lastDate: DateTime.now().subtract(
                          const Duration(days: 365 * 23)),
                    )
                )),
          );
        }
    );
  }
}

class _PassportInput extends StatelessWidget {
  static const String passportLabel = "Паспорт";

  void _onSave(String passport, BuildContext context, ProfilePageBloc bloc) {
    bloc.add(ProfilePageBlocEvent.passportChanged(passport));
    Navigator.of(context).pop();
  }

  @override
  Widget build(BuildContext context) {
    final bloc = context.read<ProfilePageBloc>();
    return BlocBuilder<ProfilePageBloc, ProfilePageBlocState>(
        buildWhen: (prev, current) => prev is ProfilePageBlocStateLoaded
            && current is ProfilePageBlocStateLoaded
            && prev.model.passport != current.model.passport,
        builder: (ctx, state) {
          final model = (state as ProfilePageBlocStateLoaded).model.passport;
          return InformationFieldWithLabel(
            name: passportLabel,
            value: model.text,
            error: model.error,
            onTap: () => Navigator.of(context).push(MaterialPageRoute(builder:
                (ctx) => DriveTextInputSubpage(
              hintText: passportLabel,
              inputTitle: "ИЗМЕНИТЬ ПАСПОРТ",
              initialValue: model.text,
              onSavePressed: (value) => _onSave(value, ctx, bloc),
            ))
            ),
          );
        });
  }
}

class _LicenseInput extends StatelessWidget {
  static const String driverLicenceLabel = "ВУ";

  void _onSave(String license, BuildContext context, ProfilePageBloc bloc) {
    bloc.add(ProfilePageBlocEvent.driverLicenseChanged(license));
    Navigator.of(context).pop();
  }

  @override
  Widget build(BuildContext context) {
    final bloc = context.read<ProfilePageBloc>();

    return BlocBuilder<ProfilePageBloc, ProfilePageBlocState>(
        buildWhen: (prev, current) => prev is ProfilePageBlocStateLoaded
          && current is ProfilePageBlocStateLoaded
          && prev.model.driverLicense != current.model.driverLicense,
        builder: (ctx, state) {
          final model = (state as ProfilePageBlocStateLoaded).model.driverLicense;
          return InformationFieldWithLabel(
            name: driverLicenceLabel,
            value: model.text,
            error: model.error,
            onTap: () => Navigator.of(context).push(MaterialPageRoute(
                builder: (ctx) => DriveEmailInputSubpage(
                  hintText: driverLicenceLabel,
                  inputTitle: "ИЗМЕНИТЬ ВОДИТЕЛЬСКОЕ УДОСТОВЕРЕНИЕ",
                  onSavePressed: (value) => _onSave(value, ctx, bloc),
                  initialValue: model.text,
                )
              )
            ),
          );
        });
  }
}

class _PersonalDataConfirmationLabelWidget extends StatelessWidget {
  const _PersonalDataConfirmationLabelWidget();
  @override
  Widget build(BuildContext context) {
    return BlocBuilder<ProfilePageBloc, ProfilePageBlocState>(
        buildWhen: (prev, current) => prev is ProfilePageBlocStateLoaded
            && current is ProfilePageBlocStateLoaded
            && prev.model.accountStatus != current.model.accountStatus,
        builder: (_, state) {
          final model = (state as ProfilePageBlocStateLoaded).model.accountStatus;
          return Text(model.text, style: DriveTextStyles.errorLabel,);
        }
    );
  }
}

class _AppLogoutButton extends StatelessWidget {
  final AuthBloc authBloc = getIt<AuthBloc>();
  _AppLogoutButton();

  void _openConfirmationModal(BuildContext context) {
    final screenSize = MediaQuery.of(context).size;
    showModalBottomSheet(
        shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(20)),
        elevation: 0,
        useSafeArea: true,
        constraints: BoxConstraints(
            maxWidth: screenSize.width * 0.95,
            minWidth: screenSize.width * 0.95,
            maxHeight: 0.2 * screenSize.height,
        ),
        context: context,
        builder: (ctx) {
          final bloc = context.read<ProfilePageBloc>();
          return ConfirmationContainerWidget(
                acceptanceText: "Да, выйти",
                declineText: "Нет",
                questionText: "Действительно выйти из приложения?",
                onAccept: () => bloc.add(const ProfilePageBlocEvent.exitPressed()),
                onDecline: () => Navigator.of(context).pop(),
          );
        }
    );
  }


  @override
  Widget build(BuildContext context) {
    return InkWell(
        borderRadius: BorderRadius.circular(7),
          onTap: () => _openConfirmationModal(context),

        child: Container(
          padding: const EdgeInsets.symmetric(vertical: 10, horizontal: 20),
          child: const Text("ВЫХОД", style: DriveTextStyles.userInput),
        )
      );
  }
}
