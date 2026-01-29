Игра начинается с [EntryPoint](https://github.com/RomanSharipov/Z-media-Test-Task/blob/main/Assets/_Project/Scripts/Infrastructure/EntryPoint/EntryPoint.cs)
Все стейты от стейт машины регистрируються в [StatemachineInstaller](https://github.com/RomanSharipov/Z-media-Test-Task/blob/main/Assets/_Project/Scripts/Infrastructure/ServicesInstallers/StatemachineInstaller.cs) в VContainer - для легкой передачи в них данных или настройки перед входом в стейт
Основной игровой стейт [GameLoopState](https://github.com/RomanSharipov/Z-media-Test-Task/blob/main/Assets/_Project/Scripts/Infrastructure/GameStateMachine/States/GameLoopState.cs)
Основной конфиг BattleConfig <img width="994" height="481" alt="image" src="https://github.com/user-attachments/assets/01f5cd2f-fee5-4980-a447-a764975f4d3e" />
с количеством юнитов у каждый команды и разбросом юнитов

Базовые статы игрока <img width="835" height="490" alt="image" src="https://github.com/user-attachments/assets/0695d5dd-9bd2-4a37-b1ea-ad4d4751a910" />
Сделал базовые статы игрока словарем c enum ключем и float значением - что бы в случае небходимости можно было лего добавить или убрать стату 
Статы считаются в [UnitDataBuilder](https://github.com/RomanSharipov/Z-media-Test-Task/blob/main/Assets/_Project/Scripts/Helpers/UnitDataBuilder.cs) у которого есть публичный метод Build - который принимаетвсе модификаторы характеристик и возвращает готовую структуру со статами

Расширяемость системы юнитов
Добавление новых форм, цветов и размеров реализовано через ScriptableObject конфиги. Для добавления нового типа не требуется изменение кода.
Добавление нового цвета

ПКМ в папке Configs → Create → BattleSim → Modifiers → Color
Заполнить Id, Color, модификаторы статов
Добавить в UnitConfigDatabase в массив Colors

Добавление новой формы

Создать префаб с компонентами Warrior, WarriorView, Movement и т.д.
ПКМ → Create → BattleSim → Modifiers → Shape
Указать префаб и модификаторы статов
Добавить в UnitConfigDatabase в массив Shapes

Добавление нового размера

ПКМ → Create → BattleSim → Modifiers → Size
Указать Scale и модификаторы статов
Добавить в UnitConfigDatabase в массив Sizes

Добавление нового стата

Добавить значение в enum StatType
Использовать в конфигах и в коде юнита

Затем в [WarriorFactory](https://github.com/RomanSharipov/Z-media-Test-Task/blob/main/Assets/_Project/Scripts/Infrastructure/Services/UnitFactory/WarriorFactory.cs) это все собирается в готового юнита

Все сервисы регистрируются в [AllServicesInstaller](https://github.com/RomanSharipov/Z-media-Test-Task/blob/main/Assets/_Project/Scripts/Infrastructure/ServicesInstallers/AllServicesInstaller.cs) - так очень быстро добавлять новый сервис 
Конфиги тоже регистририруются в VContainer [ConfigsInstaller](https://github.com/RomanSharipov/Z-media-Test-Task/blob/main/Assets/_Project/Scripts/Infrastructure/Configs/ConfigsInstaller.cs) - таких легко получать конфиги из любого места в коде
Базовый класс юнита [Warrior](https://github.com/RomanSharipov/Z-media-Test-Task/blob/main/Assets/_Project/Scripts/CoreGamePlay/Unit/Warrior.cs)
 
