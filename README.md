# Обзор
Системы, которые могут и должны быть запущены на сервере:

```c#
            //Could be done on server
            .Add(new PendingTargetProcessingSystem())
            .Add(new PlayerRotateToTargetSystem())
            .Add(new PlayerMoveToTargetSystem()) // Here target is removed. 
            .Add(new DoorButtonPlayerCollidingSystem())
```

# Общая идея разделения Сервера и Клиента
Системы разбиты на Client и Server. 
Основная логика и расчет всего происходит на стороне Server. 
Client часть используется:
 * для получения информации из Юнити сцены и заполнении сущностями ECS мир
 * также для обновления объектов на сцене на основе соответсвующих данных из ECS мира(сущностей и компонентов)
 
 В теории, если мы запускаем симулацию на сервере, то нам необходимо будет реализовать системы, которые заполнят ECS мир сущностями на основе своей "серверной сцены".

```c#

        _world = new EcsWorld();
        _systems = new EcsSystems(_world, _ecsGameShared);
        _systems
                //Client side systems
            .Add(new PlayerInitSystem())
            .Add(new DoorsInitSystem())
            .Add(new TimeSystem())
            .Add(new PendingTargetMouseClickSystem())
                
            //Could be done on server
            .Add(new PendingTargetProcessingSystem())
            .Add(new PlayerRotateToTargetSystem())
            .Add(new PlayerMoveToTargetSystem()) // Here target is removed. 
            .Add(new DoorButtonPlayerCollidingSystem())
            
            //Client side views updates
            .Add(new UpdatePlayerViewSystem())
            .Add(new UpdateDoorViewPositionSystem())
            .Init();
```

Ещё важно на сервере реализовать часть симулации времени. На клиенте за это отвечает `TimeSystem`.
 `TimeSystem` заполняет `TimeService` необходимыми данными о времени из Юнити:
 ```c#
        public float Time;
        public float DeltaTime;
        public float UnscaledDeltaTime;
        public float UnscaledTime;
```
На сервере это может быть какая-нибудь своя симуляция времени.

# Zenject

Либа Zenject присутствует, но пока ей не нашел применения в таком маленьком проекте. В теории можно было бы объекты из юнити инжектить куда надо через Zenject, но там сейчас не так много всего, чтобы заморачиваться этим.

# Потраченное время

8ч на разработку
4ч на изучение и эксперименты с ECSLite, т.к. до этого не было опыта с этим фреймворком.
