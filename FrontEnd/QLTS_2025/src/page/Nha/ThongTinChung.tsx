import { Thongtinchung } from "../../validateform/thongtinchung";

import { Box, Grid, TextField, Typography } from "@mui/material";
import { FieldErrors, UseFormRegister, UseFormSetValue } from "react-hook-form";

interface ThongtintaisanProps {
  register: UseFormRegister<Thongtinchung>;
  errors: FieldErrors<Thongtinchung>;
  setValue: UseFormSetValue<Thongtinchung>;
}

const ThongTinChung = ({}: ThongtintaisanProps) => {
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
        sx={{
          position: "absolute",
          top: "-12px",
          left: "15px",
          backgroundColor: "white",
          padding: "0 8px",
          color: "#007bff",
          fontSize: "14px", // Giảm kích thước tiêu đề
          fontWeight: "bold",
        }}
      >
        Thông tin chung
      </Typography>
      <Grid container spacing={1}>
        <Grid item xs={12}>
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Đơn vị
          </Typography>
          <TextField
            fullWidth
            name="diachi"
            size="small"
            margin="dense"
            value="Chi cục Thuế khu vực Thạch Hà - Lộc Hà"
            InputProps={{
              readOnly: true,
              sx: { fontSize: "14px", backgroundColor: "#e9ecef" },
            }}
            disabled={true}
          />

          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Tên ngôi nhà <span style={{ color: "red" }}>*</span>
          </Typography>
          <TextField
            fullWidth
            size="small"
            margin="dense"
            InputProps={{ sx: { fontSize: "14px" } }}
          />
        </Grid>
      </Grid>
    </Box>
  );
};

export default ThongTinChung;
