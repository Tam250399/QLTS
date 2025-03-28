create or replace FUNCTION GS_STRING_TO_STRINGJSON 
(
  PIN IN VARCHAR2 
) RETURN VARCHAR2 IS 
POUT VARCHAR2(4000);
BEGIN
    POUT:=PIN;
    POUT:=REPLACE(POUT,'\','\\');
    POUT:=REGEXP_REPLACE(POUT,CHR(13),'\r');
    POUT:=REGEXP_REPLACE(POUT,CHR(10),'\n');
    POUT:=REGEXP_REPLACE(POUT,CHR(9),'\t');
    POUT:=REPLACE(POUT,'"','\"');
    
  RETURN POUT;
EXCEPTION
    WHEN OTHERS THEN
    RETURN '';
END GS_STRING_TO_STRINGJSON;