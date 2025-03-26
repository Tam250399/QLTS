import { Box, Button, CircularProgress, Typography } from "@mui/material";
import { SubmitHandler, useForm } from "react-hook-form";
import ThongTinChung from "./ThongTinChung";
import GiaTriHaoMon from "./GiaTriHaoMon";
import HienTrangSuDung from "./HienTrangSuDung";
import { ThongTinNha } from "../../validateform/thongtinnha";
import SaveIcon from "@mui/icons-material/Save";
import { useEffect, useState } from "react";
import { showToast } from "../../helpers/myHelper";
import { clearToast } from "../../redux/toastLice";
import { useDispatch, useSelector } from "react-redux";
import { RootState } from "../../redux/store";
import { FaRegArrowAltCircleLeft } from "react-icons/fa";
import { useLocation, useNavigate } from "react-router-dom";

const SubmitHandlerHouses = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const { id } = location.state || {};

  const [loading, setLoading] = useState(false);
  const { message, type } = useSelector((state: RootState) => state.toast);
  const dispatch = useDispatch();

  useEffect(() => {
    showToast(message, type);
    dispatch(clearToast());
  }, [message, type]);
  const onHandleHome = () => {
    navigate("/trangchu");
  };
  const {
    register,
    handleSubmit,
    formState: { errors },
    setValue,
    setError,
    clearErrors,
    getValues
  } = useForm<ThongTinNha>({
    defaultValues: {
      LOAI_HINH_TAI_SAN_ID: id,
    },
  });

  const onSubmit: SubmitHandler<ThongTinNha> = (data) => {
    console.log("Dữ liệu form:", data);
  };

  return (
    <>
      <div className="hide-scrollbar overflow-y-auto ">
        <div className="flex justify-between items-center p-4 bg-gray-100">
          <div className="flex items-center gap-2">
            <Typography variant="h5" className="text-black font-bold">
              Nhập số dư tài sản nhà
            </Typography>
            <div className="flex items-center gap-2">
              <FaRegArrowAltCircleLeft className="text-xl text-blue-600" />
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
            <ThongTinChung
              register={register}
              errors={errors}
              setValue={setValue}
              clearErrors={clearErrors}
              getValues={getValues}
            />
          </div>
          <div className="pb-10">
            <GiaTriHaoMon
              register={register}
              errors={errors}
              setValue={setValue}
              setError={setError}
              clearErrors={clearErrors}
              getValues={getValues}
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
      </div>
    </>
  );
};

export default SubmitHandlerHouses;
