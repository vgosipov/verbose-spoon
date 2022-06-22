INSERT INTO DynamicRates (Cur_ID, "Date", Cur_OfficialRate) 
VALUES
('@CurId', TO_DATE('@Date', '@DateFormat'), '@OfficialRate')