import { useEffect, useState } from "react";
import Thongtintaisan from "./Thongtintaisan";
import { Alert, Box, Button, Typography } from "@mui/material";
import Dientichhientrang from "./Dientichhientrang";
import Giatrisd from "./Giatrisd";
import Hosogiayto from "./Hosogiayto";
import { SubmitHandler, useForm } from "react-hook-form";
import { Thongtinchung } from "../../validateform/thongtinchung";
import SaveIcon from "@mui/icons-material/Save";

const SubmitHandlers = () => {
  const {
    register,
    handleSubmit,
    formState: { errors },
    setValue,
    setError,
    clearErrors,
    watch,
  } = useForm<Thongtinchung>({
    defaultValues: {
      QUOC_GIA_ID: undefined,
      DIA_CHI: "",
      dienTich: "",
      HIEN_TRANG_SU_DUNG: {
        TRU_SO_LAM_VIEC: "",
        deO: "",
        BO_TRONG: "",
        BI_LAN_CHIEM: "",
        SU_DUNG_HON_HOP: "",
        SU_DUNG_KHAC: "",
      },
    },
  });

  const [areaError, setAreaError] = useState<string | null>(null);
  const dienTich = watch("dienTich");
  const {
    TRU_SO_LAM_VIEC,
    deO,
    BO_TRONG,
    BI_LAN_CHIEM,
    SU_DUNG_HON_HOP,
    SU_DUNG_KHAC,
  } = watch("HIEN_TRANG_SU_DUNG");

  const totalRelevantFields = [
    TRU_SO_LAM_VIEC,
    deO,
    BO_TRONG,
    BI_LAN_CHIEM,
    SU_DUNG_HON_HOP,
    SU_DUNG_KHAC,
  ].reduce<number>((sum, value) => sum + (Number(value) || 0), 0);

  useEffect(() => {
    if (dienTich && totalRelevantFields !== Number(dienTich)) {
      setAreaError("Diện tích đất phải bằng tổng của Hiện trạng sử dụng.");
    } else {
      setAreaError(null);
    }
  }, [
    TRU_SO_LAM_VIEC,
    deO,
    BO_TRONG,
    BI_LAN_CHIEM,
    SU_DUNG_HON_HOP,
    SU_DUNG_KHAC,
  ]);

  const onSubmit: SubmitHandler<Thongtinchung> = (data) => {
    if (dienTich && totalRelevantFields !== Number(dienTich)) {
      setAreaError("Diện tích đất phải bằng tổng của Hiện trạng sử dụng.");
      return;
    }
    console.log("Dữ liệu form:", data);
  };

  return (
    <>
      <Typography variant="h5" gutterBottom className="pb-10">
        Nhập số dư tài sản đất
      </Typography>

      <form onSubmit={handleSubmit(onSubmit)} className="absolute">
        <div className="pb-10">
          <Thongtintaisan
            register={register}
            errors={errors}
            setValue={setValue}
          />
        </div>
        <div className="pb-10">
          <Dientichhientrang register={register} errors={errors} />
          {areaError && (
            <Alert severity="error" sx={{ mt: 2 }}>
              {areaError}
            </Alert>
          )}
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
