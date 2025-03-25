import Thongtintaisan from "./Thongtintaisan";
import { Box, Button, Typography } from "@mui/material";
import Giatrisd from "./Giatrisd";
import Hosogiayto from "./Hosogiayto";
import { SubmitHandler, useForm } from "react-hook-form";
import { Thongtinchung } from "../../validateform/thongtinchung";
import SaveIcon from "@mui/icons-material/Save";
import Dientichhientrang from "./Dientichhientrang";
import { PostThongTinTaiSan } from "../../service/ServiceDat";
import { setToast } from "../../redux/toastLice";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";

const SubmitHandlers = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const {
    register,
    handleSubmit,
    formState: { errors },
    setValue,
    setError,
    clearErrors,
    getValues,
  } = useForm<Thongtinchung>({
    defaultValues: {
      QUOC_GIA_ID: undefined,
      DIA_CHI: "",
      GIA_TRI_SU_DUNG_DAT: {
        NGUON_KHAC: 0,
      },
    },
  });

  // const onSubmit: SubmitHandler<Thongtinchung> = (data) => {
  //   setLoading(true)
  //   try {
  //     dispatch(setToast({message:'Đăng nhập thành công' , type:'success'}))
  //     PostThongTinTaiSan(data);
  //     console.log("Dữ liệu form:", data);
  //     if (logger) {
  //       navigate('/dashboard');
  //     }
  //   } catch (error) {

  //   }

  // };

  const onSubmit: SubmitHandler<Thongtinchung> = async (data) => {
    try {
      await PostThongTinTaiSan(data);
      dispatch(setToast({ message: "Đăng nhập thành công", type: "success" }));
      navigate("/trangchu");
    } catch (error) {
      // Xử lý lỗi ở đây
    }
  };

  return (
    <>
      <Typography variant="h5" gutterBottom className="pb-10">
        Nhập số dư tài sản đất
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
          <Thongtintaisan
            register={register}
            errors={errors}
            setValue={setValue}
            clearErrors={clearErrors}
          />
        </div>
        <div className="pb-10">
          <Dientichhientrang
            register={register}
            errors={errors}
            setValue={setValue}
            getValues={getValues}
          />
        </div>

        <div className="pb-10">
          <Giatrisd
            register={register}
            errors={errors}
            setValue={setValue}
            setError={setError}
            clearErrors={clearErrors}
          />
        </div>

        <Hosogiayto register={register} setValue={setValue} />

        <Box sx={{ mt: 3, textAlign: "right" }}>
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

export default SubmitHandlers;
