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
import { CreateDonViBoPhan, GetListBoPhanSD } from "../../service/ServiceNha";
import { toast } from "react-toastify";

interface ThemMoiBoPhanProps {
  open: boolean;
  handleClose: () => void;
}
const ThemMoiBoPhan: React.FC<ThemMoiBoPhanProps> = ({ open, handleClose }) => {
  const [donvi] = useState(25417);
  const [tenBoPhan, setTenBoPhan] = useState("");
  const [address, setAddress] = useState("");
  const [phone, setPhone] = useState("");
  const [formatPhone, setFormatPhone] = useState("");
  const [trucThuoc, setTrucThuoc] = useState(0);
  const [boPhanSuDung, setBoPhanSuDung] = useState<BoPhanSuDung[]>([]);
  const [tenError, setTenError] = useState(false);
  useEffect(() => {
    const fetchData = async () => {
      try {
        const param = { donViId: donvi };
        const boPhanSuDung = await GetListBoPhanSD(param);
        setBoPhanSuDung(boPhanSuDung);
      } catch (error) {
        console.error("Lỗi khi tải dữ liệu:", error);
      }
    };

    fetchData();
  }, []);
  const handleChangeTenBP = (e: any) => {
    setTenBoPhan(e.target.value);
    setTenError(e.target.value.trim() === "");
  };
  const handleSave = async () => {
    const formData = {
      DON_VI_ID: donvi,
      TEN: tenBoPhan,
      TREE_NODE: null,
      TREE_LEVEL: 0,
      PARENT_ID: trucThuoc,
    };
    try {
      if (!tenBoPhan.trim()) {
        setTenError(true);
      } else {
        const response = await CreateDonViBoPhan(formData);
        if (response?.StatusCode === 200) {
          toast.success("Tạo mới bộ phận sử dụng thành công");
          handleCloseForm();
        } else {
          toast.error("Tạo mới bộ phận sử dụng thất bại");
        }
      }
    } catch (error) {}
  };
  const [error, setError] = useState(false);

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

  const handleCloseForm = () => {
    setTenError(false);
    setTenBoPhan("");
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
                error={tenError}
                helperText={tenError ? "Tên bộ phận không được để trống" : ""}
                onChange={handleChangeTenBP}
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
