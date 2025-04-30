# Автоматизированная система для учёта электронных запчастей для БПЛА

## 🚀 Начало работы

### 📥 Восстановление базы данных

1. Откройте **Microsoft SQL Server Management Studio**
2. Восстановите БД из бэкапа, находящегося в папке `DB` проекта

### ⚙️ Настройка подключения

Замените строку подключения в коде:

```csharp
public SqlConnection connection = new SqlConnection("Data Source=Имя_Сервера; Initial Catalog=Meteor; Integrated Security=True");
```

### 🏆 Готово
