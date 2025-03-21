import {
  TextField,
  FormControl,
  Grid,
  Typography,
  Box,
  IconButton,
  Autocomplete,
  Button,
} from "@mui/material";

import AddCircleOutlineIcon from "@mui/icons-material/AddCircleOutline";

import {
  LyDoTangDat,
  quocgia,
  ThongtinchungVkt,
} from "../../validateform/thongtinVkt";
import { FieldErrors, UseFormRegister, UseFormSetValue } from "react-hook-form";
import { useEffect, useState } from "react";
import ThemMoiBoPhan from "../../components/form/ThemMoiBoPhan";
import { GetDMQuocGia } from "../../service/ServiceDat";

interface ThongtintaisanVktProps {
  register: UseFormRegister<ThongtinchungVkt>;
  errors: FieldErrors<ThongtinchungVkt>;
  setValue: UseFormSetValue<ThongtinchungVkt>;
}
const options = [
  { label: "The Godfather", id: 1 },
  { label: "Pulp Fiction", id: 2 },
];
const ThongtinTSKT = ({
  register,
  errors,
  setValue,
}: ThongtintaisanVktProps) => {
  const [openThemBP, setOpenThemBP] = useState(false);
  const [lyDoTangDat, setLyDoTangDats] = useState<LyDoTangDat[]>([]);
  const [quocGia, setQuocGia] = useState<quocgia[]>([]);

  useEffect(() => {
    const fetchNuocSX = async () => {
      try {
        const data = await GetDMQuocGia();
        setQuocGia(data || []);
      } catch (error) {
        console.error("Lỗi khi ", error);
        setQuocGia([]);
      }
    };
    fetchNuocSX();
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
              options={options}
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
              renderOption={(props) => (
                <li {...props} style={{ fontSize: "14px" }}></li>
              )}
            />
            {errors.LOAI_TAI_SAN_ID && (
              <span className="text-red-500 text-xs">
                Bạn phải chọn loại tài sản
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
                {...register("CHIEU_DAI", { required: true })}
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
                {...register("DIEN_TICH", { required: true })}
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
              options={quocGia.map((quocGias) => quocGias.ten)}
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
            defaultValue="2017-12-31"
            InputProps={{ sx: { fontSize: "14px" } }}
            {...register("NGAY_DU_VAO_SD", { required: true })}
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
                options={lyDoTangDat}
                getOptionLabel={(option) => option.ten}
                onChange={(_, value) => {
                  const selected = lyDoTangDat.find(
                    (lydo) => lydo.id === value?.id
                  );
                  setValue("BO_PHAN_ID", selected?.id || -1);
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
            </Grid>
            <Grid item xs={6}>
              <IconButton
                sx={{
                  right: 85,
                  top: "30%",
                  transform: "translateX(-100%)",
                }}
                onClick={handleThemBoPhan} // Gọi hàm khi bấm "+"
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
