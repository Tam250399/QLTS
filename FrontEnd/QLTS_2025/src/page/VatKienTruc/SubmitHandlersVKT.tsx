import { Box, Button, Typography } from "@mui/material";
import { SubmitHandler, useForm } from "react-hook-form";
import ThongtinTSKT from "./ThongtinTSKT";
import { ThongtinchungVkt } from "../../validateform/thongtinVkt";
import HienTrangSD from "./HienTrangSD";

import GiaTriHaoMonVKT from "./GiaTriHaoMonVKT";

const SubmitHandlersVKT = () => {
  const {
    register,
    handleSubmit,
    control,
    setValue,
    setError,
    clearErrors,
    formState: { errors },
  } = useForm<ThongtinchungVkt>({
    defaultValues: {},
  });

  const onSubmit: SubmitHandler<ThongtinchungVkt> = (data) => {
    console.log("Dữ liệu form:", data);
  };

  return (
    <>
      <Typography variant="h5" gutterBottom className="pb-10">
        Nhập số dư tài sản vật kiến trúc
      </Typography>
      <form onSubmit={handleSubmit(onSubmit)}>
        <div className="pb-10">
          <ThongtinTSKT register={register} errors={errors} />
        </div>
        <div className="pb-10">
          <GiaTriHaoMonVKT
            register={register}
            errors={errors}
            setValue={setValue}
            setError={setError}
            clearErrors={clearErrors}
          />
        </div>
        <div className="pb-10">
          <HienTrangSD control={control} />
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

export default SubmitHandlersVKT;
