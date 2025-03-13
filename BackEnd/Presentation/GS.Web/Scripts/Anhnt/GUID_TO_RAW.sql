--------------------------------------------------------
--  File created - Friday-June-05-2020   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Function GUID_TO_RAW
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE FUNCTION "QLDKTS_17_02_2020"."GUID_TO_RAW" 
(
  GUID IN VARCHAR2 
) RETURN RAW IS
HEX VARCHAR2(32);
BEGIN
  HEX := substr(GUID, 7, 2)
    ||     substr(GUID, 5, 2)
    ||     substr(GUID, 3, 2)
    ||     substr(GUID, 1, 2)
    --
    ||     substr(GUID, 12, 2)
    ||     substr(GUID, 10, 2)
    --
    ||     substr(GUID, 17, 2)
    ||     substr(GUID, 15, 2)
    -- 
    ||     substr(GUID, 20, 2)
    ||     substr(GUID, 22, 2)
    -- 
    ||     substr(GUID, 25, 12);
    
    return HEXTORAW(HEX);
END GUID_TO_RAW;

/
