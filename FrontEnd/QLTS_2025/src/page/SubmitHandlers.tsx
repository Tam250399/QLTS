import React from 'react'
import Thongtintaisan from './Thongtintaisan'
import { Box, Button,Typography } from '@mui/material'
import Dientichhientrang from './Dientichhientrang';
import Giatrisd from './Giatrisd';
import Hosogiayto from './Hosogiayto';

function SubmitHandlers() {

 
  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

  };


  return (
   <>
        <Typography variant="h5" gutterBottom className='pb-10'>
        Nhập số dư tài sản đất
        </Typography>
        <form   >
          <Thongtintaisan />
          <Dientichhientrang />
          <Giatrisd />
          <Hosogiayto />
        <Box sx={{ mt: 3, textAlign: "right" }}>
        <Button
          variant="contained"
          color="primary"
          onSubmit={handleSubmit}
        >
          Gửi
        </Button>
      </Box>
        </form>

   </>
  )
}

export default SubmitHandlers