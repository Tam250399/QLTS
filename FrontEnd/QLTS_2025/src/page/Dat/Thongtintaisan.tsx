import { useEffect, useState } from "react";
import {
  Huyen,
  MucDichTS,
  quocgia,
  Thongtinchung,
  Tinh,
} from "../../validateform/thongtinchung";

import {
  TextField,
  FormControl,
  Box,
  Grid,
  Typography,
  Autocomplete,
} from "@mui/material";
import {
  FieldErrors,
  UseFormClearErrors,
  UseFormRegister,
  UseFormSetValue,
} from "react-hook-form";
import {
  GetDMDuoiTinh,
  GetDMLyDoTangDat,
  GetDMMucDichTS,
  GetDMQuocGia,
  GetDMTinhTP,
} from "../../service/ServiceDat";

interface ThongtintaisanProps {
  register: UseFormRegister<Thongtinchung>;
  errors: FieldErrors<Thongtinchung>;
  setValue: UseFormSetValue<Thongtinchung>;
  clearErrors: UseFormClearErrors<Thongtinchung>;
}

const Thongtintaisan = ({
  register,
  errors,
  setValue,
  clearErrors,
}: ThongtintaisanProps) => {
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
            {...register("TEN", { required: true })}
          />
          {errors.TEN && (
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
              options={quocGia.map((quocGias) => quocGias.TEN)}
              getOptionLabel={(option) => option}
              {...register("QUOC_GIA_ID", {
                required: "Bạn phải chọn quốc gia",
              })}
              onChange={(_, value) => {
                if (value) {
                  const selected = quocGia.find(
                    (quocGia) => quocGia.TEN === value
                  );
                  setSelectedQuocGia(selected?.ID || null);
                  setValue("QUOC_GIA_ID", selected?.ID || -1);
                  clearErrors("QUOC_GIA_ID");
                }
              }}
              renderInput={(params) => (
                <TextField
                  {...params}
                  placeholder="-- Chọn Quốc Gia --"
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
            {errors?.QUOC_GIA_ID && (
              <span className="text-red-500 text-xs">
                {errors?.QUOC_GIA_ID?.message}
              </span>
            )}
          </FormControl>

          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Quận/Huyện <span style={{ color: "red" }}>*</span>
          </Typography>
          <FormControl fullWidth margin="dense" size="small">
            <Autocomplete
              className="pt-[1px]"
              options={quans.map((quan) => quan.TEN)}
              getOptionLabel={(option) => option}
              disabled={!selectedTinh}
              {...register("QUAN_HUYEN_ID", {
                required: "Bạn phải chọn Quận/Huyện",
              })}
              onChange={(_, value) => {
                if (value) {
                  const selected = quans.find((quan) => quan.TEN === value);
                  setselectedQuan(selected?.MA || null);
                  setValue("QUAN_HUYEN_ID", selected?.ID || -1);
                  clearErrors("QUAN_HUYEN_ID");
                }
              }}
              renderInput={(params) => (
                <TextField
                  {...params}
                  placeholder="-- Chọn Quận/Huyện--"
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
            {errors?.QUAN_HUYEN_ID && (
              <span className="text-red-500 text-xs">
                {errors?.QUAN_HUYEN_ID?.message}
              </span>
            )}
          </FormControl>

          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Lý do tăng đất <span style={{ color: "red" }}>*</span>
          </Typography>
          <FormControl fullWidth margin="dense" size="small">
            <Autocomplete
              className="pt-[1px]"
              options={lyDoTangDat}
              getOptionLabel={(option) => option.TEN}
              {...register("LY_DO_TANG_ID", {
                required: "Bạn phải chọn lý do tăng đất",
              })}
              onChange={(_, value) => {
                if (value) {
                  const selected = lyDoTangDat.find(
                    (lydo) => lydo.ID === value?.ID
                  );
                  setValue("LY_DO_TANG_ID", selected?.ID || -1);
                  clearErrors("LY_DO_TANG_ID");
                }
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
              noOptionsText="Không tìm thấy quận huyện"
              renderOption={(props, option) => (
                <li {...props} style={{ fontSize: "14px" }}>
                  {option.TEN}
                </li>
              )}
            />
            {errors?.LY_DO_TANG_ID && (
              <span className="text-red-500 text-xs">
                {errors?.LY_DO_TANG_ID?.message}
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
            {...register("NGAY_TANG", { required: true })}
          />
          {errors.NGAY_TANG && (
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
              options={tinhTPs.map((tinh) => tinh.TEN)}
              getOptionLabel={(option) => option}
              disabled={!selectedQuocGia}
              {...register("TINH_THANH_PHO_ID", {
                required: "Bạn phải chọn Tỉnh/Thành phố",
              })}
              // onChange={(_, value) => setValue("tinhthanhpho", value || "")}
              onChange={(_, value) => {
                if (value) {
                  const selected = tinhTPs.find((tinh) => tinh.TEN === value);
                  setselectedTinh(selected?.MA || null);
                  setValue("TINH_THANH_PHO_ID", selected?.ID || -1);
                  clearErrors("TINH_THANH_PHO_ID");
                }
              }}
              renderInput={(params) => (
                <TextField
                  {...params}
                  placeholder="-- Chọn Tỉnh/Thành phố--"
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
            {errors.TINH_THANH_PHO_ID && (
              <span className="text-red-500 text-xs">
                {errors?.TINH_THANH_PHO_ID?.message}
              </span>
            )}
          </FormControl>

          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Xã/Phường <span style={{ color: "red" }}>*</span>
          </Typography>
          <FormControl fullWidth margin="dense" size="small">
            <Autocomplete
              className="pt-[1px]"
              options={phuongs.map((phuong) => phuong.TEN)}
              getOptionLabel={(option) => option}
              disabled={!selectedQuan}
              {...register("XA_PHUONG_ID", {
                required: "Bạn phải chọn Xã/Phường",
              })}
              onChange={(_, value) => {
                if (value) {
                  const selected = phuongs.find(
                    (phuong) => phuong.TEN === value
                  );
                  setValue("XA_PHUONG_ID", selected?.ID || -1);
                  clearErrors("XA_PHUONG_ID");
                }
              }}
              renderInput={(params) => (
                <TextField
                  {...params}
                  placeholder="-- Chọn Xã/Phường--"
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
            {errors.XA_PHUONG_ID && (
              <span className="text-red-500 text-xs">
                {errors?.XA_PHUONG_ID?.message}
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
              getOptionLabel={(option) => `${option.MA} - ${option.TEN}`}
              {...register("LOAI_TAI_SAN_ID", {
                required: "Bạn phải chọn mục đích sử dụng",
              })}
              onChange={(_, value) => {
                if (value) {
                  const selected = mucDichTS.find(
                    (mucdich) => mucdich.ID === value?.ID
                  );
                  setValue("LOAI_TAI_SAN_ID", selected?.ID || -1);
                  clearErrors("LOAI_TAI_SAN_ID");
                }
              }}
              renderInput={(params) => (
                <TextField
                  {...params}
                  placeholder="-- Chọn mục đích sử dụng --"
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
                  {option.MA} - {option.TEN}
                </li>
              )}
            />
            {errors?.LOAI_TAI_SAN_ID && (
              <span className="text-red-500 text-xs">
                {errors?.LOAI_TAI_SAN_ID?.message}
              </span>
            )}
          </FormControl>
        </Grid>
      </Grid>
    </Box>
  );
};

export default Thongtintaisan;
