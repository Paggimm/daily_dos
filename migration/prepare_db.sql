create table if not exists users (
	id serial primary key,
	name varchar(30) unique not null,
	password varchar(100) not null
);
insert into users(name, password)
values
	('bernie', 'AQAAAAEAACcQAAAAEN6JyCrrWIJ7Hs2rZYK+h+ZBOzp3Jrlo9q4QyI4+fqpmeQsUuiTx2AF/WYhdkyS7vA=='),
	('nico', 'AQAAAAEAACcQAAAAEGdMk6L4pkIgw3HIgfvzmWB2LrUpOHWwIcPZyoVFn4nraoNuoY9DXIp9M4hUeMjrUw==')
