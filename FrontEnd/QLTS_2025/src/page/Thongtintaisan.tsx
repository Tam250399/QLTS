import { useEffect, useState } from "react";
import { Thongtinchung } from "../validateform/thongtinchung";

import {
  TextField,
  Select,
  MenuItem,
  FormControl,
  Box,
  Grid,
  Typography,
  Autocomplete,
} from "@mui/material";
import { FieldErrors, UseFormRegister } from "react-hook-form";
import ApiService from "../service/ApiService";
import GetDMQuocGia from "../service/ServiceDat";
// type Inputs = {
//   diachi: string;
//   quocgia: string;
// };
const countriess = [
  "Việt Nam",
  "Nhật Bản",
  "Hàn Quốc",
  "Trung Quốc",
  "Mỹ",
  // Thêm các quốc gia khác
];

interface Country {
  id: number;
  name: string;
  email: string;
}

interface ThongtintaisanProps {
  register: UseFormRegister<Thongtinchung>;
  errors: FieldErrors<Thongtinchung>;
}

const Thongtintaisan = ({ register, errors }: ThongtintaisanProps) => {
  const [loading, setLoading] = useState<boolean>(false);
  const [countries, setCountries] = useState<Country[]>([]);

  // const fetchUsers = async () => {

  //     setLoading(true);
  //     // Gọi API GET
  //     const response = await ApiService.get<[]>('/DanhMuc/quocGia');
  //     setCountries(response.data);
  //     console.log("test" , response.data);
  // };

  // useEffect(() => {
  //   // fetchUsers();
  //   GetDMQuocGia();
  // }, []);

  const [categories, setCategories] = useState([]);

  useEffect(() => {
    const fetchCategories = async () => {
      const data = await GetDMQuocGia();
      setCategories(data);
    };
    fetchCategories();
  }, []);

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
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Quốc gia <span style={{ color: "red" }}>*</span>
          </Typography>
          <FormControl fullWidth margin="dense">
            <Autocomplete
              className="pt-[1px]"
              options={categories.map((category) => category.ten)}
              getOptionLabel={(option) => option}
              renderInput={(params) => (
                <TextField
                  {...params}
                  placeholder="-- Chọn Quốc Gia --"
                  {...register("quocgia", { required: true })}
                  sx={{
                    fontSize: "14px",
                    "& .MuiInputBase-root": {
                      height: "36px",
                    },
                  }}
                />
              )}
              noOptionsText="Không tìm thấy quốc gia"
              renderOption={(props, option) => (
                <li {...props} style={{ fontSize: "14px" }}>
                  {option}
                </li>
              )}
            />
            {errors.quocgia && (
              <span className="text-red-500 text-xs">
                Bạn phải chọn Quốc gia
              </span>
            )}
          </FormControl>

          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Quận/Huyện <span style={{ color: "red" }}>*</span>
          </Typography>
          <FormControl fullWidth margin="dense" size="small">
            <Autocomplete
              className="pt-[1px]"
              options={countriess}
              getOptionLabel={(option) => option}
              renderInput={(params) => (
                <TextField
                  {...params}
                  placeholder="-- Chọn Quận/Huyện--"
                  {...register("quanhuyen", { required: true })}
                  sx={{
                    fontSize: "14px",
                    "& .MuiInputBase-root": {
                      height: "36px",
                    },
                  }}
                />
              )}
              noOptionsText="Không tìm thấy quận huyện"
              renderOption={(props, option) => (
                <li {...props} style={{ fontSize: "14px" }}>
                  {option}
                </li>
              )}
            />
            {errors.quanhuyen && (
              <span className="text-red-500 text-xs">
                Bạn phải chọn quận huyện
              </span>
            )}
          </FormControl>

          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Lý do tăng đất <span style={{ color: "red" }}>*</span>
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
              {...register("lydotang", { required: true })}
            >
              <MenuItem value="Đăng ký lần đầu">Đăng ký lần đầu</MenuItem>
              <MenuItem value="Đăng ký lần đầu">Đăng ký lần đầu</MenuItem>
            </Select>
            {errors.lydotang && (
              <span className="text-red-500 text-xs">
                Bạn phải chọn lý do tăng
              </span>
            )}
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
            {...register("ngaytang", { required: true })}
          />
          {errors.ngaytang && (
            <span className="text-red-500 text-xs">
              Bạn phải chọn ngày tăng
            </span>
          )}
        </Grid>
        <Grid item xs={12} md={6}>
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Tỉnh/Thành phố <span style={{ color: "red" }}>*</span>
          </Typography>
          <FormControl fullWidth margin="dense" size="small">
            <Autocomplete
              className="pt-[1px]"
              options={countriess}
              getOptionLabel={(option) => option}
              renderInput={(params) => (
                <TextField
                  {...params}
                  placeholder="-- Chọn Tỉnh/Thành phố--"
                  {...register("tinhthanhpho", { required: true })}
                  sx={{
                    fontSize: "14px",
                    "& .MuiInputBase-root": {
                      height: "36px",
                    },
                  }}
                />
              )}
              noOptionsText="Không tìm thấy Tỉnh/Thành phố"
              renderOption={(props, option) => (
                <li {...props} style={{ fontSize: "14px" }}>
                  {option}
                </li>
              )}
            />
            {errors.tinhthanhpho && (
              <span className="text-red-500 text-xs">
                Bạn phải chọn Tỉnh/Thành phố
              </span>
            )}
          </FormControl>

          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Xã/Phường <span style={{ color: "red" }}>*</span>
          </Typography>
          <FormControl fullWidth margin="dense" size="small">
            <Autocomplete
              className="pt-[1px]"
              getOptionLabel={(option) => option}
              options={countriess}
              renderInput={(params) => (
                <TextField
                  {...params}
                  placeholder="-- Chọn Xã/Phường--"
                  {...register("xaphuong", { required: true })}
                  sx={{
                    fontSize: "14px",
                    "& .MuiInputBase-root": {
                      height: "36px",
                    },
                  }}
                />
              )}
              noOptionsText="Không tìm thấy Xã/Phường"
              renderOption={(props, option) => (
                <li {...props} style={{ fontSize: "14px" }}>
                  {option}
                </li>
              )}
            />
            {errors.xaphuong && (
              <span className="text-red-500 text-xs">
                Bạn phải chọn Xã/Phường
              </span>
            )}
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
              {...register("mucdich", { required: true })}
            ></Select>
            {errors.mucdich && (
              <span className="text-red-500 text-xs">
                Bạn phải chọn mục đích sử dụng
              </span>
            )}
          </FormControl>
        </Grid>
      </Grid>
    </Box>
  );
};

export default Thongtintaisan;
