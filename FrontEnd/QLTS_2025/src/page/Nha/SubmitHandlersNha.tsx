import { Box, Button, Typography } from "@mui/material";
import { SubmitHandler, useForm } from "react-hook-form";
import ThongTinChung from "./ThongTinChung";
import GiaTriHaoMon from "./GiaTriHaoMon";
import HienTrangSuDung from "./HienTrangSuDung";
import { ThongTinNha } from "../../validateform/thongtinnha";
import SaveIcon from "@mui/icons-material/Save";
import { useEffect } from "react";
import { showToast } from "../../helpers/myHelper";
import { clearToast } from "../../redux/toastLice";
import { useDispatch, useSelector } from "react-redux";
import { RootState } from "../../redux/store";

const SubmitHandlerHouses = () => {
  const { message, type } = useSelector((state: RootState) => state.toast);
  const dispatch = useDispatch();

  useEffect(() => {
    showToast(message, type);
    dispatch(clearToast());
  }, [message, type]);

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
          <ThongTinChung
            register={register}
            errors={errors}
            setValue={setValue}
            clearErrors={clearErrors}
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
      </form>
    </>
  );
};

export default SubmitHandlerHouses;
