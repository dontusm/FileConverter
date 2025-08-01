# FileConverter API

**FileConverter** — это веб-сервис на ASP.NET Core (.NET 9) для конвертации файлов в PDF и чтения содержимого PDF-файлов. Он реализован с учетом масштабируемости и возможности добавления новых форматов файлов в будущем. В текущей версии поддерживаются текстовые файлы (`text/plain`) и PDF (`application/pdf`).

## Endpoints

### `POST /api/files/convert-to-pdf`

Принимает текстовый файл (`.txt`) и возвращает его PDF-версию.

**Параметры запроса:**

- `file` (form-data): файл с MIME-типом `text/plain`

**Ответ:**

- `200 OK`: PDF-файл в бинарном виде
- `400 Bad Request`: если файл пустой, отсутствует или имеет неподдерживаемый тип

---

### `POST /api/files/read-pdf`

Принимает PDF-файл и возвращает его текстовое содержимое.

**Параметры запроса:**

- `file` (form-data): файл с MIME-типом `application/pdf`

**Ответ:**

- `200 OK`: JSON с текстом из PDF
- `400 Bad Request`: если файл пустой, отсутствует или имеет неподдерживаемый тип

---

## Запуск проекта

1. Клонируйте репозиторий:

```bash
git clone https://github.com/your-username/FileConverter.git
cd FileConverter
```

2. Запустите проект:

```bash
dotnet run --project FileConverter.Api
```

3. Перейдите в браузер по адресу:

```
https://localhost:5001/swagger
```


