import {
  Typography,
  FormGroup,
  FormControlLabel,
  Checkbox,
  Box,
} from "@mui/material";
import { Control, Controller } from "react-hook-form";
import { ThongtinchungVkt } from "../../validateform/thongtinVkt";

interface HienTrangSDProps {
  control: Control<ThongtinchungVkt>;
}

const HienTrangSDVKT: React.FC<HienTrangSDProps> = ({ control }) => {
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
      <Typography
        variant="h6"
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
        Hiện trạng sử dụng
      </Typography>
      <FormGroup
        row
        sx={{
          display: "flex",
          justifyContent: "space-between",
          flexWrap: "nowrap",
          gap: 2,
        }}
      >
        <FormControlLabel
          control={
            <Controller
              name="HIEN_TRANG_SU_DUNG.TRU_SO_LAM_VIEC"
              control={control}
              disabled
              render={({ field }) => (
                <Checkbox
                  {...field}
                  checked={field.value > 0}
                  onChange={(e) => field.onChange(e.target.checked ? 1 : 0)}
                />
              )}
            />
          }
          label="Quan lý nhà nước"
          sx={{ marginRight: 0 }}
        />
        <FormControlLabel
          control={
            <Controller
              name="HIEN_TRANG_SU_DUNG.HD_SN_KHONG_KINH_DOANH"
              control={control}
              render={({ field }) => (
                <Checkbox
                  {...field}
                  checked={field.value > 0}
                  onChange={(e) => field.onChange(e.target.checked ? 1 : 0)}
                />
              )}
            />
          }
          label="HDSN-Không KD"
          sx={{ marginRight: 0 }}
        />
        <FormControlLabel
          control={
            <Controller
              name="HIEN_TRANG_SU_DUNG.HD_SD_KINH_DOANH"
              control={control}
              render={({ field }) => (
                <Checkbox
                  {...field}
                  checked={field.value > 0}
                  onChange={(e) => field.onChange(e.target.checked ? 1 : 0)}
                />
              )}
            />
          }
          label="HDSN-Kinh doanh"
          sx={{ marginRight: 0 }}
        />
        <FormControlLabel
          control={
            <Controller
              name="HIEN_TRANG_SU_DUNG.HD_SD_KINH_DOANH_LK"
              control={control}
              render={({ field }) => (
                <Checkbox
                  {...field}
                  checked={field.value > 0}
                  onChange={(e) => field.onChange(e.target.checked ? 1 : 0)}
                />
              )}
            />
          }
          label="HDSN-LDLK"
          sx={{ marginRight: 0 }}
        />
        <FormControlLabel
          control={
            <Controller
              name="HIEN_TRANG_SU_DUNG.HD_SD_CHO_THUE"
              control={control}
              render={({ field }) => (
                <Checkbox
                  {...field}
                  checked={field.value > 0}
                  onChange={(e) => field.onChange(e.target.checked ? 1 : 0)}
                />
              )}
            />
          }
          label="HDSN-Cho thuê"
          sx={{ marginRight: 0 }}
        />
        <FormControlLabel
          control={
            <Controller
              name="HIEN_TRANG_SU_DUNG.SU_DUNG_KHAC"
              control={control}
              render={({ field }) => (
                <Checkbox
                  {...field}
                  checked={field.value > 0}
                  onChange={(e) => field.onChange(e.target.checked ? 1 : 0)}
                />
              )}
            />
          }
          label="Sử dụng khác"
          sx={{ marginRight: 0 }}
        />
      </FormGroup>
    </Box>
  );
};

export default HienTrangSDVKT;
