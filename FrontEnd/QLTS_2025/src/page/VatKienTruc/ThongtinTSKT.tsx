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

import { MucDichTS, quocgia } from "../../validateform/thongtinchung";
import {
  FieldErrors,
  UseFormClearErrors,
  UseFormRegister,
  UseFormSetValue,
} from "react-hook-form";
import { useEffect, useState } from "react";
import ThemMoiBoPhan from "../../components/form/ThemMoiBoPhan";
import {
  GetDMQuocGia,
  GetDMLyDoTangDat,
  GetDMMucDichTS,
} from "../../service/ServiceDat";
import {
  handleChangeChieuDai,
  handleChangeDienTich,
  handleChangeTheTich,
} from "../../components/form/HandleChaneVKT";
import { ThongtinchungVkt } from "../../validateform/thongtinVkt";
import { GetListBoPhanSD } from "../../service/ServiceNha";
import { BoPhanSuDung } from "../../validateform/thongtinnha";

interface ThongtintaisanVktProps {
  register: UseFormRegister<ThongtinchungVkt>;
  errors: FieldErrors<ThongtinchungVkt>;
  setValue: UseFormSetValue<ThongtinchungVkt>;
  clearErrors: UseFormClearErrors<ThongtinchungVkt>;
}

const ThongtinTSKT = ({
  register,
  errors,
  setValue,
  clearErrors,
}: ThongtintaisanVktProps) => {
  const [openThemBP, setOpenThemBP] = useState(false);
  const [quocGia, setQuocGia] = useState<quocgia[]>([]);
  const [lyDoTang, setLyDoTangs] = useState<MucDichTS[]>([]);
  const [mucDichTS, setMucDichTSs] = useState<MucDichTS[]>([]);
  const [boPhanSuDung, setBoPhanSuDung] = useState<BoPhanSuDung[]>([]);
  const [displayValues, setDisplayValues] = useState<Record<string, string>>(
    {}
  );

  useEffect(() => {
    const fetchData = async () => {
      const param = { donViId: 1 };
      try {
        const [quocGiaData, mucDichTS, boPhanSuDung, lyDoTangDat] =
          await Promise.all([
            GetDMQuocGia(),
            GetDMMucDichTS(3),
            GetListBoPhanSD(param),
            GetDMLyDoTangDat(3), // Using 1 as default loaiHinhTaiSanId
          ]);
        setQuocGia(quocGiaData || []);
        setMucDichTSs(mucDichTS);
        setLyDoTangs(lyDoTangDat || []);
        setBoPhanSuDung(boPhanSuDung || []);
      } catch (error) {
        console.error("Lỗi khi tải dữ liệu:", error);
        setQuocGia([]);
        setLyDoTangs([]);
      }
    };

    fetchData();
  }, []);

  const handleThemBoPhan = () => {
    setOpenThemBP(true);
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
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Lý do tăng<span style={{ color: "red" }}>*</span>
          </Typography>
          <FormControl fullWidth margin="dense" size="small">
            <Autocomplete
              className="pt-[1px]"
              options={lyDoTang}
              getOptionLabel={(option) => option.TEN}
              {...register("LY_DO_TANG_ID", {
                required: "Bạn phải chọn lý do tăng ",
              })}
              onChange={(_, value) => {
                if (value) {
                  const selected = lyDoTang.find(
                    (lydo) => lydo.ID === value?.ID
                  );
                  setValue("LY_DO_TANG_ID", selected?.ID || -1);
                  clearErrors("LY_DO_TANG_ID");
                }
              }}
              renderInput={(params) => (
                <TextField
                  {...params}
                  placeholder="-- Chọn lý do tăng --"
                  sx={{
                    fontSize: "14px",
                    "& .MuiInputBase-root": {
                      height: "36px",
                    },
                  }}
                />
              )}
              noOptionsText="Không tìm thấy lý do tăng"
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
          {/* {errors.LY_DO_TANG && (
            <span className="text-red-500 text-xs">
              Bạn phải nhập tên tài sản
            </span>
          )} */}
          {/* Loại tài sản */}
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Loại tài sản <span style={{ color: "red" }}>*</span>
          </Typography>
          <FormControl fullWidth margin="dense" size="small">
            <Autocomplete
              className="pt-[1px]"
              options={mucDichTS}
              getOptionLabel={(option) => `${option.MA} - ${option.TEN}`}
              {...register("LOAI_TAI_SAN_ID", {
                required: "Bạn phải chọn loại tài sản",
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
                  placeholder="-- Chọn loại tài sản --"
                  sx={{
                    fontSize: "14px",
                    "& .MuiInputBase-root": {
                      height: "36px",
                    },
                  }}
                />
              )}
              noOptionsText="Không tìm thấy loại tài sản"
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

          {/* Chiếu dài and Diện tích */}
          <Grid container spacing={2}>
            <Grid item xs={6}>
              <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
                Chiếu dài<span style={{ color: "red" }}></span>
              </Typography>
              <TextField
                size="small"
                fullWidth
                variant="outlined"
                sx={{ marginBottom: 2 }}
                InputProps={{ endAdornment: <span>m</span> }}
                value={displayValues.CHIEU_DAI || ""}
                onChange={handleChangeChieuDai(setValue, (value) =>
                  setDisplayValues((prev) => ({
                    ...prev,
                    CHIEU_DAI: value,
                  }))
                )}
              />
            </Grid>
            <Grid item xs={6}>
              <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
                Diện tích<span style={{ color: "red" }}></span>
              </Typography>
              <TextField
                size="small"
                fullWidth
                variant="outlined"
                sx={{ marginBottom: 2 }}
                InputProps={{ endAdornment: <span>m²</span> }}
                value={displayValues.DIEN_TICH || ""}
                onChange={handleChangeDienTich(setValue, (value) =>
                  setDisplayValues((prev) => ({
                    ...prev,
                    DIEN_TICH: value,
                  }))
                )}
              />
            </Grid>
          </Grid>

          {/* nước sản xuất */}
          <FormControl
            fullWidth
            variant="outlined"
            sx={{ marginBottom: 2 }}
            size="small"
          >
            <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
              Nước sản xuất<span style={{ color: "red" }}></span>
            </Typography>
            <Autocomplete
              className="pt-[1px]"
              options={quocGia.map((quocGias) => quocGias.TEN)}
              getOptionLabel={(option) => option}
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
            InputProps={{
              sx: { fontSize: "14px" },
              inputProps: {
                max: new Date().toISOString().split("T")[0], // Prevents future dates
              },
            }}
            {...register("NGAY_DU_VAO_SD", {
              required: "Bạn phải nhập ngày đưa vào sử dụng",
              validate: (value) => {
                if (!value) return "Bạn phải nhập ngày đưa vào sử dụng";
                const selectedDate = new Date(value);
                const today = new Date();
                if (selectedDate > today) {
                  return "Ngày đưa vào sử dụng không được lớn hơn ngày hiện tại";
                }
                return true;
              },
            })}
          />
          {errors?.NGAY_DU_VAO_SD && (
            <span className="text-red-500 text-xs">
              {errors?.NGAY_DU_VAO_SD?.message}
            </span>
          )}
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
            {...register("NGAY_TANG")}
          />

          {/* Bộ phận sử dụng */}

          <Grid container spacing={2}>
            <Grid item xs={6}>
              <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
                Bộ phận sử dụng
              </Typography>
              <Autocomplete
                className="pt-[2px] pb-5"
                options={boPhanSuDung}
                getOptionLabel={(option) => option.TEN}
                onChange={(_, value) => {
                  const selected = boPhanSuDung.find(
                    (lydo) => lydo.ID === value?.ID
                  );
                  setValue("BO_PHAN_ID", selected?.ID || -1);
                  clearErrors("BO_PHAN_ID");
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
                    {option.TEN}
                  </li>
                )}
              />
            </Grid>
            <Grid item xs={6}>
              <IconButton
                sx={{
                  width: "40px",
                  height: "40px",
                  position: "absolute",
                  right: "27%",
                  top: "35%",
                  transform: "translateX(-100%)",
                  "& .MuiSvgIcon-root": {
                    fontSize: "24px",
                  },
                }}
                onClick={handleThemBoPhan}
              >
                <AddCircleOutlineIcon color="primary" />
              </IconButton>
            </Grid>
          </Grid>

          {/* Thể tích */}
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Thể tích<span style={{ color: "red" }}></span>
          </Typography>
          <TextField
            size="small"
            name="THE_TICH"
            fullWidth
            variant="outlined"
            sx={{ marginBottom: 2 }}
            InputProps={{ endAdornment: <span>m³</span> }}
            value={displayValues.THE_TICH || ""}
            onChange={handleChangeTheTich(setValue, (value) =>
              setDisplayValues((prev) => ({
                ...prev,
                THE_TICH: value,
              }))
            )}
          />

          {/* Năm sản xuất */}
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Năm sản xuất<span style={{ color: "red" }}></span>
          </Typography>
          <TextField
            size="small"
            margin="dense"
            name="NAM_SX"
            InputProps={{ sx: { fontSize: "14px", width: "65px" } }}
            inputProps={{
              maxLength: 4, // Giới hạn tối đa 4 ký tự
              inputMode: "numeric", // Chỉ cho phép nhập số
            }}
          />
          {/* Mô tả chung */}
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Mô tả chung<span style={{ color: "red" }}></span>
          </Typography>
          <TextField
            size="small"
            name="MO_TA"
            fullWidth
            variant="outlined"
            sx={{ marginBottom: 2 }}
          />
        </Grid>
      </Grid>

      {/* Popup thêm bộ phận */}
      <ThemMoiBoPhan
        open={openThemBP}
        handleClose={() => setOpenThemBP(false)}
      />
    </Box>
  );
};

export default ThongtinTSKT;
