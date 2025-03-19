import { useEffect, useState } from "react";
import Thongtintaisan from "./Thongtintaisan";
import { Alert, Box, Button, Typography } from "@mui/material";
import Dientichhientrang from "./Dientichhientrang";
import Giatrisd from "./Giatrisd";
import Hosogiayto from "./Hosogiayto";
import { SubmitHandler, useForm } from "react-hook-form";
import { Thongtinchung } from "../validateform/thongtinchung";

const SubmitHandlers = () => {
  const {
    register,
    handleSubmit,
    watch,
    formState: { errors },
    setValue,
  } = useForm<Thongtinchung>({
    defaultValues: {
      diachi: "",
      dienTich: "",
      hienTrangSuDung: {
        truSoLamViec: "",
        hdSnKhongKd: "",
        hdSnKinhDoanh: "",
        hdSnChoThue: "",
        hdSnLdLk: "",
        deO: "",
        boTrong: "",
        biLanChiem: "",
        suDungHonHop: "",
        suDungKhac: "",
      },
    },
  });

  const [areaError, setAreaError] = useState<string | null>(null);
  const dienTich = watch("dienTich");
  const { truSoLamViec, deO, boTrong, biLanChiem, suDungHonHop, suDungKhac } =
    watch("hienTrangSuDung");

  const totalRelevantFields = [
    truSoLamViec,
    deO,
    boTrong,
    biLanChiem,
    suDungHonHop,
    suDungKhac,
  ].reduce((sum, value) => sum + (Number(value) || 0), 0);

  useEffect(() => {
    if (dienTich && totalRelevantFields !== Number(dienTich)) {
      setAreaError("Diện tích đất phải bằng tổng của Hiện trạng sử dụng.");
    } else {
      setAreaError(null);
    }
  }, [truSoLamViec, deO, boTrong, biLanChiem, suDungHonHop, suDungKhac]);

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
      <form onSubmit={handleSubmit(onSubmit)}>
        <div className="pb-10">
          <Thongtintaisan register={register} errors={errors} />
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
          <Giatrisd register={register} errors={errors} setValue={setValue} />
        </div>
        <Hosogiayto register={register} setValue={setValue} />

        <Box sx={{ mt: 3, textAlign: "right" }}>
          <Button variant="contained" color="primary" type="submit">
            Lưu
          </Button>
        </Box>
      </form>
    </>
  );
};

export default SubmitHandlers;
