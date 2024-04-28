import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:mobileapp/bloc/pages/payment/bloc.dart';
import 'package:mobileapp/bloc/pages/payment/events.dart';
import 'package:mobileapp/bloc/pages/payment/state.dart';
import 'package:mobileapp/ui/Components/appbar.dart';
import 'package:mobileapp/ui/Components/styles.dart';
import 'package:mobileapp/ui/components/bottom_button.dart';
import 'package:mobileapp/ui/components/card_form.dart';
import 'package:mobileapp/ui/components/center_circular_progress_indicator.dart';
import 'package:mobileapp/ui/pages/pages_list.dart';


class PaymentPageWidget extends StatelessWidget {
  const PaymentPageWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return BlocProvider(
        create: (_) => PaymentPageBloc(PaymentPageState.none),
        child: Scaffold(
          appBar: DriveAppBar(title: "ОПЛАТА"),
          body: SafeArea(
            minimum: const EdgeInsets.only(left: 20, right: 20, top: 80),
            child: _ViewBody(),
          ),
        ),
    );
  }
}

class _ViewBody extends StatefulWidget {

  @override
  State<StatefulWidget> createState() => _ViewBodyState();

}

class _ViewBodyState extends State<_ViewBody> {
  @override
  Widget build(BuildContext context) {
    return BlocConsumer<PaymentPageBloc, PaymentPageState>(
      listener: (ctx, state) {
        if(state == PaymentPageState.success) {
          Navigator.of(ctx).pushReplacementNamed(DriveRoutes.profile);
        }
      },
      builder: (ctx, state) {
        final stackLayers = <Widget>[_MainViewWidget()];

        if (state == PaymentPageState.pending) {
          stackLayers.add(const _PopScopedCircularProgressionWidget());
        }

        return Stack(
          children: stackLayers,
        );
      }
    );
  }
}

class _PopScopedCircularProgressionWidget extends StatelessWidget {
  const _PopScopedCircularProgressionWidget();

  @override
  Widget build(BuildContext context) {
    return PopScope(
      canPop: false,
      onPopInvoked: (canPop) {
      },
      child: Container(
        color: Colors.transparent,
        child: const CenterCircularProgressIndicator(),
      )
    );
  }
}

class _MainViewErrorLabel extends StatelessWidget {
  const _MainViewErrorLabel();
  @override
  Widget build(BuildContext context) {
    final isError = context.select((PaymentPageBloc bloc) => bloc.state)
        == PaymentPageState.error;
    final labelText = isError ? "При пополнении баланса произошла ошибка." : "";

    return Container(
      margin: const EdgeInsets.only(top: 20),
      child: Text(labelText,
          style: GoogleFonts.openSans(
            textStyle: const TextStyle(
              color: DriveColors.brightRedColor,
              fontWeight: FontWeight.w500,
              fontSize: 15,
            ),
          )
      ),
    );
  }
}

class _MainViewWidget extends StatefulWidget {
  @override
  State<StatefulWidget> createState() => _MainViewWidgetState();
}

class _MainViewWidgetState extends State<_MainViewWidget> {
  double money = 0;

  _MainViewWidgetState();

  @override
  Widget build(BuildContext context) {
    final screenSize = MediaQuery.of(context).size;
    final state = context.select((PaymentPageBloc bloc) => bloc.state);
    final bloc = context.read<PaymentPageBloc>();
    final isEnabled = state != PaymentPageState.pending;

    return Column(
    children: [
      Container(
        color: Colors.black87,
        padding: const EdgeInsets.symmetric(vertical: 20, horizontal: 30),
        width: screenSize.width * 0.875,
        height: screenSize.height * 0.2125,
        margin: const EdgeInsets.only(bottom: 25),
        child: const CardFromInput(isEnabled: false),
      ),
      TextFormField(
        keyboardType: TextInputType.number,
        decoration: const InputDecoration(
          hintText: "Сумма",
          focusedBorder: UnderlineInputBorder(
              borderSide: BorderSide(color:DriveColors.lightBlueColor)
          ),
        ),
        textAlign: TextAlign.center,
        enabled: isEnabled,
        onChanged: (val) {
          final formValue = double.tryParse(val);
          if (formValue != null && formValue > 0) {
            setState(() {
              money = formValue;
            });
          }
        },
      ),
      const _MainViewErrorLabel(),
      BottomButton(
        title: "ОПЛАТИТЬ",
          onPressed: isEnabled ? () => bloc.add(PaymentBlocPayEvent(money)) : null,
      ),
    ]);
  }
}