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
import { useState } from "react";
import CloseIcon from "@mui/icons-material/Close";

interface BoPhan {
  donvi: string;
  tenBoPhan: string;
  address: string;
  phone: string;
  trucThuoc: string;
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
  const [trucThuoc, setTrucThuoc] = useState("");
  const handleSave = () => {
    const formData: BoPhan = {
      donvi,
      tenBoPhan,
      address,
      phone,
      trucThuoc,
    };

    console.log("Dữ liệu form:", formData);
    handleClose();
  };
  return (
    <Dialog open={open} onClose={handleClose}>
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
        <IconButton onClick={handleClose} size="small">
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
                InputProps={{
                  sx: { fontSize: "14px" },
                }}
                onChange={(e) => setPhone(e.target.value)}
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
                options={data}
                getOptionLabel={(option) => option.ten}
                onChange={(_, value) => {
                  if (value) {
                    setTrucThuoc(value.ten);
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
                  <li {...props} key={option.id} style={{ fontSize: "14px" }}>
                    {option.ten}
                  </li>
                )}
                sx={{ height: "36px", width: "100%" }}
              />
            </Grid>
          </Grid>
        </Box>
      </DialogContent>

      <DialogActions>
        <Button onClick={handleSave} color="success" variant="contained">
          Lưu
        </Button>
      </DialogActions>
    </Dialog>
  );
};
export default ThemMoiBoPhan;
