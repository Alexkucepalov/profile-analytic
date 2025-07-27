# Backend приложение сервиса profile-analytic

## API backend
//Получение списка контрагентов по имени. Учитывается вхождение без учета регистра. 
List<JsonResult> GetContrpartnerByName(String name)
---------------------------------------------------------  
//Получение списка контрагентов по ИНН. учитывается вхождение.
List<JsonResult> GetContrpartnerByInn(long inn)
---------------------------------------------------------        
//Получение списка контрагентов по региону. Учитывается вхождение.
List<JsonResult> GetContrpartnerByDivision(String division)
---------------------------------------------------------        
 //Получение списка контрагентов по складу. Учитывается вхождение строки.
List<JsonResult> GetContrpartnerByWarehouse(String warehouse)
---------------------------------------------------------        
//Получение списка сортамента. 
List<JsonResult> GetAssortments()
---------------------------------------------------------       
//Получение документов продаж по контрагенту.
List<JsonResult> GetSaleDocumentsByContrpartner(long id)
---------------------------------------------------------        
//Получение общего веса всех покупок по конкретному контрагенту 
JsonResult GetTnsByContrpartner(long id)
---------------------------------------------------------  
//Получение объема 
List<JsonResult> GetTnsByMonths(long id)
---------------------------------------------------------  
//Получение обьема всех покупок конкретного контрагента в аналитике по месяцам 
List<JsonResult> GetTnsBySuppliers(long id)
---------------------------------------------------------  
// Получение рекомендаций для контрагента на основе алгоритма априори
List<JsonResult> GetAssortmentApriori(long id)
---------------------------------------------------------        
//Получение часто покупаемых сортаментов
List<JsonResult> GetFrequentlyAssortment()
---------------------------------------------------------       
//Получение часто покупаемых сортаментов по характеристикам конкретного контрагента (регион)
List<JsonResult> GetFrequentlyAssortmentByContrpartner(long id)
---------------------------------------------------------  
# Сборка

Сборка приложения осуществляется в Docker-контейнере. Для запуска сборки необходимо вызвать команду из корня проекта:

```bash
docker build --pull --rm -f 'backend/Dockerfile' -t 'profileanalytic-backend:latest' 'backend'
```

Если Docker не установлен, это можно сделать разными способами на разных платформах: https://docs.docker.com/engine/install/.

На платформах с менеджером пакетов APT, это можно сделать командой:
```bash
sudo apt install docker
```

# Запуск

Запуск приложения, собранного в Docker-образ, осуществляется командой:
```bash
docker run -p 5000:5000 -p 5001:5001 -e ASPNETCORE_URLS="http://+:5000/" profileanalytic-backend:latest -d
```

Для указания определенных данных для подключения к БД:
```bash
docker run -p 5000:5000 -p 5001:5001 -e ASPNETCORE_URLS="http://+:5000/" -e ConnectionStrings__Horizons="Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=password" profileanalytic-backend:latest -d
```

#TODO Для запуска приложения в режиме разработки (например, для доступа к интерфейсу Swagger или отладке ошибок), можно прокинуть переменную окружения:
```bash
docker run -p 5000:5000 -p 5001:5001 -e ASPNETCORE_URLS="http://+:5000/" -e ASPNETCORE_ENVIRONMENT=Development profileanalytic-backend:latest -d
```

После успешного запуска контейнера, приложение будет висеть на адресе http://localhost:5000/home/, куда вы можете перейти через браузер.

Но более наглядным будет проверить работу API, вписав в адресную строку браузера: http://localhost:5000/home/GetContrpartnerByName?name=%22алроса%22 или любой другой запрос.

# Перечень запросов к API

#TODO поднять Swagger в контейнере... Ругается на OpenAPI :C Но можно, при большом желании, посмотреть в backend/Controllers/HomeControllers.cs или в браузере по ссылке http://localhost:5000/swagger/v1/swagger.json в виде JSON.
