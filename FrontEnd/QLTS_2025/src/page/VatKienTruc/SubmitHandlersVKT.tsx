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
import ThongtinTSKT from "./ThongtinTSKT";
import { ThongtinchungVkt } from "../../validateform/thongtinVkt";
import HienTrangSD from "./HienTrangSD";

import GiaTriHaoMon from "../Nha/GiaTriHaoMon";
import { ThongTinNha } from "../../validateform/thongtinnha";
import SaveIcon from "@mui/icons-material/Save";

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
      <Box sx={{ mt: 2, textAlign: "right", mb: 2 }}>
        <Button
          variant="contained"
          color="primary"
          type="submit"
          onClick={handleSubmit(onSubmit)}
          startIcon={<SaveIcon />}
        >
          Lưu
        </Button>
      </Box>
      <form>
        <div className="pb-10">
          <ThongtinTSKT
            register={register}
            errors={errors}
            setValue={setValue}
          />
        </div>
        <div className="pb-10">
          <GiaTriHaoMon
            register={register as unknown as UseFormRegister<ThongTinNha>}
            errors={errors as FieldErrors<ThongTinNha>}
            setValue={setValue as unknown as UseFormSetValue<ThongTinNha>}
            setError={setError as UseFormSetError<ThongTinNha>}
            clearErrors={clearErrors as UseFormClearErrors<ThongTinNha>}
          />
        </div>
        <div className="pb-10">
          <HienTrangSD control={control} />
        </div>
        <Box sx={{ mt: 2, textAlign: "right", mb: 2 }}>
          <Button
            variant="contained"
            color="primary"
            onClick={handleSubmit(onSubmit)}
            type="submit"
            startIcon={<SaveIcon />}
          >
            Lưu
          </Button>
        </Box>
      </form>
    </>
  );
};

export default SubmitHandlersVKT;
