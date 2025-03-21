import { Box, Button, Typography } from "@mui/material";
import {
  FieldErrors,
  SubmitHandler,
  useForm,
  UseFormClearErrors,
  UseFormRegister,
  UseFormSetError,
  UseFormSetValue,
} from "react-hook-form";
import ThongTinChung from "./ThongTinChung";
import GiaTriHaoMon from "./GiaTriHaoMon";
import HienTrangSuDung from "./HienTrangSuDung";
import { ThongTinNha } from "../../validateform/thongtinnha";
import Giatrisd from "../Dat/Giatrisd";
import { Thongtinchung } from "../../validateform/thongtinchung";

const SubmitHandlerHouses = () => {
  const {
    register,
    handleSubmit,
    formState: { errors },
    setValue,
    setError,
    clearErrors,
  } = useForm<ThongTinNha>({
    defaultValues: {},
  });

  const onSubmit: SubmitHandler<ThongTinNha> = (data) => {
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
        
        {/* Ép kiểu gọi component */}
        <Giatrisd
          register={register as unknown as UseFormRegister<Thongtinchung>}
          errors={errors as FieldErrors<Thongtinchung>}
          setValue={setValue as unknown as UseFormSetValue<Thongtinchung>}
          setError={setError as UseFormSetError<Thongtinchung>}
          clearErrors={clearErrors as UseFormClearErrors<Thongtinchung>}
        />

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
