import { LyDoTangDat } from "../../validateform/thongtinchung";

import {
  Autocomplete,
  Box,
  Button,
  FormControl,
  FormControlLabel,
  Grid,
  Radio,
  RadioGroup,
  Stack,
  TextField,
  Typography,
} from "@mui/material";
import { useEffect, useState } from "react";
import { FieldErrors, UseFormRegister, UseFormSetValue } from "react-hook-form";
import AddIcon from "@mui/icons-material/Add";
import { ThongTinNha } from "../../validateform/thongtinnha";

interface ThongtinnhaProps {
  register: UseFormRegister<ThongTinNha>;
  errors: FieldErrors<ThongTinNha>;
  setValue: UseFormSetValue<ThongTinNha>;
}

const ThongTinChung = ({ register, errors, setValue }: ThongtinnhaProps) => {
  const [lyDoTangDat, setLyDoTangDats] = useState<LyDoTangDat[]>([]);
  const [quanLyDat, setQuanLyDat] = useState("co");
  const handleAdd = () => {
    alert("Bạn đã nhấn nút +!");
  };

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
      <Grid container spacing={4}>
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

          <Typography
            variant="subtitle2"
            sx={{ fontSize: "14px", marginTop: "8px" }}
          >
            Tên ngôi nhà <span style={{ color: "red" }}>*</span>
          </Typography>
          <TextField
            fullWidth
            size="small"
            margin="dense"
            {...register("TEN_NGOI_NHA", {
              required: "Bạn phải nhập tên ngôi nhà",
            })}
            InputProps={{ sx: { fontSize: "14px" } }}
          />
          {errors?.TEN_NGOI_NHA && (
            <span className="text-red-500 text-xs">
              {errors?.TEN_NGOI_NHA?.message}
            </span>
          )}
        </Grid>
        <Grid item xs={12}>
          {/* Radio Button */}
          <FormControl component="fieldset" sx={{ marginTop: "8px" }}>
            <RadioGroup
              row
              value={quanLyDat}
              onChange={(e) => setQuanLyDat(e.target.value)} // Cập nhật state khi thay đổi
            >
              <FormControlLabel
                value="co"
                control={<Radio size="small" />}
                label="Có quản lý đất"
              />
              <FormControlLabel
                value="khong"
                control={<Radio size="small" />}
                label="Không quản lý đất"
              />
            </RadioGroup>
          </FormControl>

          {/* Nếu chọn "Có quản lý đất", hiển thị ô nhập "Thuộc khuôn viên đất" */}
          {quanLyDat === "co" && (
            <>
              <Typography
                variant="subtitle2"
                sx={{ fontSize: "14px", marginTop: "8px" }}
              >
                Thuộc khuôn viên đất <span style={{ color: "red" }}>*</span>
              </Typography>
              <Box sx={{ display: "flex", alignItems: "center" }}>
                <TextField
                  fullWidth
                  name="diachi"
                  size="small"
                  margin="dense"
                  InputProps={{
                    readOnly: true,
                    sx: { fontSize: "14px", backgroundColor: "#e9ecef" },
                  }}
                  disabled={true}
                />
                <Button
                  variant="outlined"
                  sx={{
                    height: "26px",
                    marginLeft: "8px", // Tạo khoảng cách với Autocomplete
                    fontSize: "14px",
                    whiteSpace: "nowrap", // Giữ chữ trên cùng một dòng
                    textTransform: "none",
                    fontWeight: 400, // Giảm độ đậm của chữ (mặc định là 500-600)
                  }}
                  onClick={handleAdd} // Gọi hàm khi bấm "+"
                >
                  + Chọn đất
                </Button>
              </Box>
            </>
          )}

          {/* Nếu chọn "Không quản lý đất", hiển thị ô nhập "Địa chỉ nhà" */}
          {quanLyDat === "khong" && (
            <>
              <Typography
                variant="subtitle2"
                sx={{ fontSize: "14px", marginTop: "8px" }}
              >
                Địa chỉ nhà <span style={{ color: "red" }}>*</span>
              </Typography>
              <TextField
                fullWidth
                size="small"
                margin="dense"
                placeholder="Nhập số nhà, đường phố, tổ / thôn / xóm"
                InputProps={{ sx: { fontSize: "14px" } }}
                {...register("DIA_CHI_NHA", {
                  required: "Bạn phải nhập địa chỉ nhà",
                })}
              />
              {errors?.DIA_CHI_NHA && (
                <span className="text-red-500 text-xs">
                  {errors?.DIA_CHI_NHA?.message}
                </span>
              )}
            </>
          )}
        </Grid>
        <Grid item xs={12} md={6}>
          <Stack spacing={1}>
            <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
              Lý do tăng đất <span style={{ color: "red" }}>*</span>
            </Typography>
            <Autocomplete
              className="pt-[1px]"
              options={lyDoTangDat}
              getOptionLabel={(option) => option.ten}
              {...register("LY_DO_TANG_ID", {
                required: "Bạn phải chọn lý do tăng",
              })}
              onChange={(_, value) => {
                const selected = lyDoTangDat.find(
                  (lydo) => lydo.id === value?.id
                );
                setValue("LY_DO_TANG_ID", selected?.id || -1);
              }}
              renderInput={(params) => (
                <TextField
                  {...params}
                  placeholder="-- Chọn lý do tăng đất --"
                  sx={{
                    fontSize: "14px",
                    "& .MuiInputBase-root": {
                      height: "36px",
                    },
                  }}
                />
              )}
              noOptionsText="Không tìm thấy lý do tăng đất"
              renderOption={(props, option) => (
                <li {...props} style={{ fontSize: "14px" }}>
                  {option.ten}
                </li>
              )}
            />
            {errors?.LY_DO_TANG_ID && (
              <span className="text-red-500 text-xs">
                {errors?.LY_DO_TANG_ID?.message}
              </span>
            )}

            <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
              Cấp nhà <span style={{ color: "red" }}>*</span>
            </Typography>
            <Autocomplete
              className="pt-[1px]"
              options={lyDoTangDat}
              getOptionLabel={(option) => option.ten}
              {...register("CAP_NHA_ID", {
                required: "Bạn phải chọn cấp nhà",
              })}
              onChange={(_, value) => {
                const selected = lyDoTangDat.find(
                  (lydo) => lydo.id === value?.id
                );
                setValue("CAP_NHA_ID", selected?.id || -1);
              }}
              renderInput={(params) => (
                <TextField
                  {...params}
                  placeholder="-- Chọn cấp nhà --"
                  sx={{
                    fontSize: "14px",
                    "& .MuiInputBase-root": {
                      height: "36px",
                    },
                  }}
                />
              )}
              noOptionsText="Không tìm thấy cấp nhà"
              renderOption={(props, option) => (
                <li {...props} style={{ fontSize: "14px" }}>
                  {option.ten}
                </li>
              )}
            />
            {errors?.CAP_NHA_ID && (
              <span className="text-red-500 text-xs">
                {errors?.CAP_NHA_ID?.message}
              </span>
            )}
            <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
              Diện tích XD
            </Typography>
            <TextField
              fullWidth
              size="small"
              margin="dense"
              placeholder="m²"
              type="number"
              InputProps={{ sx: { fontSize: "14px" } }}
              {...register("DIEN_TICH_XD", { required: true })}
            />
            <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
              Năm xây dựng <span style={{ color: "red" }}>*</span>
            </Typography>
            <TextField
              size="small"
              margin="dense"
              InputProps={{ sx: { fontSize: "14px", width: "65px" } }}
              inputProps={{
                maxLength: 4, // Giới hạn tối đa 4 ký tự
                inputMode: "numeric", // Chỉ cho phép nhập số
              }}
              {...register("NAM_XAY_DUNG", {
                required: "Bạn phải nhập năm xây dựng",
              })}
            />
            {errors?.NAM_XAY_DUNG && (
              <span className="text-red-500 text-xs">
                {errors?.NAM_XAY_DUNG?.message}
              </span>
            )}
            <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
              Bộ phận sử dụng
            </Typography>
            <Box sx={{ display: "flex", alignItems: "center" }}>
              <Autocomplete
                className="pt-[1px]"
                options={lyDoTangDat}
                getOptionLabel={(option) => option.ten}
                onChange={(_, value) => {
                  const selected = lyDoTangDat.find(
                    (lydo) => lydo.id === value?.id
                  );
                  setValue("BO_PHAN_SD_ID", selected?.id || -1);
                }}
                renderInput={(params) => (
                  <TextField
                    {...params}
                    placeholder="-- Chọn bộ phận sử dụng --"
                    sx={{
                      fontSize: "14px",
                      "& .MuiInputBase-root": {
                        height: "36px",
                        width: "250px",
                      },
                    }}
                  />
                )}
                noOptionsText="Không tìm thấy bộ phận sử dụng"
                renderOption={(props, option) => (
                  <li {...props} style={{ fontSize: "14px" }}>
                    {option.ten}
                  </li>
                )}
              />
              {/* Button "+" */}
              <Button
                variant="outlined"
                sx={{
                  minWidth: "36px",
                  height: "36px",
                  marginLeft: "8px", // Tạo khoảng cách với Autocomplete
                }}
                onClick={handleAdd} // Gọi hàm khi bấm "+"
              >
                <AddIcon />
              </Button>
            </Box>
          </Stack>
        </Grid>
        <Grid item xs={12} md={3}>
          <Stack spacing={1}>
            <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
              Ngày tăng <span style={{ color: "red" }}>*</span>
            </Typography>
            <TextField
              fullWidth
              type="date"
              size="small"
              margin="dense"
              InputLabelProps={{ shrink: true }}
              defaultValue="2017-12-31"
              InputProps={{ sx: { fontSize: "14px" } }}
              {...register("NGAY_TANG", {
                required: "Bạn phải chọn ngày tăng",
              })}
            />
            {errors?.NGAY_TANG && (
              <span className="text-red-500 text-xs">
                {errors?.NGAY_TANG?.message}
              </span>
            )}
            <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
              Số tầng <span style={{ color: "red" }}>*</span>
            </Typography>
            <TextField
              fullWidth
              size="small"
              margin="dense"
              type="number"
              InputProps={{ sx: { fontSize: "14px" } }}
              {...register("SO_TANG", { required: "Bạn phải nhập số tầng" })}
            />
            {errors?.SO_TANG && (
              <span className="text-red-500 text-xs">
                {errors?.SO_TANG?.message}
              </span>
            )}
            <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
              DT sàn sử dụng <span style={{ color: "red" }}>*</span>
            </Typography>
            <TextField
              fullWidth
              size="small"
              margin="dense"
              type="number"
              placeholder="m²"
              InputProps={{ sx: { fontSize: "14px" } }}
              {...register("DT_SAN_SU_DUNG", {
                required: "Bạn phải nhập diện tích sàn sử dụng",
              })}
            />
            {errors?.DT_SAN_SU_DUNG && (
              <span className="text-red-500 text-xs">
                {errors?.DT_SAN_SU_DUNG?.message}
              </span>
            )}
            <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
              Ngày đưa vào sử dụng <span style={{ color: "red" }}>*</span>
            </Typography>
            <TextField
              fullWidth
              type="date"
              size="small"
              margin="dense"
              InputLabelProps={{ shrink: true }}
              defaultValue="2017-12-31"
              InputProps={{ sx: { fontSize: "14px" } }}
              {...register("NGAY_DUA_VAO_SD", {
                required: "Bạn phải chọn ngày đưa vào sử dụng",
              })}
            />
            {errors?.NGAY_DUA_VAO_SD && (
              <span className="text-red-500 text-xs">
                {errors?.NGAY_DUA_VAO_SD?.message}
              </span>
            )}
          </Stack>
        </Grid>
      </Grid>
    </Box>
  );
};

export default ThongTinChung;
