import {
  Autocomplete,
  Box,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  Grid,
  IconButton,
  TextField,
  Typography,
} from "@mui/material";
import { useEffect, useState } from "react";
import CloseIcon from "@mui/icons-material/Close";
import SaveIcon from "@mui/icons-material/Save";
import { BoPhanSuDung } from "../../validateform/thongtinnha";
import { GetListBoPhanSD } from "../../service/ServiceNha";

interface BoPhan {
  donvi: string;
  tenBoPhan: string;
  address: string;
  phone: string;
  trucThuoc: number;
}

interface ThemMoiBoPhanProps {
  open: boolean;
  handleClose: () => void;
}
const ThemMoiBoPhan: React.FC<ThemMoiBoPhanProps> = ({ open, handleClose }) => {
  const data = [
    {
      id: 1,
      ten: "Đất trụ sở",
      status: "Đã duyệt",
    },
    {
      id: 2,
      ten: "Đất trụ sở",
      status: "Đã duyệt",
    },
    {
      id: 3,
      ten: "Đất trụ sở",
      status: "Đã duyệt",
    },
    {
      id: 4,
      ten: "Đất hoạt động sự nghiệp",
      status: "Chờ duyệt",
    },
    {
      id: 5,
      ten: "Đất hoạt động sự nghiệp",
      status: "Chờ duyệt",
    },
  ];
  const [donvi] = useState("Chi cục Thuế khu vực Thạch Hà - Lộc Hà");
  const [tenBoPhan, setTenBoPhan] = useState("");
  const [address, setAddress] = useState("");
  const [phone, setPhone] = useState("");
  const [formatPhone, setFormatPhone] = useState("");
  const [trucThuoc, setTrucThuoc] = useState(0);
  const [boPhanSuDung, setBoPhanSuDung] = useState<BoPhanSuDung[]>([]);
  useEffect(() => {
    const fetchData = async () => {
      try {
        const param = { donViId: 1 };
        const boPhanSuDung = await GetListBoPhanSD(param);

        setBoPhanSuDung(boPhanSuDung);
      } catch (error) {
        console.error("Lỗi khi tải dữ liệu:", error);
      }
    };

    fetchData();
  }, []);
  const handleSave = () => {
    const formData: BoPhan = {
      donvi,
      tenBoPhan,
      address,
      phone,
      trucThuoc,
    };
    if (!validatePhone(phone)) {
      setError(true); // Báo lỗi nếu chưa đủ 10 số hoặc không hợp lệ
    } else {
      console.log("Dữ liệu form:", formData);
      handleClose();
    }
  };
  const [error, setError] = useState(false);

  const validatePhone = (value: string) => {
    const phoneRegex = /^[0-9]{10}$/; // Chỉ chấp nhận đúng 10 số
    return phoneRegex.test(value);
  };

  const handleChange = (e: any) => {
    const value = e.target.value;
    let cleanedPhone = value.replace(/\D/g, "");
    let formatted = cleanedPhone;

    // Chỉ cho phép nhập số và không vượt quá 10 ký tự
    if (/^[0-9]*$/.test(cleanedPhone) && cleanedPhone.length <= 12) {
      if (cleanedPhone.length > 6) {
        formatted = `${cleanedPhone.slice(0, 3)}-${cleanedPhone.slice(
          3,
          6
        )}-${cleanedPhone.slice(6)}`;
      } else if (cleanedPhone.length > 3) {
        formatted = `${cleanedPhone.slice(0, 3)}-${cleanedPhone.slice(3)}`;
      }
      setFormatPhone(formatted);
      setPhone(cleanedPhone);
      setError(false); // Reset lỗi khi người dùng đang nhập
    }
  };

  const handleBlur = () => {
    if (!validatePhone(phone)) {
      setError(true);
    }
  };

  const handleCloseForm = () => {
    setError(false);
    setPhone("");
    handleClose();
  };
  return (
    <Dialog open={open} onClose={handleCloseForm}>
      <DialogTitle
        sx={{
          display: "flex",
          justifyContent: "space-between",
          borderBottom: "1px solid #ccc",
          alignItems: "center",
          margin: "0 0 30px 10px",
          fontSize: "30px",
        }}
      >
        Thêm mới bộ phận của đơn vị
        <IconButton onClick={handleCloseForm} size="small">
          <CloseIcon />
        </IconButton>
      </DialogTitle>
      <DialogContent>
        <Box
          sx={{
            border: "1px solid #007bff",
            borderRadius: 2,
            p: 2,
            bgcolor: "white",
            boxShadow: 2,
            position: "relative",
          }}
        >
          <Grid container spacing={2}>
            {/* Hàng 1 */}
            <Grid
              item
              xs={12}
              md={12}
              sx={{ display: "flex", alignItems: "center" }}
            >
              <Typography
                variant="subtitle2"
                sx={{ fontSize: "14px", pr: 2, minWidth: "150px" }}
              >
                Đơn vị:
              </Typography>
              <TextField
                fullWidth
                size="small"
                value="Chi cục Thuế khu vực Thạch Hà - Lộc Hà"
                InputProps={{
                  readOnly: true,
                  sx: { fontSize: "14px", backgroundColor: "#e9ecef" },
                }}
                disabled
              />
            </Grid>

            {/* Hàng 2 */}
            <Grid
              item
              xs={12}
              md={12}
              sx={{ display: "flex", alignItems: "center" }}
            >
              <Typography
                variant="subtitle2"
                sx={{ fontSize: "14px", pr: 2, minWidth: "150px" }}
              >
                Tên bộ phận:<span style={{ color: "red" }}>*</span>
              </Typography>
              <TextField
                fullWidth
                size="small"
                InputProps={{
                  sx: { fontSize: "14px" },
                }}
                onChange={(e) => setTenBoPhan(e.target.value)}
              />
            </Grid>

            {/* Hàng 3 */}
            <Grid
              item
              xs={12}
              md={12}
              sx={{ display: "flex", alignItems: "center" }}
            >
              <Typography
                variant="subtitle2"
                sx={{ fontSize: "14px", pr: 2, minWidth: "150px" }}
              >
                Địa chỉ:
              </Typography>
              <TextField
                fullWidth
                size="small"
                InputProps={{
                  sx: { fontSize: "14px" },
                }}
                onChange={(e) => setAddress(e.target.value)}
              />
            </Grid>

            {/* Hàng 4 */}
            <Grid
              item
              xs={12}
              md={12}
              sx={{ display: "flex", alignItems: "center" }}
            >
              <Typography
                variant="subtitle2"
                sx={{ fontSize: "14px", pr: 2, minWidth: "150px" }}
              >
                Điện thoại:
              </Typography>
              <TextField
                fullWidth
                size="small"
                value={formatPhone}
                onChange={handleChange}
                onBlur={handleBlur}
                error={error}
                helperText={error ? "Số điện thoại không hợp lệ" : ""}
                inputProps={{ maxLength: 12 }}
                InputProps={{
                  sx: { fontSize: "14px" },
                }}
              />
            </Grid>

            {/* Hàng 5 */}
            <Grid
              item
              xs={12}
              md={12}
              sx={{ display: "flex", alignItems: "center" }}
            >
              <Typography
                variant="subtitle2"
                sx={{ fontSize: "14px", pr: 2, minWidth: "150px" }}
              >
                Thuộc/Trực thuộc:
              </Typography>
              <Autocomplete
                className="pt-[1px]"
                options={boPhanSuDung}
                getOptionLabel={(option) => option.TEN}
                onChange={(_, value) => {
                  if (value) {
                    setTrucThuoc(value.ID);
                  }
                }}
                renderInput={(params) => (
                  <TextField
                    {...params}
                    placeholder="-- Chọn đơn vị cấp trên của bộ phận --"
                    sx={{
                      fontSize: "8px",
                      "& .MuiInputBase-root": {
                        height: "36px",
                      },
                    }}
                  />
                )}
                noOptionsText="Không tìm thấy đơn vị"
                renderOption={(props, option) => (
                  <li {...props} key={option.ID} style={{ fontSize: "14px" }}>
                    {option.TEN}
                  </li>
                )}
                sx={{ height: "36px", width: "100%" }}
              />
            </Grid>
          </Grid>
        </Box>
      </DialogContent>

      <DialogActions>
        <Button
          variant="contained"
          color="primary"
          type="submit"
          onClick={handleSave}
          startIcon={<SaveIcon />}
        >
          Lưu
        </Button>
      </DialogActions>
    </Dialog>
  );
};
export default ThemMoiBoPhan;
