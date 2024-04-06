import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';
import '../Components/appbar.dart';
import '../Components/form_input_subpage.dart';
import '../Components/styles.dart';

class _HumanInfo {
  final String firstNameTitle;
  final String secondNameTitle;
  final String? thirdNameTitle;
  final String birthDateTitle;

  _HumanInfo({
    required this.firstNameTitle,
    required this.secondNameTitle,
    required this.birthDateTitle,
    this.thirdNameTitle,
  });

  _HumanInfo copyWith({
    String? firstNameTitle,
    String? secondNameTitle,
    String? thirdNameTitle,
    String? birthDateTitle }) {
    return _HumanInfo(
        firstNameTitle: firstNameTitle ?? this.firstNameTitle,
        secondNameTitle: secondNameTitle ?? this.secondNameTitle,
        thirdNameTitle: thirdNameTitle ?? this.thirdNameTitle,
        birthDateTitle: birthDateTitle ?? this.birthDateTitle,
    );
  }
}

class _ContactInfo {
  final String phoneNumberTitle;
  final String emailTitle;

  _ContactInfo({required this.phoneNumberTitle, required this.emailTitle});
}

class _PersonalDocuments {
  final String? driverLicenseTitle;
  final String? passportTitle;

  _PersonalDocuments({this.driverLicenseTitle, this.passportTitle});
}

class _ViewModelState {
  final _HumanInfo humanInfo;
  final _PersonalDocuments personalDocuments;
  final _ContactInfo contacts;
  final String? errorTitle;
  final String balanceTitle;
  final String? accountIsNotConfirmedTitle;

  _ViewModelState({
    required this.humanInfo,
    required this.personalDocuments,
    required this.contacts,
    required this.errorTitle,
    required this.balanceTitle,
    required this.accountIsNotConfirmedTitle
  });

  _ViewModelState copyWith({
    _HumanInfo? newHumanInfo,
    _PersonalDocuments? newPersonalDocuments,
    _ContactInfo? newContractInfo,
    String? newErrorTitle,
    String? newBalanceTitle,
    String? newAccountIsNotConfirmedTitle
  }) {
    return _ViewModelState(
        humanInfo: newHumanInfo ?? humanInfo,
        personalDocuments: newPersonalDocuments ?? personalDocuments,
        contacts: newContractInfo ?? contacts,
        errorTitle: newAccountIsNotConfirmedTitle ?? errorTitle,
        balanceTitle: newBalanceTitle ?? balanceTitle,
        accountIsNotConfirmedTitle: newAccountIsNotConfirmedTitle ?? accountIsNotConfirmedTitle);
  }
}

class _ViewModel extends ChangeNotifier {
  var _state = _ViewModelState(
      humanInfo: _HumanInfo(
          firstNameTitle: "Василий",
          secondNameTitle: "Пупкин",
          birthDateTitle: "27.02.1990"
      ),
      personalDocuments: _PersonalDocuments(),
      contacts: _ContactInfo(
          phoneNumberTitle: "+7XXXYYYZZUU",
          emailTitle: "email@example.com"
      ),
      errorTitle: "",
      balanceTitle: "0.0",
      accountIsNotConfirmedTitle: ""
  );

  _ViewModelState get state => _state;

  Future<void> onSaveNamePressed(String name) async {
    final old = _state.humanInfo.copyWith(firstNameTitle: name);
    _state = _state.copyWith(newHumanInfo: old);
    notifyListeners();
  }

  Future<void> onSaveSecondNamePressed(String secondName) async {

  }

  Future<void> onSaveThirdNamePressed(String thirdName) async {

  }

  Future<void> onSaveBirthDatePressed(DateTime newBirthDate) async {
    final formatter = DateFormat("dd.MM.yyyy");
    final newDateTitle = formatter.format(newBirthDate);
    var humanInfo = _state.humanInfo;

    _state = _state.copyWith(
        newHumanInfo: humanInfo.copyWith(birthDateTitle: newDateTitle)
    );

    notifyListeners();
  }

  Future<void> onSavePhoneNumberPressed(String newPhoneNumber) async {

  }

  Future<void> onSaveEmailPressed(String newEmail) async {

  }

  Future<void> onSavePassportPressed(String passport) async {

  }

  Future<void> onSaveDriverLicencePressed(String passport) async {

  }
}

class ProfilePageWidget extends StatelessWidget {
  const ProfilePageWidget();

  static Widget create() {
    return ChangeNotifierProvider(
        create: (_) => _ViewModel(),
        child: const ProfilePageWidget()
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: DriveAppBar(title: "ПРОФИЛЬ"),
      body: const SafeArea(
        minimum: EdgeInsets.symmetric(horizontal: 20),
        child: Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              _BalanceWidget(),
              _PersonalInformationTableWidget(),
              _PersonalDataConfirmationLabelWidget(),
              Spacer(),
              _AppLogoutButton(),
            ]),
      ),
    );
  }
}

class _PersonalDataConfirmationLabelWidget extends StatelessWidget {
  const _PersonalDataConfirmationLabelWidget();
  @override
  Widget build(BuildContext context) {
    return const Text("Данные не подтверждены!", style: DriveTextStyles.errorLabel,);
  }
}

class _AppLogoutButton extends StatelessWidget {
  const _AppLogoutButton();
  @override
  Widget build(BuildContext context) {
    return const TextButton(
      onPressed: null,
      child: Text("ВЫХОД", style: DriveTextStyles.userInput),
    );
  }
}

class _BalanceWidget extends StatelessWidget {
  const _BalanceWidget({super.key});

  @override
  Widget build(BuildContext context) {
    final balance = context.select((_ViewModel x) => x.state.balanceTitle);

    return Container(
      margin: const EdgeInsets.only(bottom: 25),
      child: Column(
          children: [
            Text(
                balance,
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
                )
            ),
          ]
      ),
    );
  }
}

class _PersonalInformationTableWidget extends StatelessWidget {
  const _PersonalInformationTableWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return Expanded(
        child: Column(
          children: [
            _ContactsSectionWidget(),
            _HumanInfoSectionWidget(),
            _DocumentSectionWidget(),
          ]
      )
    );
  }
}

class _DocumentSectionWidget extends StatelessWidget {
  static const String passportLabel = "Паспорт";
  static const String driverLicenceLabel = "ВУ";

  @override
  Widget build(BuildContext context) {
    final model = context.read<_ViewModel>();
    final documents = context.select((_ViewModel m) => m.state.personalDocuments);

    return Column(children: [
      _InformationField(
        name: passportLabel,
        value: documents.passportTitle ?? "",
        onTap: () {
          Navigator.of(context).push(MaterialPageRoute(builder:
              (_) =>
              DriveTextInputSubpage(
                  hintText: passportLabel,
                  inputTitle: "ИЗМЕНИТЬ ПАСПОРТ",
                  initialValue: documents.passportTitle,
                  onSavePressed: model.onSavePassportPressed
              )));
        },
      ),
      const Divider(),
      _InformationField(
        name: driverLicenceLabel,
        value: documents.driverLicenseTitle ?? "",
        onTap: () {
          Navigator.of(context).push(MaterialPageRoute(builder:
              (_) =>
              DriveEmailInputSubpage(
                hintText: driverLicenceLabel,
                inputTitle: "ИЗМЕНИТЬ ВОДИТЕЛЬСКОЕ УДОСТОВЕРЕНИЕ",
                onSavePressed: model.onSaveDriverLicencePressed,
                initialValue: documents.driverLicenseTitle,
              )
          )
          );
        },
      ),
    ],);
  }
}

class _ContactsSectionWidget extends StatelessWidget {
  static const phoneNumberLabel = "Номер телефона";
  static const eMailLabel = "e-mail";

  @override
  Widget build(BuildContext context) {
    final model = context.read<_ViewModel>();
    final contacts = context.select((_ViewModel m) => m.state.contacts);

    return Column(
      children: [
      _InformationField(
            name: phoneNumberLabel,
            value: contacts.phoneNumberTitle,
            onTap: () {
              Navigator.of(context).push(MaterialPageRoute(builder:
                  (_) => DrivePhoneNumberInputSubpage(
                      hintText: phoneNumberLabel,
                      inputTitle: "ИЗМЕНИТЬ НОМЕР ТЕЛЕФОНА",
                      initialValue: contacts.phoneNumberTitle,
                      onSavePressed: model.onSavePhoneNumberPressed
                  )));
            },
        ),
      const Divider(),
      _InformationField(
          name: eMailLabel,
          value: contacts.emailTitle,
          onTap: () {
              Navigator.of(context).push(MaterialPageRoute(builder:
              (_) => DriveEmailInputSubpage(
                  hintText: eMailLabel,
                  inputTitle: "ИЗМЕНИТЬ E-MAIL",
                  onSavePressed: model.onSaveEmailPressed,
                  initialValue: contacts.emailTitle,
                  )
                )
              );
            },
      ),
      const Divider(),
    ],);
  }
}

class _HumanInfoSectionWidget extends StatelessWidget {
  static const nameLabel = "Имя";
  static const secondNameLabel = "Фамилия";
  static const thirdNameLabel = "Отчество";
  static const birthDateLabel = "Дата рождения";

  @override
  Widget build(BuildContext context) {
    final model = context.read<_ViewModel>();
    final humanInfo = context.select((_ViewModel m) => m.state.humanInfo);

    return Column(
      children: [
        _InformationField(
          name: nameLabel,
          value: humanInfo.firstNameTitle,
          onTap: () {
            Navigator.of(context).push(MaterialPageRoute(builder:
                (_) => DriveTextInputSubpage(
                  hintText: nameLabel,
                  inputTitle: "ИЗМЕНИТЬ ИМЯ",
                  onSavePressed: model.onSaveNamePressed,
                  initialValue: humanInfo.firstNameTitle,
                )
              )
            );
          },
        ),
        const Divider(),
        _InformationField(
          name: secondNameLabel,
          value: humanInfo.secondNameTitle,
          onTap: () {
            Navigator.of(context).push(MaterialPageRoute(builder:
                (_) => DriveTextInputSubpage(
                  hintText: secondNameLabel,
                  inputTitle: "ИЗМЕНИТЬ ФАМИЛИЮ",
                  onSavePressed: model.onSaveSecondNamePressed,
                  initialValue: humanInfo.secondNameTitle,
                )
              )
            );
          },
        ),
        const Divider(),
        _InformationField(
          name: thirdNameLabel,
          value: humanInfo.thirdNameTitle ?? "",
          onTap: () {
            Navigator.of(context).push(MaterialPageRoute(builder:
                (_) => DriveTextInputSubpage(
              hintText: thirdNameLabel,
              inputTitle: "ИЗМЕНИТЬ ОТЧЕСТВО",
              onSavePressed: model.onSaveThirdNamePressed,
              initialValue: humanInfo.thirdNameTitle,
            )
            )
            );
          },
        ),
        const Divider(),
        _InformationField(
          name: birthDateLabel,
          value: humanInfo.birthDateTitle,
          onTap: () {
            Navigator.of(context).push(MaterialPageRoute(builder:
                (_) => DriveDateInputSubpage(
              hintText: birthDateLabel,
              inputTitle: "ИЗМЕНИТЬ ДАТУ РОЖДЕНИЯ",
              onSavePressed: model.onSaveBirthDatePressed,
              initialValue: humanInfo.birthDateTitle,
              lastDate: DateTime.now().subtract(const Duration(days: 365 * 23)),
              )
            )
            );
          },
        ),
        const Divider(),
      ],
    );
  }
}

class _InformationField extends StatelessWidget {
  final String name;
  final String? value;
  final bool isError;
  final void Function() onTap;

  const _InformationField({
    required this.name,
    required this.onTap,
    required this.value,
    this.isError = false
  });

  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onTap,
      child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [
          Text(name, style: DriveTextStyles.inputLabel),
          Row(
            mainAxisAlignment: MainAxisAlignment.end,
            crossAxisAlignment: CrossAxisAlignment.center,
            children: [
              Text(value ?? "", style: isError ? DriveTextStyles.errorLabel : DriveTextStyles.userInput),
              const Icon(Icons.arrow_right, color: Colors.black87, opticalSize: 0.2,),
            ],
          )
        ],
      ),
    );
  }
}
