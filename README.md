# 📦 Warehouse Management System

## 📌 Opis projektu

Warehouse Management System to aplikacja desktopowa stworzona w technologii **.NET (WinForms)**, służąca do zarządzania stanem magazynowym produktów.

Aplikacja umożliwia:

* przeglądanie listy produktów,
* dodawanie nowych produktów,
* edytowanie istniejących produktów,
* usuwanie produktów,
* wizualizację stanu magazynowego (kolorowanie wierszy),
* zapisywanie historii operacji do pliku `log.json`.

Projekt został stworzony jako przykład aplikacji CRUD z wykorzystaniem:

* **WinForms (interfejs użytkownika)**
* **Microsoft SQL Server (baza danych)**
* **ADO.NET (obsługa bazy danych)**

---

## 🧰 Technologie

* .NET 8 (WinForms)
* Microsoft SQL Server
* ADO.NET
* JSON (konfiguracja + logi)

---

## ⚙️ Funkcjonalności

### 📋 Zarządzanie produktami

* Dodawanie produktu (nazwa, ilość, cena)
* Edycja produktu
* Usuwanie produktu

### 🎨 Wizualizacja danych

* 🔴 Czerwony – brak produktu (<= 0)
* 🟡 Żółty – niski stan (< 10)
* 🟢 Zielony – dostępny

### 📝 Logowanie operacji

Każda operacja (dodanie, edycja, usunięcie) zapisywana jest w pliku:

```id="1k9u3l"
log.json
```

---

## 🗄️ Konfiguracja bazy danych (Microsoft SQL Server)

W projekcie znajduje się plik:

```id="c9k2za"
database.sql
```

który zawiera:

* utworzenie bazy danych
* utworzenie tabeli `Products`
* przykładowe dane

---

### Jak uruchomić bazę danych

1. Otwórz **SQL Server Management Studio (SSMS)**

2. Połącz się ze swoim serwerem SQL Server

3. Otwórz plik:

```id="0f3kzq"
database.sql
```

4. Kliknij **Execute (F5)**

Po wykonaniu skryptu baza danych zostanie automatycznie utworzona i uzupełniona danymi.

---

### Konfiguracja connection stringa

W pliku:

```id="m9d3zs"
config.json
```

ustaw swój connection string:

```json id="8z7lqp"
{
  "ConnectionString": "Server=YOUR_SERVER_NAME;Database=WarehouseDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

#### Przykład:

```json id="p2x8av"
{
  "ConnectionString": "Server=localhost;Database=WarehouseDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

---

## ▶️ Uruchomienie aplikacji

1. Sklonuj repozytorium:

```bash id="x3z1ql"
git clone https://github.com/twoj-login/warehouse-management-system.git
```

2. Otwórz projekt w **Visual Studio**

3. Upewnij się, że:

   * SQL Server działa
   * wykonałeś plik `database.sql`
   * `config.json` ma poprawny connection string

4. Uruchom projekt (`F5`)

---

## 🧠 Struktura projektu

```id="z4t9yw"
/Configuration
  Config.cs

/Data
  Database.cs
  ProductRepository.cs

/Forms
  Form1.cs
  AddProductForm.cs

/Models
  Product.cs
  Log.cs
```

---

## 🧩 Architektura

Projekt wykorzystuje prostą separację warstw:

* **UI (WinForms)** – interfejs użytkownika
* **Repository (ProductRepository)** – dostęp do danych
* **Model (Product, Log)** – struktury danych
* **Configuration** – konfiguracja połączenia

---

## 👨‍💻 Autor

Projekt wykonany jako część nauki technologii:

* .NET / WinForms
* SQL Server
* aplikacje desktopowe

---

## 📄 Licencja

Projekt edukacyjny – do dowolnego użytku.
