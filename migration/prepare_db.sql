create table if not exists users (
	id serial primary key,
	name varchar(30) unique not null,
	password varchar(100) not null
);
insert into users(name, password)
values
	('bernie', '123456'),
	('nico', '654321')
