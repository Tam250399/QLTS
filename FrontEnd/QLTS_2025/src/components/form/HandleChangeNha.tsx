import { UseFormSetValue } from "react-hook-form";
import formatCurrencyVND from "../Format/FormatVND";
import { ThongTinNha } from "../../validateform/thongtinnha";

// Định nghĩa kiểu cho fieldName để TypeScript có thể suy ra chính xác
type FieldName =
  | keyof ThongTinNha
  | `HIEN_TRANG_SU_DUNG.${keyof ThongTinNha["HIEN_TRANG_SU_DUNG"]}`;

type FieldNameGTHM =
  | keyof ThongTinNha
  | `GIA_TRI_HAO_MON.${keyof ThongTinNha["GIA_TRI_HAO_MON"]}`;

// Hàm createHandleChange với kiểu chính xác
export const createHandleChange =
  (
    fieldName: FieldName,
    setValue: UseFormSetValue<ThongTinNha>,
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

  export const createHandleGTHM =
  (
    fieldName: FieldNameGTHM,
    setValue: UseFormSetValue<ThongTinNha>,
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
export const handleChangeDienTichXD = (
  setValue: UseFormSetValue<ThongTinNha>,
  setDisplayValue: (value: string) => void
) => createHandleChange("DIEN_TICH_XD", setValue, setDisplayValue);

export const handleChangeSoTang = (
  setValue: UseFormSetValue<ThongTinNha>,
  setDisplayValue: (value: string) => void
) => createHandleChange("SO_TANG", setValue, setDisplayValue);

export const handleChangeDTSanSuDung = (
  setValue: UseFormSetValue<ThongTinNha>,
  setDisplayValue: (value: string) => void
) => createHandleChange("DT_SAN_SU_DUNG", setValue, setDisplayValue);

export const handleChangeNguyenGia = (
  setValue: UseFormSetValue<ThongTinNha>,
  setDisplayValue: (value: string) => void
) => createHandleGTHM("GIA_TRI_HAO_MON.NGUYEN_GIA", setValue, setDisplayValue);

export const handleChangeNguonKhac = (
  setValue: UseFormSetValue<ThongTinNha>,
  setDisplayValue: (value: string) => void
) => createHandleGTHM("GIA_TRI_HAO_MON.NGUON_KHAC", setValue, setDisplayValue);

export const handleChangeGiaTriConLai = (
  setValue: UseFormSetValue<ThongTinNha>,
  setDisplayValue: (value: string) => void
) => createHandleGTHM("GIA_TRI_HAO_MON.GIA_TRI_CON_LAI", setValue, setDisplayValue);

export const handleChangeTruSoLamViec = (
  setValue: UseFormSetValue<ThongTinNha>,
  setDisplayValue: (value: string) => void
) =>
  createHandleChange(
    "HIEN_TRANG_SU_DUNG.TRU_SO_LAM_VIEC",
    setValue,
    setDisplayValue
  );

export const handleChangeDeo = (
  setValue: UseFormSetValue<ThongTinNha>,
  setDisplayValue: (value: string) => void
) => createHandleChange("HIEN_TRANG_SU_DUNG.DE_O", setValue, setDisplayValue);

export const handleChangeBoTrong = (
  setValue: UseFormSetValue<ThongTinNha>,
  setDisplayValue: (value: string) => void
) =>
  createHandleChange("HIEN_TRANG_SU_DUNG.BO_TRONG", setValue, setDisplayValue);

export const handleChangeBiLanChiem = (
  setValue: UseFormSetValue<ThongTinNha>,
  setDisplayValue: (value: string) => void
) =>
  createHandleChange(
    "HIEN_TRANG_SU_DUNG.BI_LAN_CHIEM",
    setValue,
    setDisplayValue
  );

export const handleChangeSuDungHonHop = (
  setValue: UseFormSetValue<ThongTinNha>,
  setDisplayValue: (value: string) => void
) =>
  createHandleChange(
    "HIEN_TRANG_SU_DUNG.SU_DUNG_HON_HOP",
    setValue,
    setDisplayValue
  );

export const handleChangeSuDungKhac = (
  setValue: UseFormSetValue<ThongTinNha>,
  setDisplayValue: (value: string) => void
) =>
  createHandleChange(
    "HIEN_TRANG_SU_DUNG.SU_DUNG_KHAC",
    setValue,
    setDisplayValue
  );