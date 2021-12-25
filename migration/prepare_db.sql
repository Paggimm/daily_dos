create table if not exists users (
	id serial primary key,
	name varchar(30) unique not null,
	password varchar(30) not null
);
insert into users(id, name, password)
values 
	(1, 'bernie', '123456'),
	(2, 'nico', '654321')