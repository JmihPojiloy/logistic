Дипломный проект - приложение по управлению логистикой предприятия.
Реализован ограниченный функционал по оформлению заказа и эмуляция оплаты.
Проект состоит из трех частей: БД АПИ и клиентская часть. 
Смонтированы docker образы для АПИ и клиентской части.
В docker-compose установлены зависимости: БД бэк и фронт.
Запускается командой docker-compose up --build -d
Клиентская часть доступна по адресу http://localhost:4000
