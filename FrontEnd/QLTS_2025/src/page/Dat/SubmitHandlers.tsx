import Thongtintaisan from "./Thongtintaisan";
import { Box, Button, CircularProgress, Typography } from "@mui/material";
import Giatrisd from "./Giatrisd";
import Hosogiayto from "./Hosogiayto";
import { SubmitHandler, useForm } from "react-hook-form";
import { Thongtinchung } from "../../validateform/thongtinchung";
import SaveIcon from "@mui/icons-material/Save";
import Dientichhientrang from "./Dientichhientrang";
import { PostThongTinTaiSan } from "../../service/ServiceDat";
import { setToast } from "../../redux/toastLice";
import { useDispatch } from "react-redux";
import { useLocation, useNavigate } from "react-router-dom";
import { useState } from "react";
import { FaRegArrowAltCircleLeft } from "react-icons/fa";

const SubmitHandlers = () => {
  const location = useLocation();

  const { id } = location.state || {};

  console.log("test", id);

  const dispatch = useDispatch();
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);

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
      LOAI_HINH_TAI_SAN_ID: id,
      DON_VI_ID: 25417,
    },
  });

  const onHandleHome = () => {
    navigate("/trangchu");
  };

  const onSubmit: SubmitHandler<Thongtinchung> = async (data) => {
    setLoading(true);
    try {
      const postTT = await PostThongTinTaiSan(data);
      console.log("postTT", postTT);
      if (postTT == null) {
        navigate("/home");
      } else {
        dispatch(
          setToast({ message: "Lưu dữ liệu thành công", type: "success" })
        );
        navigate("/trangchu");
      }
    } catch (error) {
      // Xử lý lỗi ở đây
    } finally {
      setLoading(false);
    }
  };

  return (
    <>
      <div className=" overflow-y-auto scrollbar-hide">
        <div className="flex justify-between items-center p-4 bg-gray-100">
          <div className="flex items-center gap-2">
            <Typography variant="h6" className="text-black font-bold">
              Nhập số dư tài sản đất
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
              clearErrors={clearErrors}
            />
          </div>

          <div className="pb-10">
            <Giatrisd
              register={register}
              errors={errors}
              setValue={setValue}
              setError={setError}
              clearErrors={clearErrors}
              getValues={getValues}
            />
          </div>

          <Hosogiayto register={register} setValue={setValue} />

          <Box sx={{ mt: 3, textAlign: "right" }}>
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
          </Box>
        </form>
      </div>
    </>
  );
};

export default SubmitHandlers;
