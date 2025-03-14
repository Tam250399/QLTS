import React  from 'react'

import {
    TextField,
    Select,
    MenuItem,
    FormControl,
    InputLabel,
    Box,
    Grid,
    Typography,
    Button,
  } from "@mui/material";
import { SubmitHandler, useForm } from 'react-hook-form';

  type Inputs = {
    diachi: string
    password: string
  }

function Home() {

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<Inputs>()



  const loginHandler: SubmitHandler<Inputs> = async (data) =>{
    // const logger =  await Data(data);
    console.log(data);
    
  } 
  
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
            <TextField
              label="Địa chỉ *"
              placeholder="Nhập số nhà, đường phố, tổ/thôn/xóm"
              {...register("diachi", { required: true })}
            />
             {errors.diachi && <span className='text-red-500 text-xs'>Bạn phải nhập địa chỉ</span>}
          </FormControl>

          <FormControl fullWidth margin="dense">
            <InputLabel>Quốc gia *</InputLabel>
            <Select value="Việt Nam">
              <MenuItem value="Việt Nam">Việt Nam</MenuItem>
            </Select>
          </FormControl>
        </Grid>


        <Grid item xs={12} md={6}>
          <FormControl fullWidth margin="dense">
            <InputLabel>Tỉnh/Thành phố *</InputLabel>
            <Select>
              <MenuItem value="">-- Chọn tỉnh/thành phố --</MenuItem>
            </Select>
          </FormControl>
        </Grid>
      </Grid>

      <Box sx={{ mt: 3, textAlign: "right" }}>
      <Button variant="contained" color="primary" onClick={handleSubmit(loginHandler)}>
          Gửi
        </Button>
      </Box>
    </Box>
   
         </div>

  )
}

export default Home