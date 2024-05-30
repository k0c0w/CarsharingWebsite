// See https://aka.ms/new-console-template for more information
using SubscriptionBackgroundworker;


var worker = new Worker(new FileLogger());
await worker.Run();
