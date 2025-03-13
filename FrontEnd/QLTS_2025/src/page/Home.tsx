import React from 'react'
import {
    TextField,
    Select,
    MenuItem,
    FormControl,
    InputLabel,
    Box,
    Grid,
    Typography,
  } from "@mui/material";



function Home() {
  return (
    <div>
       <Box sx={{ border: 1, borderRadius: 2, p: 3, bgcolor: "white", boxShadow: 2 }}>
      <Typography variant="h6" gutterBottom>
        Thông tin tài sản đất
      </Typography>
      <Grid container spacing={2}>
        {/* Cột Trái */}
        <Grid item xs={12} md={6}>
          <FormControl fullWidth margin="dense">
            <TextField
              label="Đơn vị"
              value="Chi cục Thuế khu vực Thạch Hà - Lộc Hà"
              InputProps={{ readOnly: true }}
            />
          </FormControl>

          <FormControl fullWidth margin="dense">
            <TextField label="Địa chỉ *" placeholder="Nhập số nhà, đường phố, tổ/thôn/xóm" />
          </FormControl>

          <FormControl fullWidth margin="dense">
            <InputLabel>Quốc gia *</InputLabel>
            <Select value="Việt Nam">
              <MenuItem value="Việt Nam">Việt Nam</MenuItem>
            </Select>
          </FormControl>

          <FormControl fullWidth margin="dense">
            <InputLabel>Quận/Huyện *</InputLabel>
            <Select>
              <MenuItem value="">-- Chọn quận/huyện --</MenuItem>
            </Select>
          </FormControl>

          <FormControl fullWidth margin="dense">
            <InputLabel>Lý do tặng đất *</InputLabel>
            <Select>
              <MenuItem value="Đăng ký lần đầu">Đăng ký lần đầu</MenuItem>
            </Select>
          </FormControl>

          <FormControl fullWidth margin="dense">
            <TextField type="date" label="Ngày tặng *" InputLabelProps={{ shrink: true }} defaultValue="2017-12-31" />
          </FormControl>
        </Grid>

        {/* Cột Phải */}
        <Grid item xs={12} md={6}>
          <FormControl fullWidth margin="dense">
            <InputLabel>Tỉnh/Thành phố *</InputLabel>
            <Select>
              <MenuItem value="">-- Chọn tỉnh/thành phố --</MenuItem>
            </Select>
          </FormControl>

          <FormControl fullWidth margin="dense">
            <InputLabel>Xã/Phường *</InputLabel>
            <Select>
              <MenuItem value="">-- Chọn xã/phường --</MenuItem>
            </Select>
          </FormControl>

          <FormControl fullWidth margin="dense">
            <InputLabel>Mục đích sử dụng *</InputLabel>
            <Select>
              <MenuItem value="">-- Chọn mục đích sử dụng --</MenuItem>
            </Select>
          </FormControl>
        </Grid>
      </Grid>
    </Box>
   
         </div>

  )
}

export default Home