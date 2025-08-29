# Finance Tracker Web

[![.NET](https://img.shields.io/badge/.NET-7.0-purple)](https://dotnet.microsoft.com/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-15-blue)](https://www.postgresql.org/)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-blue)](https://getbootstrap.com/)
[![License](https://img.shields.io/badge/License-MIT-green)](LICENSE)

**Finance Tracker Web** — это веб-приложение для управления личными финансами, построенное на ASP.NET Core 7.0 с использованием Razor Pages. Приложение позволяет пользователям отслеживать доходы и расходы, управлять категориями транзакций и контролировать бюджет.

![Finance Tracker](https://img.shields.io/badge/Finance-Tracker-success)

##  Основные возможности

- **Учет транзакций**: Добавление, редактирование и удаление доходов и расходов
- **Управление категориями**: Создание и настройка категорий с установкой месячных лимитов
- **Контроль бюджета**: Мониторинг превышения установленных лимитов по категориям
- **Аутентификация пользователей**: Система регистрации и входа на основе ASP.NET Core Identity
- **Адаптивный интерфейс**: Современный UI с использованием Bootstrap 5 и анимациями
- **Уведомления**: Предупреждения о превышении бюджетных лимитов

## Технологический стек

- **Backend**: ASP.NET Core 7.0, Razor Pages, C#
- **Database**: PostgreSQL с Entity Framework Core
- **Frontend**: Bootstrap 5.3, Bootstrap Icons, Animate.css
- **Authentication**: ASP.NET Core Identity
- **Validation**: Data Annotations, jQuery Validation

##  Установка и запуск

### Предварительные требования

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [PostgreSQL](https://www.postgresql.org/download/) (версия 12+)
- Git

### Шаги установки

1. **Клонирование репозитория**
   ```bash
   git clone <repository-url>
   cd FinanceTrackerWeb
Настройка базы данных

## ⚙️ Настройка базы данных

1. **Создайте базу данных PostgreSQL**  
2. **Обновите строку подключения** в `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=FinanceTracker;Username=your_username;Password=your_password"
}
Примените миграции базы данных:

bash
Копировать код
dotnet ef database update
▶️ Запуск приложения
bash
Копировать код
dotnet run
Откройте в браузере:
👉 https://localhost:7000 (или http://localhost:5000)

🗄️ Структура базы данных
Основные сущности:

Users — пользователи системы (наследуется от IdentityUser)

Categories — категории транзакций с возможностью установки месячного лимита

Transactions — финансовые операции (доходы/расходы)

Миграции:

bash
Копировать код
# Создание новой миграции
dotnet ef migrations add <MigrationName>

# Обновление базы данных
dotnet ef database update
🎯 Использование
🔑 Регистрация и вход
Зарегистрируйте новую учетную запись через страницу Register

Войдите в систему, используя свои учетные данные

📂 Управление категориями
Перейдите в раздел Управление категориями

Создайте категории для доходов и расходов

Установите месячные лимиты для расходов

➕ Добавление транзакций
На главной странице нажмите Новая транзакция

Заполните сумму, дату, выберите тип и категорию

Добавьте описание (опционально)

📊 Просмотр и анализ
На главной странице отображается статистика и последние транзакции

Используйте фильтры для просмотра по категориям

Следите за предупреждениями о превышении лимитов

📁 Структура проекта
bash
Копировать код
FinanceTrackerWeb/
├── Data/
│   └── AppDbContext.cs          # Контекст базы данных
├── Models/
│   ├── Category.cs              # Модель категории
│   ├── Transaction.cs           # Модель транзакции
│   └── User.cs                  # Модель пользователя
├── Pages/
│   ├── Index.cshtml             # Главная страница
│   ├── Transactions/            # Страницы управления транзакциями
│   ├── Categories/              # Страницы управления категориями
│   ├── Budget/                  # Страницы управления бюджетом
│   └── Account/                 # Страницы аутентификации
├── Program.cs                   # Конфигурация приложения
└── FinanceTrackerWeb.csproj     # Файл проекта
⚙️ Конфигурация
appsettings.json:

json
Копировать код
{
  "ConnectionStrings": {
    "DefaultConnection": "Ваша_строка_подключения_PostgreSQL"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
Identity (аутентификация):

Минимальная длина пароля: 8 символов

Обязательные символы: цифры, строчные и прописные буквы, специальные символы

Блокировка: после 5 неудачных попыток входа


## Автор

**StrafeStreiv**
