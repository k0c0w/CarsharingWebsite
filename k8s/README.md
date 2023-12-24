Запуск кубера
1 Включить k8s в самом докере
2 Экспортировать dev сертификат aspnetapp.pfx в папку ./backend/certificates/https
	dotnet dev-certs https -ep <ПУТЬ ДО РЕПОЗИТОРИЯ>\backend\certificates\https\aspnetapp.pfx -p password12345 --trust
3 Для запуска локально нужно запушить в docker hub все images, которые используются в проекте. Название image 
"<your docker-hub name>/<imagename>:<tag(например: v1)>". Их вы будете указывать в файлах k8s
4 Подключать все deployment по шаблону "kubectl apply -f <filename>.yaml"
5 Если нужно  обновить какойто сервис, достаточно внести изменения в deployment и прописать "kubectl -f <filename>.yaml"
