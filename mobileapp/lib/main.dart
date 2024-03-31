import 'package:flutter/material.dart';
import 'package:mobileapp/Components/appbar.dart';
import 'package:mobileapp/Components/bottom_button.dart';
import 'package:mobileapp/Components/form_input_subpage.dart';

void main() {
  runApp(const DriveApp());
}

class DriveApp extends StatelessWidget {
  const DriveApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Drive',
      theme: ThemeData(
        useMaterial3: true,
      ),
      //just for example
      home: DriveDateInputSubpage (
        inputTitle: "DAte",
        hintText: "date",
        onSavePressed: (val){
          print(val);
        },
        initialValue: DateTime(2014),
),
      routes: {
        '/home': (context) => throw UnimplementedError("Implement page"),
        '/profile': (context) => throw UnimplementedError("Implement page"),
        '/subscriptions': (context) => throw UnimplementedError("Implement page"),
        '/payment': (context) => throw UnimplementedError("Implement page"),
        '/enter': (context) => throw UnimplementedError("Implement page"),
        '/register': (context) => throw UnimplementedError("Implement page"),
      }
    );
  }
}

