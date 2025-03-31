// handleChangeUtils.ts
import { UseFormSetValue } from "react-hook-form";
import formatCurrencyVND from "../Format/FormatVND";
import { ThongtinchungVkt } from "../../validateform/thongtinVkt";

// Định nghĩa kiểu cho fieldName để TypeScript có thể suy ra chính xác
type FieldName = keyof ThongtinchungVkt;

// Hàm createHandleChange với kiểu chính xác
export const createHandleChange =
  (
    fieldName: FieldName,
    setValue: UseFormSetValue<ThongtinchungVkt>,
    setDisplayValue: (value: string) => void
  ) =>
  (event: React.ChangeEvent<HTMLInputElement>) => {
    const rawValue = event.target.value;
    const cleanValue = rawValue.replace(/[^0-9]/g, "");
    const numberValue = parseInt(cleanValue, 10);

    if (!isNaN(numberValue)) {
      setValue(fieldName, numberValue, { shouldValidate: true });
      setDisplayValue(
        formatCurrencyVND(
          numberValue,
          fieldName === "THE_TICH"
            ? "m³"
            : fieldName === "CHIEU_DAI"
            ? "m"
            : "m²"
        )
      );
    } else {
      setValue(fieldName, numberValue, { shouldValidate: true });
      setDisplayValue("");
    }
  };

// Tạo các hàm handleChange cho từng field
export const handleChangeDienTich = (
  setValue: UseFormSetValue<ThongtinchungVkt>,
  setDisplayValue: (value: string) => void
) => createHandleChange("DIEN_TICH", setValue, setDisplayValue);

export const handleChangeChieuDai = (
  setValue: UseFormSetValue<ThongtinchungVkt>,
  setDisplayValue: (value: string) => void
) => createHandleChange("CHIEU_DAI", setValue, setDisplayValue);

export const handleChangeTheTich = (
  setValue: UseFormSetValue<ThongtinchungVkt>,
  setDisplayValue: (value: string) => void
) => createHandleChange("THE_TICH", setValue, setDisplayValue);
