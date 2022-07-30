/* USER TABLE */
create table if not exists users (
	"id" serial primary key,
	"name" varchar(30) unique not null,
	"password" varchar(100) not null
);
/* ACTIVITIES TABLE */
create table if not exists activities (
	"id" serial primary key,
	"userId" integer references users(id),
	"name" varchar(30) not null,
	"minDuration" integer,
	"maxDuration" integer,
	"weekdayConstraint" VARCHAR(7),
	"recurringType" VARCHAR(10),
	"recurringInterval" integer,
	"createTime" timestamp
);
/* PLANS TABLE */
create table if not exists activities (
    "id" serial primary key,
    "userId" integer references users(id),
    "activityId" integer references activities(id),
    "duration" integer,
    "date" timestamp,
    "repeatable" VARCHAR(10),
    "createTime" timestamp
);
/* USER DATA */
insert into users(name, password)
values
	/* password: 123456*/
	(
		'bernie',
		'AQAAAAEAACcQAAAAEN6JyCrrWIJ7Hs2rZYK+h+ZBOzp3Jrlo9q4QyI4+fqpmeQsUuiTx2AF/WYhdkyS7vA=='
	),
	/* password: 654321*/
	(
		'nico',
		'AQAAAAEAACcQAAAAEGdMk6L4pkIgw3HIgfvzmWB2LrUpOHWwIcPZyoVFn4nraoNuoY9DXIp9M4hUeMjrUw=='
	);
