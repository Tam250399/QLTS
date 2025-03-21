import {
  Huyen,
  LyDoTangDat,
  Phuong,
  quocgia,
  Tinh,
} from "../../validateform/thongtinchung";

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
import {
  FieldErrors,
  UseFormClearErrors,
  UseFormRegister,
  UseFormSetValue,
} from "react-hook-form";
import DeleteOutlineIcon from "@mui/icons-material/DeleteOutline";
import { ThongTinNha } from "../../validateform/thongtinnha";
import ThemMoiBoPhan from "../../components/form/ThemMoiBoPhan";
import ChonDat from "../../components/form/ChonDat";
import {
  GetDMDuoiTinh,
  GetDMLyDoTangDat,
  GetDMQuocGia,
  GetDMTinhTP,
} from "../../service/ServiceDat";

interface ThongtinnhaProps {
  register: UseFormRegister<ThongTinNha>;
  errors: FieldErrors<ThongTinNha>;
  setValue: UseFormSetValue<ThongTinNha>;
  clearErrors: UseFormClearErrors<ThongTinNha>;
}

const ThongTinChung = ({
  register,
  errors,
  setValue,
  clearErrors,
}: ThongtinnhaProps) => {
  const [lyDoTangDat, setLyDoTangDats] = useState<LyDoTangDat[]>([]);
  const [quanLyDat, setQuanLyDat] = useState("co");
  const [khuonVienDat, setKhuonVienDat] = useState("");
  const [openChonDat, setOpenChonDat] = useState(false);
  const [openThemBP, setOpenThemBP] = useState(false);
  const [tinhTPs, setTinhTPs] = useState<Tinh[]>([]);
  const [selectedTinh, setselectedTinh] = useState<string | null>(null);
  const [selectedQuocGia, setSelectedQuocGia] = useState<number | null>(null);
  const [quocGia, setQuocGia] = useState<quocgia[]>([]);
  const [quans, setQuans] = useState<Huyen[]>([]);
  const [selectedQuan, setselectedQuan] = useState<string | null>(null);
  const [phuongs, setPhuongs] = useState<Phuong[]>([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [quocGiaData, lyDoTangDat] = await Promise.all([
          GetDMQuocGia(),
          GetDMLyDoTangDat(1),
        ]);
        setQuocGia(quocGiaData);
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

  const handleThemBoPhan = () => {
    setOpenThemBP(true);
  };
  const handleChonDat = () => {
    setOpenChonDat(true);
  };
  const handleChonKhuonVienDat = (diachi: string) => {
    setKhuonVienDat(diachi);
    setValue("KHUON_VIEN_DAT", diachi);
    setOpenChonDat(false);
  };

  const handleBoChonKhuonVienDat = () => {
    setKhuonVienDat("");
    setValue("KHUON_VIEN_DAT", "");
    setOpenChonDat(false);
  };

  return (
    <>
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
                    size="small"
                    margin="dense"
                    {...register("KHUON_VIEN_DAT")}
                    value={khuonVienDat}
                    InputProps={{
                      readOnly: true,
                      sx: { fontSize: "14px", backgroundColor: "#e9ecef" },
                    }}
                    disabled={true}
                  />
                  <div>
                    <Button
                      variant="outlined"
                      sx={{
                        height: "26px",
                        marginLeft: "8px",
                        fontSize: "14px",
                        whiteSpace: "nowrap",
                        textTransform: "none",
                        fontWeight: 400,
                        marginBottom: "4px",
                      }}
                      onClick={handleChonDat}
                    >
                      + Chọn đất
                    </Button>
                    {khuonVienDat !== "" && (
                      <Button
                        variant="outlined"
                        sx={{
                          height: "26px",
                          marginLeft: "8px",
                          fontSize: "14px",
                          whiteSpace: "nowrap",
                          textTransform: "none",
                          fontWeight: 400,
                          color: "#d32f2f",
                          borderColor: "#d32f2f",
                          "&:hover": {
                            backgroundColor: "#ffebee",
                            borderColor: "#c62828",
                          },
                        }}
                        onClick={handleBoChonKhuonVienDat}
                        startIcon={
                          <DeleteOutlineIcon sx={{ color: "#d32f2f" }} />
                        }
                      >
                        Bỏ chọn đất
                      </Button>
                    )}
                  </div>
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
              {quanLyDat === "khong" && (
                <>
                  <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
                    Quốc gia <span style={{ color: "red" }}>*</span>
                  </Typography>
                  <FormControl fullWidth margin="dense">
                    <Autocomplete
                      className="pt-[1px]"
                      options={quocGia.map((quocGias) => quocGias.ten)}
                      {...register("QUOC_GIA_ID", {
                        required: "Bạn phải chọn Quốc gia",
                      })}
                      getOptionLabel={(option) => option}
                      onChange={(_, value) => {
                        if (value) {
                          const selected = quocGia.find(
                            (quocGia) => quocGia.ten === value
                          );
                          setValue("QUOC_GIA_ID", selected?.id || -1);
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
                      options={quans.map((quan) => quan.ten)}
                      {...register("QUAN_HUYEN_ID", {
                        required: "Bạn phải chọn Quận/Huyện",
                      })}
                      getOptionLabel={(option) => option}
                      disabled={!selectedTinh}
                      onChange={(_, value) => {
                        if (value) {
                          const selected = quans.find(
                            (quans) => quans.ten === value
                          );
                          setValue("QUAN_HUYEN_ID", selected?.id || -1);
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
                </>
              )}
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
                  if (value) {
                    const selected = lyDoTangDat.find(
                      (lydo) => lydo.id === value?.id
                    );
                    setValue("LY_DO_TANG_ID", selected?.id || -1);
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
                  if (value) {
                    const selected = lyDoTangDat.find(
                      (lydo) => lydo.id === value?.id
                    );
                    setValue("CAP_NHA_ID", selected?.id || -1);
                    clearErrors("CAP_NHA_ID");
                  }
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
                  {...register("BO_PHAN_SD_ID", {
                    required: "Bạn phải chọn bộ phận sử dụng",
                  })}
                  onChange={(_, value) => {
                    if (value) {
                      const selected = lyDoTangDat.find(
                        (lydo) => lydo.id === value?.id
                      );
                      setValue("BO_PHAN_SD_ID", selected?.id || -1);
                      clearErrors("BO_PHAN_SD_ID");
                    }
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
                    minWidth: "30px",
                    height: "30px",
                    marginLeft: "8px", // Tạo khoảng cách với Autocomplete
                  }}
                  onClick={handleThemBoPhan} // Gọi hàm khi bấm "+"
                >
                  +
                </Button>
              </Box>
              {errors?.BO_PHAN_SD_ID && (
                <span className="text-red-500 text-xs">
                  {errors?.BO_PHAN_SD_ID?.message}
                </span>
              )}
            </Stack>
          </Grid>
          <Grid item xs={12} md={6}>
            <Stack spacing={1}>
              <Grid spacing={1} xs={6} md={12}>
                {quanLyDat === "khong" && (
                  <>
                    <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
                      Tỉnh/Thành phố <span style={{ color: "red" }}>*</span>
                    </Typography>
                    <FormControl fullWidth margin="dense" size="small">
                      <Autocomplete
                        className="pt-[1px]"
                        options={tinhTPs.map((tinh) => tinh.ten)}
                        getOptionLabel={(option) => option}
                        disabled={!selectedQuocGia}
                        {...register("TINH_THANH_PHO_ID", {
                          required: "Bạn phải chọn Tỉnh/Thành phố",
                        })}
                        // onChange={(_, value) => setValue("tinhthanhpho", value || "")}
                        onChange={(_, value) => {
                          if (value) {
                            const selected = tinhTPs.find(
                              (tinh) => tinh.ten === value
                            );
                            setValue("TINH_THANH_PHO_ID", selected?.id || -1);
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
                      {errors?.TINH_THANH_PHO_ID && (
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
                        options={phuongs.map((phuong) => phuong.ten)}
                        getOptionLabel={(option) => option}
                        disabled={!selectedQuan}
                        {...register("XA_PHUONG_ID", {
                          required: "Bạn phải chọn Xã/Phường",
                        })}
                        onChange={(_, value) => {
                          if (value) {
                            const selected = phuongs.find(
                              (phuong) => phuong.ten === value
                            );
                            setValue("XA_PHUONG_ID", selected?.id || -1);
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
                      {errors?.XA_PHUONG_ID && (
                        <span className="text-red-500 text-xs">
                          {errors?.XA_PHUONG_ID?.message}
                        </span>
                      )}
                    </FormControl>
                  </>
                )}
              </Grid>
              <Grid item spacing={1} xs={6} md={6}>
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
                    {...register("SO_TANG", {
                      required: "Bạn phải nhập số tầng",
                    })}
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
            </Stack>
          </Grid>
        </Grid>
      </Box>

      {/* Popup thêm bộ phận */}
      <ThemMoiBoPhan
        open={openThemBP}
        handleClose={() => setOpenThemBP(false)}
      />

      {/* Popup chọn đất */}
      <ChonDat
        open={openChonDat}
        handleClose={() => setOpenChonDat(false)}
        handleChonKhuonVienDat={handleChonKhuonVienDat}
      />
    </>
  );
};

export default ThongTinChung;
