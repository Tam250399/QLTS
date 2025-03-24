// handleChangeUtils.ts
import { UseFormSetValue } from "react-hook-form";
import formatCurrencyVND from "../Format/FormatVND";
import { Thongtinchung } from "../../validateform/thongtinchung";

// Định nghĩa kiểu cho fieldName để TypeScript có thể suy ra chính xác
type FieldName =
  | keyof Thongtinchung
  | `HIEN_TRANG_SU_DUNG.${keyof Thongtinchung["HIEN_TRANG_SU_DUNG"]}`;

type FieldNameQSD =
  | keyof Thongtinchung
  | `GIA_TRI_SU_DUNG_DAT.${keyof Thongtinchung["GIA_TRI_SU_DUNG_DAT"]}`;

// Hàm createHandleChange với kiểu chính xác
export const createHandleChange =
  (
    fieldName: FieldName,
    setValue: UseFormSetValue<Thongtinchung>,
    setDisplayValue: (value: string) => void
  ) =>
  (event: React.ChangeEvent<HTMLInputElement>) => {
    const rawValue = event.target.value;
    const cleanValue = rawValue.replace(/[^0-9]/g, "");
    const numberValue = parseInt(cleanValue, 10);

    if (!isNaN(numberValue)) {
      setValue(fieldName, numberValue, { shouldValidate: true });
      setDisplayValue(formatCurrencyVND(numberValue, "m²"));
    } else {
      setValue(fieldName, numberValue, { shouldValidate: true });
      setDisplayValue("");
    }
  };

export const createHandleChangeQSD =
  (
    fieldName: FieldNameQSD,
    setValue: UseFormSetValue<Thongtinchung>,
    setDisplayValue: (value: string) => void
  ) =>
  (event: React.ChangeEvent<HTMLInputElement>) => {
    const rawValue = event.target.value;
    const cleanValue = rawValue.replace(/[^0-9]/g, "");
    const numberValue = parseInt(cleanValue, 10);

    if (!isNaN(numberValue)) {
      setValue(fieldName, numberValue, { shouldValidate: true });
      setDisplayValue(formatCurrencyVND(numberValue, "m²"));
    } else {
      setValue(fieldName, numberValue, { shouldValidate: true });
      setDisplayValue("");
    }
  };

// Tạo các hàm handleChange cho từng field
export const handleChangeDienTichs = (
  setValue: UseFormSetValue<Thongtinchung>,
  setDisplayValue: (value: string) => void
) => createHandleChange("dienTich", setValue, setDisplayValue);

export const handleChangeTruSoLamViec = (
  setValue: UseFormSetValue<Thongtinchung>,
  setDisplayValue: (value: string) => void
) =>
  createHandleChange(
    "HIEN_TRANG_SU_DUNG.TRU_SO_LAM_VIEC",
    setValue,
    setDisplayValue
  );

export const handleChangeDeo = (
  setValue: UseFormSetValue<Thongtinchung>,
  setDisplayValue: (value: string) => void
) => createHandleChange("HIEN_TRANG_SU_DUNG.deO", setValue, setDisplayValue);

export const handleChangeBoTrong = (
  setValue: UseFormSetValue<Thongtinchung>,
  setDisplayValue: (value: string) => void
) =>
  createHandleChange("HIEN_TRANG_SU_DUNG.BO_TRONG", setValue, setDisplayValue);

export const handleChangeBiLanChiem = (
  setValue: UseFormSetValue<Thongtinchung>,
  setDisplayValue: (value: string) => void
) =>
  createHandleChange(
    "HIEN_TRANG_SU_DUNG.BI_LAN_CHIEM",
    setValue,
    setDisplayValue
  );

export const handleChangeSuDungHonHop = (
  setValue: UseFormSetValue<Thongtinchung>,
  setDisplayValue: (value: string) => void
) =>
  createHandleChange(
    "HIEN_TRANG_SU_DUNG.SU_DUNG_HON_HOP",
    setValue,
    setDisplayValue
  );

export const handleChangeSuDungKhac = (
  setValue: UseFormSetValue<Thongtinchung>,
  setDisplayValue: (value: string) => void
) =>
  createHandleChange(
    "HIEN_TRANG_SU_DUNG.SU_DUNG_KHAC",
    setValue,
    setDisplayValue
  );

export const handleChangeGiaTriQSD = (
  setValue: UseFormSetValue<Thongtinchung>,
  setDisplayValue: (value: string) => void
) =>
  createHandleChangeQSD(
    "GIA_TRI_SU_DUNG_DAT.GIA_TRI_QUYEN_SD_DAT",
    setValue,
    setDisplayValue
  );
