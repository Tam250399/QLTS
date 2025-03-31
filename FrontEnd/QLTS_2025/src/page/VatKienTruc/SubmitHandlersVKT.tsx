import { Box, Button, CircularProgress, Typography } from "@mui/material";
import {
  FieldErrors,
  SubmitHandler,
  useForm,
  UseFormClearErrors,
  UseFormGetValues,
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
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import { RootState } from "../../redux/store";
import { FaRegArrowAltCircleLeft } from "react-icons/fa";
import { showToast } from "../../helpers/myHelper";
import { clearToast } from "../../redux/toastLice";

const SubmitHandlersVKT = () => {
  const [loading] = useState(false);
  const { message, type } = useSelector((state: RootState) => state.toast);
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const {
    register,
    handleSubmit,
    control,
    setValue,
    setError,
    clearErrors,
    getValues,
    formState: { errors },
  } = useForm<ThongtinchungVkt>({
    defaultValues: {},
  });

  useEffect(() => {
    showToast(message, type);
    dispatch(clearToast());
  }, [message, type]);

  const onHandleHome = () => {
    navigate("/trangchu");
  };
  const onSubmit: SubmitHandler<ThongtinchungVkt> = (data) => {
    console.log("Dữ liệu form:", data);
  };

  return (
    <>
      <div className="flex justify-between items-center p-4 bg-gray-100">
        <div className="flex items-center gap-2">
          <Typography variant="h6" className="text-black font-bold">
            Nhập số dư tài sản vật kiến trúc
          </Typography>
          <div className="flex items-center gap-2">
            <FaRegArrowAltCircleLeft className="text-xs text-blue-600" />
            <div
              onClick={onHandleHome}
              className="text-base text-blue-600 cursor-pointer hover:underline"
            >
              Quay lại danh sách
            </div>
          </div>
        </div>
        <div className="flex items-center gap-2">
          <Button
            disabled={loading}
            variant="contained"
            color="primary"
            type="submit"
            onClick={handleSubmit(onSubmit)}
            startIcon={<SaveIcon />}
          >
            {loading ? (
              <CircularProgress
                size={24}
                className="mr-2 h-4 w-4 animate-spin"
              />
            ) : null}
            {loading ? "Đang xử lý" : "Lưu dữ liệu"}
          </Button>
          <button
            onClick={onHandleHome}
            className="border border-gray-400 text-gray-600 px-4 py-2 rounded hover:bg-gray-200"
          >
            Đóng
          </button>
        </div>
      </div>
      <Box sx={{ mt: 2, textAlign: "right", mb: 2 }}></Box>
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
            getValues={getValues as unknown as UseFormGetValues<ThongTinNha>}
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
