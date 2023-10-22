Чтобы запустить бэк в докере:
1. Экспортировать dev сертификат в разделяемую папку .aspnet/https
	dotnet dev-certs https -ep C:\Users\<USER>\.aspnet\https\Carsharing.pfx -p <Пароль> --trust
2. Указать использованный ранее пароль от хэша для Kestrel
	dotnet user-secrets set "Kestrel:Certificates:Development:Password" "<Пароль>" --project <Путь к проекту Carsharing.csproj>
3. Перейти в директорию с .yml прописать
	docker-compose up -d