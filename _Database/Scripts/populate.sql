INSERT INTO order_type (id, value, binance) 
VALUES 
(0, 'Limit', 0),
(1, 'Market', 1),
(2, 'Stop',2),
(3, 'StopMarket', 3),
(4, 'StopLoss'),
(5, 'TakeProfit', 4),
(6, 'TakeProfitMarket', 5),
(7, 'TrailingStop', 6);

INSERT INTO order_status (id, value) 
VALUES 
(1, 'New'),
(2, 'PartiallyFilled'),
(3, 'Filled'),
(4, 'Canceled'),
(5, 'PendingCancel'),
(6, 'Rejected'),
(7, 'Expired');

INSERT INTO entry_type (id, name, value) 
VALUES 
(1, 'New'),
(2, 'PartiallyFilled'),
(3, 'Filled'),
(4, 'Canceled'),
(5, 'PendingCancel'),
(6, 'Rejected'),
(7, 'Expired');

DO $$
DECLARE
    account_id UUID;
BEGIN
    account_id := uuid_generate_v4();
    
    -- Populate the account_asset table with the generated UUID
    INSERT INTO account_asset (currency, available_balance, account_id)
    VALUES 
        ('USDT', 1000.00, account_id);
END $$;

insert into exchange (name) values ('BINANCE');

-- Populate the account table
INSERT INTO account (id, risk_per_trade, user_id, exchange_id)
VALUES 
    (uuid_generate_v4(),2.5, uuid_generate_v4(), 1);
   
-- Populate the account_asset table
INSERT INTO account_asset (currency, available_balance, account_id)
VALUES 
    ('USDT', 1000.00, '7225d9f0-3f42-4705-ae07-a36acaf19a93');
INSERT INTO api_key (private_key, public_key, exchangeId, user_id)
VALUES 
  ('A7rhTU8exBilrf22aG9mHEyU0gy6RcWQJZ8o5aplT7KziksVGpOQRw7KIwBbbzkO',
 	'NF6FPDMj4Ny9FE5ECn8GFSUqZFY4JaKuTRx1qkVkMFVsB3gm2NNUnR3Xgwycmyky', 1,'c96666c1-2ed3-4888-b775-9f4ebe277bed');
 
insert into trade_setting(name, risk_reward, retry_attempt, skip_attempt, candle_closed, risk_per_trade) values ('standard', 0, 0, 0, false, 0)
