import 'package:flutter/material.dart';
import 'package:mobileapp/Components/appbar.dart';

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
      home: MainPage(),
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

class MainPage extends StatelessWidget {

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      // пример внедрения AppBar
      appBar: DriveAppBar(title: "Главная"),
    );
  }
}
