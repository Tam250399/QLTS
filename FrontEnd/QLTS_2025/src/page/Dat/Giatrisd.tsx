import { Box, Grid, TextField, Typography } from "@mui/material";
import {
  FieldErrors,
  UseFormClearErrors,
  UseFormGetValues,
  UseFormRegister,
  UseFormSetError,
  UseFormSetValue,
} from "react-hook-form";
import { Thongtinchung } from "../../validateform/thongtinchung";
import { useEffect, useState } from "react";
import formatCurrencyVND from "../../components/Format/FormatVND";
import {
  handleChangeGiaTriQSD,
  handleChangeNguonKhac,
  handleChangeNguyenGia,
} from "../../components/form/HandleChageDat";

interface GiaTriSuDungDatProps {
  register: UseFormRegister<Thongtinchung>;
  errors: FieldErrors<Thongtinchung>;
  setValue: UseFormSetValue<Thongtinchung>;
  setError: UseFormSetError<Thongtinchung>;
  clearErrors: UseFormClearErrors<Thongtinchung>;
  getValues: UseFormGetValues<Thongtinchung>;
}

const Giatrisd = ({ setValue, getValues }: GiaTriSuDungDatProps) => {
  const [displayValues, setDisplayValues] = useState<Record<string, string>>(
    {}
  );

  const [qsdError, setQsdError] = useState<string | undefined>(undefined);
  const [nguonKhacError, setNguonKhacError] = useState<string | undefined>(
    undefined
  );
  const { GIA_TRI_QUYEN_SD_DAT, NGUON_KHAC } =
    getValues("GIA_TRI_SU_DUNG_DAT") || {};

  const NGUYEN_GIA = getValues("NGUYEN_GIA");
  const NGUON_NGAN_SACH =
    NGUYEN_GIA - NGUON_KHAC > 0 ? NGUYEN_GIA - NGUON_KHAC : 0;

  useEffect(() => {
    setValue("GIA_TRI_SU_DUNG_DAT.NGUON_NGAN_SACH", NGUON_NGAN_SACH, {
      shouldValidate: true,
    });
    console.log(getValues("GIA_TRI_SU_DUNG_DAT.NGUON_NGAN_SACH"));
    if (NGUON_NGAN_SACH < 1) {
      setNguonKhacError("Tổng các nguồn vốn phải bằng nguyên giá.");
    } else {
      setNguonKhacError(undefined);
    }
  }, [NGUYEN_GIA, NGUON_KHAC]);

  useEffect(() => {
    if (NGUYEN_GIA < GIA_TRI_QUYEN_SD_DAT) {
      setQsdError("Giá trị quyền sử dụng đất không được lớn hơn nguyên giá.");
    } else {
      setQsdError(undefined);
    }
  }, [NGUYEN_GIA, GIA_TRI_QUYEN_SD_DAT]);

  useEffect(() => {
    const fields = {
      NGUYEN_GIA,
      GIA_TRI_QUYEN_SD_DAT,
      NGUON_KHAC,
      NGUON_NGAN_SACH,
    };

    const newDisplayValues: Record<string, string> = {};
    Object.entries(fields).forEach(([fieldName, value]) => {
      if (value !== undefined) {
        newDisplayValues[fieldName] = formatCurrencyVND(value, "đ̲");
      }
    });

    setDisplayValues((prev) => ({ ...prev, ...newDisplayValues }));
  }, [NGUYEN_GIA, GIA_TRI_QUYEN_SD_DAT, NGUON_KHAC, NGUON_NGAN_SACH]);

  return (
    <Box
      sx={{
        border: "1px solid #007bff",
        borderRadius: 2,
        p: 3,
        bgcolor: "white",
        boxShadow: 2,
        position: "relative",
      }}
    >
      <Grid container spacing={1}>
        <Grid item xs={12} md={6}>
          <Typography
            sx={{
              position: "absolute",
              top: "-12px",
              left: "15px",
              backgroundColor: "white",
              padding: "0 8px",
              color: "#007bff",
              fontSize: "14px",
              fontWeight: "bold",
            }}
          >
            Giá trị quyền sử dụng đất
          </Typography>

          {/* Giá trị QSD đất */}
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Giá trị QSD đất <span style={{ color: "red" }}>*</span>
          </Typography>
          <TextField
            fullWidth
            size="small"
            margin="dense"
            type="text"
            placeholder="đ̲"
            value={displayValues.GIA_TRI_QUYEN_SD_DAT || ""}
            onChange={handleChangeGiaTriQSD(setValue, (value) =>
              setDisplayValues((prev) => ({
                ...prev,
                GIA_TRI_QUYEN_SD_DAT: value,
              }))
            )}
            error={!!qsdError}
            helperText={qsdError}
          />
          {/* Nguyên giá */}
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Nguyên giá <span style={{ color: "red" }}>*</span>
          </Typography>
          <TextField
            fullWidth
            size="small"
            margin="dense"
            type="text"
            placeholder="đ̲"
            value={displayValues.NGUYEN_GIA || ""}
            onChange={handleChangeNguyenGia(setValue, (value) =>
              setDisplayValues((prev) => ({ ...prev, NGUYEN_GIA: value }))
            )}
          />

          <Typography
            variant="subtitle2"
            sx={{ fontSize: "14px", fontWeight: "bold", margin: "15px 0" }}
          >
            Trong đó:
          </Typography>

          {/* Nguồn ngân sách */}
          <Typography
            variant="subtitle2"
            sx={{ fontSize: "14px", fontStyle: "italic" }}
          >
            Nguồn ngân sách
          </Typography>
          <TextField
            fullWidth
            size="small"
            margin="dense"
            type="text"
            placeholder="đ̲"
            value={displayValues.NGUON_NGAN_SACH || ""}
            disabled
          />

          {/* Nguồn khác */}
          <Typography
            variant="subtitle2"
            sx={{ fontSize: "14px", fontStyle: "italic" }}
          >
            Nguồn khác
          </Typography>
          <TextField
            fullWidth
            size="small"
            margin="dense"
            type="text"
            placeholder="đ̲"
            value={displayValues.NGUON_KHAC || ""}
            onChange={handleChangeNguonKhac(setValue, (value) =>
              setDisplayValues((prev) => ({ ...prev, NGUON_KHAC: value }))
            )}
            error={!!nguonKhacError}
            helperText={nguonKhacError}
          />
        </Grid>
      </Grid>
    </Box>
  );
};

export default Giatrisd;
