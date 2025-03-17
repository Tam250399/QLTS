import React from 'react'
import Thongtintaisan from './Thongtintaisan'
import { Box, Button,Typography } from '@mui/material'
import Dientichhientrang from './Dientichhientrang';
import Giatrisd from './Giatrisd';
import Hosogiayto from './Hosogiayto';
import { SubmitHandler, useForm } from 'react-hook-form';
import { Thongtinchung } from "../validateform/thongtinchung";

const SubmitHandlers = ()=> {


    const {
      register,
      handleSubmit,
      formState: { errors },
    } = useForm<Thongtinchung>({
      defaultValues: {
        diachi: '',
      },
    });

    const onSubmit: SubmitHandler<Thongtinchung> = (data) => {
      console.log('Dữ liệu form:', data);
    };


  return (
   <>
        <Typography variant="h5" gutterBottom className='pb-10'>
        Nhập số dư tài sản đất
        </Typography>
        <form     onSubmit={handleSubmit(onSubmit)}>
          <Thongtintaisan register={register} errors={errors}/>
          <Dientichhientrang />
          <Giatrisd />
          <Hosogiayto />
        <Box sx={{ mt: 3, textAlign: "right" }}>
        <Button 
          variant="contained"
          color="primary"
          type="submit"
        >
          Gửi
        </Button>
      </Box>
        </form>

   </>
  )
}

export default SubmitHandlers