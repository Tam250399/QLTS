import React from "react";
import { Box, Button, Typography } from "@mui/material";
import { SubmitHandler, useForm } from "react-hook-form";
import ThongTinChung from "./ThongTinChung";
import { Thongtinchung } from "../../validateform/thongtinchung";
import GiaTriHaoMon from "./GiaTriHaoMon";
import HienTrangSuDung from "./HienTrangSuDung";

const SubmitHandlerHouses = () => {
  const {
    register,
    handleSubmit,
    formState: { errors },
    setValue,
    setError,
    clearErrors,
  } = useForm<Thongtinchung>({
    defaultValues: {
      DIA_CHI: "",
    },
  });

  const onSubmit: SubmitHandler<Thongtinchung> = (data) => {
    console.log("Dữ liệu form:", data);
  };

  return (
    <>
      <Typography variant="h5" gutterBottom className="pb-10">
        Nhập số dư tài sản nhà
      </Typography>
      <form onSubmit={handleSubmit(onSubmit)}>
        <div className="pb-10">
          <ThongTinChung
            register={register}
            errors={errors}
            setValue={setValue}
          />
        </div>
        <div className="pb-10">
          <GiaTriHaoMon
            register={register}
            errors={errors}
            setValue={setValue}
            setError={setError}
            clearErrors={clearErrors}
          />
        </div>
        <div className="pb-10">
          <HienTrangSuDung register={register} errors={errors} />
        </div>
        <Box sx={{ mt: 3, textAlign: "right" }}>
          <Button variant="contained" color="primary" type="submit">
            Lưu
          </Button>
        </Box>
      </form>
    </>
  );
};

export default SubmitHandlerHouses;
