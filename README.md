# Battle Simulator

Симулятор сражений двух армий на Unity, похожий на Army Clash.

---

## Архитектура

### Entry Point
Игра начинается с [EntryPoint](https://github.com/RomanSharipov/Z-media-Test-Task/blob/main/Assets/_Project/Scripts/Infrastructure/EntryPoint/EntryPoint.cs). Все стейты регистрируются в [StatemachineInstaller](https://github.com/RomanSharipov/Z-media-Test-Task/blob/main/Assets/_Project/Scripts/Infrastructure/ServicesInstallers/StatemachineInstaller.cs) через VContainer.

Основной игровой стейт — [GameLoopState](https://github.com/RomanSharipov/Z-media-Test-Task/blob/main/Assets/_Project/Scripts/Infrastructure/GameStateMachine/States/GameLoopState.cs)

---

## Конфигурация

### Battle Config
Основной конфиг с количеством юнитов и разбросом при спавне:

<img width="994" height="481" alt="image" src="https://github.com/user-attachments/assets/01f5cd2f-fee5-4980-a447-a764975f4d3e" />

### Base Stats
Базовые статы реализованы как словарь `<StatType, float>` — легко добавить или убрать любую характеристику:

<img width="835" height="490" alt="image" src="https://github.com/user-attachments/assets/0695d5dd-9bd2-4a37-b1ea-ad4d4751a910" />

Статы собираются в [UnitDataBuilder](https://github.com/RomanSharipov/Z-media-Test-Task/blob/main/Assets/_Project/Scripts/Helpers/UnitDataBuilder.cs) — принимает все модификаторы и возвращает готовую структуру.

---

## Расширяемость

**Добавление новых форм, цветов и размеров не требует изменения кода.**

| Тип | Как добавить |
|-----|--------------|
| **Цвет** | Create → BattleSim → Modifiers → Color → добавить в UnitConfigDatabase |
| **Форма** | Создать префаб → Create → BattleSim → Modifiers → Shape → добавить в UnitConfigDatabase |
| **Размер** | Create → BattleSim → Modifiers → Size → добавить в UnitConfigDatabase |
| **Новый стат** | Добавить в enum `StatType` → использовать в конфигах |

Сборка юнита происходит в [WarriorFactory](https://github.com/RomanSharipov/Z-media-Test-Task/blob/main/Assets/_Project/Scripts/Infrastructure/Services/UnitFactory/WarriorFactory.cs).

---

## Unit Management

Все активные юниты регистрируются в [WarriorsOnLevel](https://github.com/RomanSharipov/Z-media-Test-Task/blob/main/Assets/_Project/Scripts/Infrastructure/Services/WarriorsOnLevel/WarriorsOnLevel.cs) — централизованный доступ к живым юнитам, упрощённая логика завершения боя, безопасное удаление при смерти. Никаких `Find` или `GetComponentsInChildren`.

---

## Unit Behaviour

Поведение юнитов реализовано через простую state machine в коде — без сторонних ассетов, с полным контролем над логикой.

В production-проекте для сложного AI предпочёл бы **Behaviour Designer** или **Unity Behaviour**.

---

## Дополнительная функция

**Счётчик юнитов** — отображение количества живых юнитов каждой команды в реальном времени через подписку на `WarriorsOnLevel.OnWarriorCountChanged`.

---
## Dependency Injection

- **Сервисы** — [AllServicesInstaller](https://github.com/RomanSharipov/Z-media-Test-Task/blob/main/Assets/_Project/Scripts/Infrastructure/ServicesInstallers/AllServicesInstaller.cs)
- **Конфиги** — [ConfigsInstaller](https://github.com/RomanSharipov/Z-media-Test-Task/blob/main/Assets/_Project/Scripts/Infrastructure/Configs/ConfigsInstaller.cs)

Быстрое добавление новых сервисов и доступ к конфигам из любого места.

---

## Object Pooling

Не используется намеренно — при фиксированном количестве юнитов не даёт существенного выигрыша.

При масштабировании легко добавить через [NightPool](https://github.com/MeeXaSiK/NightPool) — замена `Instantiate`/`Destroy` на методы библиотеки.

---

## Время выполнения

| День | Часы |
|------|------|
| 1 | 5 |
| 2 | 5 |
| 3 | 3 |
| **Итого** | **13 часов** |
