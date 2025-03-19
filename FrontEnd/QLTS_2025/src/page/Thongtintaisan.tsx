import { useEffect, useState } from "react";
import {
  Huyen,
  MucDichTS,
  quocgia,
  Thongtinchung,
  Tinh,
} from "../validateform/thongtinchung";

import {
  TextField,
  FormControl,
  Box,
  Grid,
  Typography,
  Autocomplete,
} from "@mui/material";
import { FieldErrors, UseFormRegister } from "react-hook-form";
import {
  GetDMDuoiTinh,
  GetDMLyDoTangDat,
  GetDMMucDichTS,
  GetDMQuocGia,
  GetDMTinhTP,
} from "../service/ServiceDat";

interface ThongtintaisanProps {
  register: UseFormRegister<Thongtinchung>;
  errors: FieldErrors<Thongtinchung>;
}

const Thongtintaisan = ({ register, errors }: ThongtintaisanProps) => {
  const [tinhTPs, setTinhTPs] = useState<Tinh[]>([]);
  const [selectedTinh, setselectedTinh] = useState<string | null>(null);
  const [selectedQuocGia, setSelectedQuocGia] = useState<number | null>(null);
  const [quocGia, setQuocGia] = useState<quocgia[]>([]);
  const [quans, setQuans] = useState<Huyen[]>([]);
  const [selectedQuan, setselectedQuan] = useState<string | null>(null);
  const [phuongs, setPhuongs] = useState<Huyen[]>([]);
  const [mucDichTS, setMucDichTSs] = useState<MucDichTS[]>([]);
  const [lyDoTangDat, setLyDoTangDats] = useState<MucDichTS[]>([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [quocGiaData, mucDichTS, lyDoTangDat] = await Promise.all([
          GetDMQuocGia(),
          GetDMMucDichTS(1),
          GetDMLyDoTangDat(1),
        ]);
        setQuocGia(quocGiaData);
        setMucDichTSs(mucDichTS);
        setLyDoTangDats(lyDoTangDat);
      } catch (error) {
        console.error("Lỗi khi tải dữ liệu:", error);
      }
    };

    fetchData();
  }, []);

  useEffect(() => {
    const fetchTinhTP = async () => {
      if (selectedQuocGia) {
        try {
          const data = await GetDMTinhTP(selectedQuocGia);
          setTinhTPs(data || []);
        } catch (error) {
          console.error("Lỗi khi ", error);
          setTinhTPs([]);
        }
      } else {
        setTinhTPs([]);
      }
    };

    const fetchQuanHuyen = async () => {
      if (selectedTinh) {
        try {
          const data = await GetDMDuoiTinh(selectedTinh);
          setQuans(data || []);
        } catch (error) {
          console.error("Lỗi khi ", error);
          setQuans([]);
        }
      } else {
        setQuans([]);
      }
    };

    const fetchPhuongXa = async () => {
      if (selectedQuan) {
        try {
          const data = await GetDMDuoiTinh(selectedQuan);
          setPhuongs(data || []);
        } catch (error) {
          console.error("Lỗi khi ", error);
        }
      } else {
        setPhuongs([]);
      }
    };

    Promise.all([fetchTinhTP(), fetchQuanHuyen(), fetchPhuongXa()]);
  }, [selectedQuocGia, selectedTinh, selectedQuan]);

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
              options={quocGia.map((quocGias) => quocGias.ten)}
              getOptionLabel={(option) => option}
              onChange={(_, value) => {
                const selected = quocGia.find((qg) => qg.ten === value);
                setSelectedQuocGia(selected?.id || null);
              }}
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
              options={quans.map((quan) => quan.ten)}
              getOptionLabel={(option) => option}
              disabled={!selectedTinh}
              onChange={(_, value) => {
                const selected = quans.find((quan) => quan.ten === value);
                setselectedQuan(selected?.ma || null);
              }}
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
            <Autocomplete
              className="pt-[1px]"
              options={lyDoTangDat.map((lydo) => lydo.ten)}
              getOptionLabel={(option) => option}
              renderInput={(params) => (
                <TextField
                  {...params}
                  placeholder="-- Chọn lý do tăng đất --"
                  {...register("lydotang", { required: true })}
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
            inputProps={{
              sx: { fontSize: "14px" },
              pattern: "[0-1][0-9]/[0-3][0-9]/[0-9]{4}", // Định dạng mm/dd/yyyy
            }}
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
              options={tinhTPs.map((tinh) => tinh.ten)}
              getOptionLabel={(option) => option}
              disabled={!selectedQuocGia}
              // onChange={(_, value) => setValue("tinhthanhpho", value || "")}
              onChange={(_, value) => {
                const selected = tinhTPs.find((tinh) => tinh.ten === value);
                setselectedTinh(selected?.ma || null);
              }}
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
              options={phuongs.map((phuong) => phuong.ten)}
              getOptionLabel={(option) => option}
              disabled={!selectedQuan}
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
            <Autocomplete
              className="pt-[1px]"
              options={mucDichTS}
              getOptionLabel={(option) => `${option.ma} - ${option.ten}`}
              renderInput={(params) => (
                <TextField
                  {...params}
                  placeholder="-- Chọn mục đích sử dụng --"
                  {...register("mucdich", { required: true })}
                  sx={{
                    fontSize: "14px",
                    "& .MuiInputBase-root": {
                      height: "36px",
                    },
                  }}
                />
              )}
              noOptionsText="Không tìm thấy mục đích sử dụng"
              renderOption={(props, option) => (
                <li {...props} style={{ fontSize: "14px" }}>
                  {option.ma} - {option.ten}
                </li>
              )}
            />
            {errors.mucdich && (
              <span className="text-red-500 text-xs">
                Bạn phải mục đích sử dụng
              </span>
            )}
          </FormControl>
        </Grid>
      </Grid>
    </Box>
  );
};

export default Thongtintaisan;
