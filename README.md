# profile-analytics

## Описание

**profile-analytics** — это веб-приложение для аналитики профиля клиента и интеллектуальных рекомендаций товаров/услуг на основе истории покупок. 

### Проблема

Компания теряет до 30% потенциального дохода из-за нерелевантных рекомендаций. Существующая система:
- Не учитывает индивидуальное поведение клиентов;
- Показывает универсальные популярные товары;
- Имеет конверсию в покупку всего 5%.

Пример: клиенту, купившему фотоаппарат, система рекомендует зубную пасту вместо объективов.

### Цель

Разработать MVP системы рекомендаций, которая:
- На основе истории покупок клиента предлагает релевантные товары и услуги;
- Использует алгоритмы Apriori (поиск ассоциативных правил) или коллаборативную фильтрацию (учет интересов других пользователей);
- Увеличивает конверсию рекомендаций за счет предложения «правильных» товаров;
- Снижает время выбора товара для клиента.

**Ожидаемый результат:**
- MVP системы рекомендаций, интегрированной в аналитическую платформу.

---

## Описание идеи

#TODO описать предлагаемое решение

## API backend
      ///
      List<JsonResult> GetContrpartnerByName(String name)
      List<JsonResult> GetContrpartnerByInn(long inn)
      List<JsonResult> GetContrpartnerByDivision(String division)
      List<JsonResult> GetContrpartnerByWarehouse(String warehouse)
      List<JsonResult> GetAssortments()
      List<JsonResult> GetSaleDocumentsByContrpartner(long id)
      JsonResult GetTnsByContrpartner(long id)
      List<JsonResult> GetTnsByMonths(long id)
      List<JsonResult> GetTnsBySuppliers(long id)
      List<JsonResult> GetAssortmentApriori(long id)
      List<JsonResult> GetFrequentlyAssortment()
      List<JsonResult> GetFrequentlyAssortmentByContrpartner(long id)

## Структура проекта

#TODO описать остальное
```
profile-analytics/
│
├── frontend/
│   │
│   ├── public/                # Статические файлы и шаблон index.html
│   ├── src/                   # Исходный код приложения
│   │   ├── assets/            # Изображения и медиа-ресурсы
│   │   ├── components/        # Реакт-компоненты
│   │   ├── api.ts             # API-запросы к серверу
│   │   ├── App.tsx            # Главный компонент приложения
│   │   ├── index.tsx          # Точка входа React
│   │   ├── index.css          # Глобальные стили
│   │   └── ...                # Прочие служебные файлы
│   ├── package.json           # Описание зависимостей и скриптов
│   └── tsconfig.json          # Конфигурация TypeScript
├── backend/
│   │
│   └── Horizont2
|        |---Program.cs        # Файл для запуска проекта
|        |---Startup.cs        # Конфигурация при запуске
|        |---appSetting.json   # Основные настройки конфигурации
|        |---Connection/       # Объекты для подключения к БД и маппинга через EntityFramework 
│        |---Models/           # Классы для связи с БД, основные сущности системы 
|        |---Services/         # Сервис для работы с данными
|        |---Controllers       # Контроллер для предоставления API front-end 
│
└── README.md              # Документация (вы читаете её)
```

---

## Контакты

Александр Куцепалов (GH:@Alexkucepalov, TG: @alexkucep) - Frontend, дизайн сайта и презентации.
Аршина Альфия (GH, TG: @sindzirarenai) - Backend, работа с данными.
Александр Воробьев (GH: kadooqq, TG: kadoqq) - DevOps, идеи, тимлидинг.
Леонид Фролов (TG: @greedyleo) - Аналитика, презентация.
Алим Махмудов (TG: @rey_mua) - Помощь.

---

## Примечания

- Проект не содержит исходных данных, предоставленных организаторами.
- Все изображения и логотипы используются только в рамках данного проекта.
- Проект поддерживает современные браузеры.

---

## TODO

- Описать предлагаемое решение.
- Добавить сборку Docker-образа для frontend приложения.
- Добавить сборку и поднятие контейнера с БД (с предустановленными данными, если можно публиковать).
- Добавить сборку / поднятие контейнеров при помощи docker-compose.
- CI/CD в GitHub actions?
