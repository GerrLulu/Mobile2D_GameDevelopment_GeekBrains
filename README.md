# Mobile2D_GameDevelopment
## Home work for GeekBrains


### Урок 1. Создание контроллера персонажа
#### Основные задания:
1. Добавьте новое состояние игры GameState - Settings. Для перехода в этот режим добавим соответствующую кнопку на экран MainMenuView, нажатие на которую будет менять состояние игры. На экране настроек создадим единственную кнопку Back, которая будет возвращаться нас обратно в MainMenuView.
2. Реализуйте новый тип управления - стрелками на клавиатуре. При нажатии клавиши ← должно происходить движение влево, а при нажатии → движение вправо.


### Урок 2. Создание игровой сборки и внедрение мобильных плагинов
#### Основные задания:
1. Собрать apk-файл для Android (нужно прикрепить к дз).
2. Для аналитики добавить новое событие, отвечающее за начало уровня (т.е. при запуске GameController) и вызвать его в соответствующий момент.
3. Для рекламы реализовать проигрыватель Rewarded рекламы. Добавить новую кнопку на MainMenuView, по нажатию на которую будет запускаться показ Rewarded рекламы.
4. Для внутриигровых покупок реализовать покупку какого-нибудь предмета. Покупку совершать по нажатию на кнопку, которую так же необходимо добавить на MainMenuView.
Дополнительные задания:
1. Реализовать для всех интегрированных сервисов паттерн "Одиночка" и убрать прямые ссылки на них через инспектор в коде.
2. Для аналитики реализовать событие о покупке предмета, используя метод Analytics.Transaction
(https://docs.unity3d.com/ScriptReference/Analytics.Analytics.Transaction.html). Можно встроить в IAPService. Примечание: чтобы быть уверенными,
что созданные методы выполняются, можно добавить в них логгирование.


### Урок 3. Создание способностей и предметов экипировки
#### Основные задания:
1. Добавьте новую характеристику для транспорта - высота прыжка (JumpHeight), как это было ранее сделано для скорости. Соответственно потребуется дополнить модель транспорта. Эту настройку мы сможем модифицировать нашими улучшениями.
2. Создайте свои элементы экипировки (2-3 штуки, например, тормозную систему или трансмиссию).
3. Создайте свои улучшения для новых элементов экипировки (2-3 штуки, в т.ч. реализуйте улучшение для высоты прыжка).
Дополнительное задание:
4. * Реализуйте способность "Прыжок", которая позволит машинке отрываться от земли на какое-то время. Можно использовать физику. Можно без анимаций и плавности. Главное не реализовать сам прыжок, а реализовать новую способность.


### Урок 4. Сборка проекта. Рефакторинг
#### Основные задания:
1. Создайте для начальных настроек игры из EntryPoint конфигурационный ScriptableObject и используйте данные из него.
2. Используйте принцип внедрения зависимостей для гаража (ShedController). Будьте внимательны к методам AddController и прочим, учитывайте когда и какой контроллер должен быть создан и уничтожен.
Дополнительное задание:
1. Попросите однокурсника провести code review одного из ваших домашних заданий. Или станьте тем, кто этот code review проведёт. В качестве факта выполнения прикрепите скриншот рецензии.


### Урок 5. Создание ИИ оппонента
#### Основные задания:
1. Попробуйте изменить формулу расчёта силы врага в Enemy.Power.
2. Добавьте кнопки, которые будут менять уровень преступности игрока. Если уровень преступности:
0-2, то появляется кнопка «пройти мирно» и можно отказаться от боя с врагом.
3-5, то доступен только бой с врагом.


### Урок 6. Создание наградных предметов
Создайте аналогичную сцену, на которой будет функционировать окно еженедельных наград. Постарайтесь переиспользовать как можно больше классов из ежедневных наград. Представьте, что вы занимаетесь расширением механики ежедневных наград на реальном проекте и вам нужно сохранить предыдущий функционал, создать новый и минимизировать дублирование кода.


### Урок 7. Знакомство с твиннерами
#### Основные задания:
1. Сделайте возможность из редактора Unity запускать и останавливать твин-анимацию для кнопок (через ContextMenu) (для остановки потребуется метод Kill от DOTween, см. документацию) (работать будет только в Playmode).
2. Модифицируйте компонент кастомной кнопки новым параметром (полем), который будет отображаться в инспекторе. (и для варианта с наследованием, и для варианта с композицией)
Дополнительное задание:
3. *Реализуйте твин-анимацию для кнопок активации способностей в игре с машинкой.


### Урок 8. Сборка проекта. Рефакторинг
#### Основное задание:
1. Добавьте возможность вернуться из игры в основное меню. Для этого создайте новую свзку View-Controller. На View расположите кнопку выхода из состояния Game.
Дополнительное задание:
2. * Добавьте новую кнопку, которая будет открывать меню паузы в игре. В этом меню потребуется две кнопки "Continue" для перехода в игру и "Menu" для перехода в основное меню игры.


### Урок 9. Знакомство с Asset Bundle и Adressables
#### Основные задания:
1. Соберите сцену или используйте существующую для реализация следующей механики: нажатие на некоторую кнопку приводит к её деактивации (interactable = false) и запускает механизм загрузки бандла, в котором хранится картинка для этой кнопки. По завершении загрузки бандла картинка устанавливается в качестве фона для деактивированной кнопки.
2. Добавьте на сцену две кнопки: AddBackground и RemoveBackground. Первая будет загружать некоторое фоновое изображение для канваса на сцене, используя Addressables. Вторая будет выгружать фоновое изображение (.sprite = null и Addressables.Release).
#### Дополнительное задание:
3. Проанализируйте и выпишите в txt-файл время загрузки ассетов из asset bundles при разных форматах сжатия.
p.s. Для хранения бандлов можно использовать как Google Drive, так и отдельный сервер, если такой имеется в вашем распоряжении.


### Урок 10. Пуш уведомления и локализация
#### Основные задания:
1. Реализуйте ещё одно Push-уведомление.
2. Локализуйте текст на главном экране проекта с машинкой (можно только en и ru).
Дополнительные задания:
1. Локализуйте картинки кнопок на главном экране проекта с машинкой (для разных языков подставлять разные картинки).
