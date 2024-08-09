--CREATE EXTENSION IF NOT EXISTS "uuid-ossp";
create table account (
    id uuid not null,
    user_id uuid not null,
    exchange_id int not null,
    primary key (id)
);

create table api_key (
    id int not null generated always as identity,
    private_key varchar(100) not null,
    public_key varchar(100) not null,
    exchange_id int not null,
    user_id uuid not null,
    primary key(exchange_id, user_id)
);

create table exchange (
    id int not null generated always as identity,
    name varchar(50) not null,
    primary key (id)
);

create table trade_order (
    id int not null generated always as identity,
    symbol varchar(20),
    quantity numeric(10, 4),
    amount numeric(10, 4),
    stop_price numeric(10, 4),
    limit_price numeric(10, 4),
    parent_order int,
    side varchar(5) not null,
    type_id int not null,
    status_id int not null,
    account_id uuid not null,
    trade_setting_id int not null,
    primary key (id)
);

create table trade_queue(

);

create table order_history (
	id int not null generated always as identity,
    realized_profit numeric(10, 4),
    created_date timestamp,
    filled_date timestamp,
    fee numeric(10, 4) not null,
    filled_price numeric(10, 4),
    trade_order_id int not null,
    primary key(id)
);

create table trade_setting (
    id int not null generated always as identity,
    name varchar(50) not null,
    risk_reward int,
    retry_attempt int not null,
    skip_attempt int not null,
    candle_closed bool not null,
    risk_per_trade numeric(5,2) not null,
    primary key (id)
);

CREATE TABLE symbol
(
    id int not null generated always as identity,
    value VARCHAR(100) NOT NULL,
    quantity_precision INT NOT NULL,
    price_precision INT NOT NULL,
    exchange_id INT NOT null,
    margin_asset varchar(50) not null,
    base_asset varchar(50) not null,
    primary key (id)
);

create table order_status (
    id int not null,
    value varchar(20) not null,
    primary key (id)
);

create table order_type (
    id int not null,
    value varchar(20) not null,
    filter varchar(256),
    primary key (id)
);

alter table exchange
add constraint fk_exchange_account_id foreign key (account_id) references account(id);

alter table account_api
add constraint fk_account_api_account_id foreign key (account_id) references account(id);

alter table trade_order
add constraint fk_trade_order_account_id foreign key (account_id) references account(id);

alter table trade_order
add constraint fk_trade_order_status_id foreign key (status_id) references order_status(id);

alter table trade_order
add constraint fk_trade_order_type_id foreign key (type_id) references order_type(id);

alter table order_history 
add constraint fk_order_history_trade_order_id foreign key (trade_order_id) references trade_order(id);

alter table account_asset 
add constraint fk_account_asset_account_id foreign key (account_id) references account(id);