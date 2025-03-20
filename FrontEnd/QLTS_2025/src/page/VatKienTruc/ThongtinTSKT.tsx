import {
  TextField,
  FormControl,
  Grid,
  Typography,
  Box,
  IconButton,
  Autocomplete,
} from "@mui/material";

import AddCircleOutlineIcon from "@mui/icons-material/AddCircleOutline";

import { ThongtinchungVkt } from "../../validateform/thongtinVkt";
import { FieldErrors, UseFormRegister } from "react-hook-form";

interface ThongtintaisanVktProps {
  register: UseFormRegister<ThongtinchungVkt>;
  errors: FieldErrors<ThongtinchungVkt>;
}

const ThongtinTSKT = ({ register, errors }: ThongtintaisanVktProps) => {
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
          fontSize: "14px",
          fontWeight: "bold",
        }}
      >
        Thông tin tài sản vật kiến trúc
      </Typography>

      <Grid container spacing={3}>
        {/* Left Column */}
        <Grid item xs={12} md={6}>
          {/* Đơn vị */}
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Đơn vị <span style={{ color: "red" }}>*</span>
          </Typography>
          <TextField
            fullWidth
            size="small"
            variant="outlined"
            sx={{ marginBottom: 2 }}
            InputLabelProps={{ shrink: true }}
          />

          {/* Tên tài sản */}

          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Tên tài sản <span style={{ color: "red" }}>*</span>
          </Typography>
          <TextField
            fullWidth
            size="small"
            variant="outlined"
            sx={{ marginBottom: 2 }}
            {...register("TEN", { required: true })}
          />
          {errors.TEN && (
            <span className="text-red-500 text-xs">
              Bạn phải nhập tên tài sản
            </span>
          )}

          {/* Lý do tăng */}
          <FormControl
            fullWidth
            variant="outlined"
            sx={{ marginBottom: 2 }}
            size="small"
          >
            <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
              Lý do tăng <span style={{ color: "red" }}>*</span>
            </Typography>
            <Autocomplete
              className="pt-[1px]"
              renderInput={(params) => (
                <TextField
                  {...params}
                  placeholder="Đăng ký lần đầu"
                  sx={{
                    fontSize: "14px",
                    "& .MuiInputBase-root": {
                      height: "36px",
                    },
                  }}
                />
              )}
              options={[]}
            />
            {errors.LY_DO_TANG_ID && (
              <span className="text-red-500 text-xs">
                Bạn phải chọn lý do tăng
              </span>
            )}
          </FormControl>
          {/* {errors.LY_DO_TANG && (
            <span className="text-red-500 text-xs">
              Bạn phải nhập tên tài sản
            </span>
          )} */}
          {/* Loại tài sản */}
          <FormControl
            fullWidth
            variant="outlined"
            sx={{ marginBottom: 2 }}
            size="small"
          >
            <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
              Loại tài sản <span style={{ color: "red" }}>*</span>
            </Typography>
            <Autocomplete
              className="pt-[1px]"
              renderInput={(params) => (
                <TextField
                  {...params}
                  placeholder="-- Chọn loại tài sản --"
                  sx={{
                    fontSize: "14px",
                    "& .MuiInputBase-root": {
                      height: "36px",
                    },
                  }}
                />
              )}
              options={[]}
            />
            {errors.LOAI_TAI_SAN_ID && (
              <span className="text-red-500 text-xs">
                Bạn phải chọn loại tài sả<nav></nav>
              </span>
            )}
          </FormControl>

          {/* Chiếu dài and Diện tích */}
          <Grid container spacing={2}>
            <Grid item xs={6}>
              <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
                Chiếu dài<span style={{ color: "red" }}>*</span>
              </Typography>
              <TextField
                size="small"
                name="discount"
                fullWidth
                variant="outlined"
                sx={{ marginBottom: 2 }}
                InputProps={{ endAdornment: <span>m</span> }}
              />
            </Grid>
            <Grid item xs={6}>
              <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
                Diện tích<span style={{ color: "red" }}>*</span>
              </Typography>
              <TextField
                size="small"
                name="area"
                fullWidth
                variant="outlined"
                sx={{ marginBottom: 2 }}
                InputProps={{ endAdornment: <span>m²</span> }}
              />
            </Grid>
          </Grid>

          {/* Nguồn nước xuất */}
          <FormControl
            fullWidth
            variant="outlined"
            sx={{ marginBottom: 2 }}
            size="small"
          >
            <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
              Nước sản xuất<span style={{ color: "red" }}>*</span>
            </Typography>
            <Autocomplete
              className="pt-[1px]"
              renderInput={(params) => (
                <TextField
                  {...params}
                  placeholder="-- Chọn Quốc gia --"
                  sx={{
                    fontSize: "14px",
                    "& .MuiInputBase-root": {
                      height: "36px",
                    },
                  }}
                />
              )}
              options={[]}
            />
            {errors.NAM_SX && (
              <span className="text-red-500 text-xs">
                Bạn phải chọn loại tài sả<nav></nav>
              </span>
            )}
          </FormControl>

          {/* Ngày dự vào sử dụng */}
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Ngày đưa vào sử dụng<span style={{ color: "red" }}>*</span>
          </Typography>
          <TextField
            fullWidth
            type="date"
            size="small"
            margin="dense"
            InputLabelProps={{ shrink: true }}
            defaultValue="2017-12-31"
            InputProps={{ sx: { fontSize: "14px" } }}
          />
        </Grid>

        {/* Right Column */}

        <Grid item xs={12} md={6}>
          <div className="pt-[22px]">
            <TextField
              label="Viện Dược Liệu"
              disabled
              size="small"
              fullWidth
              sx={{ marginBottom: 1 }}
            />
          </div>

          {/* Ngày tăng */}
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Ngày tăng<span style={{ color: "red" }}>*</span>
          </Typography>
          <TextField
            fullWidth
            type="date"
            disabled
            size="small"
            margin="dense"
            InputLabelProps={{ shrink: true }}
            defaultValue="2017-12-31"
            sx={{ marginBottom: 2 }}
            InputProps={{ sx: { fontSize: "14px" } }}
          />

          {/* Bộ phận sử dụng */}

          <Grid container spacing={2}>
            <Grid item xs={6}>
              <FormControl
                className="w-80"
                variant="outlined"
                size="small"
                sx={{ marginBottom: 2, position: "relative" }}
              >
                <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
                  Bộ phận sử dụng<span style={{ color: "red" }}>*</span>
                </Typography>
                <Autocomplete
                  className="pt-[1px]"
                  renderInput={(params) => (
                    <TextField
                      {...params}
                      placeholder="-- Chọn bộ phận sử dụng --"
                      sx={{
                        fontSize: "14px",
                        "& .MuiInputBase-root": {
                          height: "36px",
                        },
                      }}
                    />
                  )}
                  options={[]}
                />
                {errors.LOAI_TAI_SAN_ID && (
                  <span className="text-red-500 text-xs">
                    Bạn phải chọn loại tài sả<nav></nav>
                  </span>
                )}
              </FormControl>
            </Grid>
            <Grid item xs={6}>
              <IconButton
                sx={{
                  right: 0,
                  top: "60%",
                  transform: "translateY(-60%)",
                }}
              >
                <AddCircleOutlineIcon color="primary" />
              </IconButton>
            </Grid>
          </Grid>

          {/* Thể tích */}
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Thể tích<span style={{ color: "red" }}>*</span>
          </Typography>
          <TextField
            size="small"
            name="totalArea"
            fullWidth
            variant="outlined"
            sx={{ marginBottom: 2 }}
            InputProps={{ endAdornment: <span>m³</span> }}
          />

          {/* Năm sản xuất */}
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Năm sản xuất<span style={{ color: "red" }}>*</span>
          </Typography>
          <TextField
            size="small"
            name="exportYear"
            className="w-40"
            variant="outlined"
            sx={{ marginBottom: 2 }}
          />
          {/* Mô tả chung */}
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Mô tả chung<span style={{ color: "red" }}>*</span>
          </Typography>
          <TextField
            size="small"
            name="assetName"
            fullWidth
            variant="outlined"
            sx={{ marginBottom: 2 }}
          />
        </Grid>
      </Grid>
    </Box>
  );
};

export default ThongtinTSKT;
