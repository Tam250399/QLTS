import React from "react";
import {
  TextField,
  Select,
  MenuItem,
  FormControl,
  Box,
  Grid,
  Typography,
  Button,
  Autocomplete,
} from "@mui/material";
import { SubmitHandler, useForm  } from "react-hook-form";
type Inputs = {
  diachi: string;
  quocgia: string;
};
const countries = [
  'Việt Nam',
  'Nhật Bản',
  'Hàn Quốc',
  'Trung Quốc',
  'Mỹ',
  // Thêm các quốc gia khác
];


function Home() {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<Inputs>();

  const loginHandler: SubmitHandler<Inputs> = async (data) => {
    // const logger =  await Data(data);
    console.log(data);
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
        Thông tin tài sản đất
      </Typography>

      <Grid container spacing={1}>
        <Grid item xs={12}>
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Đơn vị
          </Typography>
          <TextField
            fullWidth
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
            Địa chỉ <span style={{ color: "red" }}>*</span>
          </Typography>
          <TextField
            fullWidth
            size="small"
            margin="dense"
            placeholder="Nhập số nhà, đường phố, tổ/thôn/xóm"
            InputProps={{ sx: { fontSize: "14px" } }}
            {...register("diachi", { required: true })}
          />
          {errors.diachi && (
            <span className="text-red-500 text-xs">Bạn phải nhập địa chỉ</span>
          )}
        </Grid>

        {/* Cột Trái */}
        <Grid item xs={12} md={6}>

          <Typography variant="subtitle2" sx={{ fontSize: "14px" }} >
            Quốc gia <span style={{ color: "red" }}>*</span>
          </Typography>
          <FormControl fullWidth margin="dense"  >
          <Autocomplete className="pt-[1px]"
            options={countries}
            getOptionLabel={(option) => option}
            renderInput={(params) => (
              <TextField 
                {...params}
                placeholder="-- Chọn Quốc Gia --"
                {...register('quocgia', { required: true })}
                sx={{ fontSize: '14px' ,
              '& .MuiInputBase-root': {
                height: '36px'
              },

                }}
              />
            )}
            noOptionsText="Không tìm thấy quốc gia"
            renderOption={(props, option) => (
              <li {...props} style={{ fontSize: '14px' }}>
                {option}
              </li>
            )}
          />
      {errors.quocgia && (
        <span className="text-red-500 text-xs">Bạn phải chọn Quốc gia</span>
      )}
    </FormControl>

          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Quận/Huyện <span style={{ color: "red" }}>*</span>
          </Typography>
          <FormControl fullWidth margin="dense" size="small">
            <Select
              displayEmpty
              renderValue={(selected) =>
                selected && typeof selected === "string" ? (
                  selected
                ) : (
                  <Typography sx={{ color: "grey.500", fontSize: "14px" }}>
                    -- Chọn quận/huyện --
                  </Typography>
                )
              }
              sx={{ fontSize: "14px" }}
            >
              <MenuItem value="">-- Chọn quận/huyện --</MenuItem>
            </Select>
          </FormControl>

          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Lý do tặng đất <span style={{ color: "red" }}>*</span>
          </Typography>
          <FormControl fullWidth margin="dense" size="small">
            <Select
              displayEmpty
              renderValue={(selected) =>
                selected && typeof selected === "string" ? (
                  selected
                ) : (
                  <Typography sx={{ color: "grey.500", fontSize: "14px" }}>
                    -- Đăng ký lần đầu --
                  </Typography>
                )
              }
              sx={{ fontSize: "14px" }}
            >
              <MenuItem value="">Đăng ký lần đầu</MenuItem>
            </Select>
          </FormControl>

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
          />
        </Grid>
        <Grid item xs={12} md={6}>
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Tỉnh/Thành phố <span style={{ color: "red" }}>*</span>
          </Typography>
          <FormControl fullWidth margin="dense" size="small">
            <Select
              displayEmpty
              renderValue={(selected) =>
                selected && typeof selected === "string" ? (
                  selected
                ) : (
                  <Typography sx={{ color: "grey.500", fontSize: "14px" }}>
                    -- Chọn tỉnh/thành phố --
                  </Typography>
                )
              }
              sx={{ fontSize: "14px" }}
            >
              <MenuItem value="">-- Chọn tỉnh/thành phố --</MenuItem>
            </Select>
          </FormControl>

          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Xã/Phường <span style={{ color: "red" }}>*</span>
          </Typography>
          <FormControl fullWidth margin="dense" size="small">
            <Select
              displayEmpty
              renderValue={(selected) =>
                selected && typeof selected === "string" ? (
                  selected
                ) : (
                  <Typography sx={{ color: "grey.500", fontSize: "14px" }}>
                    -- Chọn xã/phường --
                  </Typography>
                )
              }
              sx={{ fontSize: "14px" }}
            >
              <MenuItem value="">-- Chọn xã/phường --</MenuItem>
            </Select>
          </FormControl>

          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Mục đích sử dụng <span style={{ color: "red" }}>*</span>
          </Typography>
          <FormControl fullWidth margin="dense" size="small">
            <Select
              displayEmpty
              renderValue={(selected) =>
                selected && typeof selected === "string" ? (
                  selected
                ) : (
                  <Typography sx={{ color: "grey.500", fontSize: "14px" }}>
                    -- Chọn mục đích sử dụng --
                  </Typography>
                )
              }
              sx={{ fontSize: "14px" }}
            >
              <MenuItem value="">-- Chọn mục đích sử dụng --</MenuItem>
            </Select>
          </FormControl>
        </Grid>
      </Grid>
      <Box sx={{ mt: 3, textAlign: "right" }}>
        <Button
          variant="contained"
          color="primary"
          onClick={handleSubmit(loginHandler)}
        >
          Gửi
        </Button>
      </Box>
    </Box>
  );
}

export default Home;
