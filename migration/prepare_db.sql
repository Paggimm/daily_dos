create database dailydos;
use dailydos;
create table user(id int auto_increment primary key, name varchar(40), password varchar(20));
insert into user (name, password) values ('bernie', '123456'),('nico', '654321');
create User 'admin'@'localhost' identified by 'admin';
grant all privileges on * to 'admin'@'localhost';
