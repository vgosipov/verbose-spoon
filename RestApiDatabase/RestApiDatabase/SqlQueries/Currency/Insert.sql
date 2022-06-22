INSERT INTO currency
(Cur_ID, Cur_ParentID, Cur_Code, Cur_Abbreviation, Cur_Name, Cur_Name_Bel,
Cur_Name_Eng, Cur_QuotName, Cur_QuotName_Bel, Cur_QuotName_Eng, Cur_NameMulti,
Cur_Name_BelMulti, Cur_Name_EngMulti, Cur_Scale, Cur_Periodicity, Cur_DateStart, Cur_DateEnd)
VALUES
('@Id', '@ParentId', '@Code', '@Abbreviation', '@Name', '@NameBel', '@NameEng',
'@QuotName', '@QuotNameBel', '@QuotNameEng', '@NameMulti', '@NameBelMulti', '@NameEngMulti',
'@Scale', '@Periodicity', TO_DATE('@DateStart', '@DateStartFormat'), TO_DATE('@DateEnd', '@DateEndFormat'))