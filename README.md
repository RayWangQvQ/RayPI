# RayPI

RayPI是一个基于.NET Core 3.1的极简风Web开发框架，支持领域驱动，并集成了基础的CRUD，开箱即用。

事实上，对于简单的项目，我们只需要定义Entity与DTO，即可自动化完成RESTful风格的WebApi接口开发。

## Features

* 极简，易读、易写、易扩展，避免过度封装
* 领域驱动（DDD）：同时支持贫血型和充血型模型，当需求很简单时完全可以当成三层架构去写
* 支持微服务架构
* 支持读写分离（CQRS）
* 集成CRUD
* 集成AutoMapper
* 集成EventBus（默认RabbitMQ实现）

欢迎star，代码或设计有可以优化的地方，也欢迎pr一起维护~
